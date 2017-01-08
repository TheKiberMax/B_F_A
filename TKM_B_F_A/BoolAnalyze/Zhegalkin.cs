using System;

namespace TKM_B_F_A.BoolAnalyze
{
    static class Zhegalkin
    {
        /// <summary>
        /// Сборка полинома Жегалкина методом треугольника
        /// </summary>
        /// <param name="vArr">Таблица истинности функции</param>
        /// <param name="names">имена переменных</param>
        /// <returns>функция в виде полинома</returns>
        public static string[] Triangle(bool[,] vArr, string[] names)
        {
            int n = vArr.GetLength(0) - 1;
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
            Output.OutputArray(triangl, triangl.GetLength(1), triangl.GetLength(1));
            string[] polynom = new string[1] { "0" };
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
