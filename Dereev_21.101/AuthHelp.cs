using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Dereev_21._101.Models;

namespace HashPassword
{
    public class AuthHelp
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
    }
}
