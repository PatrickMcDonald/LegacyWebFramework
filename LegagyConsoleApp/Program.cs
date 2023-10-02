using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace LegagyConsoleApp
{
    internal class Program
    {
        private static readonly AsyncLocal<Guid> AsyncLocal = new AsyncLocal<Guid>();

        static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            StartActivity();

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            WriteDebug("MainAsync");

            await Task.Delay(100);

            WriteDebug("await");

            await Task.Run(() =>
            {
                StartActivity();
                WriteDebug("Task.Run1");
            });

            await Task.Run(() =>
            {
                WriteDebug("Task.Run2");
            });

            Parallel.For(0, 4, i =>
            {
                WriteDebug($"Parallel{i}");
            });
        }

        private static void StartActivity()
        {
            var activity = new Activity("Console activity");
            Activity.Current = activity.Start();

            AsyncLocal.Value = Guid.NewGuid();
        }

        private static void WriteDebug(string title)
        {
            Console.WriteLine("{0,-9} {1} {2} {3}", title, Thread.CurrentThread.ManagedThreadId, AsyncLocal.Value, Activity.Current.Id);
        }
    }
}
