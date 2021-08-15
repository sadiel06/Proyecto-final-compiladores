using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procesos
{
    public class AnalizadorSintactico
    {
        public AnalizadorSintactico()
        {
            TablaSimbolos = new TablaSimbolos();
        }

        public TablaSimbolos TablaSimbolos { get; set; }

        public void ConstruirTablaSimbolos(List<Lexema> lexemas)
        {
            TablaSimbolos = new TablaSimbolos();
            TablaSimbolos.ProcesarListadoLexemas(lexemas);
        }

        #region Reglas




        public string AplicarReglasTipoVar(int indice, List<Lexema> lexemas)
        {
            string mensajeError = "";
            Lexema lexema = lexemas[indice]; //Saco el elemento para el cual voy a analizar
            if (lexema.TipoElemento != Enums.TipoElemento.TipoDato) //Si no es del tipo var, exploto
            {
                throw new ArgumentException("El tipo de elemento no es del tipo identificador de tipeo");
            }

            if (!EsUltimoElemento(indice, lexemas))
            {

                Lexema lexemaSiguiente = lexemas[indice + 1];
                if (lexemaSiguiente.TipoElemento != Enums.TipoElemento.Variable &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Corchete)
                {
                    mensajeError = "Error de sintaxis, se esperaba un identificador, pero se encontró " + lexemaSiguiente.Texto + ". Elemento: " + lexema.Texto;
                }
                else if (lexemaSiguiente.TipoElemento == Enums.TipoElemento.Corchete)
                {
                    if (lexemaSiguiente.Texto == "]")
                    {
                        mensajeError = "Error de sintaxis, construccion erronea de un arreglo. Elemento: " + lexema.Texto;
                    }
                    else
                    {
                        for (int i = indice + 1; i < lexemas.Count; i++)
                        {
                            if (lexemas[i].Texto == ";")
                            {
                                mensajeError = "Error de sintaxis, construccion erronea de un arreglo. Elemento: " + lexema.Texto;
                                break;
                            }
                            if (TablaSimbolos.Tokens[i].NombreToken == "]")
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                mensajeError = "Error de sintaxis, se esperaba ;. Elemento: " + lexema.Texto;
            }

            return mensajeError;
        }

        public string AplicarReglasVariable(int indice, List<Lexema> lexemas)
        {
            string mensajeError = "";
            Lexema lexema = lexemas[indice]; //Saco el elemento para el cual voy a analizar
            if (lexema.TipoElemento != Enums.TipoElemento.Variable) //Si no es del tipo var, exploto
            {
                throw new ArgumentException("El tipo de elemento no es del tipo variable");
            }

            if (!EsUltimoElemento(indice, lexemas))
            {
                Lexema lexemaSiguiente = lexemas[indice + 1];

                if (lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorAsignacion &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorAritmetico &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorDecremental &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorIncremental &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorLogico &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorRelacional &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Coma &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.OperadorTerminador &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Parentesis)
                {
                    mensajeError = "Sintaxis incorrecta para el tipo variable. Elemento: " + lexema.Texto;
                }
            }
            else
            {
                mensajeError = "Error de sintaxis, se esperaba ;. Elemento: " + lexema.Texto;
            }
            return mensajeError;

        }

        public string AplicarReglasAsignacion(int indice, List<Lexema> lexemas)
        {
            string mensajeError = "";
            Lexema lexema = lexemas[indice]; //Saco el elemento para el cual voy a analizar
            if (lexema.TipoElemento != Enums.TipoElemento.OperadorAsignacion) //Si no es del tipo var, exploto
            {
                throw new ArgumentException("El tipo de elemento no es del tipo operador asignacion");
            }

            if (!EsUltimoElemento(indice, lexemas))
            {
                Lexema lexemaSiguiente = lexemas[indice + 1];

                if (lexemaSiguiente.TipoElemento != Enums.TipoElemento.Variable &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Numero &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Cadena &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Caracter &&
                    lexemaSiguiente.TipoElemento != Enums.TipoElemento.Parentesis)
                {
                    mensajeError = "Sintaxis incorrecta, asignacion no válida. Elemento: " + lexema.Texto;
                }
            }
            else
            {
                mensajeError = "Error de sintaxis, se esperaba un valor para asignar. Elemento: " + lexema.Texto;
            }
            return mensajeError;
        }

        public string AplicarReglasOperadorAritmetico(int indice, List<Lexema> lexemas)
        {
            string mensajeError = "";
            Lexema lexema = lexemas[indice]; //Saco el elemento para el cual voy a analizar
            if (lexema.TipoElemento != Enums.TipoElemento.OperadorAritmetico) //Si no es del tipo var, exploto
            {
                throw new ArgumentException("El tipo de elemento no es del tipo operador aritmetico");
            }

            if (!EsUltimoElemento(indice, lexemas))
            {
                Lexema lexemaSiguiente = lexemas[indice + 1];
                Lexema lexemaAnterior = lexemas[indice - 1];

                if ((lexemaAnterior.TipoElemento != Enums.TipoElemento.Numero &&
                     lexemaAnterior.TipoElemento != Enums.TipoElemento.Variable &&
                     lexemaAnterior.TipoElemento != Enums.TipoElemento.Parentesis) ||
                    (lexemaSiguiente.TipoElemento != Enums.TipoElemento.Numero &&
                     lexemaSiguiente.TipoElemento != Enums.TipoElemento.Variable &&
                     lexemaSiguiente.TipoElemento != Enums.TipoElemento.Parentesis))
                {
                    mensajeError = "Sintaxis incorrecta, uso inadecuado del operador. Elemento: " +"\""+ lexema.Texto + "\"";
                }
            }
            else
            {
                mensajeError = "Error de sintaxis, se esperaba un valor para operar. Elemento: " + lexema.Texto;
            }
            return mensajeError;
        }

        public bool EsUltimoElemento(int indice, List<Lexema> lexemas)
        {
            return indice == lexemas.Count - 1;
        }

        #endregion

        public List<Bloque> RealizarAnalisisSintax(List<Lexema> lexemas,ref int posCursor, int indiceStop = 0, int finBloquePadre = 0)
        {
            List<Bloque> bloques = new List<Bloque>();

            while (posCursor < indiceStop)
            {
                bloques.Add(ObtenerSiguienteBloque(lexemas,ref posCursor, finBloquePadre));
            }

            return bloques;
        }

        public Bloque ObtenerSiguienteBloque(List<Lexema> lexemas,ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            //Si el elemento leido es del tipo int,float,char, etc
            if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.TipoDato)
            {
                //Debo verficar que si se trata de una sentencia o de una funcion
                if (lexemas[posCursor + 1].TipoElemento == Enums.TipoElemento.Variable)
                {
                    bloque = ProcesarTipoVariable(lexemas, ref posCursor, finBloquePadre);
                }
                else
                {
                    bloque = BloqueError(lexemas, "Se esperaba una declaración despues de " + lexemas[posCursor].Texto,
                        ref posCursor, finBloquePadre);
                }
                
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.Variable)
            {
                bloque = ProcesarVariable(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "return")
            {
                bloque = AnalizarSentencia(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "for")
            {
                bloque = AnalizarFor(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "while")
            {
                bloque = AnalizarWhile(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "do")
            {
                bloque = AnalizarDoWhile(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "if")
            {
                bloque = AnalizarIf(lexemas, ref posCursor, finBloquePadre);
            }
            else if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.PalabraReservada &&
                     lexemas[posCursor].Texto == "else")
            {
                bloque = AnalizarElse(lexemas, ref posCursor, finBloquePadre);
            }


            return bloque;
        }

        #region Procesador de sintaxis

        public Bloque ProcesarTipoVariable(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {

            if (posCursor + 2 < lexemas.Count - 1 && lexemas[posCursor + 2].Texto == "(") //Se trata de una funcion
            {
                int indiceBalance = IndiceBalance(lexemas, posCursor + 3, "(", ")");
                if (indiceBalance == -1)
                {
                    return BloqueError(lexemas, "Parentesis mal construidos", ref posCursor, finBloquePadre);
                }

                if (lexemas[indiceBalance + 1].Texto == "{")
                {
                    return AnalizarBloqueFuncion(lexemas, ref posCursor, finBloquePadre);
                }
                else if (lexemas[indiceBalance + 1].TipoElemento == Enums.TipoElemento.OperadorAritmetico ||
                         lexemas[indiceBalance + 1].TipoElemento == Enums.TipoElemento.OperadorLogico ||
                         lexemas[indiceBalance + 1].TipoElemento == Enums.TipoElemento.OperadorRelacional ||
                         lexemas[indiceBalance + 1].TipoElemento == Enums.TipoElemento.OperadorTerminador)
                {
                    return AnalizarSentencia(lexemas, ref posCursor, finBloquePadre);
                }

                return BloqueError(lexemas, "Error en la forma de llamar/construir una funcion", ref posCursor, finBloquePadre);


            }
            else if (posCursor + 2 < lexemas.Count - 1 && lexemas[posCursor + 2].TipoElemento == Enums.TipoElemento.OperadorAsignacion)
            {
                return AnalizarSentencia(lexemas, ref posCursor, finBloquePadre);
            }
            else
            {
                //Se trata de una sentencia
                return AnalizarSentencia(lexemas, ref posCursor, finBloquePadre);
            }
        }

        public Bloque ProcesarVariable(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            int indicePuntoComa = IndicePuntoComa(lexemas, posCursor);
            if (indicePuntoComa == -1 && PuntoComaCorrecto(lexemas, posCursor, indicePuntoComa) == false)
            {
                Bloque bloqueR = new Bloque()
                {
                    Incia = posCursor,
                    Error = "Sentencia de tipo variable mal construida, faltan ;"

                };
                posCursor++;
                return bloqueR;
            }
            if (lexemas[posCursor + 1].Texto == "=" || lexemas[posCursor + 1].Texto == "++" ||
                lexemas[posCursor + 1].Texto == "--" || lexemas[posCursor + 1].Texto == "+=" ||
                lexemas[posCursor + 1].Texto == "-=" || lexemas[posCursor + 1].Texto == "*=" ||
                lexemas[posCursor + 1].Texto == "/=")
            {
                return AnalizarSentencia(lexemas, ref posCursor, finBloquePadre);
            }

            return BloqueError(lexemas,
                "Se esperaba un simbolo de asignacion pero se encontro: " + lexemas[posCursor].Texto, ref posCursor,
                finBloquePadre);
        }

        public Bloque AnalizarBloqueFuncion(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Funcion;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;

            if (lexemas[posCursor].Texto == "(")
            {
                if (lexemas.FirstOrDefault(x => x.Texto == ")") != null)
                {
                    int indexFinParen = IndiceBalance(lexemas, posCursor + 1, "(", ")");
                    //Leo hasta el parentesis
                    for (int i = posCursor; i <= indexFinParen; i++)
                    {
                        bloque.Lexemas.Add(lexemas[posCursor]);
                        posCursor++;
                    }

                    if (lexemas[posCursor].Texto == "{")
                    {
                        bloque.Lexemas.Add(lexemas[posCursor]);
                        posCursor++;

                        //Debo mandar a leer mas bloques
                        int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                        if (indexBalance != -1) //Entonces esta balanceada
                        {

                            bloque.BloquesInterno = RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);
                            bloque.Finaliza = posCursor;
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;
                        }
                        else
                        {
                            bloque.Error = "Se esperaba cierre de funcion \"}\"";
                        }

                    }

                    else
                    {
                        bloque.Error = "Se esperaba {";
                    }
                }
                else
                {
                    bloque.Error = "Se esperaba )";
                }
            }
            else
            {
                bloque.Error = "Se esperaba (";
            }




            return bloque;
        }

        public Bloque AnalizarFor(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Estructura;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;

            if (lexemas.ElementAtOrDefault(posCursor) != null && lexemas[posCursor].Texto == "(")
            {

                if (lexemas.FirstOrDefault(x => x.Texto == ")") != null)
                {
                    int indexFinParen = IndiceBalance(lexemas, posCursor + 1, "(", ")");

                    //Validar estructura del for
                    int indicePrimerPuntoComa = IndicePuntoComa(lexemas, posCursor);
                    if (indicePrimerPuntoComa != -1 && InitForCorrecto(lexemas, posCursor, indicePrimerPuntoComa))
                    {

                        //Leo hasta el primer punto coma
                        for (int i = posCursor; i <= indicePrimerPuntoComa; i++)
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;
                        }

                        int indiceSeguntoPuntoComa = IndicePuntoComa(lexemas, posCursor);

                        if (indiceSeguntoPuntoComa != -1 &&
                            CondicionEstructuraCorrecta(lexemas, posCursor, indiceSeguntoPuntoComa))
                        {

                            //Leo hasta el segundo punto coma
                            for (int i = posCursor; i <= indiceSeguntoPuntoComa; i++)
                            {
                                bloque.Lexemas.Add(lexemas[posCursor]);
                                posCursor++;
                            }

                            if (IncrementoForCorrecto(lexemas, posCursor, indexFinParen))
                            {

                                //Leo hasta el parentesis
                                for (int i = posCursor; i <= indexFinParen; i++)
                                {
                                    bloque.Lexemas.Add(lexemas[posCursor]);
                                    posCursor++;
                                }

                                if (lexemas.ElementAtOrDefault(posCursor) != null && lexemas[posCursor].Texto == "{")
                                {
                                    bloque.Lexemas.Add(lexemas[posCursor]);
                                    posCursor++;

                                    //Debo mandar a leer mas bloques
                                    int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                                    if (indexBalance != -1) //Entonces esta balanceada
                                    {

                                        bloque.BloquesInterno =
                                            RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);
                                        bloque.Finaliza = posCursor;
                                        bloque.Lexemas.Add(lexemas[posCursor]);
                                        posCursor++;
                                    }
                                    else
                                    {
                                        bloque.Error = "Se esperaba cierre de funcion \"}\"";
                                    }

                                }

                                else
                                {
                                    bloque.Error = "Se esperaba {";
                                }
                            }
                            else
                            {
                                bloque.Error = "Estructura del for incorrecta, condicion de incremento incorrecta";
                            }



                        }
                        else
                        {
                            bloque.Error = "Estructura del for incorrecta, condicion del for incorrecta";
                        }

                    }
                    else
                    {
                        bloque.Error = "Estructura del for incorrecta, inicializador del for incorrecto";
                    }




                }
                else
                {
                    bloque.Error = "Se esperaba )";
                }
            }
            else
            {
                bloque.Error = "Se esperaba (";
            }

            if (string.IsNullOrEmpty(bloque.Error) == false)
            {
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }
            }


            return bloque;

        }

        public Bloque AnalizarWhile(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Estructura;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;

            if (lexemas[posCursor].Texto == "(")
            {

                if (lexemas.FirstOrDefault(x => x.Texto == ")") != null)
                {
                    bloque.Lexemas.Add(lexemas[posCursor]);
                    posCursor++;
                    int indexFinParen = IndiceBalance(lexemas, posCursor + 1, "(", ")");
                    if (CondicionEstructuraCorrecta(lexemas, posCursor, indexFinParen))
                    {
                        //Leo hasta el parentesis
                        for (int i = posCursor; i <= indexFinParen; i++)
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;
                        }

                        if (lexemas[posCursor].Texto == "{")
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;

                            //Debo mandar a leer mas bloques
                            int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                            if (indexBalance != -1) //Entonces esta balanceada
                            {

                                bloque.BloquesInterno =
                                    RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);
                                bloque.Finaliza = posCursor;
                                bloque.Lexemas.Add(lexemas[posCursor]);
                                posCursor++;
                            }
                            else
                            {
                                bloque.Error = "Se esperaba cierre de funcion \"}\"";
                            }

                        }

                        else
                        {
                            bloque.Error = "Se esperaba \"{\"";
                        }
                    }
                    else
                    {
                        bloque.Error = "La condicion del while es incorrecta";
                    }
                    
                }
                else
                {
                    bloque.Error = "Se esperaba \")\"";
                }
            }
            else
            {
                bloque.Error = "Se esperaba \"(\"";
            }

            if (string.IsNullOrEmpty(bloque.Error) == false)
            {
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }
            }


            return bloque;

        }

        public Bloque AnalizarDoWhile(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Estructura;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;

            if (lexemas[posCursor].Texto == "{")
            {
                bloque.Lexemas.Add(lexemas[posCursor]);
                posCursor++;

                //Debo mandar a leer mas bloques
                int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                if (indexBalance != -1) //Entonces esta balanceada
                {

                    bloque.BloquesInterno =
                        RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);

                    //Procedo a leer el while
                    if (lexemas[posCursor].Texto == "}")
                    {
                        bloque.Lexemas.Add(lexemas[posCursor]);
                        posCursor++;

                        if (lexemas[posCursor].Texto == "while")
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;
                            if (lexemas[posCursor].Texto == "(")
                            {

                                if (lexemas.FirstOrDefault(x => x.Texto == ")") != null)
                                {
                                    bloque.Lexemas.Add(lexemas[posCursor]);
                                    posCursor++;
                                    int indexFinParen = IndiceBalance(lexemas, posCursor + 1, "(", ")");
                                    if (CondicionEstructuraCorrecta(lexemas, posCursor, indexFinParen))
                                    {
                                        //Leo hasta el parentesis
                                        for (int i = posCursor; i <= indexFinParen; i++)
                                        {
                                            bloque.Lexemas.Add(lexemas[posCursor]);
                                            posCursor++;
                                        }

                                        if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.OperadorTerminador)
                                        {
                                            bloque.Lexemas.Add(lexemas[posCursor]);
                                            bloque.Finaliza = posCursor;
                                            posCursor++;
                                            
                                        }
                                        else
                                        {
                                            bloque.Error = "La sentencia do...while requiere terminar con \";\"";
                                        }
                                       
                                    }
                                    else
                                    {
                                        bloque.Error = "La condicion del while es incorrecta";
                                    }

                                }
                                else
                                {
                                    bloque.Error = "Se esperaba \")\"";
                                }
                            }
                            else
                            {
                                bloque.Error = "Se esperaba \"(\"";
                            }
                        }
                        else
                        {
                            bloque.Error = "Estructura do...while incorrecta, falta la condicion \"while\" ";

                        }



                    }
                    else
                    {
                        bloque.Error = "Se esperaba cierre de funcion \"}\"";
                    }
                    
                    
                }
                else
                {
                    bloque.Error = "Se esperaba cierre de funcion \"}\"";
                }

            }

            else
            {
                bloque.Error = "Se esperaba \"{\"";
            }

            if (string.IsNullOrEmpty(bloque.Error) == false)
            {
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }
            }


            return bloque;

        }

        public Bloque AnalizarIf(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Estructura;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;

            if (lexemas[posCursor].Texto == "(")
            {

                if (lexemas.FirstOrDefault(x => x.Texto == ")") != null)
                {
                    bloque.Lexemas.Add(lexemas[posCursor]);
                    posCursor++;
                    int indexFinParen = IndiceBalance(lexemas, posCursor + 1, "(", ")");
                    if (CondicionEstructuraCorrecta(lexemas, posCursor, indexFinParen))
                    {
                        //Leo hasta el parentesis
                        for (int i = posCursor; i <= indexFinParen; i++)
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;
                        }

                        if (lexemas[posCursor].Texto == "{")
                        {
                            bloque.Lexemas.Add(lexemas[posCursor]);
                            posCursor++;

                            //Debo mandar a leer mas bloques
                            int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                            if (indexBalance != -1) //Entonces esta balanceada
                            {

                                bloque.BloquesInterno =
                                    RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);
                                bloque.Finaliza = posCursor;
                                bloque.Lexemas.Add(lexemas[posCursor]);
                                posCursor++;
                            }
                            else
                            {
                                bloque.Error = "Se esperaba cierre de funcion \"}\"";
                            }

                        }

                        else
                        {
                            bloque.Error = "Se esperaba \"{\"";
                        }
                    }
                    else
                    {
                        bloque.Error = "La condicion del if es incorrecta";
                    }

                }
                else
                {
                    bloque.Error = "Se esperaba \")\"";
                }
            }
            else
            {
                bloque.Error = "Se esperaba \"(\"";
            }

            if (string.IsNullOrEmpty(bloque.Error) == false)
            {
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }
            }


            return bloque;

        }

        public Bloque AnalizarElse(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque();
            bloque.TipoBloque = Enums.TipoBloque.Estructura;
            bloque.Incia = posCursor;
            bloque.Lexemas.Add(lexemas[posCursor]);
            posCursor++;


            //Hay que ir a ver que haya un if justo arriba del else

            if (lexemas[posCursor].Texto == "{")
            {
                bloque.Lexemas.Add(lexemas[posCursor]);
                posCursor++;

                //Debo mandar a leer mas bloques
                int indexBalance = IndiceBalance(lexemas, posCursor, "{", "}");
                if (indexBalance != -1) //Entonces esta balanceada
                {

                    bloque.BloquesInterno =
                        RealizarAnalisisSintax(lexemas, ref posCursor, indexBalance - 1, indexBalance);
                    bloque.Finaliza = posCursor;
                    bloque.Lexemas.Add(lexemas[posCursor]);
                    posCursor++;
                }
                else
                {
                    bloque.Error = "Se esperaba cierre de funcion \"}\"";
                }

            }

            else
            {
                bloque.Error = "Se esperaba \"{\"";
            }

            if (string.IsNullOrEmpty(bloque.Error) == false)
            {
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }
            }


            return bloque;

        }

        public Bloque AnalizarSentencia(List<Lexema> lexemas, ref int posCursor, int finBloquePadre)
        {
            //Debo leer hasta encontrar un ;
            int indicePuntoComa = IndicePuntoComa(lexemas, posCursor);


            Bloque bloque = new Bloque();
            bloque.Incia = posCursor;

            if (indicePuntoComa == -1 || PuntoComaCorrecto(lexemas, posCursor, indicePuntoComa) == false)
            {
                bloque.Error = "Se esperaba ;";
                finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;
                for (int i = posCursor; i < finBloquePadre; i++)
                {
                    bloque.Lexemas.Add(lexemas[i]);
                    posCursor++;
                }

            }
            else if (!DelimitadoresBalanceados(lexemas, "(", ")", posCursor, indicePuntoComa))
            {
                return BloqueError(lexemas, "Los parentesis para esta sentencia no estan balanceados", ref posCursor,
                    finBloquePadre);
            }
            else
            {

                for (int i = posCursor; i <= indicePuntoComa; i++)
                {
                    string error = BuscarError(lexemas[i], i, lexemas);
                    if (string.IsNullOrEmpty(error))
                    {
                        bloque.Lexemas.Add(lexemas[i]);
                    }
                    else
                    {
                        bloque.Error = error;
                    }
                    
                    posCursor++;
                }
                //for (int i = posCursor; i <= indicePuntoComa; i++)
                //{
                //    bloque.Lexemas.Add(lexemas[i]);
                //    posCursor++;
                //}

            }

            bloque.Finaliza = posCursor - 1;
            return bloque;
        }

        public string BuscarError(Lexema lexema,int indiceLexema, List<Lexema> lexemas)
        {
            switch (lexema.TipoElemento)
            {
                case Enums.TipoElemento.Variable:
                    return AplicarReglasVariable(indiceLexema, lexemas);
                    
                case Enums.TipoElemento.TipoDato:
                    return AplicarReglasTipoVar(indiceLexema, lexemas);
                    
                case Enums.TipoElemento.OperadorAritmetico:
                    return AplicarReglasOperadorAritmetico(indiceLexema, lexemas);
                   
                case Enums.TipoElemento.OperadorRelacional:
                    return "";
                    
                case Enums.TipoElemento.OperadorLogico:
                    return "";
                    
                case Enums.TipoElemento.OperadorAsignacion:
                    return AplicarReglasAsignacion(indiceLexema, lexemas);
                    
                case Enums.TipoElemento.OperadorMisc:
                    return "";
                    
                case Enums.TipoElemento.PalabraDefinicion:
                    return "";
                    
                case Enums.TipoElemento.OperadorTerminador:
                    return "";
                    
                
                case Enums.TipoElemento.OperadorIncremental:
                    return "";
                    
                case Enums.TipoElemento.OperadorDecremental:
                    return "";
                    
                case Enums.TipoElemento.Cadena:
                    return "";
                    
                case Enums.TipoElemento.Caracter:
                    return "";
                    
                
            }

            return "";
        }

        #endregion

        #region Utilidades

        public bool DelimitadoresBalanceados(List<Lexema> lexemas, string simbolApertura, string simbolCierre,
            int indiceInicial, int indiceFinal)
        {
            int apertura = 0;
            int cierre = 0;

            for (int i = indiceInicial; i < indiceFinal; i++)
            {
                if (lexemas[i].Texto == simbolApertura)
                {
                    apertura++;
                }
                else if (lexemas[i].Texto == simbolCierre)
                {
                    cierre++;
                }
            }

            return apertura == cierre;
        }

        public int IndiceBalance(List<Lexema> lexemas, int posCursor, string simboloApertura, string simboloCierre)
        {
            bool salir = false;
            int abiertos = 0;

            do
            {
                if (posCursor < lexemas.Count)
                {
                    if (lexemas[posCursor].Texto == simboloApertura)
                    {
                        abiertos++;

                    }
                    else if (lexemas[posCursor].Texto == simboloCierre)
                    {
                        if (abiertos == 0) //Me encontre un } y no hay abiertos
                        {
                            salir = true;
                            break;
                        }
                        else
                        {
                            abiertos--;
                        }
                    }

                    posCursor++;
                }
                else
                {
                    posCursor = -1;
                    salir = true;
                }



            } while (!salir);

            return posCursor;
        }

        public int IndicePuntoComa(List<Lexema> lexemas, int posCursor)
        {
            bool salir = false;

            do
            {
                if (posCursor < lexemas.Count)
                {
                    if (lexemas[posCursor].Texto == ";")
                    {
                        salir = true;
                    }
                    else
                    {
                        posCursor++;
                    }
                }
                else
                {
                    salir = true;
                    posCursor = -1;
                }


            } while (!salir);



            return posCursor;
        }


        public Bloque BloqueError(List<Lexema> lexemas, string mensajeError, ref int posCursor, int finBloquePadre)
        {
            Bloque bloque = new Bloque()
            {
                Incia = posCursor,
                Error = mensajeError

            };
            finBloquePadre = finBloquePadre == 0 ? lexemas.Count : finBloquePadre;

            for (int i = posCursor; i < finBloquePadre; i++)
            {
                bloque.Lexemas.Add(lexemas[i]);
                posCursor++;
            }

            return bloque;
        }

        public bool PuntoComaCorrecto(List<Lexema> lexemas, int posCursor, int indicePuntoComa)
        {


            int asignaciones = 0;
            int declarartipoVar = 0;
            int llaves = 0;
            for (int i = posCursor; i < indicePuntoComa; i++)
            {
                if (lexemas[i].TipoElemento == Enums.TipoElemento.OperadorAsignacion)
                {
                    asignaciones++;
                }

                if (lexemas[i].TipoElemento == Enums.TipoElemento.TipoDato)
                {
                    declarartipoVar++;
                }

                if (lexemas[i].TipoElemento == Enums.TipoElemento.Llave)
                {
                    llaves++;
                }
            }

            if (asignaciones > 1 || declarartipoVar > 1 || llaves > 0)
            {
                return false;
            }

            return true;
        }

        public bool InitForCorrecto(List<Lexema> lexemas, int posCursor, int indicePuntoComa)
        {
            if (indicePuntoComa - posCursor == 5) //Analizar bloque de asignacion y declaracion
            {
                if (lexemas[posCursor + 1].Texto == "int" &&
                    lexemas[posCursor + 2].TipoElemento == Enums.TipoElemento.Variable &&
                    lexemas[posCursor + 3].TipoElemento == Enums.TipoElemento.OperadorAsignacion &&
                    lexemas[posCursor + 4].TipoElemento == Enums.TipoElemento.Numero)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                    
            }
            else if (indicePuntoComa - posCursor == 4)
            {
                if (lexemas[posCursor + 1].TipoElemento == Enums.TipoElemento.Variable &&
                    lexemas[posCursor + 2].TipoElemento == Enums.TipoElemento.OperadorAsignacion &&
                    lexemas[posCursor + 3].TipoElemento == Enums.TipoElemento.Numero)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CondicionEstructuraCorrecta(List<Lexema> lexemas, int posCursor, int indiceFin)
        {
            if ((lexemas[posCursor].TipoElemento == Enums.TipoElemento.Numero ||
                 lexemas[posCursor].TipoElemento == Enums.TipoElemento.Variable) &&
                (lexemas[posCursor + 1].TipoElemento == Enums.TipoElemento.OperadorLogico ||
                 lexemas[posCursor + 1].TipoElemento == Enums.TipoElemento.OperadorRelacional) &&
                (lexemas[posCursor + 2].TipoElemento == Enums.TipoElemento.Numero ||
                 lexemas[posCursor + 2].TipoElemento == Enums.TipoElemento.Variable))
            {
                return true;
            }

            return false;
        }

        public bool IncrementoForCorrecto(List<Lexema> lexemas, int posCursor, int indiceParentesis)
        {
            if (indiceParentesis - posCursor == 2) //Estamos ante una estructura contractada
            {
                if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.Variable && lexemas[posCursor + 1].TipoElemento ==
                    Enums.TipoElemento.OperadorIncremental)
                {
                    return true;
                }

                if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.Variable && lexemas[posCursor + 1].TipoElemento ==
                    Enums.TipoElemento.OperadorDecremental)
                {
                    return true;
                }

                return false;
            }
            else if (indiceParentesis - posCursor == 5) //Estamos ante una estructura completa
            {
                if (lexemas[posCursor].TipoElemento == Enums.TipoElemento.Variable &&
                    lexemas[posCursor + 1].TipoElemento == Enums.TipoElemento.OperadorAsignacion &&
                    lexemas[posCursor + 2].Texto == lexemas[posCursor].Texto &&
                    lexemas[posCursor + 3].TipoElemento == Enums.TipoElemento.OperadorAritmetico &&
                    lexemas[posCursor + 4].TipoElemento == Enums.TipoElemento.Numero)
                {
                    return true;
                }

                return false;

            }

            return false;
        }

        

        #endregion


        //Procesador de Sintaxis
        public List<string> ExtraerErroresBloques(List<Bloque> bloques)
        {
            List<string> errores = new List<string>();
            foreach (Bloque bloque in bloques)
            {
                if (!string.IsNullOrEmpty(bloque.Error))
                {
                    errores.Add(bloque.ErrorCompleto());
                }

                List<string> erroresParciales = bloque.ErroresHijo();
                if (erroresParciales != null && erroresParciales.Count > 0)
                {
                    errores.AddRange(erroresParciales.Where(y => !string.IsNullOrEmpty(y)));
                }
            }
            return errores;
        }


    }
}
