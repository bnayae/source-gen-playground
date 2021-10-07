using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SourceGenerator.FromContent
{
    [Generator]
    public class XmlGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);
            var ns = mainMethod?.ContainingNamespace.Name;

            // find anything that matches our files
            var allFiles = context.AdditionalFiles;
            var files = allFiles.Where(at => at.Path.EndsWith(".xml"));
            foreach (AdditionalText xmlFile in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(xmlFile.Path);
                var builder = new StringBuilder();
                XmlDocument? xmlDoc = new XmlDocument();
                string? text = xmlFile?.GetText(context.CancellationToken)?.ToString();
                if (string.IsNullOrEmpty(text)) continue;
                try
                {
                    xmlDoc.LoadXml(text);
                }
                catch
                {
                    //TODO: issue a diagnostic that says we couldn't parse it
                    context.AddSource($"{fileName}.generated.cs", "// Corrupted XML");
                    return;
                }
                // do some transforms based on the file context
                builder.AppendLine(@$"using System;

namespace {mainMethod?.ContainingNamespace.Name ?? "Oops"}
{{
    public class {fileName}
    {{");
                for (int i = 0; i < xmlDoc.DocumentElement.ChildNodes.Count; i++)
                {
                    XmlElement child = (XmlElement)xmlDoc.DocumentElement.ChildNodes[i];
                    string name = child.GetAttribute("name");

                    builder.AppendLine($"\t\tpublic string {name}() => \"{name} was generated\";");
                }
                builder.AppendLine(@$"
    }}
}}");
                context.AddSource($"{fileName}.generated.cs", builder.ToString());
            }
        }
    }
}
