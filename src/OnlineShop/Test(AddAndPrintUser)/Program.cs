using DAL;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_AddAndPrintUser_
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var db = new AppContext())
            //{
            //    Console.Write("Enter a name for a new User: ");
            //    var name = Console.ReadLine();

            //    var user = new User { Name = name };
            //    db.Users.Add(user);
            //    db.SaveChanges();

            //    var query = from b in db.Users
            //                orderby b.Name
            //                select b;

            //    Console.WriteLine("All users in the database:");
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine(item.Name);
            //    }

            //    Console.WriteLine("Press any key to exit...");
            //    Console.ReadKey();
            //}
        }
    }
}
