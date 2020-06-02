using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DBContext
    {
        public static int getRoleByUserName(string name)
        {
            using (var db = new Model1())
            {
                return db.Users.Where(x => x.Account==name).Select(x=>x.Role).FirstOrDefault();
            }
        }
    }
}