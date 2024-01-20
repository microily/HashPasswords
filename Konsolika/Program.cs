using Konsolika.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashPassword;

namespace Konsolika
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = Helper.GetContext();
            Helper helper = new Helper();
            Console.WriteLine("Surname: "); string sname = Console.ReadLine();
            Console.WriteLine("Name: "); string name = Console.ReadLine();
            Console.WriteLine("Otchestvo: "); string otchestvo = Console.ReadLine();
            Console.WriteLine("login: "); string login = Console.ReadLine();
            Console.WriteLine("password: "); string password = Console.ReadLine();
            var user = new User();
            var worker = new Workers();
            user.login = login;
            user.password = Hash.Pass(password);
            helper.CreateUsers(user);
            worker.Name = name;
            worker.Surname = sname;
            worker.id_user = user.id_user;
            worker.Otchestvo = otchestvo;
            helper.CreateWorkers(worker);
            Console.WriteLine($"{worker.Name} {worker.Surname} - {user.login} {user.password}");
            Console.ReadLine();
        }
    }
}
