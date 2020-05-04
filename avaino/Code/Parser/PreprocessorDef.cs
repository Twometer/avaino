using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code.Parser
{
    public class PreprocessorDef : ICodeEntity
    {
        public PreprocessorDef(string name, string value)
        {
            Value = value;
            Name = name;
        }

        public string Value { get; set; }

        public string Name { get; set; }
    }
}
