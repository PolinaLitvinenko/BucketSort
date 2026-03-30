using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BucketSort
{
    internal class BucketSort
    {
        public long Iterations { get; private set; }
        public long Comparisons { get; private set; }
        public double TimeMs { get; private set; }

        public double[] Sort(double[] array, int bucketSize = 10)
        {
            Iterations = 0;
            Comparisons = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();

            if (array == null || array.Length == 0)
            {
                stopwatch.Stop();
                TimeMs = stopwatch.Elapsed.TotalMilliseconds;
                return array ?? new double[0];
            }

            double[] workingArray = new double[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                Iterations++;
                workingArray[i] = array[i];
            }

            double minValue = workingArray[0];
            double maxValue = workingArray[0];

            for (int i = 1; i < workingArray.Length; i++)
            {
                Iterations++;
                Comparisons++;
                if (workingArray[i] < minValue)
                {
                    minValue = workingArray[i];
                }
                Comparisons++;
                if (workingArray[i] > maxValue)
                {
                    maxValue = workingArray[i];
                }
            }

            int bucketCount = (int)((maxValue - minValue) / bucketSize) + 1;
            Iterations++;

            List<double>[] buckets = new List<double>[bucketCount];
            for (int i = 0; i < bucketCount; i++)
            {
                Iterations++;
                buckets[i] = new List<double>();
            }

            for (int i = 0; i < workingArray.Length; i++)
            {
                Iterations++;
                double element = workingArray[i];
                int bucketIndex = (int)((element - minValue) / bucketSize);
                Comparisons++;
                if (bucketIndex >= bucketCount)
                {
                    bucketIndex = bucketCount - 1;
                }
                buckets[bucketIndex].Add(element);
            }

            for (int i = 0; i < buckets.Length; i++)
            {
                Iterations++;
                if (buckets[i].Count > 1)
                {
                    InsertionSort(buckets[i]);
                }
            }

            double[] result = new double[workingArray.Length];
            int resultIndex = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                Iterations++;
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    Iterations++;
                    result[resultIndex] = buckets[i][j];
                    resultIndex++;
                }
            }

            stopwatch.Stop();
            TimeMs = stopwatch.Elapsed.TotalMilliseconds;

            return result;
        }

        private void InsertionSort(List<double> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                Iterations++;
                double key = list[i];
                int j = i - 1;
                Comparisons++;
                while (j >= 0 && list[j] > key)
                {
                    Iterations++;
                    Comparisons++;
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
        }
    }
}
