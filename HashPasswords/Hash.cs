using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace HashPassword
{
    /// <summary>
    /// Класс для хеширования паролей с использованием SHA-256
    /// </summary>
    public class Hash
    {
        
        /// <param name="password">Исходный пароль в виде строки</param>
        /// <returns>Хешированный пароль в виде строки</returns>
        public static string Pass(string password)
        {
            // Создание объекта для вычисления хеша SHA-256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Преобразование пароля в байтовый массив
                byte[] sourceBytePassw = Encoding.UTF8.GetBytes(password);

                // Вычисление хеша для байтового массива пароля
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(sourceBytePassw);

                // Преобразование хеша в строку и удаление разделителей "-"
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", String.Empty);

                // Возвращение хешированного пароля
                return hashPassw;
            }
        }
    }
}
    