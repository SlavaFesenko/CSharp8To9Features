using System;

namespace CSharp8To9Features.PatternMatchingSwitchExpressions
{
    public static class PatternMatchingExamples
    {
        public static void Execute()
        {
            Rectangle? shape = default;

            // null pattern
            if (shape is null) // понятно, что if не обязателен, просто для примера is вместо ==
            {
                shape ??= new Rectangle { Height = 0, Length = 3 };
            }

            // Constant pattern
            if (shape.Height is 0) // так себе, в отличии от null тут читабельнее ==
            {
                Console.WriteLine("This rectangle has no height");
            }

            // Type patterns
            object? shape2 = new Square() { Side = 2d };
            if (shape2 is Square s) // это классика
            {
                Console.WriteLine($"Square is found. side={s.Side}");
            }

            // Property pattern
            if (shape2 is Square { Side: var side }) // если сам объект не нужен, а нужно только его поле
            {
                Console.WriteLine($"Square is found. side={side}");
            }

            Circle? shape3 = new Circle() { Radius = 100 };

            // Relational pattern
            if (shape3 is Circle { Radius: >= 100 } s1) // удобно, все проверки в одном месте
            {
                Console.WriteLine($"Huge circle is found. radius={s1.Radius}");
            }

            // === Negated pattern

            // previously: 
            if (!(shape is null)) { } // shape != null явно читабельнее
            // same as
            if (shape2 is { }) { } // такой формат записи точно издевательство, ладно еще для switch

            if (shape3 is not null and Circle { Radius: not 0 }) { } // излишний код, лучше как ниже
            if (shape3 is Circle { Radius: not 0 and var rad}) // var rad взят только для примера
            {
                // shape is not null here
                Console.WriteLine($"Radius: {shape3.Radius} or {rad}");
            }

            // === Combo patterns

            // type pattern + property pattern + relational pattern + var pattern + recursive pattern + conjunctive pattern
            if (shape3 is Circle { Radius: > 0 and <= 200 and var radius } && radius % 2 == 0)
            {
                Console.WriteLine("pattern combinators - v1");
            }

            object shape4 = new Rectangle() { Length = 1D, Height = 2D };

            // type pattern + positional pattern + relational + var pattern + recursive pattern + co
            // тут нужно знать про неявно используемый метод .Deconstruct(out double length, out double height)
            if (shape4 is Rectangle(>= 1d, var height))
            {
                Console.WriteLine("pattern combinators - v2: " + height);
            }

            // === Switch expression usage example

            Console.WriteLine("Area of:");
            foreach (var sh in new object[] { shape!, shape2!, shape3!, shape4 })
            {
                double area = SwitchExpressionExample.CalculateAreaSwitchExpressionNested(sh);
                Console.WriteLine($"{sh.GetType().Name, 10}: {area, 10:F2}", ConsoleColor.Blue);
            }

            // === Switch expression + LINQ.Select

            FizzAndBuzzExample.Solve();
        }
    }
}
