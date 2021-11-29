using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8To9Features.PatternMatchingSwitchExpressions
{
    public static class NumberRangeSwitchExample
    {
        public static void Present()
        {
            int price = 25000;

            // C# 8 without range
            var percent = price switch
            {
                var n when n >= 1000000 => 7f,
                var n when n >= 900000 => 7.1f,
                var n when n >= 800000 => 7.2f,

                _ => 0f // default value
            };

            // C# 8 with range
            percent = price switch
            {
                var n when n >= 1000000 => 7f,
                var n when n < 1000000 && n >= 900000 => 7.1f,
                var n when n < 900000 && n >= 800000 => 7.2f,

                _ => 0f // default value
            };

            // C# 9 without range
            percent = price switch
            {
                >= 1000000 => 7f,
                >= 900000 => 7.1f,
                >= 800000 => 7.2f,
                _ => 0f // default value
            };

            // C# 9 with range
            percent = price switch
            {
                >= 1000000 => 7f,
                < 1000000 and >= 900000 => 7.1f,
                < 900000 and >= 800000 => 7.2f,
                _ => 0f // default value
            };
        }
    }
}
