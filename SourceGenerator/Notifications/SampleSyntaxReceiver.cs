using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Immutable;
using System.Linq;

namespace SourceGenerator
{
    internal class SampleSyntaxReceiver : ISyntaxReceiver
    {
        public ImmutableArray<TypeDeclarationSyntax> Items = ImmutableArray<TypeDeclarationSyntax>.Empty;

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax tds &&
                    tds.Kind() == SyntaxKind.InterfaceDeclaration)
            {
                var atts = from al in tds.AttributeLists
                           from a in al.Attributes
                           where a.Name.ToString() == "EventSourceProducer"
                           select a;
                if (!atts.Any()) return;

                Items = Items.Add(tds);
            }
        }
    }
}