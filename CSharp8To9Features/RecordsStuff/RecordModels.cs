namespace CSharp8To9Features.RecordsStuff
{
    public class Person_ClassSet
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class Person_ClassInit
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    // мануальный рекорд, автогенерит только .ToString()++, то есть ни конструктора, ни .Deconstruct()
    public record Person_Record
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }

    // positional record, содержит свойства, конструктор, .Deconstruct(), .ToString()++ по-умолчанию
    public record Person_RecordBase(string FirstName, string LastName);

    public record Student_Record(string FirstName, string LastName) : Person_RecordBase(FirstName, LastName)
    {
        public int Score { get; set; } // не init, поэтому может многоразово меняться
    }
}
