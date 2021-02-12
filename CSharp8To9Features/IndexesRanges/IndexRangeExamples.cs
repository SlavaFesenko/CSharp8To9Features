using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8To9Features.IndexesRanges
{
    public static class IndexRangeExamples
    {
        public static void DoStuff()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };

            Console.WriteLine(string.Join(",", array));

            Console.WriteLine("[2..^3] == array[new Range(2, new Index(3, fromEnd: true))]");
            var slice1 = array[2..^2];    // array[new Range(2, new Index(3, fromEnd: true))]
            Console.WriteLine(string.Join(",", slice1)); // 3 => перечисли со второго по второй с конца


            Console.WriteLine("[..^3] == array[Range.EndAt(new Index(3, fromEnd: true))]");
            var slice2 = array[..^3];     // array[Range.EndAt(new Index(3, fromEnd: true))]
            Console.WriteLine(string.Join(",", slice2)); // 1,2 => перечисли все элементы до третьего с конца


            Console.WriteLine("[2..] == array[Range.StartAt(2)]");
            var slice3 = array[2..];      // array[Range.StartAt(2)]
            Console.WriteLine(string.Join(",", slice3)); // 3,4,5 => перечисли все, начиная со второго элемента


            Console.WriteLine("[..] == array[Range.All]");
            var slice4 = array[..];       // array[Range.All]
            Console.WriteLine(string.Join(",", slice4)); // 1,2,3,4,5 => взять все элементы

            // BANG 💥
            //Console.WriteLine(array[^10]);
        }

        public static void ComplicatedExample()
        {
            int[] sequence = Sequence(1000);

            Console.WriteLine(LeftToRightAverage().ToList());

            IEnumerable<StatInfo> LeftToRightAverage()
            {
                for (int start = 0; start < sequence.Length; start += 100)
                {
                    Range r = start..(start + 10);
                    var (min, max, average) = MovingAverage(sequence, r);
                    // Console.WriteLine($"From {r.Start} to {r.End}:    \tMin: {min},\tMax: {max},\tAverage: {average}");
                    yield return new StatInfo()
                    {
                        Label = $"From {r.Start} to {r.End}",
                        Min = min,
                        Max = max,
                        Average = average,
                    };
                }
            }

            Console.WriteLine(RightToLeftAverage().ToList());

            IEnumerable<StatInfo> RightToLeftAverage()
            {
                for (int start = 0; start < sequence.Length; start += 100)
                {
                    Range r = ^(start + 10)..^start;
                    var (min, max, average) = MovingAverage(sequence, r);
                    // Console.WriteLine($"From {r.Start} to {r.End}:  \tMin: {min},\tMax: {max},\tAverage: {average}");
                    yield return new StatInfo()
                    {
                        Label = $"From {r.Start} to {r.End}",
                        Min = min,
                        Max = max,
                        Average = average,
                    };
                }
            }
        }

        static (int min, int max, double average) MovingAverage(int[] subSequence, Range range) =>
        (
            subSequence[range].Min(),
            subSequence[range].Max(),
            subSequence[range].Average()
        );

        static int[] Sequence(int count) =>
            Enumerable.Range(0, count).Select(x => (int)(Math.Sqrt(x) * 100)).ToArray();

        public class StatInfo
        {
            public string Label { get; set; }
            public int Min { get; set; }
            public int Max { get; set; }
            public double Average { get; set; }
        }
    }
}
