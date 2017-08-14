using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueWait
{
    class Program
    {

        static QueueWait<int> _qw = new QueueWait<int>();

        static void Main(string[] args)
        {
            var thPush1 = new Thread(SetPush1);
            thPush1.Start();
            var thPush2 = new Thread(SetPush2);
            thPush2.Start();
            var thPop = new Thread(GetPop);
            thPop.Start();
            Console.WriteLine("started");
        }

        static private void GetPop()
        {
            while (true)
            {
                var a = _qw.pop();
                Console.WriteLine("получено значение "+a.ToString());
            }
        }

        static private void SetPush1()
        {
            while (true)
            {
                Console.WriteLine("push10");
                _qw.push(10);
                Thread.Sleep(750);
            }
        }

        static private void SetPush2()
        {
            while (true)
            {
                Console.WriteLine("push9");
                _qw.push(9);
                Thread.Sleep(500);
            }
        }

    }
}
