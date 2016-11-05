using System;
using System.Linq;

namespace TKM_B_F_A
{
    static class Shannon
    {
        static public Nodes ns;
        static public void Manager(string[] func, string[,] stek)
        {
            string[] f;
            // создаем граф
            ns = new Nodes(func);
            //начинаем идти вглубь
            for (int i = 1; i < ns.Data.Length; i++)
            {
                //берем значение в текущем узле
                f = ns.Data[i].name;
                //проверяем, что оно не одна переменная
                if (f.Length == 1)
                {
                    //иначе пропускаем его
                    continue;
                }
                else
                {
                    //получаем узлы потомки и их связи
                    string[,][] cn = Decomp(f, stek);
                    //проверяем, что первый(f0) не равен 0
                    for (int j = 0; j < 2; j++)
                    {
                        if ((cn[j, 0][0] == "0"))
                        {
                            continue;
                        }
                        else
                        {
                            //проверяем что это одна переменная
                            if (cn[j, 0].Length == 1 && cn[j, 0][0] != "1")
                            {

                                //если это так то создаем узел связанный с конечным
                                string[] ed = new string[1];
                                ed[0] = "1";
                                Node ne = new Node(cn[j, 0], ed, ParseToStr(cn[j, 0]));
                                //добавляем в массив узлов
                                ns.Add(ne);
                                //добавляем связь конечному узлу
                                ns.Data[0].Add(cn[j, 0], ParseToStr(cn[j, 0]));
                            }
                            //проверям, существует ли уже такой узел
                            int k = (ns.SearchIndex(cn[j, 0]));
                            //если существует
                            if (k != -1)
                            {
                                //добавляем ему связь с родителем.
                                ns.Data[k].Add(f, ParseToStr(cn[j, 1]));
                            }
                            else
                            {
                                //иначе создаем узел
                                Node ne = new Node(cn[j, 0], f, ParseToStr(cn[j, 1]));
                                //добавляем в массив узлов
                                ns.Add(ne);
                                //добавляем связь родителю
                                ns.Data[i].Add(cn[j, 0], ParseToStr(cn[j, 1]));
                            }
                        }
                    }
                }
            }
        }

        static string[,][] Decomp(string[] func, string[,] stek)
        {
            string[] f0; string[] f1; string v;
            v = CurVar(func, ref stek);
            //ставим 0 вместо переменной
            f0 = Repl(func, v, false);
            //минимизация результата
            f0 = MinF(f0);
            //ставим 1 вместо переменной
            f1 = Repl(func, v, true);
            //минимизация результата
            f1 = MinF(f1);
            string[,][] res = new string[2, 2][];
            res[0, 0] = f0;
            res[0, 1] = ("-" + v).ToCharArray().Select(c => c.ToString()).ToArray();
            res[1, 0] = f1;
            res[1, 1] = (v).ToCharArray().Select(c => c.ToString()).ToArray();
            return res;
        }

        static string CurVar(string[] func, ref string[,] stek)
        {
            int[] b = new int[stek.GetLength(0)];
            for (int i = 0; i < stek.GetLength(0); i++)
            {
                for (int j = 0; j < func.Length; j++)
                {
                    if (func[j].Contains(stek[i, 0]))
                    {
                        b[i]++;
                        return stek[i, 0];
                    }
                }
            }
            return "0";
        }

        static string[] Repl(string[] f, string v, bool flag)
        {
            string[] tech = new string[f.Length];
            Array.Copy(f, tech, f.Length);
            string n = "0";
            if (flag) { n = "1"; }
            for (int i = 0; i < f.Length; i++)
            {
                if (tech[i].Contains(v))
                {
                    tech[i] = tech[i].Replace(v, n);
                }
            }
            return tech;
        }

        static string[] MinF(string[] f)
        {
            char[] tc = new char[1] { '1' };
            //замена х*0=0
            for (int k = 0; k < f.Length; k++)
            {
                if (f[k].Contains("-0"))
                {
                    f[k] = f[k].Replace("-0", "1");
                }
                if (f[k].Contains("0"))
                {
                    f[k] = "0";
                }
            }
            //замена х*1=х
            for (int k = 0; k < f.Length; k++)
            {
                if (f[k].Contains("1") && f[k].Length > 1)
                {
                    f[k] = f[k].Replace("1", "");
                    if (f[k] == "-") { f[k] = "0"; }
                }
            }
            //замена х+х=0
            for (int i = 0; i < f.Length; i++)
            {
                for (int j = 0; j < f.Length; j++)
                {
                    if ((f[i] == f[j]) && (i != j))
                    {
                        f[i] = "0";
                        f[j] = "0";
                    }
                }
            }
            int n = 0;
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == "0") { n++; }
            }
            if (f.Length - n != 0)
            {
                string[] tech = new string[f.Length - n];

                int m = 0;
                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i] != "0")
                    {
                        tech[m] = f[i];
                        m++;
                    }
                }
                f = tech;
            }
            else
            {
                f = new string[1] { "0" };
            }

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == "1" && i != 0)
                {
                    string str = "";
                    str = f[0];
                    f[0] = "1";
                    f[i] = str;
                }
            }
            return f;
        }

        static public string ParseToStr(string[] cn)
        {
            string wt = "";
            for (int j = 0; j < cn.Length; j++)
            {
                wt += cn[j];
            }
            return wt;
        }

        static public string[,] AdjMatrix(Node[] nodes)
        {
            string[,] matrix = new string[nodes.Length + 1, nodes.Length + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = "0";
                }
            }
            for (int i = 0; i < nodes.Length; i++)
            {
                matrix[i + 1, 0] = ParseToStr(nodes[i].name);

            }
            for (int i = 0; i < nodes.Length; i++)
            {
                matrix[0, i + 1] = ParseToStr(nodes[i].name);

            }
            for (int i = 0; i < nodes.Length; i++)
            {
                for (int j = 0; j < nodes[i].connections.GetLength(0); j++)
                {
                    for (int g = 1; g <= nodes.Length; g++)
                    {
                        if (matrix[0, g] == ParseToStr(nodes[i].connections[j, 0]))
                        {
                            matrix[i + 1, g] = ParseToStr(nodes[i].connections[j, 1]);
                        }
                        if (matrix[g, 0] == ParseToStr(nodes[i].connections[j, 0]))
                        {
                            matrix[g, i + 1] = ParseToStr(nodes[i].connections[j, 1]);
                        }
                    }
                }
            }
            return matrix;
        }
    }
}
