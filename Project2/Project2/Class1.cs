using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp1
{
    class tHread
    {
        public int Fourth_Task_N;
        public Thread th;
        public int I;
        public void Fourth_Task_Second_Thread_A()
        {
            string writePath = @"D:\Даник\Учеба\3 семестр\ООП\Лабораторные работы\15\Project2\Project2\TextFile1.txt";
            try
            {
                Thread.Sleep(100);
                Console.Clear();
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    tHread th = new tHread();
                    for (int i = 1; i < th.Fourth_Task_N; i += 2)
                    {
                        if (i <= 5)
                            Thread.Sleep(10);
                        Console.WriteLine(i);
                        sw.WriteLine(i);
                    }
                    Console.WriteLine("Запись выполнена");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Fourth_Task_Second_Thread_B()
        {
            string writePath = @"D:\Даник\Учеба\3 семестр\ООП\Лабораторные работы\15\Project2\Project2\TextFile1.txt";
            try
            {
                Thread.Sleep(100);
                Console.Clear();
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    tHread th = new tHread();
                    for (int i = 0; i < th.Fourth_Task_N; i += 2)
                    {
                        if (i <= 5)
                            Thread.Sleep(11);
                        Console.WriteLine(i);
                        sw.WriteLine(i);
                    }
                    Console.WriteLine("Запись выполнена");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
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
            //получает домен
            AppDomain domain = AppDomain.CurrentDomain;
            Console.WriteLine($"Name: {domain.FriendlyName}");
            Console.WriteLine($"Base Directory: {domain.BaseDirectory}");
            Console.WriteLine($"Setup Information: {domain.SetupInformation}");
            //Assembly - Представляет сборку, которая является многократно используемым, версионным и самоописывающимся строительным блоком приложения
            //Получает сборки, которые были загружены в контекст выполнения этого домена приложения.
            Assembly[] assemblies = domain.GetAssemblies();
            foreach (Assembly asm in assemblies)
                Console.WriteLine("\t" + asm.GetName().Name);

            // Получить и отобразить понятное имя домена приложения по умолчанию.
            string callingDomainName = Thread.GetDomain().FriendlyName;
            Console.WriteLine(callingDomainName);

            // Получить и отобразить полное имя сборки EXE.
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
            Console.WriteLine(" Информация о домене приложения \n");
            Console.WriteLine("Имя: {0}\n ID: {1}\n По умолчанию? {2}\n Путь: {3}\n",
                defaultD.FriendlyName, defaultD.Id, defaultD.IsDefaultAppDomain(), defaultD.BaseDirectory);

            Console.WriteLine("Загружаемые сборки: \n");
            // Извлекаем информацию о загружаемых сборках с помощью LINQ-запроса
            var infAsm = from asm in defaultD.GetAssemblies()
                         orderby asm.GetName().Name
                         select asm;
            foreach (var a in infAsm)
                Console.WriteLine(" Имя: \t{0}\n Версия: \t{1}", a.GetName().Name, a.GetName().Version);
            Console.ReadKey();
            //Выгружает указанный домен приложения.
            AppDomain.Unload(defaultD);
        }

        public static void Count(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
            }
        }

    }
    class Program
    {
        static void Third_Task(object manualEvent)
        {
            Console.Write("Enter value(more than 0): ");
            int x = 10;
            
            if (x < 0)
            {
                Console.WriteLine("Error");
                return;
            }
            string writePath = @"D:\Даник\Учеба\3 семестр\ООП\Лабораторные работы\15\Project2\Project2\TextFile1.txt";
            try
            {
                Thread.Sleep(100);
                Console.Clear();
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    for (int i = 0; i <= x; i++)
                    {
                        if (i == 5)
                        {
                            //Представляет событие синхронизации потока, которое при сигнале должно быть сброшено вручную.
                            ManualResetEvent ev = (ManualResetEvent)manualEvent;
                            while (ev.WaitOne())
                            {
                                Console.WriteLine("work");
                                Thread.Sleep(10);
                                Console.Clear();
                            }
                        }
                        Thread.Sleep(200);
                        Console.WriteLine("Задержка при записи");
                        sw.WriteLine(i);
                    }
                }

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Count(object obj)
        {
            int x = (int)obj;
            for (int i = 1; i < 9; i++, x++)
            {
                Console.WriteLine($"{x * i}");
                Thread.Sleep(100);
                Console.Clear();
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            tHread th = new tHread();
            Thread th_one = new Thread(th.First_Task);
            Thread th_two = new Thread(th.Second_Task);
            Thread th_three = new Thread(Third_Task);

            Thread th_four_A = new Thread(th.Fourth_Task_Second_Thread_A);
            Thread th_four_B = new Thread(th.Fourth_Task_Second_Thread_B);
            th_one.Start();
            th_two.Start();
            th_four_A.Start();
            th_four_B.Start();
            Thread.Sleep(200);
            Console.Clear();
            //приостанавливаю
            //th_three.Start();
            Thread thread = new Thread(new ParameterizedThreadStart(Third_Task));
            ManualResetEvent manualEvent = new ManualResetEvent(true);
            thread.Start(manualEvent);

            Thread.Sleep(2000);

            //pause thread
            Console.WriteLine("stop thread");
            manualEvent.Reset();

            Thread.Sleep(2000);

            //resume thread
            Console.WriteLine("start thread");
            manualEvent.Set();
            //pause thread
            manualEvent.Reset();

            Console.WriteLine("Pause");
            Console.WriteLine("IsAlive " + th_three.IsAlive);
            Console.WriteLine("IsBackground " + th_three.IsBackground);
            Console.WriteLine("IsThreadPoolThread " + th_three.IsThreadPoolThread);
            Console.WriteLine("Name " + th_three.Name);
            Console.WriteLine("Priority " + th_three.Priority);

            Thread.Sleep(2000);
            Console.WriteLine("Run");
            
            //возобновил работу потока
            th_two.Join();
            th_one.Join();
            int num = 0;
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Count);
            // создаем таймер
            Timer timer = new Timer(tm, num, 0, 2000);

            Thread.Sleep(2000);
        }
    }
}
