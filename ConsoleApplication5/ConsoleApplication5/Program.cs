using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;


namespace ConsoleApplication5
{

    class Program
    {
       static System.Timers.Timer timer;
        const double interval60Mins = 60 * 60 * 1000;//1 hour
        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Write("Completion of hour");
            Console.WriteLine("### Timer Stopped ### \n");
            Console.WriteLine("### Scheduled Task Started ### \n\n");
            Console.WriteLine("Hello World!!! - Performing scheduled task\n");
            Console.WriteLine("### Task Finished ### \n\n");
            schedule_Timer();
            checker c = new checker();
            c.programchecker();

        }

        static void schedule_Timer()
        {
            Console.WriteLine("### Timer Started ###");

            DateTime nowTime = DateTime.Now;
            DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 21, 02, 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
            if (nowTime > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
            timer = new System.Timers.Timer(tickTime);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Watchman started running  "+ DateTime.Now.ToString());
            Worker worker = new Worker();
            Thread t = new Thread(worker.DoWork);
            System.Timers.Timer checkForTime = new System.Timers.Timer(interval60Mins);
            
            checkForTime.Enabled = true;
            t.IsBackground = true;
            t.Start();
            schedule_Timer();
           

            while (true)
            {
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.C && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    worker.KeepGoing = false;
                    break;
                }
                
                
            }
            t.Join();
        }
    }

    class checker
    {
        public void programchecker()
        {
            var procs = Process.GetProcesses();
            foreach (var proc in procs)
            {
                TimeSpan runtime;
                try
                {
                    runtime = DateTime.Now - proc.StartTime;
                }
                catch (System.ComponentModel.Win32Exception ex)
                {
                    // Ignore processes that give "access denied" error.
                    if (ex.NativeErrorCode == 5)
                        continue;
                    throw;
                }

                Console.WriteLine("{0}  {1}", proc, runtime);
            }

        }

        public void timeChecker()
        {

        }
    }

    class Worker
    {
        public bool KeepGoing { get; set; }

        public Worker()
        {
            KeepGoing = true;
        }
        public void processchecker()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {
                Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
            }
        }


        public void DoWork()
        {
            
            while (KeepGoing);
            {
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.C && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    Thread.Sleep(200);
                    
                }
            }
            
        }
    }
}
