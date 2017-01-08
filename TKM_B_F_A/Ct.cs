namespace TKM_B_F_A
{
    static class Ct
    {
        /// <summary>
        /// Преобразование bool=>int(Это C#, не С++!!!)
        /// </summary>
        /// <param name="flag">bool values</param>
        /// <returns>int values</returns>
        static public int BooleanToInt(bool flag)
        {
            if (flag)
            {
                return 1;
            }
            return 0;
        }
    }
}
