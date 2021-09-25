using Microsoft.CodeAnalysis;

namespace SourceGenerator
{
    [Generator]
    public class MainPartialGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // build up the source code
            string source = $@"
using System;

namespace {mainMethod?.ContainingNamespace.Name ?? "Oops"}
{{
    public static partial class {mainMethod?.ContainingType.Name ?? "Oops"}
    {{
        static partial void HelloFrom(string name)
        {{
            Console.WriteLine($""Generator says: Hi from '{{name}}'"");
        }}
    }}
}}
";
            // add the source code to the compilation
            context.AddSource("generatedSource", source);
        }
    }
}
