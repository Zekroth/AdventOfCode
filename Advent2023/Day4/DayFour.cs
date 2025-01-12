using AdventOfCode.Day4;
using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2023.Day4
{
    public class DayFour
    {
        public DayFour() { }

        public static async Task DayFourPartOne(string cookie)
        {
            var input = AdventOfCode.Utils.DataImporter.GetData(2023, 4, cookie);

            var rows = new List<Row>();

            int count = 0;
            foreach (var item in input.Result.Split("\n"))
            {
                var r = new Row(item);
                var m = r.CountMatches();
                count += m == 0 ? 0 : 2 ^ m - 1;
            }

            Console.WriteLine($"The result is {count}");
        }
    }
}
