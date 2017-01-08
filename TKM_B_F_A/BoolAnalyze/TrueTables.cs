using System;
using System.Collections;
using System.Linq;

namespace TKM_B_F_A
{
    static class TrueTables
    {
        /// <summary>
        /// Создание таблицы истинности булевой функции
        /// </summary>
        /// <param name="func">булева функция в векторной форме</param>
        /// <returns>таблица истинности</returns>
        static public bool[,] VTable(string func)
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
        /// Создание таблицы истинности булевой функции
        /// </summary>
        /// <param name="func">булева функция</param>
        /// <param name="names">массив переменных</param>
        /// <returns>таблица истинности</returns>
        static public bool[,] STable(string func, string[] names)
        {
            int n = names.Length;
            int m = (int)Math.Pow(2, n);
            //n++;
            bool[,] iArray = new bool[n + 1, m];
            for (int i = 0; i < m; i++)
            {
                string tech = func;
                var t = new BitArray(decimal.GetBits(i)).Cast<bool>().ToArray();
                var bo = t.Take(n).ToArray();
                for (int k = 0; k < n; k++)
                {
                    iArray[k, i] = bo[n - 1 - k];
                }
                for (int j = 0; j < n; j++)
                {
                    tech = tech.Replace(names[j], Ct.BooleanToInt(iArray[j, i]).ToString());
                }
                tech = Min(tech);

                switch (tech)
                {
                    case "1":
                        iArray[n, i] = true;
                        break;
                    case "0":
                        iArray[n, i] = false;
                        break;
                }
            }
            return iArray;
        }

        static string Min(string str)
        {
            string old = "";
            do
            {
                old = str;
                do
                {
                    old = str;

                    str = str.Replace("-0", "1");
                    str = str.Replace("-1", "0");

                    str = str.Replace("(0)", "0");
                    str = str.Replace("(1)", "1");

                    str = str.Replace("1^0", "0");
                    str = str.Replace("0^0", "0");
                    str = str.Replace("0^1", "0");
                    str = str.Replace("1^1", "1");

                    str = str.Replace("10", "0");
                    str = str.Replace("00", "0");
                    str = str.Replace("01", "0");
                    str = str.Replace("11", "1");
                } while (old != str);

                str = str.Replace("1V0", "1");
                str = str.Replace("0V0", "0");
                str = str.Replace("0V1", "1");
                str = str.Replace("1V1", "1");

                str = str.Replace("1+0", "1");
                str = str.Replace("0+0", "0");
                str = str.Replace("0+1", "1");
                str = str.Replace("1+1", "1");
            } while (old != str);

            return str;
        }
    }
}
