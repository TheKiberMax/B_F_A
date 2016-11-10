﻿using System;
using System.Collections;
using System.Linq;

namespace TKM_B_F_A
{
    class Decomp
    {
        static string[] names;
        static void Main(string[] args)
        {
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            names = new string[] { "x", "y", "z", "w" };
            Console.WriteLine("Input vector");
            string func = Console.ReadLine();
            int m = func.Length;
            int n = Convert.ToInt32(Math.Log(m, 2));
            Console.WriteLine();
            Console.WriteLine("Truth table");
            Console.WriteLine("x y z w f");
            bool[,] vArr = VTable(func);
            OutputArray(vArr, m, n + 1);
            Console.WriteLine();
            Console.WriteLine("Pascal's triangle");
            Console.WriteLine();
            string[] pol = GegalkinPolynom(vArr, n);
            Console.WriteLine();
            Console.WriteLine("Gegalkin Polynom");
            Console.WriteLine();
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
            string[,] stek = OrderVar(vArr, n);
            Console.WriteLine("Variables priority");
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write(stek[i, 0] + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Shannon.Manager(pol, stek);
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
            OutputArray(res);
            Console.ReadKey();
        }

        /// <summary>
        /// Создание таблицы истинности булевой функции
        /// </summary>
        /// <param name="func">булева функция в векторной форме</param>
        /// <returns>таблица истинности</returns>
        static bool[,] VTable(string func)
        {
            int m = func.Length;
            int n = (int)(Math.Log(m, 2) + 1);
            bool[,] iArray = new bool[n, m];
            for (int i = 0; i < m; i++)
            {
                var t = new BitArray(decimal.GetBits(i)).Cast<bool>().ToArray();
                var bo = t.Take(n).ToArray();
                for (int k = 0; k < n - 1; k++)
                {
                    iArray[k, i] = bo[n - 2 - k];
                }
                switch (func[i])
                {
                    case '1':
                        iArray[n - 1, i] = true;
                        break;
                    case '0':
                        iArray[n - 1, i] = false;
                        break;
                }
            }
            return iArray;
        }

        /// <summary>
        /// Вывод двумерного булевого массива
        /// </summary>
        /// <param name="arr">булевый массив</param>
        /// <param name="m">кол-во строк</param>
        /// <param name="n">кол-во столбцов</param>
        static void OutputArray(bool[,] arr, int m, int n)
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(BooleanToInt(arr[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Преобразование bool=>int(Это C#, не С++!!!)
        /// </summary>
        /// <param name="flag">bool values</param>
        /// <returns>int values</returns>
        static int BooleanToInt(bool flag)
        {
            if (flag)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Вывод двумерного массива строк
        /// </summary>
        /// <param name="arr">массив</param>
        static void OutputArray(string[,] arr)
        {
            int m = 0; int n = 0;
            m = arr.GetLength(0);
            n = arr.GetLength(1);
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(arr[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Функция вычисления очередности разложения
        /// </summary>
        /// <param name="vArr">таблица истинности функции</param>
        /// <param name="n">арность функции</param>
        /// <returns>очередность использования переменных</returns>
        static string[,] OrderVar(bool[,] vArr, int n)
        {
            int m = (int)(Math.Pow(2, n));
            //создаем массив значенией производных
            int[] p = new int[n];
            for (int arg = 0; arg < n; arg++)
            {
                int l = 0; int l2 = m / 2;
                bool[,] d = new bool[n + 1, m];
                for (int i = 0; i < m; i++)
                {
                    if (vArr[arg, i])
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            d[j, l] = vArr[j, i];
                        }
                        l++;
                    }
                    else
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            d[j, l2] = vArr[j, i];
                        }
                        l2++;
                    }
                }
                l2 = m / 2; l = 0;
                for (int i = 0; i < m / 2; i++)
                {
                    if ((d[n, i]) ^ (d[n, l2 + i]))
                    {
                        p[arg]++;
                    }
                }
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

        /// <summary>
        /// Сборка полинома Жегалкина
        /// </summary>
        /// <param name="vArr">Таблица истинности функции</param>
        /// <param name="n">арность функции</param>
        /// <returns>функция в виде полинома</returns>
        static string[] GegalkinPolynom(bool[,] vArr, int n)
        {
            int m = Convert.ToInt32(Math.Pow(2, n));
            int m1 = m;
            int m2 = m;
            bool[,] triangl = new bool[m, m];
            for (int i = 0; i < m; i++)
            {
                triangl[i, 0] = vArr[n, i];
            }
            for (int j = 0; j < m1 - 1; j++)
            {
                for (int i = 0; i < m2 - 1; i++)
                {
                    triangl[i, j + 1] = triangl[i, j] ^ triangl[i + 1, j];
                }
                m2--;
            }
            //вывод треугольника
            OutputArray(triangl, triangl.GetLength(1), triangl.GetLength(1));
            string[] polynom = new string[1];
            bool flag = true;
            for (int i = 0; i < m; i++)
            {
                if (triangl[0, i])
                {
                    if (i == 0)
                    {
                        polynom[0] = "1";
                        flag = false;
                    }
                    else
                    {
                        string tech = "";
                        for (int j = 0; j < n; j++)
                        {
                            if (vArr[j, i])
                            {
                                tech += names[j];
                            }
                        }
                        if (flag)
                        {
                            polynom[0] = tech;
                            flag = false;
                        }
                        else
                        {
                            string[] t = new string[polynom.Length + 1];
                            polynom.CopyTo(t, 0);
                            t[t.Length - 1] = tech;
                            polynom = t;
                        }
                    }
                }
            }
            return polynom;
        }
    }
}