using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DddCqrsExample.Framework
{
    public abstract class Aggregate
    {
        public string Id { get; set; }
        public int Version { get; set; }
    }
}