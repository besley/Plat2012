using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Extensions;

namespace ProductSysConsole
{
    public class Mono
    {
        private static int i = 0;
        public Mono()
        {
            Console.WriteLine(string.Format("Mono Count:{0}", i++));
        }
    }

    public interface IUser
    {
        string UserName { get; set; }
    }

    public class User : IUser
    {
        public string UserName { get; set; }
    }

    public interface ISqlUserRepository
    {
        object Get(string name);
    }

    public class SqlUserRepository : ISqlUserRepository
    {
        public object Get(string userName)
        {
            return new User { UserName = "Summy" };
        }
    }

    public interface IUserService
    {
        string UserName { get; set; }
        User GetUser(string userName);
    }

    public class UserService : IUserService
    {
        private static int i = 0;
        public string UserName
        {
            get;
            set;
        }

        public  User GetUser(string userName)
        {
            return (User)(new SqlUserRepository()).Get(userName);
        }

        public UserService()
        {
            Console.WriteLine(string.Format("User Service Count:{0}", i++));
        }
    }


    public class InjectionStartup
    {
        public static Container gContainer { get; private set; }
        public static void Startup()
        {
            var container = new Container();
            container.Register<IUser>(() => new User());
            //container.Register<IUserService>(delegate { return new UserService(); });
            //container.Register<IUserService>( () => new UserService());
            container.Register<IUserService, UserService>();
            container.RegisterSingle<ISqlUserRepository, SqlUserRepository>();
            container.RegisterInitializer<IUser>( User => User.UserName = "Yammy");
            container.RegisterSingle<Mono>();
            
            container.Verify();
            gContainer = container;
        }
    }

    public class ConsoleUnit
    {
        private readonly IUserService userService;
        private readonly ISqlUserRepository userRepository;
        private readonly IUser user;
        private readonly Mono mono;

        public ConsoleUnit()
        {
            InjectionStartup.Startup();
            this.userRepository = InjectionStartup.gContainer.GetInstance<ISqlUserRepository>();
            this.userService = InjectionStartup.gContainer.GetInstance<IUserService>();
            this.user = InjectionStartup.gContainer.GetInstance<IUser>();
            this.mono = InjectionStartup.gContainer.GetInstance<Mono>();
        }

        public void PrintUserInfo()
        {
            Console.WriteLine(string.Format("injection user:{0}", user.UserName));

            var user1 = userService.GetUser("name");
            Console.WriteLine(string.Format("repository user:{0}", user1.UserName));

            Console.WriteLine(mono.ToString());

        }
    }

    public class InjectionTest
    {
        public static void RunTest()
        {
            (new ConsoleUnit()).PrintUserInfo();
        }
    }
}
