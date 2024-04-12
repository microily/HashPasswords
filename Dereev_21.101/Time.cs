using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dereev_21._101
{
    // Класс для определения текущего времени и доступности системы
    internal class Time
    {
        /// <summary>
        /// Возвращает приветствие в зависимости от текущего времени
        /// </summary>
        /// <returns>Строка приветствия</returns>
        public string get_time()
        {
            DateTime datetime = DateTime.Now;
            string what_now;

            // Проверка текущего времени и формирование соответствующего приветствия
            if (datetime.Hour >= 10 && datetime.Hour < 12)
            {
                what_now = "Доброе утро";
                return what_now;
            }
            else if (datetime.Hour >= 12 && datetime.Hour < 17)
            {
                what_now = "Добрый день";
                return what_now;
            }
            else if (datetime.Hour >= 17 && datetime.Hour < 19)
            {
                what_now = "Добрый вечер";
                return what_now;
            }
            else
            {
                return null; // Возвращает null в случае, если время не соответствует ни одному приветствию
            }
        }

        /// <summary>
        /// Проверяет доступность системы в текущее время
        /// </summary>
        /// <returns>True, если система доступна, иначе False</returns>
        public static bool Access()
        {
            DateTime datetime = DateTime.Now;

            // Проверка текущего времени для определения доступности системы
            if (datetime.Hour >= 10 && datetime.Hour <= 19)
            {
                return true; // Система доступна в интервале с 10:00 до 19:00
            }
            else
            {
                return false; // Система недоступна в остальное время
            }
        }
    }
}
