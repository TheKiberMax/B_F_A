using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TKM_B_F_A
{
    class Derivatives
    {
        /// <summary>
        /// Функция вычисления весов производных
        /// </summary>
        /// <param name="vArr">таблица истинности функции</param>
        /// <returns>массив весов</returns>
        static public int[] Weights(bool[,] vArr)
        {
            int n = vArr.GetLength(0)-1;
            int m = (int)(Math.Pow(2, n));
            //создаем массив с весами производных
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
            }
            return p;
        }

    }
}
