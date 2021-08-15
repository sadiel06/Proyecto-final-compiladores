using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public static class Enums
    {
        public enum TipoElemento
        {
            Variable,
            PalabraReservada,
            TipoDato,
            Numero,
            OperadorAritmetico,
            OperadorRelacional,
            OperadorLogico,
            OperadorAsignacion,
            OperadorMisc,
            PalabraDefinicion,
            OperadorTerminador,
            Parentesis,
            Corchete,
            Llave,
            Error,
            OperadorIncremental,
            OperadorDecremental,
            Cadena,
            Caracter,
            Coma,
            TipoVariable
        }

        public enum OperadoresAritmeticos
        {
            Suma,
            Resta,
            Multiplicacion,
            Division,
            Modulo,
            Incremental,
            Decremental
        }

        public enum OperadoresRelacionales
        {
            Igualdad,
            Desigualdad,
            Mayor,
            MayorIgual,
            Menor,
            MenorIgual
        }

        public enum OperadoresLogicos
        {
            And,
            Or,
            Not
        }

        public enum OperadoresAsignacion
        {
            Igual,
            MasIgual,
            MenosIgual,
            MultiplicacionIgual,
            DivisionIgual,
            ModuloIgual
        }

        public enum OperadoresMisc
        {
            SizeOf,
            Direccion,
            Puntero,
            Condicional
        }

        public enum TipoVariable
        {
            Char,
            UChar,
            SChar,
            Int,
            UInt,
            Short,
            UShort,
            Long,
            ULong,
            Float,
            Double,
            LongDouble,
            Void,
            CharString

        }

        public enum PalabrasReservadas
        {
            If,
            Else,
            Switch,
            Case,
            Default,
            Break,
            For,
            While,
            Do,
            GoTo,
            Return,
            Continue,
            Enum,
            Struct,
            TypeDef,
            Union,
            Volatile

            
        }

        public enum PalabrasDefinicion
        {
            Auto,
            Signed,
            Const,
            Extern,
            Register,
            Unsigned
        }

        public enum TipoBloque
        {
            Sentencia,
            Estructura,
            Funcion
        }

        public static string TipoVariablePatron()
        {
            string patron = "";
            foreach (string name in Enum.GetNames(typeof(TipoVariable)))
            {
                patron = patron + name.ToLower() + "|";
            }

            patron = patron.Remove(patron.Length - 1);

            return patron;
        }

        public static string TipoPalabraReservadaPatron()
        {
            string patron = "";
            foreach (string name in Enum.GetNames(typeof(PalabrasReservadas)))
            {
                patron = patron + name.ToLower() + "|";
            }

            patron = patron.Remove(patron.Length - 1);

            return patron;
        }

        public static string TipoPalabraDefinicionPatron()
        {
            string patron = "";
            foreach (string name in Enum.GetNames(typeof(PalabrasDefinicion)))
            {
                patron = patron + name.ToLower() + "|";
            }

            patron = patron.Remove(patron.Length - 1);

            return patron;
        }


    }
}
