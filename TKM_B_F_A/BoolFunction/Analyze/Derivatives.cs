namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Класс производных булевой функции
    /// </summary>
    class Derivatives
    {
        #region =============== Fields ======================
        /// <summary>
        /// Веса производных
        /// </summary>
        private int[] _weights;
        /// <summary>
        /// Порядок переменных
        /// </summary>
        private string[] _stek;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса производных булевой функции
        /// </summary>
        /// <param name="table">таблица истинности</param>
        /// <param name="names">имена переменных</param>
        public Derivatives(bool[,] table, string[] names)
        {
            _stek = OrderVal(table, names);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Веса производных
        /// </summary>
        public int[] Weights => _weights;
        /// <summary>
        /// Порядок переменных
        /// </summary>
        public string[] ValuesOrder => _stek;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Функция вычисления весов производных
        /// </summary>
        /// <param name="table">таблица истинности функции</param>
        /// <returns>массив весов</returns>
        private int[] CalcWeights(bool[,] table)
        {
            int n = table.GetLength(0)-1;
            int m = table.GetLength(1);
            //создаем массив с весами производных
            int[] p = new int[n];
            for (int arg = 0; arg < n; arg++)
            {
                int l = 0;
                int l2 = m / 2;
                bool[,] d = new bool[n + 1, m];
                for (int i = 0; i < m; i++)
                {
                    if (table[arg, i])
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            d[j, l] = table[j, i];
                        }
                        l++;
                    }
                    else
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            d[j, l2] = table[j, i];
                        }
                        l2++;
                    }
                }
                l2 = m / 2;
                l = 0;
                for (int i = 0; i < l2; i++)
                {
                    if ((d[n, i]) ^ (d[n, l2 + i]))
                    {
                        p[arg]++;
                    }
                }
            }
            return p;
        }
        /// <summary>
        /// Функция вычисления очередности разложения
        /// </summary>
        /// <param name="table">таблица истинности функции</param>
        /// <param name="names">имена переменных</param>
        /// <returns>очередность использования переменных</returns>
        private string[] OrderVal(bool[,] table, string[] names)
        {
            int n = table.GetLength(0) - 1;
            int m = table.GetLength(1);
            if(_weights == null)
            {
                _weights = CalcWeights(table);
            }
            int[] p = _weights;
            string[] res = new string[n];
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
                res[i] = names[k];
                p[k] = 0;
            }
            return res;
        }
        #endregion
    }
}
