namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Класс графа
    /// </summary>
    class Nodes
    {
        #region =============== Fields ======================
        /// <summary>
        /// Массив узлов графа
        /// </summary>
        private Node[] _nodes;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса графа
        /// </summary>
        /// <param name="func">Исходная булевая функция</param>
        public Nodes(string[] func)
        {
            string[] str = new string[1];
            str[0] = "1";
            Node ne = new Node(str);
            Add(ne);
            ne = new Node(func);
            Add(ne);
        }
        /// <summary>
        /// Конструктор класса графа для 1 ноды
        /// </summary>
        public Nodes()
        {
            string[] str = new string[1];
            str[0] = "1";
            Node ne = new Node(str);
            Erase();
            Add(ne);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Возвращает массив узлов. Только для чтения
        /// </summary>
        public Node[] Data => _nodes;
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Добавляет узел в массив узлов
        /// </summary>
        /// <param name="ne">имя узла</param>
        public void Add(Node ne)
        {
            if (_nodes == null || _nodes.Length == 0)
            {
                _nodes = new Node[1];
                _nodes[0] = ne;
            }
            else
            {
                Node[] tech = new Node[_nodes.Length + 1];
                _nodes.CopyTo(tech, 0);
                tech[tech.Length - 1] = ne;
                _nodes = tech;
            }
        }
        /// <summary>
        /// Удаляет все узлы.
        /// </summary>
        public void Erase()
        {
            _nodes = null;
        }
        /// <summary>
        /// Поиск узла по значению функции в узле
        /// </summary>
        /// <param name="cn">функция в узле</param>
        /// <returns>индекс в массиве узлов(если узел существует), -1 - если узла не существует</returns>
        public int SearchIndex(string[] cn)
        {
            if (!(_nodes == null || _nodes.Length == 0))
            {
                for (int i = 0; i < _nodes.Length; i++)
                {
                    if (_nodes[i].Name.Length == cn.Length)
                    {
                        bool f = false;
                        for (int j = 0; j < cn.Length; j++)
                        {
                            if (_nodes[i].Name[j] != cn[j])
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
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
    #endregion
}