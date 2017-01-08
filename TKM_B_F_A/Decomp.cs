using System;
using TKM_B_F_A.BoolAnalyze;

namespace TKM_B_F_A
{
    class Decomp
    {
        static void Main(string[] args)
        {
            string[] names;
            do
            {
                int m = 0;
                int n = 0;
                bool[,] vArr;
                string[] pol;
                string func = "";

                Console.CursorVisible = true;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Input variables");
                names = Console.ReadLine().Split();
                Console.WriteLine();
                Console.WriteLine("Select input mode" + Environment.NewLine + "1 - Standart mode" + Environment.NewLine + "2 - Vector mode");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        Console.WriteLine("Input function //^v-" + Environment.NewLine);
                        func = Console.ReadLine();
                        for (int i = 0; i < names.Length; i++)
                        {
                            names[i] = names[i].ToUpper();
                        }
                        func = func.ToUpper();
                        n = names.Length;
                        m = (int)Math.Pow(2, n);
                        vArr = TrueTables.STable(func, names);
                        break;

                    default:
                        Console.WriteLine("Input vector" + Environment.NewLine);
                        func = Console.ReadLine();
                        m = func.Length;
                        n = Convert.ToInt32(Math.Log(m, 2));
                        vArr = TrueTables.VTable(func);
                        break;
                }
                Console.WriteLine();

                Console.WriteLine("Truth table");
                for (int i = 0; i < names.Length; i++)
                {
                    Console.Write(names[i] + " ");
                }
                Console.Write("f" + Environment.NewLine);
                Output.OutputArray(vArr, m, n + 1);
                Console.WriteLine();

                Console.WriteLine("Pascal's triangle" + Environment.NewLine);
                pol = Zhegalkin.Triangle(vArr, names);
                Console.WriteLine();
                Console.WriteLine("Gegalkin Polynom" + Environment.NewLine);
                for (int i = 0; i < pol.Length; i++)
                {
                    if (i == pol.Length - 1)
                    {
                        Console.Write(pol[i]);
                    }
                    else
                    {
                        Console.Write(pol[i] + "+");
                    }
                }
                Console.WriteLine();

                Console.WriteLine("Derivatives");
                string[,] stek = OrderVar(vArr, names);
                Console.WriteLine("Variables priority" + Environment.NewLine);
                for (int i = 0; i < n; i++)
                {
                    Console.Write(stek[i, 0] + " ");
                }
                Console.WriteLine();
                Console.WriteLine();

                Shannon.Manager(pol, stek);
                if (Shannon.ns == null)
                {
                    Console.WriteLine("Values at nodes(in contact circuit)");
                    Console.WriteLine("-");
                    Console.WriteLine();
                    Console.WriteLine("Adjacency matrix");
                    Console.WriteLine("-");
                    Console.WriteLine("Zero Nods!");
                }
                else
                {
                    Node[] nodes = Shannon.ns.Data;
                    for (int i = 0; i < nodes.Length - 1; i++)
                    {
                        Node ne = nodes[i + 1];
                        nodes[i + 1] = nodes[i];
                        nodes[i] = ne;
                    }

                    Console.WriteLine("Values at nodes(in contact circuit)");
                    Console.WriteLine();
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        Console.Write((i + 1).ToString() + " - ");
                        for (int j = 0; j < nodes[i].Name.Length; j++)
                        {
                            if (j == nodes[i].Name.Length - 1)
                            {
                                Console.Write(nodes[i].Name[j]);
                            }
                            else
                            {
                                Console.Write(nodes[i].Name[j] + "+");
                            }
                        }
                        Console.WriteLine();
                    }

                    string[,] res = Shannon.AdjMatrix(nodes);
                    //Приведение к треугольному виду
                    for (int i = 1; i < res.GetLength(0); i++)
                    {
                        res[0, i] = i.ToString();
                        res[i, 0] = i.ToString();
                        for (int j = 1; j < res.GetLength(1); j++)
                        {
                            if (j < i)
                            {
                                res[i, j] = "0";
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Adjacency matrix");
                    Console.WriteLine();
                    Output.OutputArrayInConsole(res);
                    Console.WriteLine();
                }
                Console.WriteLine("Press Y to exit/N to continue");
                ConsoleKeyInfo kc = Console.ReadKey(true);
                if (kc.KeyChar == 'y' || kc.KeyChar == 'Y')
                {
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// Функция вычисления очередности разложения
        /// </summary>
        /// <param name="vArr">таблица истинности функции</param>
        /// <param name="names">имена переменных</param>
        /// <returns>очередность использования переменных</returns>
        static string[,] OrderVar(bool[,] vArr, string[] names)
        {
            int n = vArr.GetLength(0) - 1;
            int m = (int)(Math.Pow(2, n));
            //создаем массив значенией производных
            int[] p = Derivatives.Weights(vArr);// new int[n];
            for (int arg = 0; arg < n; arg++)
            {
                Console.WriteLine();
                Console.WriteLine("|" + names[arg] + "| = " + p[arg]);
            }
            Console.WriteLine();
            string[,] res = new string[n, 2];
            int[] r = new int[n];
            for (int i = 0; i < n; i++)
            {
                int v = -1; int k = 0;
                for (int j = 0; j < n; j++)
                {
                    if (v < p[j])
                    {
                        v = p[j];
                        k = j;
                    }
                }
                res[i, 0] = names[k];
                res[i, 1] = p[k].ToString();
                p[k] = 0;
            }
            return res;
        }
    }
}
