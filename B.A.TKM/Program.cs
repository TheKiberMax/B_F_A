using System;
using System.Collections;
using System.Linq;

namespace TKM_B_F_A
{
    class Program
    {
        static string[] names;
        static string str;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            //Console.WriteLine("Input names from variables");
            //str = Console.ReadLine();
            //names = str.Split(' ');
            names = new string[] { "x", "y", "z", "w" };
            Console.WriteLine("Input vector");
            string func = Console.ReadLine();
            int m = func.Length;
            int n = Convert.ToInt32(Math.Log(m, 2));
            Console.WriteLine();
            Console.WriteLine(str + " f");
            bool[,] vArr = VTable(func);
            OutputArray(vArr, m, n + 1);
            Console.ReadKey();
            Console.WriteLine("triangl");
            Console.WriteLine();
            string[] pol = GegalkinPolynom(vArr, n);
            for (int i = 0; i < pol.Length; i++)
            {
                Console.Write(pol[i] + "+");
            }
            Console.WriteLine();
            Console.WriteLine("derivatives");
            Console.WriteLine();
            string[,] stek = OrderVar(vArr, n);

            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write(stek[i,0] + " ");
            }
            Console.WriteLine();
           
           // stek =new string[] { "x","w","y","z"};
            //OutputArray(d, m, n);
            Console.ReadKey();
            Shannon.Manager(pol, stek);
            Node[] fff = Shannon.ns.Data;
            for (int i = 0; i < fff.Length; i++)
            {
                for (int j = 0; j < fff[i].name.Length; j++)
                {
                    Console.Write(fff[i].name[j] + "+");
                }
                Console.WriteLine();
            }
            string[,] res = Shannon.AdjMatrix(fff);
            OutputArray(res);
            Console.ReadKey();
        }

        /// <summary>
        /// Создание таблицы истинности
        /// </summary>
        /// <param name="func">булева функция в векторной форме</param>
        /// <returns></returns>
        static bool[,] VTable(string func)
        {
            int m = func.Length;
            int n = Convert.ToInt32(Math.Log(m, 2) + 1);
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
                    Console.Write(Convert.ToInt32(arr[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }
        static void OutputArray(string[,] arr)
        {
            int m = 0; int n=0;
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
        /// <returns></returns>
        static string[,] OrderVar(bool[,] vArr, int n) 
        {
            //n++;
            int m = Convert.ToInt32(Math.Pow(2, n));
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
                Console.WriteLine();
            }

            string[,] res = new string[n,2];
            int[] r = new int[n];
            for (int i = 0; i < n; i++)
            {
               // res[i, 0] = names[i];
               // res[i, 1] = p[i].ToString();
                int v = -1; int k = 0; int k2 = 0;

                for (int j = 0; j < n; j++)
                {
                    if (v < p[j])
                    {
                        v = p[j];
                        k = j;
                    }
                }
                res[i,0] = names[k];
                res[i,1] = p[k].ToString();
                p[k] = 0;

            }
            return res;
        }

        /// <summary>
        /// Функция сборки полинома Жегалкина
        /// </summary>
        /// <param name="vArr">Таблица истинности функции</param>
        /// <param name="n">арность функции</param>
        /// <returns></returns>
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
            //string[] pol = new string[polynom.Length];
            //for (int i = 0; i < polynom.Length; i++)
            //{

            //    if (polynom[i] == "1")
            //    {
            //        pol[0] = "1";
            //        continue;
            //    }
            //    switch (polynom[i])
            //    {
            //        case "1":
            //            pol[0] = "1";
            //            break;
            //        case "x":
            //            pol[1] = "x";
            //            break;
            //        case "y":
            //            pol[2] = "y";
            //            break;
            //        case "z":
            //            pol[3] = "z";
            //            break;
            //        case "w":
            //            pol[1] = "w";
            //            break;
            //    }
            //}
            
            return polynom;
        }

    }
}
