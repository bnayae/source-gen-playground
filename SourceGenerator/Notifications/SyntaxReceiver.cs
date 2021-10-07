using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Collections.Immutable;
using System.Linq;

namespace SourceGenerator
{
    internal class SyntaxReceiver : ISyntaxReceiver
    {
        public ImmutableArray<string?> Items = ImmutableArray<string?>.Empty;

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax
                {
                    AttributeLists:
                    {
                    }
                })
            {
            }
            //    if (syntaxNode is InvocationExpressionSyntax
            //        {
            //            ArgumentList:
            //            {
            //                Arguments:
            //                {
            //                    Count: <= 2
            //                } arguments
            //            }, Expression: MemberAccessExpressionSyntax
            //            {
            //                Name:
            //                {
            //                    Identifier:
            //                    {
            //                        ValueText: "Validate"
            //                    }
            //                }
            //            }
            //        })
            //    {
            //        Items = Items.AddRange(arguments.Select(m => m.NameColon?.ColonToken.ValueText));
            //    }
        }
    }
}