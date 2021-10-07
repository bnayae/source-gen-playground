using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class EventSourceProducer : Attribute { }

    [EventSourceProducer]
    public interface ISample
    {
        ValueTask<string[]> StartAsync(int id, string name);
    }
}
