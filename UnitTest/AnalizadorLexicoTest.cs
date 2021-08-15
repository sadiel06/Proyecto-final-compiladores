using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Procesos;

namespace UnitTest
{
    [TestFixture]
    public class AnalizadorLexicoTest
    {
        private AnalizadorLexico _anaLex;

        [SetUp]
        public void SetUp()
        {
            _anaLex = new AnalizadorLexico();
        }

        [Test]
        public void RetirarComentarios_TextoConVariosComentarios_RegresaCodigoSinComentarios()
        {
            //Preparar
            string codigo = "int x = 4; //Declaracion de variable\r\ndouble y; /*Comentario de varias lineas*/\r\n\r\n/*\r\nEste comentario tiene varias \r\nlineas\r\n*/\r\n\r\nint z = 44; string m = \"mi mama me mima\";";


            //Ejecutar
            string codigoLimpio = _anaLex.RetirarComentarios(codigo);

            //Probar
            Assert.That(codigoLimpio, Is.EqualTo("int x = 4; \r\ndouble y; \r\n\r\n\r\n\r\nint z = 44; string m = \"mi mama me mima\";"));
        }

        [Test]
        public void RetirarComentarios_TextoSinComentarios_DejaTextoIntacto()
        {
            //Preparar
            string codigo = "int x = 4;";

            //Ejecutar
            string codigoLimpio = _anaLex.RetirarComentarios(codigo);

            //Probar
            Assert.That(codigoLimpio, Is.EqualTo(codigo));

        }

        [Test]
        public void RetirarSaltos_TextoConSaltos_RegresaCodigoUnaLinea()
        {
            //Preparar
            string codigo = "int x = 4; \r\ndouble y; \r\n\r\n\r\n\r\nint z = 44;";

            //Ejecutar
            string codigoProcesado = _anaLex.RetirarSaltos(codigo);

            //Probar
            Assert.That(codigoProcesado, Is.EqualTo("int x = 4; double y; int z = 44;"));
        }

        [Test]
        public void RetirarSaltos_TextoNormal_DejaTextoIntacto()
        {
            //Preparar
            string codigo = "int x = 4; double y; int z = 44;";

            //Ejecutar
            string codigoProcesado = _anaLex.RetirarSaltos(codigo);

            //Probar
            Assert.That(codigoProcesado, Is.EqualTo(codigo));
        }

        [Test]
        public void ExtraerLexemas_TextoNormal_RegresaListaLexemas()
        {
            //Preparar
            string codigo = "int x = 4; double y; int z = 44;";

            //Ejecutar
            List<Lexema> lexemas = _anaLex.ExtraerLexemas(codigo);

            List<Lexema> lexemasEsperados = new List<Lexema>()
            {
                new Lexema(){Texto = "int"},
                new Lexema(){Texto = "x"},
                new Lexema(){Texto = "="},
                new Lexema(){Texto = "4"},
                new Lexema(){Texto = ";"},
                new Lexema(){Texto = "double"},
                new Lexema(){Texto = "y"},
                new Lexema(){Texto = ";"},
                new Lexema(){Texto = "int"},
                new Lexema(){Texto = "z"},
                new Lexema(){Texto = "="},
                new Lexema(){Texto = "44"},
                new Lexema(){Texto = ";"},
            };

            //Probar
            Assert.That(lexemas[0].Texto, Is.EqualTo(lexemasEsperados[0].Texto));
            Assert.That(lexemas[4].Texto, Is.EqualTo(lexemasEsperados[4].Texto));
            Assert.That(lexemas[7].Texto, Is.EqualTo(lexemasEsperados[7].Texto));
            Assert.That(lexemas[11].Texto, Is.EqualTo(lexemasEsperados[11].Texto));
        }

        [Test]
        public void ExtrarLexemas_SinCodigo_RegresaListaVacia()
        {
            //Preparar
            string codigo = "";

            //Ejecutar
            List<Lexema> lexemas = _anaLex.ExtraerLexemas(codigo);

            //Probar
            Assert.That(lexemas, Is.Empty);
        }

        [Test]
        public void ProcesoCompleto_AnalizaCodigoComentarios_RegresaListadoLexemas()
        {
            //Preparar
            string codigo = "const int x = 4; //Declaracion de la variable 4\r\n\r\nint y = 44;\r\n\r\nint z=x*y; x-=1; if(x == 4 && !y || z != 4 ){}[x]";

            //Ejecutar
            string codigoSinComentarios = _anaLex.RetirarComentarios(codigo);
            string codigoSinSaltos = _anaLex.RetirarSaltos(codigoSinComentarios);               
            List<Lexema> lexemas = _anaLex.ExtraerLexemas(codigoSinSaltos);

            //Probar
            Assert.That(lexemas.Count, Is.EqualTo(41));
            Assert.That(lexemas[3].Texto, Is.EqualTo("="));
            Assert.That(lexemas[12].Texto, Is.EqualTo("z"));
            Assert.That(lexemas[19].Texto, Is.EqualTo("-="));
            Assert.That(lexemas[22].Texto, Is.EqualTo("if"));
            Assert.That(lexemas[25].Texto, Is.EqualTo("=="));
            Assert.That(lexemas[1].TipoElemento, Is.EqualTo(Enums.TipoElemento.TipoVariable));
        }

        [Test]
        public void ExtraerLexemas_LexicoInvalido_RegresaLexemaError()
        {
            //Preparar
            string codigo = "int 44fs;";

            //Ejecutar
            List<Lexema> lexemas = _anaLex.ExtraerLexemas(codigo);

            //Probar
            Assert.That(lexemas[1].TipoElemento, Is.EqualTo(Enums.TipoElemento.Error));
        }

        [Test]
        public void ExtraerLexemas_VariableConGuion_RegresaTipoVariable()
        {
            //Preparar
            string codigo = "int _var";

            //Ejecutar
            List<Lexema> lexemas = _anaLex.ExtraerLexemas(codigo);

            //Probar
            Assert.That(lexemas[1].TipoElemento, Is.EqualTo(Enums.TipoElemento.Variable));
            Assert.That(lexemas[1].Texto, Is.EqualTo("_var"));
        }

    }
}
