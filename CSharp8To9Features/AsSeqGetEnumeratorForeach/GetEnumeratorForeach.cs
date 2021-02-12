using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp8To9Features.AsSeqGetEnumeratorForeach
{
    public static class GetEnumeratorForeach
    {
        public static async Task DoStuffAsync()
        {
            // GetEnumerator as extension method to be used in foreach
            await foreach (var item in (1, 2, 3))
            {
                Console.WriteLine(item);
            }

            //List<int> list = new List<int> { 4, 5, 6 }; // list не поддерживается
            //int[] array = new int[] { 7, 8, 9 }; // array тоже
            //await foreach (var item in array)
            //{
            //    Console.WriteLine(item);
            //}

            // в классе Extensions добавлены 2 реализации GetEnumerator - синхронная и асинхронная,
            // поэтому в зависимости от наличия/отсутствия await перед foreach будет вызвана соответствующая перегрузка

            // по поводу порядка возврата и обработки элементов в асинхронной последовательности - 
            // обрабатываются они в рандомном порядке, но возвращаются строго в том порядке, в котором были вызваны
        }
    }

    static class Extensions
    {
        public static IAsyncEnumerator<int> GetAsyncEnumerator(
            this ValueTuple<int, int, int> tuple) => GetAsyncEnumeratorInternal(tuple);

        public static IEnumerator<int> GetEnumerator(
            this ValueTuple<int, int, int> tuple) => GetEnumeratorInternal(tuple);

        static async IAsyncEnumerator<int> GetAsyncEnumeratorInternal(ValueTuple<int, int, int> tuple)
        {
            foreach (var item in tuple)
            {
                await Task.Delay(100);
                Console.WriteLine($"\tDelay...");
                yield return item;
            }
        }

        public static IEnumerator<int> GetEnumeratorInternal(
            ValueTuple<int, int, int> tuple)
        {
            yield return tuple.Item1!;
            yield return tuple.Item2!;
            yield return tuple.Item3!;
        }
    }
}
