using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Procesos
{
 
    /// Objeto para presentar registros en la TABLA DE SIMBOLOS 
    
    public class RegistroTabla
    {

        public int Codigo { get; set; }
        
        public string Nombre { get; set; } /// Almacenar el nombre de variable por cada una que encuentre


        public Enums.TipoVariable? TipoVariable { get; set; } //Para saber el tipo de variable

       
        public string Valor { get; set; }  /// Valor que posee una variable

    }
}
