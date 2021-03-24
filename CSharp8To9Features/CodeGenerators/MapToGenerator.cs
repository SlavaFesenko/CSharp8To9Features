using Microsoft.CodeAnalysis;

namespace CSharp8To9Features.CodeGenerators
{
    [Generator]
    public sealed class MapToGenerator : ISourceGenerator
    {
        /// <summary>
        /// Находит части кода (классы, методы) к которым необходимо применить SourceGeneration
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MapToReceiver());
        }

        /// <summary>
        /// Производит непосредственную работу по генерации нового кода
        /// </summary>
        /// <param name="context"></param>
        public void Execute(GeneratorExecutionContext context)
        {

        }
    }
}
