namespace TKM.Core
{
    /// <summary>
    /// Класс преобразования типов
    /// </summary>
    static class MyConvert
    {
        #region =============== Methods =====================
        /// <summary>
        /// Преобразование bool=>int(Это C#, не С++!!!)
        /// </summary>
        /// <param name="flag">bool value</param>
        /// <returns>int values</returns>
        static public int BoolToInt(bool flag)
        {
            if (flag)
            {
                return 1;
            }
            return 0;
        }
        /// <summary>
        /// Преобразование int=>bool(Это C#, не C++!!!)
        /// </summary>
        /// <param name="n">int value</param>
        /// <returns>0 - false, else - true</returns>
        static public bool IntToBool(int n)
        {
            if (n==0)
            {
                return false ;
            }
            return true;
        }

        static public string CharToString(char ch)
        {
            return ch.ToString();
        }
        /// <summary>
        /// Преобразование массива логических значений в строку
        /// </summary>
        /// <param name="array">массив</param>
        /// <returns></returns>
        static public string BoolArrayToString(bool[,] array)
        {
            int n = array.GetLength(0);
            int m = array.GetLength(1);
            string tech = "";
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    tech += BoolToInt(array[i, j]) + " ";
                }
                tech += "\n";
            }
            return tech;
        }
        /// <summary>
        /// Преобразование массива строк в строку
        /// </summary>
        /// <param name="array">одномерный массив</param>
        /// <param name="str">разделитель(по умолчанию - " ")</param>
        /// <param name="numeric">флаг вывода нумерованным списком</param>
        /// <returns>строка</returns>
        static public string StringArrayToString(string[] array, string str=" ", bool numeric=false)
        {
            string tech = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (numeric)
                {
                    tech += ((i + 1).ToString() + " - ");
                }
                    tech += array[i] + str;
            }
            return tech;
        }
        /// <summary>
        /// Преобразование массива строк в строку
        /// </summary>
        /// <param name="array">двумерный массив</param>
        /// <param name="str">разделитель(по умолчанию - " ")</param>
        /// <returns>строка</returns>
        static public string StringArrayToString(string[,] array, string str = " ")
        {
            string tech = "";
            int m = 0; int n = 0;
            m = array.GetLength(0);
            n = array.GetLength(1);
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    tech +=array[i, j] + str;
                }
                tech += "\n";
            }
            return tech;
        }
        #endregion
    }
}
