using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class Tripleta
    {
        public int Codigo { get; set; }

        public Lexema Operador { get; set; }

        public Lexema PrimerOperando { get; set; }

        public Lexema SegundoOperando { get; set; }

        public Bloque Bloque { get; set; }
    }
}
