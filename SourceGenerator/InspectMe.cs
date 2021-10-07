using System;
using System.Collections.Generic;
using System.Text;

namespace SourceGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public partial class InspectMe : Attribute
    {
    }
}
