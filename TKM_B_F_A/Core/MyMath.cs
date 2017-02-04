namespace TKM.Core
{
    /// <summary>
    /// Класс оптимизированных вычислений
    /// </summary>
    static class MyMath
    {
        #region =============== Methods =====================
        /// <summary>
        /// Вычисление показателя степени двойки
        /// </summary>
        /// <param name="x">число, степень двойки, меньше чем 2^32</param>
        /// <returns>показатель степени</returns>
        static public int Pow2Index(int x)
        {
            var pow = 0;
            while (x > 0) { x = x >> 1; pow++; }; 
            pow--;
            return pow;
        }
        /// <summary>
        /// Возведение в степень двойки
        /// </summary>
        /// <param name="x">показатель степени, меньше 32</param>
        /// <returns></returns>
        static public int Pow2(int x)
        {
            //x<=31
            int res=2;
            for (int i = 1; i < x; i++)
                res <<= 1;
            return res;
        }
        #endregion
    }
}
