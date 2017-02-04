using TKM.BoolFunction.Analyze;
namespace TKM.BoolFunction
{
    /// <summary>
    /// Класс анализатора функций
    /// </summary>
    class Analyzer
    {
        #region =============== Fields ======================
        /// <summary>
        /// Функция
        /// </summary>
        private Function _f;
        /// <summary>
        /// Треугольник Паскаля
        /// </summary>
        private bool[,] _triangle;
        /// <summary>
        /// Полином Жегалкина
        /// </summary>
        private string[] _polynome;
        /// <summary>
        /// Веса производных
        /// </summary>
        private int[] _weights;
        /// <summary>
        /// Очередность переменных
        /// </summary>
        private string[] _stek;
        /// <summary>
        /// Класс реализации разложения Шеннона
        /// </summary>
        private Shannon _shannon;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса анализатора булевых функция
        /// </summary>
        /// <param name="func">функция</param>
        public Analyzer(Function func)
        {
            _f = func;
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Треугольник Паскаля. Только для чтения 
        /// </summary>
        public bool[,] Triangle
        {
            get
            {
                if (_triangle == null)
                {
                    _triangle = new Zhegalkin(_f.Table,_f.Names).PascalTriangle;
                }
                return _triangle;
            }
        }
        /// <summary>
        /// Полином Жегалкина в виде массива конъюктивов. Только для чтения
        /// </summary>
        public string[] GegalkinPolynome
        {
            get
            {
                if (_polynome == null)
                {
                    _polynome = new Zhegalkin(_f.Table, _f.Names).ZhegalkinPolynom;
                }
                return _polynome;
            }
        }
        /// <summary>
        /// Веса производных булевой функции. Только для чтения
        /// </summary>
        public int[] DerivativesWeights
        {
            get
            {
                if(_weights == null)
                {
                    _weights = new Derivatives(_f.Table,_f.Names).Weights;
                }
                return _weights;
            }
        }
        /// <summary>
        /// Очередность переменных для разложения Шеннона. Только для чтения
        /// </summary>
        public string[] VarStek
        {
            get
            {
                if(_stek == null)
                {
                    _stek = new Derivatives(_f.Table, _f.Names).ValuesOrder;
                }
                return _stek;
            }
        }
        /// <summary>
        /// Экземпляр класса реализующего разложение Шеннона. Только для чтения
        /// </summary>
        public Shannon Shannon
        {
            get
            {
                if (_shannon == null)
                {
                    _shannon = new Shannon(_polynome, _stek);
                }
                return _shannon;
            }
        }
        #endregion
    }
}
