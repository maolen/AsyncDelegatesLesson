using System;
using System.Threading;

namespace AsyncDelegateLesson
{
    class Program
    {
        /*
         * Все процессы делятся на два типа:
         * 1) I/O Bound не (потребляют мощности)привязаны к самому процессору, а к устройствам ввода-вывода
         *  (файлы, база данных, сеть) НЕ нужно для неё создавать поток, так как не потребляет мощности процессора
         * 2) Processes Bound вычисление факториала, то что происходит внутри процесса
        */
        //delegate int MyAction(); 
        private static Func<int, int> action;
        static void Main(string[] args)
        {
            //var action = new MyAction(CalculateSophisticNumber);
            action = new Func<int, int>(CalculateSophisticNumber);
            //action();
            var result = action.BeginInvoke(200, ProcessResult, null);

            //while(!result.IsCompleted)
            //{
            //    Console.WriteLine("Идёт работа, ждём...");
            //    Thread.Sleep(500);
            //}
            //Console.WriteLine(result.IsCompleted);
            
            Console.WriteLine("Главный поток завершил работу");
            Console.ReadLine();
        }
        private static void ProcessResult(IAsyncResult result)
        {
            Console.WriteLine(action.EndInvoke(result));
        }

        private static int CalculateSophisticNumber(int number)
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - выполняет работу");
            Thread.Sleep(5000);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - закончил работу метода");
            return 20000;
        }
    }
}
