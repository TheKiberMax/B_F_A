using System;
using System.Text.RegularExpressions;
using TKM.Core;
namespace TKM.BoolFunction
{
    /// <summary>
    /// Класс булевой функции
    /// </summary>
    class Function
    {
        #region =============== Fields ======================
        /// <summary>
        /// массив имен переменных функции
        /// </summary>
        private string[] _names;
        /// <summary>
        /// таблица истинности функции
        /// </summary>
        private bool[,] _table;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса булевой функции
        /// </summary>
        /// <param name="func">строка с функцией</param>
        public Function(string func)
        {
            Parse(func);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Массив с именами переменных. Только для чтения 
        /// </summary>
        public string[] Names => _names;
        /// <summary>
        /// Массив с таблицей истинности. Только для чтения
        /// </summary>
        public bool[,] Table => _table;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Метод извлечения данных из строки
        /// </summary>
        /// <param name="func">строка с функцией</param>
        private void Parse(string func)
        {
            if (ItsVector(func))
            {
                _table = new Analyze.TrueTable(func).Table;
                var lenght = MyMath.Pow2Index(func.Length);
                _names = new string[lenght];
                for(int i=0;i<lenght;i++)
                {
                    _names[i] = ((char)(65 + i)).ToString();
                }
            }
            else
            {
                func = func.ToUpper();
                Regex regex = new Regex("[^A-Z]|V");
                func = regex.Replace(func, "");
                string tech = "";
                for (int i = 0; i < func.Length; i++)
                {
                    if (!tech.Contains(func[i].ToString()))
                    {
                        tech += func[i];
                    }
                }
                _names = Array.ConvertAll(tech.ToCharArray(), MyConvert.CharToString);
                Array.Sort(_names);
                _table = new Analyze.TrueTable(func,_names).Table;
            }
        }
        /// <summary>
        /// Метод определения вида записи функции
        /// </summary>
        /// <param name="data">строка с функцией</param>
        /// <returns>true - функция в виде вектора значений, false - общий вид</returns>
        private bool ItsVector(string data)
        {
            var lh = data.Length;
            if (MyConvert.IntToBool(lh) && ! MyConvert.IntToBool(lh & (lh - 1)))
            {
                foreach (char c in data)
                    if (c < 48 || c > 49)
                    {
                        return false;
                    }

                return true;
            }
            return false;
        }
        #endregion
    }
}
