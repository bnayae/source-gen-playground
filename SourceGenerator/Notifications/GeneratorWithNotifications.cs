using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;
using System.Text;

namespace SourceGenerator
{
    [Generator]
    public class GeneratorWithNotifications : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SampleSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SampleSyntaxReceiver syntax) return;

            foreach (var item in syntax.Items)
            {
                var source = new StringBuilder();
                source.AppendLine($"[ System.CodeDom.Compiler.GeneratedCode(\"{nameof(GeneratorWithNotifications)}\")]");
                source.AppendLine($"public class {item.Identifier.ValueText}Gen {{ ");

                foreach (var method in item.Members)
                {
                    if (method is MethodDeclarationSyntax mds)
                    {
                        source.AppendLine($"public void {mds.Identifier.ValueText}(");

                        var ps = mds.ParameterList.Parameters.Select(p => $"{p.Type} {p.Identifier.ValueText}");
                        source.AppendLine(string.Join(", ", ps));
                        source.Append(") { }");
                    }
                }
                source.AppendLine("}");
                if (source != null)
                {
                    context.AddSource($"{item.Identifier.ValueText}.cs", source.ToString());
                }

            }
        }
    }
}
