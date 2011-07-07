using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class DecisionSuport
    {
        private Dictionary<String, int> _tableCH;
        private Dictionary<String, Dictionary<String, int>> _tableSW;
        private Dictionary<String, Dictionary<String, int>> _tableX;
        private Dictionary<String, Dictionary<String, int>> _tableClass;
        private Dictionary<String, Dictionary<String, float>> _tableAHP;
        private Dictionary<String, Dictionary<String, float>> _tableResult;
        private Dictionary<String, Dictionary<String, Dictionary<String, float>>> _tablePriorAHP;
        Dictionary<int, double> _matrizIndicesAleatorios;

        public DecisionSuport()
        {
            _tableCH = new Dictionary<string, int>();
            _tableSW = new Dictionary<string, Dictionary<string, int>>();
            _tableX = new Dictionary<string, Dictionary<string, int>>();
            _tableClass = new Dictionary<string, Dictionary<string, int>>();
            _tableAHP = new Dictionary<string, Dictionary<string, float>>();
            _tableResult = new Dictionary<string, Dictionary<string, float>>();
            _tablePriorAHP = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
            _matrizIndicesAleatorios = new Dictionary<int, double>();

            // Matriz de Indices aleatórios de Saaty
            _matrizIndicesAleatorios.Add(1, 0.00);
            _matrizIndicesAleatorios.Add(2, 0.00);
            _matrizIndicesAleatorios.Add(3, 0.58);
            _matrizIndicesAleatorios.Add(4, 0.90);
            _matrizIndicesAleatorios.Add(5, 1.12);
            _matrizIndicesAleatorios.Add(6, 1.24);
            _matrizIndicesAleatorios.Add(7, 1.32);
            _matrizIndicesAleatorios.Add(8, 1.41);
            _matrizIndicesAleatorios.Add(9, 1.45);
            _matrizIndicesAleatorios.Add(10, 1.49);
            _matrizIndicesAleatorios.Add(11, 1.51);
            _matrizIndicesAleatorios.Add(12, 1.54);
            _matrizIndicesAleatorios.Add(13, 1.56);
            _matrizIndicesAleatorios.Add(14, 1.57);
            _matrizIndicesAleatorios.Add(15, 1.58);
            _matrizIndicesAleatorios.Add(16, 1.60);


        }

        /* Métodos get e set */
        public Dictionary<String, int> TableCH
        {
            get { return _tableCH; }
            set { _tableCH = value; }
        }

        public Dictionary<String, Dictionary<String, int>> TableSW
        {
            get { return _tableSW; }
            set { _tableSW = value; }
        }

        public Dictionary<String, Dictionary<String, float>> TableAHP
        {
            get { return _tableAHP; }
            set { _tableAHP = value; }
        }

        public Dictionary<String, Dictionary<String, int>> TableX
        {
            get { return _tableX; }
            set { _tableX = value; }
        }

        public Dictionary<String, Dictionary<String, int>> TableClass
        {
            get { return _tableClass; }
            set { _tableClass = value; }
        }

        public Dictionary<string, Dictionary<string, float>> TableResult
        {
            get { return _tableResult; }
            set { _tableResult = value; }
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, float>>> TablePriorAHP
        {
            get { return _tablePriorAHP; }
            set { _tablePriorAHP = value; }
        }


        /*Métodos de Cálculo de Classificações*/
        public Dictionary<string, int> registerClass(String idChar, int points)
        {
            if (!_tableCH.ContainsKey(idChar))
            {
                _tableCH.Add(idChar, points);
            }
            else
            {
                _tableCH.Remove(idChar);
                _tableCH.Add(idChar, points);
            }
            return _tableCH;
        }

        public Dictionary<String, float> normalizeSMART(Dictionary<string, int> tableCH)
        {
            Dictionary<String, float> smartNorm = new Dictionary<string, float>();
            int valor;
            int total = 0;
            foreach (String id in tableCH.Keys)
            {
                tableCH.TryGetValue(id, out valor);
                total += valor;
            }
            float resultado;
            foreach (String id in tableCH.Keys)
            {
                tableCH.TryGetValue(id, out valor);
                resultado = (float)valor / (float)total;
                smartNorm.Add(id, resultado);
            }


            return smartNorm;
        }

        // regista os resultados
        public Dictionary<String, Dictionary<String, float>> registerClassAHP(String idCharA, String idCharB, float points)
        {
            Dictionary<String, float> tableAux = new Dictionary<string, float>();
            Dictionary<String, float> tableA;
            if (!_tableAHP.ContainsKey(idCharA))
            {
                tableAux.Add(idCharB, points);
                _tableAHP.Add(idCharA, tableAux);
            }
            else
            {
                _tableAHP.TryGetValue(idCharA, out tableA);
                if (!tableA.ContainsKey(idCharB))
                {
                    tableA.Add(idCharB, points);
                }
                else
                {
                    tableA.Remove(idCharB);
                    tableA.Add(idCharB, points);
                }
            }

            return _tableAHP;
        }

        /* Chama a tabela resultante do register class AHP. Devolve uma "Matriz" com os valores normalizados
        SÓ NORMALIZA SE A SOMA DOS VALORES FOR DIFERENTE DE 1*/

        public Dictionary<String, Dictionary<String, float>> normalizeAHP(Dictionary<String, Dictionary<String, float>> table)
        {
            Dictionary<String, float> table1 = new Dictionary<string, float>();
            Dictionary<String, float> tableSomas = new Dictionary<string, float>();
            Dictionary<String, Dictionary<String, float>> tableAux = new Dictionary<string, Dictionary<string, float>>();

            float valor;
            float resultado;

            foreach (String idCharA in table.Keys)
            {
                table.TryGetValue(idCharA, out table1);
                float totalValor = 0;
                foreach (String idCharB in table1.Keys)
                {
                    table1.TryGetValue(idCharB, out valor);
                    totalValor += valor;
                }
                tableSomas.Add(idCharA, totalValor);

            }

            Dictionary<String, float> tableCorrespondencia;
            Dictionary<String, float> tableAuxiliar = new Dictionary<string, float>();
            Dictionary<String, float> tableAuxiliar1 = new Dictionary<string, float>();
            float valor1 = 0;

            foreach (String idCharA in table.Keys)
            {
                table.TryGetValue(idCharA, out tableAuxiliar);
                tableCorrespondencia = new Dictionary<string, float>();
                foreach (String idCharB in table1.Keys)
                {
                    tableAuxiliar.TryGetValue(idCharB, out valor);
                    tableSomas.TryGetValue(idCharA, out valor1);
                    resultado = valor / valor1;

                    if (!tableAux.ContainsKey(idCharA))
                    {
                        tableCorrespondencia.Add(idCharB, resultado);
                        tableAux.Add(idCharA, tableCorrespondencia);
                    }
                    else
                    {
                        tableAux.TryGetValue(idCharA, out tableAuxiliar1);
                        tableAux.Remove(idCharA);
                        tableAuxiliar1.Add(idCharB, resultado);
                        tableAux.Add(idCharA, tableAuxiliar1);

                    }
                }

            }

            return tableAux;
        }

        //Recebe a matriz normalizada. Calcular Médias da matriz normalizada
        public Dictionary<String, float> pesosFinais(Dictionary<String, Dictionary<String, float>> tableNorma)
        {
            Dictionary<String, float> tableCorrespondencia;
            Dictionary<String, float> tableAuxiliar = new Dictionary<string, float>();
            Dictionary<String, float> tableAuxiliar1;
            Dictionary<String, float> tablePesosFinais = new Dictionary<string, float>();
            Dictionary<String, Dictionary<String, float>> tableNormalInverted = new Dictionary<string, Dictionary<string, float>>();

            float valor;
            int numCar = 0;

            //inverter a tabela normalizada ou seja trocar as caracteristicas de <idCharA,<idcharB,valor>> para <idCharB,<idcharA,valor>>
            foreach (String idCharA in tableNorma.Keys)
            {
                tableNorma.TryGetValue(idCharA, out tableAuxiliar);
                tableAuxiliar1 = new Dictionary<string, float>();
                foreach (String idCharB in tableAuxiliar.Keys)
                {
                    tableCorrespondencia = new Dictionary<string, float>();
                    tableAuxiliar.TryGetValue(idCharB, out valor);
                    if (!tableNormalInverted.ContainsKey(idCharB))
                    {
                        tableCorrespondencia.Add(idCharA, valor);
                        tableNormalInverted.Add(idCharB, tableCorrespondencia);
                    }
                    else
                    {
                        tableNormalInverted.TryGetValue(idCharB, out tableAuxiliar1);
                        tableNormalInverted.Remove(idCharB);
                        tableAuxiliar1.Add(idCharA, valor);
                        tableNormalInverted.Add(idCharB, tableAuxiliar1);
                    }
                }
            }

            tableAuxiliar.Clear();
            tableAuxiliar1 = new Dictionary<string, float>();
            foreach (String idCharA in tableNormalInverted.Keys)
            {
                float valorTotal = 0;
                tableNormalInverted.TryGetValue(idCharA, out tableAuxiliar);

                foreach (String idCharB in tableAuxiliar.Keys)
                {
                    tableAuxiliar.TryGetValue(idCharB, out valor);
                    valorTotal += valor;
                }

                tableAuxiliar1.Add(idCharA, valorTotal);
            }

            foreach (String id in tableAuxiliar.Keys)
            {
                numCar++;
            }

            foreach (String id in tableAuxiliar1.Keys)
            {

                tableAuxiliar1.TryGetValue(id, out valor);

                tablePesosFinais.Add(id, (valor / numCar));
            }

            return tablePesosFinais;
        }


        /*Métodos dé Cálculo de Prioridades*/

        // **************Value Fn***************
        public Dictionary<String, Dictionary<String, int>> filter(String idChar)
        {
            Dictionary<String, int> tableAux = new Dictionary<string, int>();

            Dictionary<String, Dictionary<String, int>> tableXAux = new Dictionary<string, Dictionary<string, int>>();
            int valor;
            Dictionary<String, int> ch; // se dps nao der declarar espaco
            foreach (String idSof in _tableSW.Keys)
            {
                _tableSW.TryGetValue(idSof, out ch);
                foreach (String idCh in ch.Keys)
                {
                    if (idCh.Equals(idChar))
                    {
                        ch.TryGetValue(idCh, out valor);
                        tableAux.Add(idSof, valor);
                    }
                }
            }
            tableXAux.Add(idChar, tableAux);
            return tableXAux;
        }

        public int calMin(String idChar, Dictionary<String, Dictionary<String, int>> tableX)
        {
            int min = 0;
            int flag = 1;
            Dictionary<String, int> list;
            foreach (String id in tableX.Keys)
            {
                if (id.Equals(idChar))
                {
                    tableX.TryGetValue(id, out list);
                    foreach (int valor in list.Values)
                    {
                        if (flag == 1)
                        {
                            min = valor;
                            flag = 0;
                        }
                        else
                        {
                            if (min > valor)
                            {
                                min = valor;
                            }
                        }
                    }
                }
            }
            return min;
        }

        public int calMax(String idChar, Dictionary<String, Dictionary<String, int>> tableX)
        {
            int max = 0;
            int flag = 1;
            Dictionary<String, int> list;
            foreach (String id in tableX.Keys)
            {
                if (id.Equals(idChar))
                {
                    tableX.TryGetValue(id, out list);
                    foreach (int valor in list.Values)
                    {
                        if (flag == 1)
                        {
                            max = valor;
                            flag = 0;
                        }
                        else
                        {
                            if (max < valor)
                            {
                                max = valor;
                            }
                        }
                    }
                }
            }
            return max;
        }

        /*Auxiliar da calcValueMax*/
        public float formulaMax(int min, int max, int x)
        {
            float resultado;
            int a = x - min;
            int b = max - min;
            if(b!=0)
            {
             resultado= (float)a / (float)b;
            }
            else resultado = 0;

            return resultado;
        }

        /*Auxiliar da calcValueMin*/
        public float formulaMin(int min, int max, int x)
        {
            float resultado;
            int a = max - x;
            int b = max - min;
            if (b != 0)
            {
                resultado = (float)a / (float)b;
            }
            else resultado = 0;

            return resultado;
        }

        public Dictionary<String, float> calValueMax(int min, int max, Dictionary<String, Dictionary<String, int>> tableX)
        {
            Dictionary<String, float> tablePrior = new Dictionary<string, float>();
            Dictionary<String, float> tableAux = new Dictionary<string, float>();
            Dictionary<String, int> listClass;
            float resultado;
            int valor;

            float valueX;
            float valorNorm;
            float resTotal = 0;
            //Calculos das prioridades
            foreach (String idA in tableX.Keys)
            {
                tableX.TryGetValue(idA, out listClass);

                foreach (String idSoft in listClass.Keys)
                {
                    listClass.TryGetValue(idSoft, out valor);
                    resultado = formulaMax(min, max, valor);
                    resTotal += resultado;
                    if (!tableAux.ContainsKey(idSoft))
                    {
                        tableAux.Add(idSoft, resultado);
                    }
                    else
                    {
                        tableAux.Remove(idSoft);
                        tableAux.Add(idSoft, resultado);
                    }
                }
            }

            //Calculos das prioridades normalizadas
            int sofTotal = tableAux.Count();
            foreach (String id in tableAux.Keys)
            {
                tableAux.TryGetValue(id, out valueX);
                if (resTotal != 0)
                {
                    valorNorm = valueX / resTotal;
                }
                else 
                {
                    valorNorm = (float)1 / (float) sofTotal;
                }
                tablePrior.Add(id, valorNorm);
            }

            return tablePrior;
        }

        public Dictionary<String, float> calValueMin(int min, int max, Dictionary<String, Dictionary<String, int>> tableX)
        {
            Dictionary<String, float> tablePrior = new Dictionary<string, float>();
            Dictionary<String, float> tableAux = new Dictionary<string, float>();
            float resultado;
            int valor;
            int numSoft = 0;
            float resTotal = 0;
            float valueX;
            float valorNorm;

            //Calculos das prioridades
            foreach (Dictionary<String, int> listClass in tableX.Values)
            {
                foreach (String idSoft in listClass.Keys)
                {
                    numSoft++;
                    listClass.TryGetValue(idSoft, out valor);
                    resultado = formulaMin(min, max, valor);
                    resTotal += resultado;
                    if (!tableAux.ContainsKey(idSoft))
                    {
                        tableAux.Add(idSoft, resultado);
                    }
                    else
                    {
                        tableAux.Remove(idSoft);
                        tableAux.Add(idSoft, resultado);
                    }
                }
            }
            //Calculos das prioridades normalizadas
            int sofTotal = tableAux.Count();

            foreach (String id in tableAux.Keys)
            {
                tableAux.TryGetValue(id, out valueX);
                if (resTotal != 0)
                {
                    valorNorm = valueX / resTotal;
                }
                else
                {
                    valorNorm = (float) 1 / (float) sofTotal;
                }
                tablePrior.Add(id, valorNorm);
            }

            return tablePrior;
        }

        public Dictionary<String, Dictionary<String, float>> registerPriority(String idChar, Dictionary<String, float> tablePrior)
        {
            if (!_tableResult.ContainsKey(idChar))
            {
                _tableResult.Add(idChar, tablePrior);
            }
            else
            {
                _tableResult.Remove(idChar);
                _tableResult.Add(idChar, tablePrior);
            }
            return _tableResult;
        }




        #region Métodos Relativos ao AHP em Prioridades

        //A diferença entre estes métodos e os métodos da classificação é que têm que andar com o id da caracteristica porque cada uma pode ter o seu método
        // regista os resultados
        public Dictionary<String, Dictionary<String, Dictionary<String, float>>> registerPriorAHP(String idChar, String idSofA, String idSofB, float points)
        {
            Dictionary<String, Dictionary<String, float>> tablePrior = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, Dictionary<String, float>> tablePriorAux = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, float> tableAux = new Dictionary<string, float>();
            if (!_tablePriorAHP.ContainsKey(idChar))
            {
                tableAux.Add(idSofB, points);
                tablePrior.Add(idSofA, tableAux);
                _tablePriorAHP.Add(idChar, tablePrior);
            }
            else
            {
                _tablePriorAHP.TryGetValue(idChar, out tablePriorAux);
                if (!tablePriorAux.ContainsKey(idSofA))
                {
                    tableAux.Add(idSofB, points);
                    tablePriorAux.Add(idSofA, tableAux);
                }
                else
                {
                    tablePriorAux.TryGetValue(idSofA, out tableAux);
                    if (!tableAux.ContainsKey(idSofB))
                    {
                        tableAux.Add(idSofB, points);
                    }
                    else
                    {
                        tableAux.Remove(idSofB);
                        tableAux.Add(idSofB, points);
                    }
                }
            }

            return _tablePriorAHP;
        }

        /* Chama a tabela resultante do register class AHP. Devolve uma "Matriz" com os valores normalizados
           SÓ NORMALIZA SE A SOMA DOS VALORES FOR DIFERENTE DE 1
           Tudo direito
         */
        public Dictionary<String, Dictionary<String, Dictionary<String, float>>> normalizePriorityAHP(Dictionary<String, Dictionary<String, Dictionary<String, float>>> table)
        {
            float valor;
            float resultado;
            float total;
            Dictionary<String, Dictionary<String, float>> tableAux2;
            Dictionary<String, float> table1 = new Dictionary<string, float>();
            Dictionary<String, float> table3 = new Dictionary<string, float>();
            Dictionary<String, float> tableSomas;
            Dictionary<String, Dictionary<String, float>> tableGlobalSomas = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, Dictionary<String, float>> tableAux = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, Dictionary<String, Dictionary<String, float>>> tableNorm = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();


            foreach (String idChar in table.Keys)
            {
                tableSomas = new Dictionary<string, float>();
                table.TryGetValue(idChar, out tableAux2);
                foreach (String idSofA in tableAux2.Keys)
                {
                    tableAux2.TryGetValue(idSofA, out table1);
                    float totalValor = 0;
                    foreach (String idSofB in table1.Keys)
                    {
                        table1.TryGetValue(idSofB, out valor);
                        totalValor += valor;
                    }

                    tableSomas.Add(idSofA, totalValor);
                }
                tableGlobalSomas.Add(idChar, tableSomas);
            }

  
            Dictionary<String, float> tableCorrespondencia;
            Dictionary<String, float> tableAuxiliar = new Dictionary<string, float>();
            Dictionary<String, float> tableAuxiliar1 = new Dictionary<string, float>();
            Dictionary<String, Dictionary<String, float>> tableAuxiliar2;

            foreach (String idCh in table.Keys)
            {
                table.TryGetValue(idCh, out tableAux2);
                tableAuxiliar2 = new Dictionary<String, Dictionary<string, float>>();
                tableGlobalSomas.TryGetValue(idCh, out tableSomas);
                foreach (String idSofA in tableAux2.Keys)
                {
                    tableAux2.TryGetValue(idSofA, out tableAuxiliar);
                    tableCorrespondencia = new Dictionary<string, float>();
                    foreach (String idSofB in tableAuxiliar.Keys)
                    {
                        tableAuxiliar.TryGetValue(idSofB, out valor);
                        tableSomas.TryGetValue(idSofA, out total);
                        resultado = valor / total;
                        if (!tableAuxiliar2.ContainsKey(idSofA))
                        {
                            tableCorrespondencia.Add(idSofB, resultado);
                            tableAuxiliar2.Add(idSofA, tableCorrespondencia);
                        }

                        else
                        {
                            tableAuxiliar2.TryGetValue(idSofA, out tableAuxiliar1);
                            tableAuxiliar2.Remove(idSofA);
                            tableAuxiliar1.Add(idSofB, resultado);
                            tableAuxiliar2.Add(idSofA, tableAuxiliar1);
                        }
                    }
                }
                tableNorm.Add(idCh, tableAuxiliar2);
            }

            return tableNorm;
        }

        //Recebe a matriz normalizada. Calcular Médias da matriz normalizada
        public Dictionary<String, Dictionary<String, float>> pesosPriorFinais(Dictionary<String, Dictionary<String, Dictionary<String, float>>> tableNorma)
        {
            Dictionary<String, float> tableCorrespondencia;
            Dictionary<String, float> tableAuxiliar = new Dictionary<string, float>();
            Dictionary<String, float> tableAuxiliar1;
            Dictionary<String, Dictionary<String, float>> tableNormalInvertedAux = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, Dictionary<String, float>> tableNormalized = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<String, Dictionary<String, Dictionary<String, float>>> tableNormalInverted = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
            float valor;
            

            //inverter a tabela normalizada ou seja trocar as caracteristicas de <idCharA,<idcharB,valor>> para <idCharB,<idcharA,valor>>
            foreach (String idChar in tableNorma.Keys)
            {
                tableNorma.TryGetValue(idChar, out tableNormalized);
                tableNormalInvertedAux = new Dictionary<string, Dictionary<string, float>>();
                foreach (String idSofA in tableNormalized.Keys)
                {
                    tableNormalized.TryGetValue(idSofA, out tableAuxiliar);
                    foreach (String idSofB in tableAuxiliar.Keys)
                    {
                        tableCorrespondencia = new Dictionary<string, float>();
                        tableAuxiliar.TryGetValue(idSofB, out valor);
                        if (!tableNormalInvertedAux.ContainsKey(idSofB))
                        {
                            tableCorrespondencia.Add(idSofA, valor);
                            tableNormalInvertedAux.Add(idSofB, tableCorrespondencia);
                        }
                        else
                        {
                            tableNormalInvertedAux.TryGetValue(idSofB, out tableAuxiliar1);
                            tableNormalInvertedAux.Remove(idSofB);
                            if (!tableAuxiliar1.ContainsKey(idSofA))
                            {
                                tableAuxiliar1.Add(idSofA, valor);
                            }
                            else
                            {
                                tableAuxiliar1.Remove(idSofA);
                                tableAuxiliar1.Add(idSofA, valor);
                            }

                            tableNormalInvertedAux.Add(idSofB, tableAuxiliar1);
                        }
                    }
                }

                tableNormalInverted.Add(idChar, tableNormalInvertedAux);
            }

            /*
            Dictionary<String, Dictionary<String, float>> tableA;
            Dictionary<String, float> tableB;
            float x;
            
            Console.WriteLine("\n************ Inverted Normalized *************");
            foreach (String idChar in tableNormalInverted.Keys)
            {
                tableNormalInverted.TryGetValue(idChar, out tableA);
                Console.WriteLine("Id Char: " + idChar);
                foreach (String idSofA in tableA.Keys)
                {
                    tableA.TryGetValue(idSofA, out tableB);
                    Console.WriteLine("\tID Sof: " + idSofA);
                    foreach (String idSofB in tableB.Keys)
                    {
                        tableB.TryGetValue(idSofB, out x);
                        Console.WriteLine("\t\tId Sof: " + idSofB);
                        Console.WriteLine("\t\tValor: " + x);
                    }

                }
            }
            */
            tableAuxiliar.Clear();
            
            Dictionary<String, float> tableAuxiliar2;
            foreach (String idCh in tableNormalInverted.Keys)
            {
                tableAuxiliar1 = new Dictionary<string, float>();
                tableNormalInverted.TryGetValue(idCh, out tableNormalInvertedAux);
                foreach (String idSofA in tableNormalInvertedAux.Keys)
                {
                    float valorTotal = 0;
                    tableNormalInvertedAux.TryGetValue(idSofA, out tableAuxiliar);
                    
                    foreach (String idSofB in tableAuxiliar.Keys)
                    {
                        tableAuxiliar.TryGetValue(idSofB, out valor);
                        valorTotal += valor;
                    }

                    tableAuxiliar1.Add(idSofA, valorTotal);
                }
                int numCar = 0;
                foreach (String id in tableAuxiliar.Keys)
                {
                    numCar++;
                }

                float valorG;
                tableAuxiliar2 = new Dictionary<string, float>();
                foreach (String id in tableAuxiliar1.Keys)
                {
                    tableAuxiliar1.TryGetValue(id, out valorG);
                    float result = (valorG / numCar);
                    tableAuxiliar2.Add(id, result);
                }
                if (!_tableResult.ContainsKey(idCh))
                {
                    _tableResult.Add(idCh, tableAuxiliar2);
                }
                else 
                {
                    _tableResult.Remove(idCh);
                    _tableResult.Add(idCh, tableAuxiliar2);
                }
            }

            return _tableResult;
        }

        #endregion


        /* TESTAR CONSISTENCIAS
         * Os métodos para teste de consistencia serão todos os que se seguem até ser indicado o contrário tem em atenção que no método principal este
         * quando aplicado ao cálculo de PRIORIDES (Não CLASSIFICAÇÕES) este não recebe o id da caracteristica, isto tem que ser feito antes, ou seja,
         * deve-se ir a tabela grande buscar os dados de prioridades para aquela caracteristica.
         */


        /* FASE 1
         * Fazer os primeiros calculos para a taxa de consistencia
         * Caso não seja consistente deve-se passar para a fase 2;
         * 
         * metodo geral recebe:
         *      - Matriz AHP
         *      - Matriz de Classificações
         */

        public Dictionary<int, double> calculaMatrizC(Dictionary<String, Dictionary<String, float>> matrixRegisterAHP, Dictionary<String, float> matrixFinalPesos)
        {
            Dictionary<String, float> tableAuxiliar1;
            Dictionary<string, float> tableCorrespondencia;
            Dictionary<int, double> matrixPesos = new Dictionary<int, double>();
            Dictionary<int, double> matrixC = new Dictionary<int, double>();
            Dictionary<String, float> tableAuxiliar = new Dictionary<String, float>();
            Dictionary<String, Dictionary<String, float>> matrizRegisterAHPinverted = new Dictionary<string, Dictionary<string, float>>();
            float valor;
            // Como a matrixRegisterAHP está orientada á coluna e não à linha é perciso invertela para ficar orientada à linha;

            foreach (String idA in matrixRegisterAHP.Keys)
            {
                matrixRegisterAHP.TryGetValue(idA, out tableAuxiliar);
                tableAuxiliar1 = new Dictionary<string, float>();
                foreach (String idB in tableAuxiliar.Keys)
                {
                    tableCorrespondencia = new Dictionary<string, float>();
                    tableAuxiliar.TryGetValue(idB, out valor);
                    if (!matrizRegisterAHPinverted.ContainsKey(idB))
                    {
                        tableCorrespondencia.Add(idA, valor);
                        matrizRegisterAHPinverted.Add(idB, tableCorrespondencia);
                    }
                    else
                    {
                        matrizRegisterAHPinverted.TryGetValue(idB, out tableAuxiliar1);
                        matrizRegisterAHPinverted.Remove(idB);
                        tableAuxiliar1.Add(idA, valor);
                        matrizRegisterAHPinverted.Add(idB, tableAuxiliar1);
                    }
                }
            }

            // Converte a matriz de pesos finais numa associação numero - float
            int num = 1;
            float valorP;
            foreach (String id in matrixFinalPesos.Keys)
            {
                matrixFinalPesos.TryGetValue(id, out valorP);
                matrixPesos.Add(num, valorP);
                num++;
            }



            tableCorrespondencia = new Dictionary<string, float>();
            double peso;
            int idNumber = 0;

            float valorA = 0; // valor que se retira da matriz de correspondencias

            foreach (String idA in matrizRegisterAHPinverted.Keys)
            {
                idNumber++;
                int idColum = 0;
                double totalFinal = 0;
                matrizRegisterAHPinverted.TryGetValue(idA, out tableCorrespondencia);
                foreach (String idB in tableCorrespondencia.Keys)
                {
                    idColum++;
                    tableCorrespondencia.TryGetValue(idB, out valorA);
                    foreach (int id in matrixPesos.Keys)
                    {
                        if (idColum == id)
                        {
                            matrixPesos.TryGetValue(id, out peso);
                            totalFinal = totalFinal + (peso * valorA);
                        }
                    }
                }
                matrixC.Add(idNumber, totalFinal);
            }

            return matrixC;
        }

        public Dictionary<int, double> calculaMatrizD(Dictionary<int, double> matrixC, Dictionary<String, float> matrixFinalPesos)
        {
            Dictionary<int, double> matrixD = new Dictionary<int, double>();
            Dictionary<int, double> matrixPesos = new Dictionary<int, double>();


            int num = 1;
            float valorP;
            foreach (String id in matrixFinalPesos.Keys)
            {
                matrixFinalPesos.TryGetValue(id, out valorP);
                matrixPesos.Add(num, valorP);
                num++;
            }



            double peso;
            double valorC;

            double totalFinal = 0;
            foreach (int idP in matrixPesos.Keys)
            {
                matrixPesos.TryGetValue(idP, out peso);
                foreach (int idC in matrixC.Keys)
                {
                    if (idC == idP)
                    {
                        matrixC.TryGetValue(idC, out valorC);
                        totalFinal = (float)valorC / (float)peso;
                    }
                }
                matrixD.Add(idP, totalFinal);
            }



            return matrixD;
        }

        public double taxaConsitencia(Dictionary<int, double> matrixD)
        {
            double tc;
            double ia;
            double ic;
            int numTotal = 0;
            double lambMax = 0;
            double total = 0;
            double valor;

            //calcula lambda max
            foreach (int id in matrixD.Keys)
            {
                numTotal++;
                matrixD.TryGetValue(id, out valor);
                total += valor;
            }


            lambMax = (double)total / (double)numTotal;

            // indice de inconsistencia
            ic = ((double)(lambMax - numTotal) / (double)(numTotal - 1));

            // taxa de inconsistencia
            _matrizIndicesAleatorios.TryGetValue(numTotal, out ia);

            if (ia == 0)
            {
                tc = 0;
            }
            else
            {
                tc = (double)ic / (double)ia;
            }
            return tc;
        }

        /* FASE 2
         * A matriz não é consistente
         * Tantar melhorar a taxa de consistência
         */

        // Metodos que recebem os pesos com int double em vez de string double. Auxiliares da iterações
        public Dictionary<int, double> normalizaMatrizC(Dictionary<int, double> matrixC)
        {
            Dictionary<int, double> matrixCNorm = new Dictionary<int, double>();
            double total = 0;
            double valor;

            foreach (int id in matrixC.Keys)
            {
                matrixC.TryGetValue(id, out valor);
                total += valor;
            }
            double resultado;
            foreach (int id in matrixC.Keys)
            {
                matrixC.TryGetValue(id, out valor);
                resultado = (double)valor / (double)total;
                matrixCNorm.Add(id, resultado);
            }

            return matrixCNorm;
        }

        public Dictionary<int, double> calculaMatrizCInt(Dictionary<String, Dictionary<String, float>> matrixRegisterAHP, Dictionary<int, double> matrixFinalPesos)
        {
            Dictionary<String, float> tableAuxiliar1;
            Dictionary<string, float> tableCorrespondencia;
            Dictionary<int, double> matrixPesos = new Dictionary<int, double>();
            Dictionary<int, double> matrixC = new Dictionary<int, double>();
            Dictionary<String, float> tableAuxiliar = new Dictionary<String, float>();
            Dictionary<String, Dictionary<String, float>> matrizRegisterAHPinverted = new Dictionary<string, Dictionary<string, float>>();
            float valor;
            // Como a matrixRegisterAHP está orientada á coluna e não à linha é perciso invertela para ficar orientada à linha;

            foreach (String idA in matrixRegisterAHP.Keys)
            {
                matrixRegisterAHP.TryGetValue(idA, out tableAuxiliar);
                tableAuxiliar1 = new Dictionary<string, float>();
                foreach (String idB in tableAuxiliar.Keys)
                {
                    tableCorrespondencia = new Dictionary<string, float>();
                    tableAuxiliar.TryGetValue(idB, out valor);
                    if (!matrizRegisterAHPinverted.ContainsKey(idB))
                    {
                        tableCorrespondencia.Add(idA, valor);
                        matrizRegisterAHPinverted.Add(idB, tableCorrespondencia);
                    }
                    else
                    {
                        matrizRegisterAHPinverted.TryGetValue(idB, out tableAuxiliar1);
                        matrizRegisterAHPinverted.Remove(idB);
                        tableAuxiliar1.Add(idA, valor);
                        matrizRegisterAHPinverted.Add(idB, tableAuxiliar1);
                    }
                }
            }

            // Converte a matriz de pesos finais numa associação numero - float
            int num = 1;
            double valorP;
            foreach (int id in matrixFinalPesos.Keys)
            {
                matrixFinalPesos.TryGetValue(id, out valorP);
                matrixPesos.Add(num, valorP);
                num++;
            }



            tableCorrespondencia = new Dictionary<string, float>();
            double peso;
            int idNumber = 0;

            float valorA = 0; // valor que se retira da matriz de correspondencias

            foreach (String idA in matrizRegisterAHPinverted.Keys)
            {
                idNumber++;
                int idColum = 0;
                double totalFinal = 0;
                matrizRegisterAHPinverted.TryGetValue(idA, out tableCorrespondencia);
                foreach (String idB in tableCorrespondencia.Keys)
                {
                    idColum++;
                    tableCorrespondencia.TryGetValue(idB, out valorA);
                    foreach (int id in matrixPesos.Keys)
                    {
                        if (idColum == id)
                        {
                            matrixPesos.TryGetValue(id, out peso);
                            totalFinal = totalFinal + (peso * valorA);
                        }
                    }
                }
                matrixC.Add(idNumber, totalFinal);
            }

            return matrixC;
        }

        public Dictionary<int, double> calculaMatrizDInt(Dictionary<int, double> matrixC, Dictionary<int, double> matrixFinalPesos)
        {
            Dictionary<int, double> matrixD = new Dictionary<int, double>();
            double peso;
            double valorC;

            double totalFinal = 0;
            foreach (int idP in matrixFinalPesos.Keys)
            {
                matrixFinalPesos.TryGetValue(idP, out peso);
                foreach (int idC in matrixC.Keys)
                {
                    if (idC == idP)
                    {
                        matrixC.TryGetValue(idC, out valorC);
                        totalFinal = (float)valorC / (float)peso;
                    }
                }
                matrixD.Add(idP, totalFinal);
            }

            return matrixD;
        }

        public double iteracoesNaoConsistencia(Dictionary<String, Dictionary<String, float>> matrixRegisterAHP, Dictionary<int, double> matrixC)
        {
            double consFinal = 0;
            int numIteracoes = 0;
            double diferenca = 10;
            double diferencaAnt = 0;
            Dictionary<int, double> matrixDif = new Dictionary<int, double>();
            Dictionary<int, double> matrixCNewNormalized = new Dictionary<int, double>();
            Dictionary<int, double> matrixD = new Dictionary<int, double>();
            Dictionary<int, double> matrixPesos = new Dictionary<int, double>();
            Dictionary<int, double> matrixCNew = new Dictionary<int, double>();
            Dictionary<int, double> matrixPesosAntigos = new Dictionary<int, double>();
            matrixPesos = matrixC;
            double aux;


            while (diferenca > 0.0001)
            {
                numIteracoes++;
                diferenca = 10;
                matrixDif.Clear();
                matrixPesos = normalizaMatrizC(matrixPesos);

                matrixCNew = calculaMatrizCInt(_tableAHP, matrixPesos);
                matrixCNewNormalized = normalizaMatrizC(matrixCNew);

                matrixPesosAntigos = matrixPesos;

                matrixPesos = matrixCNewNormalized;

                // Tem que fazer a difrença entre a normalizada da primeira iteração e a normalizada da seuginda
                double valorNew;
                double valorOld;
                double resultado;

                foreach (int id in matrixPesos.Keys)
                {
                    matrixPesos.TryGetValue(id, out valorNew);
                    foreach (int idOld in matrixPesosAntigos.Keys)
                    {
                        if (id == idOld)
                        {
                            matrixPesosAntigos.TryGetValue(id, out valorOld);
                            resultado = valorNew - valorOld;
                            matrixDif.Add(id, Math.Abs(resultado));
                        }
                    }
                }
                double al;
                foreach (int id in matrixDif.Keys)
                {
                    matrixDif.TryGetValue(id, out al);
                }

                int flag = 0;
                foreach (int id in matrixDif.Keys)
                {
                    matrixDif.TryGetValue(id, out aux);
                    if (flag == 0)
                    {
                        diferencaAnt = aux;
                        flag = 1;
                    }
                    else
                    {
                        if (aux > diferencaAnt)
                        {
                            diferencaAnt = aux;
                        }
                    }
                }
                diferenca = diferencaAnt;
            }

            matrixD = calculaMatrizDInt(matrixCNew, matrixPesosAntigos);

            consFinal = taxaConsitencia(matrixD);
            return consFinal;
        }


        /* ANÁLISES FINAIS
         * Tem que existir tipos de análises finais, ou seja,
         *  - SMART e ValueFn
         *  - SMART e AHP
         *  - AHP e ValueFn
         *  - AHP e AHP
         */

        public Dictionary<int, Dictionary<String, float>> analiseFinalSmart(Dictionary<String, float> tableCHNorm, Dictionary<String, Dictionary<String, float>> tableValueFn)
        {
            // Rank -> (IDSOft, prioridade)
            Dictionary<int, Dictionary<String, float>> ranks = new Dictionary<int, Dictionary<string, float>>();
            Dictionary<String, float> aux = new Dictionary<string, float>();
            Dictionary<String, float> tablePrioXClass;
            Dictionary<String, float> tableClass;
            Dictionary<String, Dictionary<String, float>> tablePriorAux = new Dictionary<string, Dictionary<string, float>>();
            float valorNorm;
            float valorDesnorm;
            float resultado;

            //Multiplica os valores das prioridades pelas classificações 
            foreach (String id in tableCHNorm.Keys)
            {
                tableCHNorm.TryGetValue(id, out valorNorm);
                tablePrioXClass = new Dictionary<string, float>();

                foreach (String idA in tableValueFn.Keys)
                {
                    if (idA.Equals(id))
                    {
                        tablePrioXClass = new Dictionary<string, float>();
                        tableValueFn.TryGetValue(idA, out aux);
                        foreach (String idSof in aux.Keys)
                        {
                            aux.TryGetValue(idSof, out valorDesnorm);
                            resultado = valorNorm * valorDesnorm;
                            tablePrioXClass.Add(idSof, resultado);
                        }
                    }
                }
                tablePriorAux.Add(id, tablePrioXClass);
            }




            Dictionary<String, List<float>> tableCl = new Dictionary<string, List<float>>();
            List<float> prior;
            List<float> priorAux;
            float valorX;
            // associa a um dado software uma lista com as classificações obtidas em todas as caracteriscas
            foreach (String id in tablePriorAux.Keys)
            {
                tablePriorAux.TryGetValue(id, out tableClass);

                foreach (String idA in tableClass.Keys)
                {
                    tableClass.TryGetValue(idA, out valorX);
                    if (!tableCl.ContainsKey(idA))
                    {
                        prior = new List<float>();
                        prior.Add(valorX);
                        tableCl.Add(idA, prior);
                    }
                    else
                    {
                        tableCl.TryGetValue(idA, out priorAux);
                        tableCl.Remove(idA);
                        priorAux.Add(valorX);
                        tableCl.Add(idA, priorAux);
                    }
                }
            }

            List<float> listP;

            Dictionary<String, float> rankAux = new Dictionary<string, float>();
            Dictionary<String, float> rankAux2 = new Dictionary<string, float>();
            foreach (String id in tableCl.Keys)
            {
                float soma = 0;
                tableCl.TryGetValue(id, out listP);
                foreach (float valor in listP)
                {
                    soma += valor;
                }
                rankAux.Add(id, soma);
            }



            // Verifica qual o maior vai retiralo da matriz rankAux e acrescentar na matriz rank aux 2
            // É necessário verificar se ele adiciona à cabeça ou na cauda se for à cabeça alterar para ver o minimo
            while (rankAux.Count != 0)
            {
                float valorMax = 0;
                foreach (String id in rankAux.Keys)
                {
                    float valorH;
                    rankAux.TryGetValue(id, out valorH);

                    if (valorH > valorMax)
                    {
                        valorMax = valorH;
                    }

                }

                String idv = "";
                float valorP;
                foreach (String id in rankAux.Keys)
                {
                    rankAux.TryGetValue(id, out valorP);
                    if (valorMax == valorP)
                    {
                        idv = String.Copy(id);

                    }

                }
                if (!rankAux2.ContainsKey(idv))
                {
                    rankAux2.Add(idv, valorMax);
                }
                else
                {
                    rankAux2.Remove(idv);
                    rankAux2.Add(idv, valorMax);
                }
                rankAux.Remove(idv);

            }


            int i = 1;
            float valorL;
            // atribui o rank
            Dictionary<String, float> rankAux3;
            foreach (String id in rankAux2.Keys)
            {
                rankAux2.TryGetValue(id, out valorL);
                rankAux3 = new Dictionary<string, float>();
                rankAux3.Add(id, valorL);
                ranks.Add(i, rankAux3);
                i++;
            }
            return ranks;
        }

        public Dictionary<int, Dictionary<String, float>> analiseFinalAHP(Dictionary<String, float> pesosFinais, Dictionary<String, Dictionary<String, float>> tableValueFn)
        {
            // Rank -> (IDSOft, prioridade)
            Dictionary<int, Dictionary<String, float>> ranks = new Dictionary<int, Dictionary<string, float>>();
            Dictionary<String, float> aux = new Dictionary<string, float>();
            Dictionary<String, float> tablePrioXClass;
            Dictionary<String, float> tableClass;
            Dictionary<String, Dictionary<String, float>> tablePriorAux = new Dictionary<string, Dictionary<string, float>>();
            float valorNorm;
            float valorDesnorm;
            float resultado;

            //Multiplica os valores das prioridades pelas classificações 
            foreach (String id in pesosFinais.Keys)
            {
                pesosFinais.TryGetValue(id, out valorNorm);
                tablePrioXClass = new Dictionary<string, float>();

                foreach (String idA in tableValueFn.Keys)
                {
                    if (idA.Equals(id))
                    {
                        tablePrioXClass = new Dictionary<string, float>();
                        tableValueFn.TryGetValue(idA, out aux);
                        foreach (String idSof in aux.Keys)
                        {
                            aux.TryGetValue(idSof, out valorDesnorm);
                            resultado = (float)valorNorm * (float)valorDesnorm;
                            tablePrioXClass.Add(idSof, resultado);
                        }
                    }
                }
                tablePriorAux.Add(id, tablePrioXClass);
            }




            Dictionary<String, List<float>> tableCl = new Dictionary<string, List<float>>();
            List<float> prior;
            List<float> priorAux;
            float valorX;
            // associa a um dado software uma lista com as classificações obtidas em todas as caracteriscas
            foreach (String id in tablePriorAux.Keys)
            {
                tablePriorAux.TryGetValue(id, out tableClass);

                foreach (String idA in tableClass.Keys)
                {
                    tableClass.TryGetValue(idA, out valorX);
                    if (!tableCl.ContainsKey(idA))
                    {
                        prior = new List<float>();
                        prior.Add(valorX);
                        tableCl.Add(idA, prior);
                    }
                    else
                    {
                        tableCl.TryGetValue(idA, out priorAux);
                        tableCl.Remove(idA);
                        priorAux.Add(valorX);
                        tableCl.Add(idA, priorAux);
                    }
                }
            }

            List<float> listP;

            Dictionary<String, float> rankAux = new Dictionary<string, float>();
            Dictionary<String, float> rankAux2 = new Dictionary<string, float>();
            foreach (String id in tableCl.Keys)
            {
                float soma = 0;
                tableCl.TryGetValue(id, out listP);
                foreach (float valor in listP)
                {
                    soma += valor;
                }
                rankAux.Add(id, soma);
            }



            // Verifica qual o maior vai retiralo da matriz rankAux e acrescentar na matriz rank aux 2
            // É necessário verificar se ele adiciona à cabeça ou na cauda se for à cabeça alterar para ver o minimo
            while (rankAux.Count != 0)
            {
                float valorMax = 0;
                foreach (String id in rankAux.Keys)
                {
                    float valorH;
                    rankAux.TryGetValue(id, out valorH);

                    if (valorH > valorMax)
                    {
                        valorMax = valorH;
                    }

                }

                String idv = "";
                float valorP;
                foreach (String id in rankAux.Keys)
                {
                    rankAux.TryGetValue(id, out valorP);
                    if (valorMax == valorP)
                    {
                        idv = String.Copy(id);

                    }

                }
                if (!rankAux2.ContainsKey(idv))
                {
                    rankAux2.Add(idv, valorMax);
                }
                else
                {
                    rankAux2.Remove(idv);
                    rankAux2.Add(idv, valorMax);
                }
                rankAux.Remove(idv);

            }


            int i = 1;
            float valorL;
            // atribui o rank
            Dictionary<String, float> rankAux3;
            foreach (String id in rankAux2.Keys)
            {
                rankAux2.TryGetValue(id, out valorL);
                rankAux3 = new Dictionary<string, float>();
                rankAux3.Add(id, valorL);
                ranks.Add(i, rankAux3);
                i++;
            }
            return ranks;
        }

        
    }

    // Atenção falta meter condições por exemplo para as consistencias que tem que ser menor que 0.1 se for maior tem que chamar a das iterações

}

