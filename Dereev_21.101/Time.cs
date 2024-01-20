using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dereev_21._101
{
    internal class Time
    {
        public string get_time()
        {
            DateTime datetime = DateTime.Now;
            string what_now;
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
                return null;
            }
        }
        public static bool Access()
        {
            DateTime datetime = DateTime.Now;
            if (datetime.Hour >= 10 && datetime.Hour <= 19)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
