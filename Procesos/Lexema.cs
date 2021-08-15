using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class Lexema
    {
        public Lexema()
        {
            this.Error = false;
        }

        /// <summary>
        /// Indica el texto del lexema: [variable] [numero] etc
        /// </summary>
        public string Texto { get; set; }

        public Enums.TipoElemento TipoElemento { get; set; }

        public bool Error { get; set; }

        public string MensajeError { get; set; }

    }
}
