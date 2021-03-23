using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp8To9Features.RecordsStuff
{
    public class TargetTypedNewExpressions
    {
        public static void Show()
        {
            //it works not only with records, but still it is a good example
            List<PersonGroup<Person>> groups = new()
            {
                new()
                {
                    Name = "FirstGroup",
                    PersonToScore = new()
                    {
                        [new("John", "Doe")] = new() { 100, 99 },
                        [new("James", "Smith")] = new() { 90, 89 },
                    }
                },
                new PersonGroup<Person>()
                {
                    Name = "SecondGroup",
                    PersonToScore = new Dictionary<Person, List<int>>()
                    {
                        [new Person("Scott", "Hunter")] = new List<int>() { 80, 79 },
                        [new Person("Scott", "Hanselman")] = new List<int>() { 70, 69 },
                    }
                },
            };

            foreach (var group in groups)
            {
                Console.WriteLine($"group:{group.Name}\t" +
                    $"score:{group.PersonToScore.SelectMany(kvp => kvp.Value).Sum()}");

                Console.WriteLine($"{string.Join(',', group.PersonToScore.Keys)}{Environment.NewLine}");
            }
        }
    }

    class PersonGroup<T> where T : Person
    {
        public Dictionary<T, List<int>> PersonToScore { get; init; } = new();
        public string Name { get; init; } = string.Empty;
    }

    record Person(string FirstName, string LastName);
}
