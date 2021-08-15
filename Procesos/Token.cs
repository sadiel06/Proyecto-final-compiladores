using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class Token
    {

        /// Token literal: int,definicion,+
        public string NombreToken { get; set; }


        /// Registro en la tabla de simbolos, tendrá valor si es un identificador
        /// si no tiene valor es porque es una palabra reservada o constante
        public int? IdTablaSimbolos { get; set; }

        /// Tendrá el tipo de elemento que estaremos tratando
        public Enums.TipoElemento TipoElemento { get; set; }


    }
}
