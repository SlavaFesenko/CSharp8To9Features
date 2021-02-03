using System;
using System.Text;

namespace CSharp8To9Features.DifferentExpressions
{
    public class NullCoOperators
    {
        public static void DoStuff()
        {
            // '?' - null conditional operator
            // '??' - null coalescing operator
            // '??=' - null coalescing assignment operator

            int?[] scores = { 1, default, 10, 20 }; // C#7 default literal (вместо default(int?))

            Console.WriteLine($"scores[1] is null: {scores[1] is null}"); // true
            Assign();
            Console.WriteLine($"scores[1]: {scores[1]}"); // 5

            StringBuilder sb = null; // просто для примера
            var x = sb?.Length ?? 0; // без '?? 0' конце просто присвоит null, так как StringBuilder nullable

            void Assign()
            {
                ref var score = ref scores[1];
                score ??= 5; // same as: score = score ?? 5; это и есть null coalescing assignment C# 8
                _ = score ?? throw new InvalidOperationException(nameof(Assign)); // C# 7 throw expressions, тут не выполнится
                score ??= 7; // присваивание не произойдет
            }
        }
    }
}
