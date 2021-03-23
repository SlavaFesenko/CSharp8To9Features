using System;

namespace CSharp8To9Features.RecordsStuff
{
    public class RecordsExample
    {
        public static void Show()
        {
            RecordsVsClasses();

            RecordsDeconstruct();

            RecordsInheritanceVsValueSemantics();
        }

        private static void RecordsVsClasses()
        {
            var personClassSet = new Person_ClassSet()
            {
                FirstName = "First",
                LastName = "Person",
            };

            personClassSet.LastName = "ModifiedPerson";

            var personClassInit = new Person_ClassInit()
            {
                FirstName = "Second",
                LastName = "Person",
            };

            //personClassInit.LastName = "ModifiedPerson"; // у init set нельзя переделать значение
            // хотя через рефлексию, по идее, можно, так как в основе это обычный set {}

            //var p11 = personClassInit with { LastName: "ModifiedPerson" }; // для class-ов так пока нельзя

            // record преобразуется компилятором в class, так что перфоманс одинаковый
            var personRecord = new Person_Record()
            {
                FirstName = "Third",
                LastName = "Person",
            };

            var p3 = personRecord with { LastName = "ModifiedPerson" }; // 'with' делает shallow copy + change
            var p4 = personRecord with { LastName = "ModifiedPerson" };

            Console.WriteLine($"{p3}"); // встроенный .ToString() выводит свойства, а не тип данных как у class
            Console.WriteLine($"{p4}");

            // Value-based equality, то есть если контент одинаковый => true
            Console.WriteLine($"object.Equals({nameof(p3)},{nameof(p4)}): {Equals(p3, p4)}");
            // false, ссылки разные, так как разные объекты
            Console.WriteLine($"object.ReferenceEquals({nameof(p3)},{nameof(p4)}): {ReferenceEquals(p3, p4)}");
        }

        private static void RecordsDeconstruct()
        {
            // для авто-record-ов генерируется метод .Deconstruct(), используемый
            
            // в pattern matching
            object personWithDeconstruct = new Person_RecordBase("Pete", "Toster");
            if (personWithDeconstruct is Person_RecordBase(FirstName: (not null or "") and var fn, var sn))
            {
                Console.WriteLine($"PersonWithDeconstruct: {fn}, {sn};");
            }

            // и просто для создания кортежа
            var personWithDeconstruct2 = new Person_RecordBase("Pete", "Toster");
            var (firstname, secondname) = personWithDeconstruct2;
            Console.WriteLine($"Deconstructed personWithDeconstruct2: {firstname}, {secondname}");
        }

        private static void RecordsInheritanceVsValueSemantics()
        {
            Person_RecordBase basePersonRecord = new("John", "Doe");
            Student_Record studentRecord = new("John", "Doe");

            // хотя контент равен, хотя и родственники, но типы разные, потому => false
            Console.WriteLine($"object.Equals: {Equals(basePersonRecord, studentRecord)}");

            Person_RecordBase studentPerson = new Student_Record("John", "Doe");
            var pb1 = studentPerson with { }; // pb1 is Student_Record, берется по наследнику, а не по предку
            Console.WriteLine($"{nameof(pb1)} type is {pb1.GetType()}"); // Student_Record

            // хоть тип pb2 указан явно, это все равно Student
            Person_RecordBase pb2 = studentRecord with { };

            // false, так как pb2 это Student_Record, а basePerson - Person_RecordBase
            Console.WriteLine($"object.Equals({nameof(basePersonRecord)},{nameof(pb2)}): " +
                $"{Equals(basePersonRecord, pb2)}");

            // true, так как копии, хоть и через базовый класс, конечный тип все равно Student_Record
            Console.WriteLine($"object.Equals({nameof(studentRecord)},{nameof(pb2)}): " +
                $"{Equals(studentRecord, pb2)}");
        }
    }
}
