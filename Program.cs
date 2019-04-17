using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static Semaphore _pool;

        private static int _padding;

        public static void Main()
        {
        _pool = new Semaphore(0, 3);

            Thread ta = new Thread(new ParameterizedThreadStart(Worker));
            Thread tb = new Thread(new ParameterizedThreadStart(Worker));
            Thread tc = new Thread(new ParameterizedThreadStart(Worker));
            Thread td = new Thread(new ParameterizedThreadStart(Worker));
            Thread te = new Thread(new ParameterizedThreadStart(Worker));

            ta.Start("A");
            tb.Start("B");
            tc.Start("C");
            td.Start("D");
            te.Start("E");

            Thread.Sleep(500);

            Console.WriteLine("Total de permissoes: 3. Numero de Threads: 5");
            _pool.Release(3);

            Console.Read();
        }

        private static void Worker(object num)
        {
            Console.WriteLine("{0} : Verificando semaforos...", num);
            _pool.WaitOne();

            Console.WriteLine("{0} : usando uma permissao.", num);

            for (int i = 0; i <= 5; i++)
            {
                Console.WriteLine("{0} : executando {1}", num, i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Thread {0} : libera o semáforo.", num);
            Console.WriteLine("Thread {0} : contagem de semáforo anterior: {1}",
                num, _pool.Release() + 1);
        }
    }
}
