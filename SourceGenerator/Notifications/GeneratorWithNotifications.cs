using Microsoft.CodeAnalysis;

namespace SourceGenerator
{
    [Generator]
    public class GeneratorWithNotifications : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver) return;

            var source = "class Bar { }";

            if (source != null)
            {
                context.AddSource("generated.cs", source);
            }
        }
    }
}
