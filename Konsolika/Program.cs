using Konsolika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashPassword;

namespace Konsolika
{
    class Program
    {
        static void Main(string[] args)
        {
            // Получение экземпляра базы данных и создание объекта Helper для работы с ней
            var db = Helper.GetContext();
            Helper helper = new Helper();

            // Запрос данных о пользователе и работнике с консоли
            Console.WriteLine("Surname: "); string sname = Console.ReadLine();
            Console.WriteLine("Name: "); string name = Console.ReadLine();
            Console.WriteLine("Otchestvo: "); string otchestvo = Console.ReadLine();
            Console.WriteLine("login: "); string login = Console.ReadLine();
            Console.WriteLine("password: "); string password = Console.ReadLine();

            // Создание нового пользователя и хеширование пароля
            var user = new User();
            var worker = new Workers();
            user.login = login;
            user.password = Hash.Pass(password);

            // Добавление пользователя в базу данных
            helper.CreateUsers(user);

            // Заполнение данных о работнике и добавление его в базу данных
            worker.Name = name;
            worker.Surname = sname;
            worker.id_user = user.id_user;
            worker.Otchestvo = otchestvo;
            helper.CreateWorkers(worker);

            // Вывод информации о работнике и пользователе в консоль
            Console.WriteLine($"{worker.Name} {worker.Surname} - {user.login} {user.password}");

            // Ожидание ввода пользователя перед завершением программы
            Console.ReadLine();
        }
    }
}
