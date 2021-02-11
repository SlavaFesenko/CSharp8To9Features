namespace CSharp8To9Features.DifferentExpressions
{
    using System;
    using System.Runtime.CompilerServices;
#nullable enable

    public class StaticLocalFunctionsExample
    {
        public static void DoStuff()
        {
            // target typed expressions C# 9

            // TODO: target type expressions
            Person?[] people = new Person?[]
            {
                new Person("John", "Doe"),
                default,
                new Person("Foo", "Bar"),
            };

            Assign();
            Console.WriteLine($"people[1]: {people[1]}");

            // данная локальная функция демонстрирует пример замыканий в C#, так как имеет доступ к 
            // данным из более высокоуровневой функции
            void Assign()
            {
                ref var p = ref FindFreePlace();
                p = people[0];

                // ref-return C# 8
                // local function attributes C# 9, непонятно зачем показывает, откуда был вызван код
                ref Person FindFreePlace([CallerFilePath] string? sourceFilePath = default)
                {
                    Console.WriteLine($"Called from: {sourceFilePath}");
                    return ref Find(people)!;

                    // static local function C# 8
                    // смысл статики в лучшей читабельность и избегании замыканий, могущих заимпактить перфоманс
                    static ref T Find<T>(T[] source)
                    {
                        for (var i = 0; i < source.Length; i++)
                        {
                            if (source[i] is null)
                            {
                                return ref source[i];
                            } 
                        }

                        throw new InvalidOperationException(nameof(Find));
                    }
                }
            }
        }

        public record Person(string FirstName, string LastName);
    }
}
