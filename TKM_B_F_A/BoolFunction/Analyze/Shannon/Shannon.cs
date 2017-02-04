using TKM.Core;
namespace TKM.BoolFunction.Analyze
{
    /// <summary>
    /// Реализация разложения Шеннона
    /// </summary>
    class Shannon
    {
        #region =============== Fields ======================
        /// <summary>
        /// граф
        /// </summary>
        private Nodes _ns;
        #endregion

        #region =============== Constructors ================
        /// <summary>
        /// Конструктор класса реализующего разложения Шеннона
        /// </summary>
        /// <param name="func">функция в виде полинома Жегалкина</param>
        /// <param name="stek">очередность переменных</param>
        public Shannon(string[] func, string[] stek)
        {
            Manager(func, stek);
        }
        #endregion

        #region =============== Properties ==================
        /// <summary>
        /// Значения фукции в узлах графа 
        /// </summary>
        public string[] ValueInNodes
        {
            get
            {
                if (_ns == null)
                {
                    return new string[] { "Zero Nods" };
                }
                else
                {
                    Node[] nodes = _ns.Data;
                    for (int i = 0; i < nodes.Length - 1; i++)
                    {
                        Node ne = nodes[i + 1];
                        nodes[i + 1] = nodes[i];
                        nodes[i] = ne;
                    }
                    string[] res = new string[1];
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        if (i == 0)
                        {
                            res[i] = MyConvert.StringArrayToString(nodes[i].Name, "+");
                        }
                        else
                        {
                            string[] tech = new string[i + 1];
                            res.CopyTo(tech, 0);
                            tech[i] = MyConvert.StringArrayToString(nodes[i].Name, "+");
                            res = tech;
                        }
                    }
                    return res;
                }
            }
        }
        /// <summary>
        /// Матрица смежности графа
        /// </summary>
        public string[,] AMatrix
        {
            get
            {
                if (_ns == null)
                {
                    return new string[1, 1] { { "Zero Nods!" } };
                }
                else
                {
                    string[,] res = AdjMatrix(_ns.Data);
                    for (int i = 1; i < res.GetLength(0); i++)
                    {
                        res[0, i] = i.ToString();
                        res[i, 0] = i.ToString();
                    }
                    return res;
                }
            }
        }
        #endregion

