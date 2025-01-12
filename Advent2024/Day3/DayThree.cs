using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2024
{
    public class DayThree
    {
        public static async Task DayThreePartOne(string cookie)
        {
            var data = await DataImporter.GetData(2024, 3, cookie);

            var muls = Regex.Matches(data, "mul\\((?<mul1>\\d+),(?<mul2>\\d+)\\)");

            var counter = 0;

            foreach (Match mul in muls)
            {
                counter = counter + (Int32.Parse(mul.Groups[1].Value) * Int32.Parse(mul.Groups[2].Value));
            }

            Console.WriteLine($"The result is {counter}");
        }

        public static async Task DayThreePartTwo(string cookie)
        {
            var data = "do()" + await DataImporter.GetData(2024, 3, cookie) + "don't()";

            var enabled = "";
            bool flag = false;
            for (var i = 0; i < data.Length - 6; i++)
            {
                if (data[i] == 'd' && data[i + 1] == 'o' && data[i + 2] == '(' && data[i + 3] == ')' )
                {
                    flag = true;
                }

                if (data[i] == 'd' && data[i + 1] == 'o' && data[i + 2] == 'n' && data[i + 3] == '\'' && data[i + 4] == 't' && data[i + 5] == '(' && data[i + 6] == ')')
                {
                    flag = false;
                }

                if (flag)
                {
                    enabled = enabled + data[i];
                }
            }

            var muls = Regex.Matches(enabled, "mul\\((?<mul1>\\d+),(?<mul2>\\d+)\\)");

            long counter = 0;

            foreach (Match mul in muls)
            {
                counter = counter + (Int64.Parse(mul.Groups[1].Value) * Int64.Parse(mul.Groups[2].Value));
            }

            Console.WriteLine($"The result is {counter}");
        }
    }
}
