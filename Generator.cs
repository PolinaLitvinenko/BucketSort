using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BucketSort.Test;

namespace BucketSort
{
    internal class Generator
    {
        private Random _rnd = new Random();

        public double[] Generate(int size, double min = 0, double max = 1000)
        {
            double[] arr = new double[size];

            for (int i = 0; i < size; i++)
            {
                arr[i] = min + _rnd.NextDouble() * (max - min);
                arr[i] = Math.Round(arr[i], 2);
            }

            return arr;
        }

        public void Save(double[] array, string filename)
        {
            string directory = Path.GetDirectoryName(filename);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    writer.WriteLine(array[i].ToString("F2"));
                }
            }
        }

        public double[] Load(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                int size = int.Parse(reader.ReadLine());
                double[] array = new double[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = double.Parse(reader.ReadLine());
                }
                return array;
            }
        }

        public void GenerateAll(string dir = "TestData")
        {
            int[] sizes = new int[50];
            for (int i = 0; i < 50; i++)
            {
                sizes[i] = 100 + i * 202;  
            }
            sizes[49] = 10000;
            for (int i = 0; i < sizes.Length; i++)
            {
                Save(Generate(sizes[i]), $"{dir}/data_{sizes[i]}.txt");
            }
            Console.WriteLine($"Всего создано: {sizes.Length} файлов в папке {dir}/");
        }

    }
}

