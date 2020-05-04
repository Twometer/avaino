using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code.Parser
{
    public class StructDef : ICodeEntity
    {
        public string Name { get; set; }

        public StructDef(string name)
        {
            Name = name;
        }
    }
}
