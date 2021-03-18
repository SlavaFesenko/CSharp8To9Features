using System;

namespace CSharp8To9Features.PatternMatchingSwitchExpressions
{
    public static class SwitchExpressionExample
    {
        public static double CalculateAreaSwitchExpressionNested(object obj)
        { // метод лучше читается с кавычками {}, без expression bodied стиля
            
            // result добавлен, чтобы показать, что можно присвоить значение switch expression переменной
            var result = obj switch
            {
                null => throw new ArgumentNullException(nameof(obj)),

                // старый формат записи, явно уступает новому синтаксису в удобстве и компактности
                Square sq when sq.Side == 0 && sq.Side < 0 => 0, // 'and' вместо '&&' в when нельзя
                Square sq when sq.Side != 0 => sq.Side * sq.Side,

                Circle ci =>
                ci switch
                {
                    { Radius: 0 } => 0, // так не сделаешь: sq.Radius == 0 => 0, ошибка
                    { Radius: var r } => r * r * Math.PI
                },

                Rectangle re =>
                re switch
                {
                    // and ('&&' нельзя) для примера, по логике должно быть or
                    { Length: < 0 } and { Height: < 0 } => throw new InvalidOperationException(),
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

            return result;
        }
    }
}
