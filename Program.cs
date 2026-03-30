using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("1. Генерация данных");
                Console.WriteLine("2. Тесты производительности");
                Console.WriteLine("3. Выход");
                Console.Write("\nВыбор: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("--- Генерация данных ---");
                    Generator gen = new Generator();
                    gen.GenerateAll();
                }
                else if (choice == "2")
                {
                    Console.WriteLine("--- Тесты производительности ---");
                    Test tester = new Test();
                    List<Test.Result> results = tester.RunAll();

                    if (results.Count > 0)
                    {
                        tester.PrintStats(results);
                        tester.SaveCsv(results);
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Выход из программы");
                    return;
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }
    }
    
}
