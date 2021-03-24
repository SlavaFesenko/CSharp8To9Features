using CSharp8To9Features.CodeGenerators;
using CSharp8To9Features.IndexesRanges;
using CSharp8To9Features.PatternMatchingSwitchExpressions;
using CSharp8To9Features.RecordsStuff;

namespace CSharp8To9Features
{
    class Program
    {
        static void Main(string[] args)
        {
            //IndexRangeExamples.DoStuff();
            //PatternMatchingExamples.Execute();
            RecordsExample.Show();
            CodeGeneratorPresenter.Present();
        }

        //static async System.Threading.Tasks.Task Main(string[] args)
        //{
        //    await AsSeqGetEnumeratorForeach.GetEnumeratorForeach.DoStuffAsync();
        //}
    }
}
