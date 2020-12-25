using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductBase
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // закружаем данные из файла
            DataLogic.Open("DataBase1"); 

            UserLogic.StartWindow();

            // загружаем данные в файл
            DataLogic.Close();

        }

        /* 
         * 3.
         * Товар. 
         * Произвести выборку стран, в которые экспортируется выбранный товар, и указать объем экспорта (общий и по каждой стране). 
         * Стран, куда экспортируется товар может быть несколько. 
         * Товаров – тоже не один. 
         */
    }
}
