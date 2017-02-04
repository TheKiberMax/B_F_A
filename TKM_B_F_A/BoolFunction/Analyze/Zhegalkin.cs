using TKM.Core;
namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Класс полинома жегалкина
    /// </summary>
    class Zhegalkin
    {
        #region =============== Fields ======================
        /// <summary>
        /// Треугольник Паскаля
        /// </summary>
        private bool[,] _triangle;
        /// <summary>
        /// Полином Жегалкина
        /// </summary>
        private string[] _polynom;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса реализующего нахождения полинома Жегалкина 
        /// </summary>
        /// <param name="table">таблица истинности</param>
        /// <param name="names">имена переменных</param>
        public Zhegalkin(bool[,] table, string[] names)
        {
           _polynom = Polynom(table, names);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Треугольник Паскаля. Только для чтения
        /// </summary>
        public bool[,] PascalTriangle => _triangle;
        /// <summary>
        /// Полином Жегалкина. Только для чтения
        /// </summary>
        public string[] ZhegalkinPolynom => _polynom;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Треугольник Паскаля
        /// </summary>
        /// <param name="table">Таблица истинности функции</param>
        /// <returns>треугольник Паскаля</returns>
        private bool[,] Triangle(bool[,] table)
        {
            int n = table.GetLength(0) - 1;
            int m = table.GetLength(1); 
            int m1 = m;
            int m2 = m;
            bool[,] triangl = new bool[m, m];
            for (int i = 0; i < m; i++)
            {
                triangl[i, 0] = table[n, i];
            }
            for (int j = 0; j < m1 - 1; j++)
            {
                for (int i = 0; i < m2 - 1; i++)
                {
                    triangl[i, j + 1] = triangl[i, j] ^ triangl[i + 1, j];
                }
                m2--;
            }
            return triangl;
        }
        /// <summary>
        /// Сборка полинома Жегалкина методом треугольника
        /// </summary>
        /// <param name="table">Таблица истинности функции</param>
        /// <param name="names">имена переменных функции</param>
        /// <returns>полином в виде массива конъюктивов</returns>
        private string[] Polynom(bool[,] table, string[] names)
        {
            if(_triangle == null)
            {
                _triangle = Triangle(table);
            }
            bool[,] triangle = _triangle;
            int n = table.GetLength(0) - 1;
            int m = MyMath.Pow2(n);
            string[] polynom = new string[1] { "0" };
            bool flag = true;
            for (int i = 0; i < m; i++)
            {
                if (triangle[0, i])
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
                            if (table[j, i])
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
        #endregion
    }
}
