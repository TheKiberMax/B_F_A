using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TKM.IO
{
    /// <summary>
    /// Интерфейс ввода/вывода
    /// </summary>
    interface IIO
    {
        /// <summary>
        /// Получение входных данных
        /// </summary>
        /// <returns>строка с данными</returns>
        string Get();
        /// <summary>
        /// Передача выходных данных
        /// </summary>
        /// <param name="data">строка с данными</param>
        void Set(string data);
    }
}
