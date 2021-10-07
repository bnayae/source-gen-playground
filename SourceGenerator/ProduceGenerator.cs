using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Linq;
using System.Text;

namespace SourceGenerator
{
    [Generator]
    public class ProduceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var interfaces = context.Compilation.SyntaxTrees.SelectMany(x => x.GetRoot()
                .DescendantNodes()
                .Where(x => x is TypeDeclarationSyntax tds &&
                    tds.Kind() == SyntaxKind.InterfaceDeclaration &&
                    tds.AttributeLists.Any(al => al.Attributes.Any(a => a.Name.ToString() == "EventSourceProducer"))))
                .Select(x => x as TypeDeclarationSyntax);
            foreach (var item in interfaces)
            {
                var source = new StringBuilder();
                source.AppendLine($"public class {item.Identifier.ValueText}Generated {{ ");

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

            {
                var source = $"public class EmptyGenerated {{ }}";

                if (source != null)
                {
                    context.AddSource($"empty.cs", source);
                }
            }
        }
    }
}
