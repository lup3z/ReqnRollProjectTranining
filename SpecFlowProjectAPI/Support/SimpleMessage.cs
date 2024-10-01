using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectAPI.Support
{
    public class SimpleMessage
    {
        public string Type { get; set; }
        public object Content { get; set; }
    }

    // Clase para el contenido complejo del primer mensaje
    public class ComplexContent
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }
}
