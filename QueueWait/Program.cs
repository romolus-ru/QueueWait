﻿using System;
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
            var thPush3 = new Thread(SetPush3);
            thPush3.Start();
            var thPush4 = new Thread(SetPush4);
            thPush4.Start();
            var thPop1 = new Thread(GetPop1);
            thPop1.Start();
            var thPop2 = new Thread(GetPop2);
            thPop2.Start();
            Console.WriteLine("started");
        }

        static private void GetPop1()
        {
            while (true)
            {
                var a = _qw.pop();
                Console.WriteLine("pop1  "+a.ToString());
            }
        }

        static private void GetPop2()
        {
            while (true)
            {
                var a = _qw.pop();
                Console.WriteLine("pop2  " + a.ToString());
            }
        }

        static private void SetPush1()
        {
            while (true)
            {
                Console.WriteLine("push10");
                _qw.push(10);
                Thread.Sleep(120);
            }
        }

        static private void SetPush2()
        {
            while (true)
            {
                Console.WriteLine("push9");
                _qw.push(9);
                Thread.Sleep(110);
            }
        }
        static private void SetPush3()
        {
            while (true)
            {
                Console.WriteLine("push3");
                _qw.push(3);
                Thread.Sleep(150);
            }
        }
        static private void SetPush4()
        {
            while (true)
            {
                Console.WriteLine("push4");
                _qw.push(4);
                Thread.Sleep(150);
            }
        }

    }
}
