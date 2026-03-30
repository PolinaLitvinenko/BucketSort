using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort
{
    internal class Test
    {
        private BucketSort sorter = new BucketSort();
        private Generator gen = new Generator();

        public class Result
        {
            public int Size;
            public long Iterations;
            public long Comparisons;
            public double TimeMs;
            public bool Ok;
        }

        public List<Result> RunAll(string dir = "TestData")
        {
            List<Result> results = new List<Result>();

            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Папка TestData не найдена!");
                return results;
            }

            string[] files = Directory.GetFiles(dir, "*.txt");

            for (int f = 0; f < files.Length; f++)
            {
                string fileName = Path.GetFileNameWithoutExtension(files[f]);
                string sizeStr = fileName.Replace("data_", "");
                int size;
                if (!int.TryParse(sizeStr, out size))
                {
                    continue;
                }
                double[] data = gen.Load(files[f]);
                double[] copy = new double[data.Length];
                for (int i = 0; i < data.Length; i++) copy[i] = data[i];

                double[] sorted = sorter.Sort(copy);

                bool isSorted = true;
                for (int i = 1; i < sorted.Length; i++)
                {
                    if (sorted[i] < sorted[i - 1]) { isSorted = false; break; }
                }

                Result r = new Result();
                r.Size = size;
                r.Iterations = sorter.Iterations;
                r.Comparisons = sorter.Comparisons;
                r.TimeMs = sorter.TimeMs;
                r.Ok = isSorted;
                results.Add(r);
            }

            return results;
        }

        public void SaveCsv(List<Result> results, string file = "results.csv")
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine("Size,Iterations,Comparisons,TimeMs,Ok");
                for (int i = 0; i < results.Count; i++)
                {
                    writer.WriteLine($"{results[i].Size},{results[i].Iterations},{results[i].Comparisons},{results[i].TimeMs:F4},{results[i].Ok}");
                }
            }
            Console.WriteLine($"Результаты сохранены в файл: {file}");
        }

        public void PrintStats(List<Result> results)
        {
            Console.WriteLine("Результаты:");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Размер | Итерации | Сравнения | Время(мс)");
            Console.WriteLine("--------------------------------------------");

            var ordered = results.OrderBy(r => r.Size).ToList();
            for (int i = 0; i < ordered.Count; i++)
            {
                Result r = ordered[i];
                Console.WriteLine($"{r.Size,6} | {r.Iterations,9:N0} | {r.Comparisons,9:N0} | {r.TimeMs,8:F2}");
            }

            Console.WriteLine("--------------------------------------------");

            int successCount = results.Count(r => r.Ok);
            double avgTime = results.Average(r => r.TimeMs);
            long avgIterations = (long)results.Average(r => r.Iterations);

            Console.WriteLine($"Всего тестов: {results.Count}");
            Console.WriteLine($"Успешно: {successCount}");
            Console.WriteLine($"Среднее время: {avgTime:F2} мс");
            Console.WriteLine($"Среднее итераций: {avgIterations:N0}");
        }


    }
}
