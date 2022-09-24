using System;
using System.Threading;
using System.Collections.Generic;

namespace MartinezBraulioD01Actividad12
{
    class Program
    {
        static Fork _f;
        static Philosopher _p;
        static Semaphore _handler;
        static List<int> _eatingCountReached;

        static void DrawLine(int left, int top, ConsoleColor c,  string text)
        {
            Console.ForegroundColor = c;
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        static void CriticalSection(InnerPhilosopher p)
        {
            int left = p.PhilosopherID;
            int right = (p.PhilosopherID + 1) % 5;
            while (true)
            {
                if (p.EatingCount <= 6)
                {
                    _handler.WaitOne();
                    _f[left].Semaphore.WaitOne();
                    _f[right].Semaphore.WaitOne();
                    DrawLine(0, left, ConsoleColor.White, string.Format("{0} esta comiendo por {1} vez con {2} y {3}", 
                        left, p.EatingCount, left, right));
                    p.EatingCount++;
                    Thread.Sleep(500);
                    _f[left].Semaphore.Release();
                    _f[right].Semaphore.Release();
                    _handler.Release();
                }
                else
                {
                    if (!_eatingCountReached.Contains(left))
                    {
                        _eatingCountReached.Add(left);
                        DrawLine(40, left, ConsoleColor.Red, "[TERMINO]");
                        break;
                    }
                }
            }
            if (_eatingCountReached.Count == 5)
            {
                DrawLine(0, 6, ConsoleColor.White, "Los filosofos terminaron de comer");
                Environment.Exit(0);
            }
        }

        static void Main(string[] args)
        {
            _f = new();
            _p = new();
            _handler = new(1, 4);
            _eatingCountReached = new(5);
            Thread t0 = new(() => CriticalSection(_p[0]));
            Thread t1 = new(() => CriticalSection(_p[1]));
            Thread t2 = new(() => CriticalSection(_p[2]));
            Thread t3 = new(() => CriticalSection(_p[3]));
            Thread t4 = new(() => CriticalSection(_p[4]));
            t0.IsBackground = true;
            t1.IsBackground = true;
            t2.IsBackground = true;
            t3.IsBackground = true;
            t4.IsBackground = true;
            t0.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            Console.Read();
        }
    }
}
