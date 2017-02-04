namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Класс узла графа
    /// </summary>
    class Node
    {
        #region =============== Fields ======================
        /// <summary>
        /// Массив связей узла
        /// </summary>
        private string[,][] _connections;
        /// <summary>
        /// Значение функции в узле
        /// </summary>
        private string[] _name;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса узла графа(с добавлением связи)
        /// </summary>
        /// <param name="ne">имя узла</param>
        /// <param name="cs">имя связанного узла</param>
        /// <param name="wt">вес</param>
        public Node(string[] ne, string[] cs, string wt)
        {
            _name = ne;
            Add(cs, wt);
        }
        /// <summary>
        /// Конструктор класса узла графа
        /// </summary>
        /// <param name="ne">имя узла</param>
        public Node(string[] ne)
        {
            _name = ne;
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Возвращает массив связей узла. Только для чтения
        /// </summary>
        public string[,][] Connections => _connections;
        /// <summary>
        /// Возвращает функцию в узле. Только для чтения
        /// </summary>
        public string[] Name => _name;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Добавляет связь узлу
        /// </summary>
        /// <param name="cn">имя связанного узла </param>
        /// <param name="wt">вес</param>
        public void Add(string[] cn, string wt)
        {
            if (_connections == null || _connections.Length == 0)
            {
                _connections = new string[1, 2][];
                _connections[0, 0] = cn;
                string[] t = new string[wt.Length];
                for (int i = 0; i < wt.Length; i++)
                {
                    t[i] = wt[i].ToString();
                }

                _connections[0, 1] = t;
            }
            else
            {
                if (!Search(cn))
                {
                    string[,][] tech = new string[_connections.GetLength(0) + 1, _connections.GetLength(1)][];
                    for (int i = 0; i < _connections.GetLength(0); i++)
                    {
                        for (int j = 0; j < _connections.GetLength(1); j++)
                        {
                            tech[i, j] = _connections[i, j];
                        }
                    }
                    tech[tech.GetLength(0) - 1, 0] = cn;
                    string[] t = new string[wt.Length];
                    for (int i = 0; i < wt.Length; i++)
                    {
                        t[i] = wt[i].ToString();
                    }
                    tech[tech.GetLength(0) - 1, 1] = t;
                    _connections = tech;
                }
            }
        }
        /// <summary>
        /// Метод поиска связи по значению функции в связанном узле
        /// </summary>
        /// <param name="cn">функция в связанном узле</param>
        /// <returns>true если связь существует</returns>
        public bool Search(string[] cn)
        {
            for (int i = 0; i < _connections.GetLength(0); i++)
            {
                if (_connections[i, 0].Length == cn.Length)
                {
                    bool f = false;
                    for (int j = 0; j < cn.Length; j++)
                    {
                        if (_connections[i, 0][j] != cn[j])
                        {
                            f = false;
                            break;
                        }
                        else
                        {
                            f = true;
                        }
                    }
                    if (f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    #endregion
}
