using Microsoft.CodeAnalysis;

namespace SourceGenerator
{
    [Generator]
    public class FooGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var source = "class Foo { }";

            if (source != null)
            {
                context.AddSource("generated.cs", source);
            }
        }
    }
}
