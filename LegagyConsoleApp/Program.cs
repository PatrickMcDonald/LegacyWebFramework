using System;
using System.Threading;
using System.Threading.Tasks;

namespace LegagyConsoleApp
{
    internal class Program
    {
        private static readonly AsyncLocal<Guid> AsyncLocal = new AsyncLocal<Guid>();

        static void Main(string[] args)
        {
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
            AsyncLocal.Value = Guid.NewGuid();
        }

        private static void WriteDebug(string title)
        {
            Console.WriteLine("{0,-9} {1} {2}", title, Thread.CurrentThread.ManagedThreadId, AsyncLocal.Value);
        }
    }
}
