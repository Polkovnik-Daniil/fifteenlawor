using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp1
{
    class tHread
    {
        public int Fourth_Task_N;
        public void First_Task()
        {
            Process[] proc = Process.GetProcesses();
            int j = 0;
            foreach (var il in proc)
            {
                Console.WriteLine($"ID:\t{il.Id}");
                Console.WriteLine($"\nProcessName:\t{il.ProcessName}");
                Console.WriteLine($"\nBasePriority:\t\t{il.BasePriority}");
                Console.WriteLine($"\nHandleCount:\t{il.HandleCount}");
                Console.WriteLine($"\nResponding:\t {il.Responding}");
                j++;
                if (j == 10)
                    break;
            }
        }
        public void Second_Task()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Name: {domain.FriendlyName}");
            Console.WriteLine($"Base Directory: {domain.BaseDirectory}");
            Console.WriteLine($"Setup Information: {domain.SetupInformation}");

            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine("\t" + asm.GetName().Name);

            // Get and display the friendly name of the default AppDomain.
            string callingDomainName = Thread.GetDomain().FriendlyName;
            Console.WriteLine(callingDomainName);

            // Get and display the full name of the EXE assembly.
            string exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine(exeAssembly);

            MakeNewDomain();
        }

        static void Fourth_Task_First_Thread()
        {
            tHread th = new tHread();
            for (int i = 0; i < th.Fourth_Task_N; i += 2)
            {
                Thread.Sleep(10);
                Console.WriteLine(i);
            }
        }
        public void MakeNewDomain()
        {
            // Создадим новый домен приложения
            AppDomain newD = AppDomain.CreateDomain("ProfessorWebAppDomain");
            InfoDomainApp(newD);
            // Уничтожение домена приложения
            AppDomain.Unload(newD);
        }

        public void InfoDomainApp(AppDomain defaultD)
        {
            Console.WriteLine("*** Информация о домене приложения ***\n");
            Console.WriteLine("-> Имя: {0}\n-> ID: {1}\n-> По умолчанию? {2}\n-> Путь: {3}\n",
                defaultD.FriendlyName, defaultD.Id, defaultD.IsDefaultAppDomain(), defaultD.BaseDirectory);

            Console.WriteLine("Загружаемые сборки: \n");
            // Извлекаем информацию о загружаемых сборках с помощью LINQ-запроса
            var infAsm = from asm in defaultD.GetAssemblies()
                         orderby asm.GetName().Name
                         select asm;
            foreach (var a in infAsm)
                Console.WriteLine("-> Имя: \t{0}\n-> Версия: \t{1}", a.GetName().Name, a.GetName().Version);
        }
        static void Fourth_Task_Second_Thread()
        {
            tHread th = new tHread();
            for (int i = 1; i < th.Fourth_Task_N; i += 2)
            {
                Thread.Sleep(11);
                Console.WriteLine(i);
            }
        }
        public static void Count(object obj)
        {
            int x = (int)obj;
            for(int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            tHread th = new tHread();
            Thread th_one = new Thread(th.First_Task);
            Thread th_two = new Thread(th.Second_Task);
            th_one.Start();
            Thread.Sleep(200);
            Console.Clear();
            th_two.Start();
            th_two.Join();
            th_one.Join();
            int x = 0;
            TimerCallback tm = new TimerCallback(tHread.Count);
            //Timer timer = new Timer(tm, x, 2000);
        }
    }
}
