using System;
using System.Threading;

namespace MartinezBraulioD01Actividad11
{
    class Program
    {
        private readonly static char _product = '*';
        private readonly static char _consumed = ' ';
        static Container _container;
        static Semaphore _s;
        static Random _rand;

        private static void Producer()
        {
            while (true)
            {
                CriticalSection();
            }
        }

        private static void Consumer()
        {
            while (true)
            {
                CriticalSection();
            }
        }

        private static void DrawLine(int left, int top, ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        private static void DrawNumericLine(int left, int top, ConsoleColor color)
        {
            string s = "   0   1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21";
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.WriteLine(s);
        }

        private static void ClearLine(int left, int top)
        {
            string s = new(' ', Console.BufferWidth);
            Console.SetCursorPosition(left, top);
            Console.WriteLine(s);
        }

        private static void CriticalSection()
        {
            _s.WaitOne();
            if (Thread.CurrentThread.Name == "Productor")
            {
                int prod_indx, current;
                for (int i = 0; i < _rand.Next(23); i++)
                {
                    if (_container.IsFull()) 
                    {
                        DrawLine(0, 0, ConsoleColor.Red, "Contenedor lleno esperando al consumidor       ");
                        Thread.Sleep(200);
                        ClearLine(0, 0);
                        break;
                    }
                    current = _container.Add(_product, _consumed);
                    prod_indx = current == 0 ? 4 : current * 4 + 3;
                    DrawLine(0, 2, ConsoleColor.Green, _container.ToString());
                    DrawLine(0, 0, ConsoleColor.Green, string.Format("{0} esta trabajando " +
                        "en la posicion {1}   ", Thread.CurrentThread.Name, current));
                    ClearLine(0, 1);
                    DrawLine(prod_indx, 1, ConsoleColor.Green, "P");
                    Thread.Sleep(500 * _rand.Next(1, 5));
                }
            }
            else
            {
                int cons_indx;
                for (int i = 0; i < _rand.Next(23); i++)
                {
                    if (_container.IsEmpty())
                    {
                        DrawLine(0, 0, ConsoleColor.Red, "Contenedor vacio esperando al productor       ");
                        Thread.Sleep(200);
                        ClearLine(0, 0);
                        break;
                    }
                    _container.Del(_consumed, _product);
                    cons_indx = _container.Front == 0 ? 4 : _container.Front * 4 + 3;
                    DrawLine(0, 2, ConsoleColor.DarkYellow, _container.ToString());
                    DrawLine(0, 0, ConsoleColor.DarkYellow, string.Format("{0} esta retirando " +
                        "en la posicion {1}   ", Thread.CurrentThread.Name, _container.Front));
                    ClearLine(0, 3);
                    DrawLine(cons_indx, 3, ConsoleColor.DarkYellow, "C");
                    Thread.Sleep(500 * _rand.Next(1, 5));
                }
            }
            _s.Release();
        }

        static void Main(string[] args)
        {
            _container = new();
            _s = new(1, 1);
            _rand = new();
            DrawNumericLine(0, 4, ConsoleColor.White);
            Thread producer = new(Producer);
            producer.Name = "Productor";
            producer.IsBackground = true;
            Thread consumer = new(Consumer);
            consumer.Name = "Consumidor";
            consumer.IsBackground = true;
            producer.Start();
            consumer.Start();
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            Console.Read();
        }
    }
}
