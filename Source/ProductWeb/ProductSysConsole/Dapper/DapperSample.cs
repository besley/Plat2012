using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Plat.DataRepository;
using Plat.WebUtility;

namespace ProductSysConsole
{
   // public class EPRole
   // {
   //     public int Id { get; set; }
   //     public string Name { get; set; }
   // }

   // public class Role
   // {
   //     public int Id { get; set; }
   //     public string Name { get; set; }
   //     public string Notes { get; set; }
   // }

   //public class DapperSample
   // {
   //    private static string connectionString = "Data Source=127.0.0.1\\MSSQL2012;Initial Catalog=PLUGINDB;Integrated Security=False;User ID=sa;Password=123456;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
   //    public static void Test()
   //    {
   //        InsertTest();
   //        QuerySingleTest();
           
   //    }

   //    private static void InsertTest()
   //    {
   //        IDatabase database = new Database(new SqlConnection(connectionString));
   //        using (var session = new Session(database))
   //        {
   //            IRepository repository = new DapperRepository(session);
   //            //repository.Execute(@"insert EPRole(Name) values(@name)", new { name = "RoleLi" }).Equals(1);
   //            repository.Insert<EPRole>(new EPRole { Name = "RoleZhang" });
   //        }
   //    }

   //    private static void QuerySingleTest()
   //    {
   //        IDatabase database = new Database(new SqlConnection(connectionString));
   //        ISession session = new Session(database);
   //        IRepository repository = new DapperRepository(session);

   //        var role = repository.GetById<EPRole>(1);
   //        var entity = AutoMapperHelper<EPRole, Role>.AutoConvert(role);
   //        Console.WriteLine(entity.Name);
   //    }

   //    private static void QueryListTest()
   //    {
   //        IDatabase database = new Database(new SqlConnection(connectionString));
   //        ISession session = new Session(database);
   //        IRepository repository = new DapperRepository(session);

   //        IEnumerable<Role> roles = repository.Get<Role>(@"select * from eprole where id=@id", new { id = 1 });
   //        foreach (var role in roles)
   //            Console.WriteLine(role.Name);
   //    }
   // }
}