        #region =============== Methods =====================
        /// <summary>
        /// Метод реализующий разложение
        /// </summary>
        /// <param name="func">функция для разложения</param>
        /// <param name="stek">очередность использования переменных</param>
        public void Manager(string[] func, string[] stek)
        {
            string[] f;
            // создаем граф
            _ns = new Nodes(func);
            //начинаем идти вглубь
            for (int i = 1; i < _ns.Data.Length; i++)
            {
                //берем значение в текущем узле
                f = _ns.Data[i].Name;
                //получаем узлы потомки и их связи
                string[,][] cn = Decomp(f, stek);
                //проверяем, что первый(f0) не равен 0
                for (int j = 0; j < 2; j++)
                {
                    if ((cn[j, 0][0] == "0"))
                    {
                        continue;
                    }
                    else
                    {
                        //проверям, существует ли уже такой узел
                        int k = (_ns.SearchIndex(cn[j, 0]));
                        //если существует
                        if (k != -1)
                        {
                            //добавляем ему связь с родителем.
                            _ns.Data[k].Add(f, MyConvert.StringArrayToString(cn[j, 1],""));
                            _ns.Data[i].Add(cn[j, 0], MyConvert.StringArrayToString(cn[j, 1],""));
                        }
                        else
                        {
                            //иначе создаем узел
                            Node ne = new Node(cn[j, 0], f, MyConvert.StringArrayToString(cn[j, 1],""));
                            //добавляем в массив узлов
                            _ns.Add(ne);
                            //добавляем связь родителю
                            _ns.Data[i].Add(cn[j, 0], MyConvert.StringArrayToString(cn[j, 1],""));
                        }
                    }
                }
            }
            if (_ns.Data.Length == 2)
            {
                if (MyConvert.StringArrayToString(_ns.Data[1].Name,"") == MyConvert.StringArrayToString(_ns.Data[0].Name,""))
                {
                    _ns = new Nodes();
                }
                else
                {
                    if (MyConvert.StringArrayToString(_ns.Data[1].Name,"") == "0")
                    {
                        _ns = null;
                    }
                }
            }
        }
        /// <summary>
        /// Разложение функции на две функции меньшей арности
        /// </summary>
        /// <param name="func">функция дл разложения</param>
        /// <param name="stek">очередность использования переменных</param>
        /// <returns>две функции меньшей арности с соответствующими значениями использованной переменной</returns>
        private string[,][] Decomp(string[] func, string[] stek)
        {
            string[] f0; string[] f1; string v;
            v = CurValue(func, ref stek);
            //ставим 0 вместо переменной
            f0 = Repl(func, v, false);
            //минимизация результата
            f0 = MinF(f0);
            //ставим 1 вместо переменной
            f1 = Repl(func, v, true);
            //минимизация результата
            f1 = MinF(f1);
            string[,][] res = new string[2, 2][];
            res[0, 0] = f0;
            res[0, 1] = new string[2] { "-", v };
            res[1, 0] = f1;
            res[1, 1] = new string[1] { v };
            return res;
        }
        /// <summary>
        /// Выбор переменной для текущего этапа разложения
        /// </summary>
        /// <param name="func">функция для разложения</param>
        /// <param name="stek">очередность использования переменных</param>
        /// <returns>переменная для текущего этапа разложения / "0" если дальнейшее разложение невозможно</returns>
        private string CurValue(string[] func, ref string[] stek)
        {
            int[] b = new int[stek.GetLength(0)];
            for (int i = 0; i < stek.GetLength(0); i++)
            {
                for (int j = 0; j < func.Length; j++)
                {
                    if (func[j].Contains(stek[i]))
                    {
                        b[i]++;
                        return stek[i];
                    }
                }
            }
            return "0";
        }
        /// <summary>
        /// Подстановка значения в переменную
        /// </summary>
        /// <param name="f">функция</param>
        /// <param name="v">переменная</param>
        /// <param name="flag">значение переменной</param>
        /// <returns>измененная функция</returns>
        private string[] Repl(string[] f, string v, bool flag)
        {
            string[] tech = new string[f.Length];
            //System.Array.Copy(f, tech, f.Length);
            for (int i = 0; i < f.Length; i++)
            {
                tech[i] = f[i];
            }
            string n = "0";
            if (flag) { n = "1"; }
            for (int i = 0; i < f.Length; i++)
            {
                if (tech[i].Contains(v))
                {
                    tech[i] = tech[i].Replace(v, n);
                }
            }
            return tech;
        }
        /// <summary>
        /// Минимизация функции на основе алгебры Жегалкина
        /// </summary>
        /// <param name="f">функция для минимизации</param>
        /// <returns>минимизированная функция</returns>
        private string[] MinF(string[] f)
        {
            //замена х*0=0
            for (int k = 0; k < f.Length; k++)
            {
                f[k] = f[k].Replace("-0", "1");
                if (f[k].Contains("0"))
                {
                    f[k] = "0";
                }
            }
            //замена х*1=х
            for (int k = 0; k < f.Length; k++)
            {
                if (f[k].Contains("1") && f[k].Length > 1)
                {
                    f[k] = f[k].Replace("1", "");
                    if (f[k] == "-") { f[k] = "0"; }
                }
            }
            //замена х+х=0
            for (int i = 0; i < f.Length; i++)
            {
                for (int j = 0; j < f.Length; j++)
                {
                    if ((f[i] == f[j]) && (i != j))
                    {
                        f[i] = "0";
                        f[j] = "0";
                    }
                }
            }
            int n = 0;
            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == "0") { n++; }
            }

            if (f.Length - n != 0)
            {
                string[] tech = new string[f.Length - n];

                int m = 0;
                for (int i = 0; i < f.Length; i++)
                {
                    if (f[i] != "0")
                    {
                        tech[m] = f[i];
                        m++;
                    }
                }
                f = tech;
            }
            else
            {
                f = new string[1] { "0" };
            }

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] == "1" && i != 0)
                {
                    string str = "";
                    str = f[0];
                    f[0] = "1";
                    f[i] = str;
                }
            }
            return f;
        }
        /// <summary>
        /// Создание матрицы смежности графа
        /// </summary>
        /// <param name="nodes">маасив узлов</param>
        /// <returns>матрица смежности</returns>
        private string[,] AdjMatrix(Node[] nodes)
        {
            string[,] matrix = new string[nodes.Length + 1, nodes.Length + 1];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = "0";
                }
            }
            for (int i = 0; i < nodes.Length; i++)
            {
                matrix[i + 1, 0] = MyConvert.StringArrayToString(nodes[i].Name,"");
                matrix[0, i + 1] = MyConvert.StringArrayToString(nodes[i].Name,"");
            }

            if (nodes.Length == 1)
            {
                return matrix;
            }
            else
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    for (int j = 0; j < nodes[i].Connections.GetLength(0); j++)
                    {
                        for (int g = 1; g <= nodes.Length; g++)
                        {
                            if (matrix[0, g] == MyConvert.StringArrayToString(nodes[i].Connections[j, 0],""))
                            {
                                matrix[i + 1, g] = MyConvert.StringArrayToString(nodes[i].Connections[j, 1],"");
                            }
                            if (matrix[g, 0] == MyConvert.StringArrayToString(nodes[i].Connections[j, 0],""))
                            {
                                matrix[g, i + 1] = MyConvert.StringArrayToString(nodes[i].Connections[j, 1],"");
                            }
                        }
                    }
                }
                return matrix;
            }
        }
    }
    #endregion
}
