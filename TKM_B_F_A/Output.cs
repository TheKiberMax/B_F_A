using System;

namespace TKM_B_F_A
{
    static class Output
    {
        /// <summary>
        /// Вывод двумерного массива строк
        /// </summary>
        /// <param name="arr">массив</param>
        static public void OutputArrayInConsole(string[,] arr)
        {
            int m = 0; int n = 0;
            m = arr.GetLength(0);
            n = arr.GetLength(1);
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(arr[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Вывод двумерного булевого массива
        /// </summary>
        /// <param name="arr">булевый массив</param>
        /// <param name="m">кол-во строк</param>
        /// <param name="n">кол-во столбцов</param>
        static public void OutputArray(bool[,] arr, int m, int n)
        {
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(Ct.BooleanToInt(arr[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
