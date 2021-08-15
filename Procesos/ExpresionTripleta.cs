using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class ExpresionTripleta
    {
        public int Codigo { get; set; }

        public Tripleta TripletaInicial { get; set; }

        public Lexema Operador { get; set; }

        public Lexema PrimerLexema { get; set; }

        public Lexema SegundoLexema { get; set; }

        public Tripleta PrimerTripleta { get; set; }

        public Tripleta SegundaTripleta { get; set; }

        public string TextoMandatorio { get; set; }


    }
}
