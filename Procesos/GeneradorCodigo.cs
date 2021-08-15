using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class GeneradorCodigo
    {

        public GeneradorCodigo()
        {
            Triplos = new List<Tripleta>();
            ExpresionTripletas = new List<ExpresionTripleta>();
        }

        public List<Tripleta> Triplos;

        public List<ExpresionTripleta> ExpresionTripletas { get; set; }


        public List<ExpresionTripleta> GenerarCodigo(List<Bloque> bloques)
        {
            Triplos = new List<Tripleta>();
            ExpresionTripletas = new List<ExpresionTripleta>();
            foreach (Bloque bloque in bloques)
            {
                ProcesarBloque(bloque);
            }

            return ExpresionTripletas;
        }


        private void ProcesarBloque(Bloque bloque)
        {
            //Entra acá si estoy haciendo una declaracion
            if (bloque.TipoBloque == Enums.TipoBloque.Estructura)
            {
             ProcesarEstructura(bloque);   
            }
            else if (bloque.Lexemas[0].TipoElemento == Enums.TipoElemento.TipoDato &&
                bloque.Lexemas[1].TipoElemento == Enums.TipoElemento.Variable && bloque.Lexemas[2].TipoElemento ==
                Enums.TipoElemento.OperadorTerminador)
            {
                Tripleta tripleta = GenerarTripleta(null, bloque.Lexemas[0], bloque.Lexemas[1], bloque);
                GenerarExpesion(tripleta);
            }
            //Verifico si es una asignación simple con declaración
            else if (bloque.Lexemas[0].TipoElemento == Enums.TipoElemento.TipoDato &&
                     bloque.Lexemas[1].TipoElemento == Enums.TipoElemento.Variable &&
                     bloque.Lexemas[2].TipoElemento == Enums.TipoElemento.OperadorAsignacion &&
                     (bloque.Lexemas[3].TipoElemento == Enums.TipoElemento.Variable ||
                      bloque.Lexemas[3].TipoElemento == Enums.TipoElemento.Numero ||
                      bloque.Lexemas[3].TipoElemento == Enums.TipoElemento.Cadena) && bloque.Lexemas[4].TipoElemento ==
                     Enums.TipoElemento.OperadorTerminador)
            {
                Tripleta tripleta = GenerarTripleta(null, bloque.Lexemas[0], bloque.Lexemas[1], bloque);
                GenerarExpesion(tripleta);
            }

            //Si se realiza alguna operacion aritmetica en el bloque
            else if (bloque.Lexemas.Any(x => x.TipoElemento == Enums.TipoElemento.OperadorAritmetico))
            {
                ProcesarOperacion(bloque);
            }
            else if (bloque.Lexemas[0].TipoElemento == Enums.TipoElemento.Variable && bloque.Lexemas[1].TipoElemento ==
                     Enums.TipoElemento.OperadorIncremental)
            {
                Tripleta tripleta = BuscarTripletaPorNombre(bloque.Lexemas[0].Texto);
                GenerarExpesion(tripleta,
                    new Lexema() {Texto = "+", TipoElemento = Enums.TipoElemento.OperadorAritmetico}, null,
                    new Lexema() {Texto = "1", TipoElemento = Enums.TipoElemento.Numero},
                    tripleta, null);
            }
            else if (bloque.Lexemas[0].TipoElemento == Enums.TipoElemento.Variable && bloque.Lexemas[1].TipoElemento ==
                     Enums.TipoElemento.OperadorDecremental)
            {
                Tripleta tripleta = BuscarTripletaPorNombre(bloque.Lexemas[0].Texto);
                GenerarExpesion(tripleta,
                    new Lexema() { Texto = "-", TipoElemento = Enums.TipoElemento.OperadorAritmetico }, null,
                    new Lexema() { Texto = "1", TipoElemento = Enums.TipoElemento.Numero },
                    tripleta, null);
            }
        }


        private void ProcesarOperacion(Bloque bloque)
        {
            //En esta accion se cae cuando queremos procesar un bloque con operaciones

            Tripleta tripletaInicial = new Tripleta();
            //Primero vemos el primero valor, si es una asignacion o hay declaracion
            if (bloque.Lexemas[0].TipoElemento == Enums.TipoElemento.TipoDato)
            {
                //Entra aca si estamos instanciando y asignando, entonces primero debemos hacer 
                tripletaInicial = GenerarTripleta(null, bloque.Lexemas[0], bloque.Lexemas[1], bloque);
                GenerarExpesion(tripletaInicial);
            }
            else
            {
                //Entra aca si es que el elemento no se está asignando, es decir, ya fue asignado antes
                tripletaInicial = BuscarTripletaPorNombre(bloque.Lexemas[0].Texto);
            }

            int cantOp = bloque.Lexemas.Count(x => x.TipoElemento == Enums.TipoElemento.OperadorAritmetico);

            if (cantOp == 1) //Solo hay un operador
            {
                //Al entrar aca la expresion es de la forma x= y + z
                int indiceOp = bloque.Lexemas.IndexOf(
                    bloque.Lexemas.First(x => x.TipoElemento == Enums.TipoElemento.OperadorAritmetico));

                Lexema lexemaUno = new Lexema();
                Lexema lexemaDos = new Lexema();
                Tripleta tripletaUno = new Tripleta();
                Tripleta tripletaDos = new Tripleta();

                if (bloque.Lexemas[indiceOp - 1].TipoElemento != Enums.TipoElemento.Variable)
                {
                    lexemaUno = bloque.Lexemas[indiceOp - 1];
                    tripletaUno = null;
                }
                else
                {
                    tripletaUno = BuscarTripletaPorNombre(bloque.Lexemas[indiceOp - 1].Texto);
                    lexemaUno = null;
                }

                if (bloque.Lexemas[indiceOp + 1].TipoElemento != Enums.TipoElemento.Variable)
                {
                    lexemaDos = bloque.Lexemas[indiceOp + 1];
                    tripletaDos = null;
                }
                else
                {
                    tripletaDos = BuscarTripletaPorNombre(bloque.Lexemas[indiceOp + 1].Texto);
                    lexemaDos = null;
                }


                GenerarExpesion(tripletaInicial, bloque.Lexemas[indiceOp], lexemaUno, lexemaDos, tripletaUno,
                    tripletaDos);
            }
            else if (cantOp == 2) //Hay dos operadores
            {
                //Al entrar aca la expresion es de la forma x = y + z + d
                //Primero vemos si es que algun operador es * o /

                if (bloque.Lexemas.Any(x => x.Texto == "*" || x.Texto == "/")) //Si hay * o /
                {
                    int indicePrimerOp = bloque.Lexemas.IndexOf(
                        bloque.Lexemas.First(x => x.TipoElemento == Enums.TipoElemento.OperadorAritmetico));
                    Tripleta nuevaTripleta = null;
                    Lexema segundoLexema = null;
                    Tripleta segundaTripleta = null;


                    //Si el primer operando es * o /
                    if (bloque.Lexemas[indicePrimerOp].Texto == "*" || bloque.Lexemas[indicePrimerOp].Texto == "/")
                    {
                        nuevaTripleta = GenerarTripleta(bloque.Lexemas[indicePrimerOp],
                            bloque.Lexemas[indicePrimerOp - 1], bloque.Lexemas[indicePrimerOp + 1], bloque);

                        GenerarExpesion(nuevaTripleta);

                        //A partir de aca falta leer el segundo operando y su variable

                        if (bloque.Lexemas[indicePrimerOp + 3].TipoElemento == Enums.TipoElemento.Variable)
                        {
                            segundaTripleta = BuscarTripletaPorNombre(bloque.Lexemas[indicePrimerOp + 3].Texto);
                        }
                        else
                        {
                            segundoLexema = bloque.Lexemas[indicePrimerOp + 3];
                        }

                        GenerarExpesion(tripletaInicial, bloque.Lexemas[indicePrimerOp + 2], null, segundoLexema,
                            nuevaTripleta, segundaTripleta);
                    }
                    else
                    {
                        nuevaTripleta = GenerarTripleta(bloque.Lexemas[indicePrimerOp +2],
                            bloque.Lexemas[indicePrimerOp + 1], bloque.Lexemas[indicePrimerOp + 3], bloque);
                        GenerarExpesion(nuevaTripleta);

                        //A partir de aca falta leer el segundo operando y su variable

                        if (bloque.Lexemas[indicePrimerOp  - 1].TipoElemento == Enums.TipoElemento.Variable)
                        {
                            segundaTripleta = BuscarTripletaPorNombre(bloque.Lexemas[indicePrimerOp + 3].Texto);
                        }
                        else
                        {
                            segundoLexema = bloque.Lexemas[indicePrimerOp - 1];
                        }

                        GenerarExpesion(tripletaInicial,bloque.Lexemas[indicePrimerOp], segundoLexema,null, segundaTripleta,nuevaTripleta);

                    }
                }
                else //Entra aca si no hay * o /
                {
                    int indicePrimerOp = bloque.Lexemas.IndexOf(
                        bloque.Lexemas.First(x => x.TipoElemento == Enums.TipoElemento.OperadorAritmetico));
                    Tripleta nuevaTripleta = GenerarTripleta(bloque.Lexemas[indicePrimerOp],
                        bloque.Lexemas[indicePrimerOp - 1], bloque.Lexemas[indicePrimerOp + 1], bloque);

                    GenerarExpesion(nuevaTripleta);
                    Lexema segundoLexema = null;
                    Tripleta segundaTripleta = null;


                    //A partir de aca falta leer el segundo operando y su variable

                    if (bloque.Lexemas[indicePrimerOp + 3].TipoElemento == Enums.TipoElemento.Variable)
                    {
                        segundaTripleta = BuscarTripletaPorNombre(bloque.Lexemas[indicePrimerOp + 3].Texto);
                    }
                    else
                    {
                        segundoLexema = bloque.Lexemas[indicePrimerOp + 3];
                    }

                    GenerarExpesion(tripletaInicial, bloque.Lexemas[indicePrimerOp + 2], null, segundoLexema,
                        nuevaTripleta, segundaTripleta);
                }

            }
        }

        private void ProcesarEstructura(Bloque bloque)
        {
            if (bloque.Lexemas[0].Texto == "while") //Estamos ante un while
            {
                ProcesarWhile(bloque);
            }
            else if (bloque.Lexemas[0].Texto == "do") //Estamos en un do-while
            {
                ProcesarDoWhile(bloque);
            }
            else if (bloque.Lexemas[0].Texto == "if") //Si es un if
            {
                ProcesarIf(bloque);
            }
            else if (bloque.Lexemas[0].Texto == "else")
            {
                ProcesarElse(bloque);
            }
            else if (bloque.Lexemas[0].Texto == "for")
            {
                ProcesarFor(bloque);
            }
        }

        private void ProcesarWhile(Bloque bloque)
        {
            //Primero debemos saber que dentro del while hay toda una estructura, entonces mandamos a procesar los bloques internos

            
            int indiceInicio = bloque.Lexemas.FindIndex(x => x.Texto == "(");
            int indiceFin = bloque.Lexemas.FindIndex(x => x.Texto == ")");
            List<Lexema> lexemasCondicion = bloque.Lexemas.Select((v, i) => new {Index = i, Value = v})
                .Where(p => p.Index > indiceInicio && p.Index < indiceFin)
                .Select(y => y.Value)
                .ToList();

            string textoCondicion = GenerarTextoCondicion(lexemasCondicion);
            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            int iniciaBucle = code + 2;
            string textoIf = "IF " + textoCondicion + "goto " + "L" + iniciaBucle;
            //Mandamos a generar la expresión padre
            GenerarExpresionTexto(textoIf);

            //Ahora se manda a procesar los bloques internos
            foreach (Bloque bloqueInterno in bloque.BloquesInterno)
            {
                ProcesarBloque(bloqueInterno);
            }

            //Ahora que se han procesado los bloques, creamos otro if
            GenerarExpresionTexto(textoIf);

        }

        private void ProcesarDoWhile(Bloque bloque)
        {
            //Primero debemos setear donde es que inicia nuestro bloque, en que L
            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            int iniciaBucle = code + 1;
            //Ahora se manda a procesar los bloques internos
            foreach (Bloque bloqueInterno in bloque.BloquesInterno)
            {
                ProcesarBloque(bloqueInterno);
            }

            int indiceInicio = bloque.Lexemas.FindIndex(x => x.Texto == "(");
            int indiceFin = bloque.Lexemas.FindIndex(x => x.Texto == ")");
            List<Lexema> lexemasCondicion = bloque.Lexemas.Select((v, i) => new { Index = i, Value = v })
                .Where(p => p.Index > indiceInicio && p.Index < indiceFin)
                .Select(y => y.Value)
                .ToList();

            string textoCondicion = GenerarTextoCondicion(lexemasCondicion);
            string textoIf = "IF " + textoCondicion + "goto " + "L" + iniciaBucle;
            GenerarExpresionTexto(textoIf);


        }

        private void ProcesarIf(Bloque bloque)
        {

            //Primero debemos setear donde es que inicia nuestro bloque, en que L
            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            int iniciaBucle = code + 2;

            int indiceInicio = bloque.Lexemas.FindIndex(x => x.Texto == "(");
            int indiceFin = bloque.Lexemas.FindIndex(x => x.Texto == ")");
            List<Lexema> lexemasCondicion = bloque.Lexemas.Select((v, i) => new { Index = i, Value = v })
                .Where(p => p.Index > indiceInicio && p.Index < indiceFin)
                .Select(y => y.Value)
                .ToList();

            string textoCondicion = GenerarTextoCondicion(lexemasCondicion);
            string textoIf = "IF " + textoCondicion + "goto " + "L" + iniciaBucle;
            GenerarExpresionTexto(textoIf);

            //Ahora se manda a procesar los bloques internos
            foreach (Bloque bloqueInterno in bloque.BloquesInterno)
            {
                ProcesarBloque(bloqueInterno);
            }


        }

        private void ProcesarElse(Bloque bloque)
        {
            //Primero debemos setear donde es que inicia nuestro bloque, en que L
            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            int iniciaBucle = code + 2;

            

            
            string textoIf = "ELSE " + "goto " + "L" + iniciaBucle;
            GenerarExpresionTexto(textoIf);

            //Ahora se manda a procesar los bloques internos
            foreach (Bloque bloqueInterno in bloque.BloquesInterno)
            {
                ProcesarBloque(bloqueInterno);
            }
        }

        private void ProcesarFor(Bloque bloque)
        {
            int indiceInicio = bloque.Lexemas.FindIndex(x => x.Texto == "(");
            int indiceFin = bloque.Lexemas.FindIndex(x => x.Texto == ")");
            List<Lexema> lexemasCondicionFor = bloque.Lexemas.Select((v, i) => new { Index = i, Value = v })
                .Where(p => p.Index > indiceInicio && p.Index < indiceFin)
                .Select(y => y.Value)
                .ToList();

            int indicePrimerBloque = bloque.Lexemas.FindIndex(x => x.Texto == ";");
            Bloque primeroBloque = new Bloque();
            for (int i = 0; i < indicePrimerBloque -1; i++)
            {
                primeroBloque.Lexemas.Add(lexemasCondicionFor[i]);
            }

            int indiceSegundoBloque = bloque.Lexemas.FindLastIndex(x => x.Texto == ";");
            Bloque bloqueFinal = new Bloque();
            for (int i = indiceSegundoBloque -1; i < indiceFin -2; i++)
            {
                bloqueFinal.Lexemas.Add(lexemasCondicionFor[i]);
                
            }

            
            List<Lexema> lexemasBloqueCondicion = bloque.Lexemas.Select((v, i) => new { Index = i, Value = v })
                .Where(p => p.Index > indicePrimerBloque && p.Index < indiceSegundoBloque)
                .Select(y => y.Value)
                .ToList();


            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            int iniciaBucle = code + 2;
            ProcesarBloque(primeroBloque); //Se procesa de primero el bloque del iniciador del for

            //Se procesa lo que tiene dentro el for
            foreach (Bloque bloque1 in bloque.BloquesInterno)
            {
                ProcesarBloque(bloque1);
            }

            //Agregamos el bloque de aumento
            ProcesarBloque(bloqueFinal);

            //Se agrega el if
            string textoCondicion = GenerarTextoCondicion(lexemasBloqueCondicion);
            string textoIf = "IF " + textoCondicion + "goto " + "L" + iniciaBucle;
            GenerarExpresionTexto(textoIf);
        }

        private string GenerarTextoCondicion(List<Lexema> lexemasCondicion)
        {
            string textoResult = "";

            foreach (Lexema lexema in lexemasCondicion)
            {
                if (lexema.TipoElemento == Enums.TipoElemento.Variable)
                {
                    Tripleta tripleta = BuscarTripletaPorNombre(lexema.Texto);
                    textoResult = textoResult + "T" + tripleta.Codigo + " ";
                }
                else
                {
                    textoResult = textoResult + lexema.Texto + " ";
                }
            }

            return textoResult;
        }

        private Tripleta GenerarTripleta(Lexema operador, Lexema primerOp, Lexema segundoOp, Bloque bloque)
            {
                int code = Triplos.Count == 0 ? 0 : Triplos.Max(x => x.Codigo);

                Tripleta tripleta = new Tripleta()
                {
                    Codigo = code + 1,
                    Operador = operador,
                    PrimerOperando = primerOp,
                    SegundoOperando = segundoOp,
                    Bloque = bloque
                };

                Triplos.Add(tripleta);
                return tripleta;
            }

        private ExpresionTripleta GenerarExpesion(Tripleta tripletaInicial, Lexema operador = null,
            Lexema primerLexema = null,
            Lexema segundoLexema = null, Tripleta primerTripleta = null, Tripleta segundaTripleta = null)
        {

            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            ExpresionTripleta expresion = new ExpresionTripleta()
            {
                Codigo = code + 1,
                TripletaInicial = tripletaInicial,
                Operador = operador,
                PrimerTripleta = primerTripleta,
                SegundaTripleta = segundaTripleta,
                PrimerLexema = primerLexema,
                SegundoLexema = segundoLexema
            };

            ExpresionTripletas.Add(expresion);
            return expresion;
        }

        private ExpresionTripleta GenerarExpresionTexto(string texto)
        {
            int code = ExpresionTripletas.Count == 0 ? 0 : ExpresionTripletas.Max(x => x.Codigo);
            ExpresionTripleta expresion = new ExpresionTripleta()
            {
                Codigo = code + 1,
                TextoMandatorio = texto
                
            };

            ExpresionTripletas.Add(expresion);
            return expresion;
        }

        private Tripleta BuscarTripletaPorBloque(Bloque bloque)
        {
            return Triplos
                .FirstOrDefault(x => x.Bloque.Incia == bloque.Incia && x.Bloque.Finaliza == bloque.Finaliza);
        }

        private Tripleta BuscarTripletaPorNombre(string nombre)
        {
            return Triplos.FirstOrDefault(x => x.SegundoOperando.Texto == nombre);
        }

        public string GenerarTextoResult(List<ExpresionTripleta> expresiones)
        {
            string result = "";

            foreach (var expresion in expresiones)
            {
                if (string.IsNullOrEmpty(expresion.TextoMandatorio))
                {
                    if (expresion.Operador == null)
                    {
                        if (expresion.TripletaInicial.Operador == null)
                        {
                            result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                     expresion.TripletaInicial.SegundoOperando.Texto + "\r\n";
                        }
                        else if (expresion.TripletaInicial.PrimerOperando != null &&
                                 expresion.TripletaInicial.SegundoOperando != null)
                        {
                            if (expresion.TripletaInicial.PrimerOperando.TipoElemento == Enums.TipoElemento.Variable &&
                                expresion.TripletaInicial.SegundoOperando.TipoElemento != Enums.TipoElemento.Variable)
                            {
                                result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                         "T" + BuscarTripletaPorNombre(expresion.TripletaInicial.PrimerOperando.Texto)
                                             .Codigo +
                                         expresion.TripletaInicial.Operador.Texto +
                                         expresion.TripletaInicial.SegundoOperando.Texto + "\r\n";
                            }
                            else if (expresion.TripletaInicial.PrimerOperando.TipoElemento !=
                                     Enums.TipoElemento.Variable &&
                                     expresion.TripletaInicial.SegundoOperando.TipoElemento ==
                                     Enums.TipoElemento.Variable)
                            {
                                result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                         expresion.TripletaInicial.PrimerOperando.Texto +
                                         expresion.TripletaInicial.Operador.Texto +
                                         "T" + BuscarTripletaPorNombre(expresion.TripletaInicial.SegundoOperando.Texto)
                                             .Codigo + "\r\n";
                            }
                            else if (expresion.TripletaInicial.PrimerOperando.TipoElemento ==
                                     Enums.TipoElemento.Variable &&
                                     expresion.TripletaInicial.SegundoOperando.TipoElemento ==
                                     Enums.TipoElemento.Variable)
                            {
                                result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                         "T" + BuscarTripletaPorNombre(expresion.TripletaInicial.PrimerOperando.Texto)
                                             .Codigo +
                                         expresion.TripletaInicial.Operador.Texto +
                                         "T" + BuscarTripletaPorNombre(expresion.TripletaInicial.SegundoOperando.Texto)
                                             .Codigo + "\r\n";
                            }
                            else if (expresion.TripletaInicial.PrimerOperando.TipoElemento !=
                                     Enums.TipoElemento.Variable &&
                                     expresion.TripletaInicial.SegundoOperando.TipoElemento !=
                                     Enums.TipoElemento.Variable)
                            {
                                result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                         expresion.TripletaInicial.PrimerOperando.Texto +
                                         expresion.TripletaInicial.Operador.Texto +
                                         expresion.TripletaInicial.SegundoOperando.Texto + "\r\n";
                            }
                        }
                    }
                    else if (expresion.Operador != null && expresion.PrimerTripleta != null &&
                             expresion.SegundaTripleta != null)
                    {
                        result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " + "T" +
                                 expresion.PrimerTripleta.Codigo + expresion.Operador.Texto + "T" +
                                 expresion.SegundaTripleta.Codigo + "\r\n";
                    }
                    else if (expresion.Operador != null && expresion.PrimerLexema != null &&
                             expresion.SegundoLexema != null)
                    {
                        result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                 expresion.PrimerLexema.Texto +
                                 expresion.Operador.Texto + expresion.SegundoLexema.Texto + "\r\n";
                    }
                    else if (expresion.Operador != null && expresion.PrimerTripleta != null &&
                             expresion.SegundoLexema != null)
                    {
                        result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " + "T" +
                                 expresion.PrimerTripleta.Codigo +
                                 expresion.Operador.Texto + expresion.SegundoLexema.Texto + "\r\n";
                    }
                    else if (expresion.Operador != null && expresion.PrimerLexema != null &&
                             expresion.SegundaTripleta != null)
                    {
                        result = result + "L" + expresion.Codigo + ": " + "T" + expresion.TripletaInicial.Codigo + ":= " +
                                 expresion.PrimerLexema.Texto +
                                 expresion.Operador.Texto + "T" + expresion.SegundaTripleta.Codigo + "\r\n";
                    }
                }
                else
                {
                    result = result + expresion.TextoMandatorio + "\r\n";
                }


               
            }


            return result;
        }

        }
    }

    