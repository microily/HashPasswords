using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Konsolika.Models;

namespace Konsolika
{
    /// <summary>
    /// Класс Helper предоставляет методы для работы с базой данных AtelieEntities.
    /// </summary>
    public class Helper
    {
        // Статическое поле для хранения единственного экземпляра базы данных.
        private static AtelieEntities s_firstDBEntities;

        /// <summary>
        /// Получает контекст базы данных AtelieEntities. Создает его, если он еще не создан.
        /// </summary>
        /// <returns>Экземпляр AtelieEntities</returns>
        public static AtelieEntities GetContext()
        {
            if (s_firstDBEntities == null)
            {
                s_firstDBEntities = new AtelieEntities();
            }
            return s_firstDBEntities;
        }

        /// <summary>
        /// Создает новую запись о пользователе в базе данных.
        /// </summary>
        /// <param name="user">Объект User для добавления в базу данных</param>
        public void CreateUsers(User user)
        {
            s_firstDBEntities.User.Add(user);
            s_firstDBEntities.SaveChanges();
        }

        /// <summary>
        /// Создает новую запись о работнике в базе данных.
        /// </summary>
        /// <param name="worker">Объект Workers для добавления в базу данных</param>
        public void CreateWorkers(Workers worker)
        {
            s_firstDBEntities.Workers.Add(worker);
            s_firstDBEntities.SaveChanges();
        }

        /// <summary>
        /// Выводит информацию о пользователях в консоль.
        /// </summary>
        public void PrintU()    
        {
            var users = s_firstDBEntities.User.ToList();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.id_user}, Login: {user.login}, Password: {user.password}");
            }
        }

        /// <summary>
        /// Обновляет запись о пользователе в базе данных.
        /// </summary>
        /// <param name="user">Объект User с обновленными данными</param>
        public void UpdateUsers(User user)
        {
            s_firstDBEntities.Entry(user).State = EntityState.Modified;
            s_firstDBEntities.SaveChanges();
        }

        /// <summary>
        /// Фильтрует и выводит работников, имена которых начинаются с букв "А" или "М".
        /// </summary>
        public void FiltrWorkers()
        {
            var workers = s_firstDBEntities.Workers.Where(x => x.Name.StartsWith("А") || x.Name.StartsWith("М"));
        }

        /// <summary>
        /// Сортирует работников по имени в алфавитном порядке.
        /// </summary>
        public void SortWorkers()
        {
            var workers = s_firstDBEntities.Workers.OrderBy(x => x.Name);
        }
    }
}
