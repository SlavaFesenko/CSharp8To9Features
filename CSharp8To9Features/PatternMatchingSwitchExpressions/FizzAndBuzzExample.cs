using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp8To9Features.PatternMatchingSwitchExpressions
{
    public class FizzAndBuzzExample
    {
        public static void Solve()
        {
            SolveFizzBuzz(Enumerable.Range(1, 100));
        }

        public static void SolveFizzBuzz(IEnumerable<int> sequence)
        {
            sequence
                .Select(i => (i % 3 == 0, i % 5 == 0, i)
                    switch
                    {
                        (true, false, _) => "Fizz",
                        (false, true, _) => "Buzz",
                        (true, true, _) => "FizzBuzz",
                        _ => $"{i}"
                    })
                .ToList()
                .ForEach(Console.WriteLine);
        }
    }
}
