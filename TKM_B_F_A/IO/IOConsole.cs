using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TKM.IO
{   
    /// <summary>
    /// Класс ввода/вывода в консоль. Наследует интерфейс IIO
    /// </summary>
    class IOConsole : IIO
    {
        #region =============== Methods =====================
        /// <summary>
        /// Конструктор класса ввода/вывода в консоль
        /// </summary>
        /// <param name="CursorVisible">видимость курсора. необязательный параметр</param>
        /// <param name="color">цвет консоли. необязательный параметр</param>
        public IOConsole()//bool CursorVisible = true, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            //Console.CursorVisible = true;
            //Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        /// <summary>
        /// Переопределение метода интерфейса IIO. Получение входных данных
        /// </summary>
        /// <returns>строка с данными</returns>
        public string Get()
        {
             return Input();
        }
        /// <summary>
        /// Переопределение метода интерфейса IIO. Передача выходных данных
        /// </summary>
        /// <param name="data">строка с данными</param>
        public void Set(string data)
        {
            Console.WriteLine(data);
        }
        /// <summary>
        /// Запрос входных данных
        /// </summary>
        /// <returns>строка с данными</returns>
        private string Input()
        {
            Console.WriteLine("Input function //^v-" + Environment.NewLine);
            string func = Console.ReadLine();
            Console.WriteLine();
            return func;
        }
        #endregion
    }
}
