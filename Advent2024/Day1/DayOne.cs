using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2024
{
    public class DayOne
    {
        public DayOne() { }

        private static async Task<int[][]> AcquireData(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 1, cookie);

            var lists = data.Split('\n')
                .ToList();

            var la = new List<Int32>();
            var lb = new List<Int32>();

            for (int i = 0; i < lists.Count - 1; i++)
            {
                var l = lists[i].Split("   ");
                la.Add(Int32.Parse(l[0]));
                lb.Add(Int32.Parse(l[1]));
            }

            var col_a = la.ToArray();
            var col_b = lb.ToArray();

            Array.Sort(col_a);
            Array.Sort(col_b);

            return new Int32[2][] { col_a, col_b };
        }

        public static async Task DayOnePartOne(string cookie)
        {
            var data = await AcquireData(cookie);

            // CALCOLO DISTANZA

            Console.WriteLine(IsThisShitSorted(data[0]));
            Console.WriteLine(IsThisShitSorted(data[1]));


            int d = 0;

            for (int i = 0; i < data[0].Count(); i++)
            {
                d = d + Math.Abs(data[1][i] - data[0][i]);
            }

            Console.WriteLine($"The distance is: {d}");

            return;
        }

        public static async Task DayOnePartTwo(string cookie)
        {
            // Acquire data
            var data = await AcquireData(cookie);

            // Count distinct occurencies
            var counted = CountTokens<Int32>(data[1]);

            // Evaluate similarity score
            int total = 0;
            foreach (var item in data[0])
            {
                if (counted.Keys.Contains(item))
                    total = total + (item * counted[item]);
            }

            // Print out result
            Console.WriteLine($"The result is {total}");
        }

        private static bool IsThisShitSorted(int[] arr)
        {
            int last = arr[0];
            for (int i = 1; i < arr.Length - 1; i++)
            {
                if (arr[i] < last )
                {
                    return false;
                }
            }
            return true;
        }

        private static Dictionary<T, int> CountTokens<T>(IEnumerable<T> arr) where T : notnull
        {

            var account = arr.Distinct().ToDictionary((key) => key, (val) =>
            {
                return 0;
            });

            foreach (var item in arr)
            {
                account[item]++;
            }

            return account;
        }
    }
}
