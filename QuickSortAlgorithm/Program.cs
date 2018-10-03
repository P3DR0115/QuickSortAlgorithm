using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSortAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<int, double>> dataSet = new List<Tuple<int, double>>();

            try
            {
                loadData(dataSet);
            }
            catch(Exception e)
            {
                setUpList(dataSet);
            }

            DateTime begin = DateTime.Now;

            Quick_Sort(dataSet, 0, dataSet.Count-1);
            Console.WriteLine("Done Sorting");

            DateTime end = DateTime.Now;

            TimeSpan timeSpan = end - begin;
            Console.WriteLine(timeSpan);

            saveToFile(dataSet);
            Console.ReadLine();

            for (int i = 0; i < dataSet.Count; i++)
            {
                Console.WriteLine(dataSet[i].Item1 + ", " + dataSet[i].Item2);
            }

            Console.ReadLine();

        }

        static List<Tuple<int, double>> setUpList(List<Tuple<int, double>> dataSet)
        {
            Random rnd = new Random();

            for (int i = 0; i < 1000000; i++)
            {                                
                dataSet.Add(new Tuple<int, double>(i, rnd.NextDouble()));
            }
            Console.WriteLine("Done Creating the 1000000 numbers");

            saveToFile(dataSet);

            return dataSet;
        }

        static void saveToFile(List<Tuple<int, double>> dataSet)
        {
            string tempString = "";

            string[] dataTemp = new string[1000000];
            
            for (int i = 0; i < dataSet.Count; i++)
            {
                tempString = Convert.ToString(dataSet[i].Item1);
                tempString += ", ";
                tempString += Convert.ToString(dataSet[i].Item2);
                dataTemp[i] = tempString;
            }

            System.IO.File.WriteAllLines(@"C:\Users\P3dro\source\repos\Algorithms\QuickSortAlgorithm\dataSorted.csv", dataTemp);
            Console.WriteLine("Done Saving to File (Merge Sorted)");
        }

        static List<Tuple<int, double>> loadData(List<Tuple<int, double>> dataSet)
        {
            string[] dataTemp = new string[1000000];
            string[] temp;

            dataTemp = System.IO.File.ReadAllLines(@"C:\Users\P3dro\source\repos\Algorithms\QuickSortAlgorithm\data.csv");

            for(int i = 0; i < dataTemp.Length; i++)
            {
                temp = dataTemp[i].Split(',');

                dataSet.Add(new Tuple<int, double>(Convert.ToInt32(temp[0]), Convert.ToDouble(temp[1])));
            }
            Console.WriteLine("Done Loading Data!");

            return dataSet;
        }
        
        private static void Quick_Sort(List<Tuple<int, double>> arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort(arr, pivot + 1, right);
                }
            }

        }

        private static int Partition(List<Tuple<int, double>> arr, int left, int right)
        {
            double pivot = arr[left].Item2;
            bool done = false;

            while (!done)
            {

                while (arr[left].Item2 < pivot)
                {
                    left++;
                }

                while (arr[right].Item2 > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left].Item2 == arr[right].Item2) return right;

                    Tuple<int, double> temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    done = true;
                }
            }

            return right;
        }
        
    }

}

