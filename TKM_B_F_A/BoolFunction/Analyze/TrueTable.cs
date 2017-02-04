using System.Collections;
using System.Linq;
using TKM.Core;
namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Класс таблицы истинности булевой функции.
    /// </summary>
    class TrueTable
    {
        #region =============== Fields ======================
        /// <summary>
        /// Таблица истинности
        /// </summary>
        private bool[,] _table;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса таблицы истинности
        /// </summary>
        /// <param name="f">функция в виде вектора</param>
        public TrueTable(string f)
        {
            _table = VTable(f);
        }
        /// <summary>
        /// Конструктор класса таблицы истинности
        /// </summary>
        /// <param name="f">функция в виде формулы</param>
        /// <param name="names">имена переменных</param>
        public TrueTable(string f, string[] names)
        {
            _table = STable(f, names);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Таблица истинности. Только для чтения
        /// </summary>
        public bool[,] Table => _table;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Создание таблицы истинности булевой функции из вектора
        /// </summary>
        /// <param name="func">булева функция в векторной форме</param>
        /// <returns>таблица истинности</returns>
        private bool[,] VTable(string func)
        {
            int m = func.Length;
            int n = MyMath.Pow2Index(m) + 1;
            bool[,] iArray = new bool[n, m];
            for (int i = 0; i < m; i++)
            {
                // Хаки дотнета наше все. хотя надо выпилить 
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
        /// Создание таблицы истинности булевой функции из 
        /// </summary>
        /// <param name="func">булева функция</param>
        /// <param name="names">массив переменных</param>
        /// <returns>таблица истинности</returns>
        private bool[,] STable(string func, string[] names)
        {
            int n = names.Length;
            int m = MyMath.Pow2(n);
            bool[,] iArray = new bool[n + 1, m];
            for (int i = 0; i < m; i++)
            {
                string tech = func;
                // Хаки дотнета наше все. хотя надо выпилить 
                var t = new BitArray(decimal.GetBits(i)).Cast<bool>().ToArray();
                var bo = t.Take(n).ToArray();
                for (int k = 0; k < n; k++)
                {
                    iArray[k, i] = bo[n - 1 - k];
                }
                for (int j = 0; j < n; j++)
                {
                    tech = tech.Replace(names[j], MyConvert.BoolToInt(iArray[j, i]).ToString());
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
        /// <summary>
        /// Минимизация функции
        /// </summary>
        /// <param name="str">функция</param>
        /// <returns></returns>
        private string Min(string str)
        {
            string old = "";
            do
            {
                old = str;
                do
                {
                    old = str;
                    // Замена отрицания
                    str = str.Replace("-0", "1");
                    str = str.Replace("-1", "0");
                    // Замена скобок
                    str = str.Replace("(0)", "0");
                    str = str.Replace("(1)", "1");
                    // Замена конъюнкции 
                    str = str.Replace("1^0", "0");
                    str = str.Replace("0^0", "0");
                    str = str.Replace("0^1", "0");
                    str = str.Replace("1^1", "1");
                    // Тоже, только для слитной записи 
                    str = str.Replace("10", "0");
                    str = str.Replace("00", "0");
                    str = str.Replace("01", "0");
                    str = str.Replace("11", "1");
                } while (old != str);
                // Замена дизъюнкции
                str = str.Replace("1V0", "1");
                str = str.Replace("0V0", "0");
                str = str.Replace("0V1", "1");
                str = str.Replace("1V1", "1");
                // Тоже
                str = str.Replace("1+0", "1");
                str = str.Replace("0+0", "0");
                str = str.Replace("0+1", "1");
                str = str.Replace("1+1", "1");
            } while (old != str);
            return str;
        }
        #endregion
    }
}
