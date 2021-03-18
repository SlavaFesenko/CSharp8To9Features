using System;

namespace CSharp8To9Features.PatternMatchingSwitchExpressions
{
    public static class SwitchExpressionExample
    {
        public static double CalculateAreaSwitchExpressionNested(object obj)
        { // метод лучше читается без expression bodied стиля
            return obj switch
            {
                null => throw new ArgumentNullException(nameof(obj)),

                // старый формат записи, явно уступает новому синтаксису в удобстве
                //Square sq when sq.Side == 0 => 0,
                //Square sq when sq.Side != 0 => sq.Side * sq.Side,

                Square sq =>
                sq switch
                {
                    { Side: 0 } => 0, // так не сделаешь: sq.Side == 0 => 0, ошибка
                    { Side: var s } => s * s,
                },

                Circle ci =>
                ci switch
                {
                    { Radius: 0 } => 0,
                    { Radius: var r } => r * r * Math.PI
                },

                Rectangle re =>
                re switch
                {
                    { Length: < 0 } or { Height: < 0 } => throw new InvalidOperationException(),
                    { Length: 0 } or { Height: 0 } => 0,
                    { Length: var l, Height: var h } => l * h
                },

                Triangle tr =>
                tr switch
                {
                    { Base: 0 } or { Height: 0 } => 0,
                    { Base: var b, Height: var h } => b * h / 2
                },

                _ => throw new NotSupportedException()
            };
        }
    }
}
