using System;
using System.Linq.Expressions;

namespace CSharp8To9Features.DifferentExpressions
{
    public static class LambdaDiscardsExample
    {
        public static void DoStuff()
        {
#nullable enable

            var (first, second) = ((1, 2, 3), (4, 5, 6));
            Expression<Func<(int x, int y, int z), (int a, int b, int c), int>> a =
                (first, second) => first.x + first.y + first.z + second.a + second.b + second.c;

            Console.WriteLine($"{a}");
            Console.WriteLine($"{a.Compile()(first, second)}");

            a = (_, second) => second.a + second.b + second.c;

            Console.WriteLine($"{a}");
            Console.WriteLine($"{a.Compile()(first, second)}");

            a = (_, _) => 0;

            Console.WriteLine($"{a}");
            Console.WriteLine($"{a.Compile()(first, second)}");

            //a = (first, _) => Unwrap(first);

            //static int Unwrap((int, int, int) first)
            //{
            //    var (res, _, _) = first;
            //    return res;
            //}

            //Console.WriteLine($"{a}");
            //Console.WriteLine($"{a.Compile()(first, second)}");
        }
    }
}
