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
    public class Helper
    {

        private static AtelieEntities s_firstDBEntities;

        public static AtelieEntities GetContext()
        {
            if (s_firstDBEntities == null)
            {
                s_firstDBEntities = new AtelieEntities();
            }
            return s_firstDBEntities;
        }
        public void CreateUsers(User user)
        {
            s_firstDBEntities.User.Add(user);
            s_firstDBEntities.SaveChanges();
        }
        public void CreateWorkers(Workers worker)
        {
            s_firstDBEntities.Workers.Add(worker);
            s_firstDBEntities.SaveChanges();
        }
        public void PrintU()
        {
            var users = s_firstDBEntities.User.ToList();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.id_user}, Login: {user.login}, Password: {user.password}");
            }
        }
        public void UpdateUsers(User user)
        {
            s_firstDBEntities.Entry(user).State = EntityState.Modified;
            s_firstDBEntities.SaveChanges();
        }
        public void FiltrWorkers()
        {
            var workers = s_firstDBEntities.Workers.Where(x => x.Name.StartsWith("А") || x.Name.StartsWith("М"));
        }
        public void SortWorkers()
        {
            var workers = s_firstDBEntities.Workers.OrderBy(x => x.Name);
        }
    }
}
