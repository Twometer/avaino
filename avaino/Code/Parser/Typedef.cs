using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code.Parser
{
    public class Typedef : ICodeEntity
    {
        public Typedef(string source, string name)
        {
            Source = source;
            Name = name;
        }

        public string Source { get; set; }

        public string Name { get; set; }
    }
}
