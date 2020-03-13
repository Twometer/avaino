using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code.Parser
{
    public class ClassDef : ICodeEntity
    {
        public ClassDef(string name)
        {
            Name = name;
        }

        public string Name { get; set ; }
    }
}
