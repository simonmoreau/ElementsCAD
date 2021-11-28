using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.Models
{
    class Function
    {
        public string Directory { get; set; }
        public string DllName { get; set; }
        // public FunctionDefinition FunctionDefinition { get; set; }
        public Dictionary<string, object> InputsValues { get; set; }

        public string ModelOutput { get; set; }
    }
}
