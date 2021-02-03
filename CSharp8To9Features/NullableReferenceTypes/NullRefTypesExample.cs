// global at csproj: <nullable>enable/warning/annotations</nullable> (выбрать одно) at <PropertyGroup> section
//#nullable enable warnings          // включает предупреждения о потенциальных NRE
//#nullable enable annotations       // активирует определение ref types с атрибутом '?'
#nullable enable                     // == warnings + annotations       

namespace CSharp8To9Features.NullableReferenceTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public static class NullRefTypesExample
    {
        public class Person
        {
            public string FirstName { get; set; }
            public string? MiddleName { get; set; } // атрибут '?' показывает, что значения этого поля может не быть
            public string LastName { get; set; }

            public Person(string firstName, string lastName, string? middleName) =>
                    (FirstName, MiddleName, LastName) = (firstName, middleName, lastName);

            public string FullName => $"{FirstName} {MiddleName?[0]} {LastName}"; // '?', проверяет на null перед обращением
        }

        public static void DoStuff()
        {
            var p = new Person("John", "Doe", default);
            Console.WriteLine(p.FullName);

            if (p.MiddleName is not null)
            {
                Console.WriteLine($"The middle name is: {p.MiddleName}");
            }
            else
            {
                // '!' - bang/null-forgiving operator, используется, чтобы сказать компилятору "я знаю, что делаю"
                //int? lenth = p.MiddleName!.Length; // '!' - NRE без ворнинга 
                int? lenth = p.MiddleName?.Length; // '?', вернется null, без NRE
                Console.WriteLine(lenth);
            }

            // в pattern matching автоматически проверяется на null, если да, то просто идет мимо
            // иначе туда '?' не влепишь
            if (p.MiddleName is { Length: > 0 } name)
            {
                Console.WriteLine($"The middle name is: {name}");
            }
        }

        #region Nullable Attributes примеры использования

        // когда возвращаемое значение == true, object result также не будет null
        public static bool TryGetValue1(string str, [NotNullWhen(true)] out object result) { result = 1; return true; }

        // когда возвращаемое значение == false, object result может быть null
        public static bool TryGetValue2(string str, [MaybeNullWhen(false)] out object result) { result = 1; return true; }

        [return: NotNullIfNotNull("input")] // если input is not null, то и результат не будет null
        public static string Foo1(string input) => "someResult";

        [return: MaybeNull] // результат может быть null
        public static string Foo2(string input) => "someResult";

        [return: NotNull] // результат точно не null
        public static string Foo3(string input) => "someResult";

        public static string Foo5([DisallowNull] string input) => "someResult"; // input не допускается null

        // если Condition == true, то Value is not null
        public class Bar1
        {
            [MemberNotNullWhen(true, nameof(Value))]
            public bool Condition { get; set; }
            public string? Value { get; set; } 
        }

        public class Bar2
        {
            // без этого атрибута компилятор ругается, хотя setter допускает по логике null
            [AllowNull]
            public string ScreenName
            {
                get => _screenName;
                set => _screenName = value ?? GenerateRandomScreenName();
            }
            private string _screenName = GenerateRandomScreenName();

            static string GenerateRandomScreenName() => "dyhedy";

            // а в этом случае хотя string is nullable, нo null передавать нельзя
            [DisallowNull]
            public string? ReviewComment
            {
                get => _comment;
                set => _comment = value ?? throw new ArgumentNullException(nameof(value), "Cannot set to null");
            }
            string? _comment;
        }

        public class Bar3
        {
            [DoesNotReturn] // метод всегда обрывает выполнение на исключении
            private void FailFast()
            {
                throw new InvalidOperationException();
            }

            // если переданный параметр == false, то метод оборвется на исключении
            private void FailFast([DoesNotReturnIf(false)] bool isValid)
            {
                if (!isValid)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        #endregion
    }
}

#nullable restore                    // отмена nullable фичи, помещается в конце блока кода
