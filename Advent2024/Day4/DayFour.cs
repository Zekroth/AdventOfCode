using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Advent2024
{
    public class DayFour
    {
        public DayFour() { }

        public static async Task DayFourPartOne(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 4, cookieValue: cookie);

            var l = data.Split('\n')[0].Count() + 1;
            // HORIZONTAL MATCHES COUNT
            int h = 0;
            for (int i = 0; i < data.Length - 3; i++)
            {
                if (data[i] == 'X' && data[i + 1] == 'M' && data[i + 2] == 'A' && data[i + 3] == 'S')
                {
                    h++;
                }
                if (data[i] == 'S' && data[i + 1] == 'A' && data[i + 2] == 'M' && data[i + 3] == 'X')
                {
                    h++;
                }
            }
            // VERTICAL MATCHES COUNT
            int v = 0;
            for (int i = 0; i < data.Length - 3 * l; i++)
            {
                if (data[i] == 'X' && data[i + l] == 'M' && data[i + (2 * l)] == 'A' && data[i + (3 * l)] == 'S')
                {
                    v++;
                }
                if (data[i] == 'S' && data[i + l] == 'A' && data[i + (2 * l)] == 'M' && data[i + (3 * l)] == 'X')
                {
                    v++;
                }
            }
            // DIAGONAL MATCHES COUNT
            int d = 0;
            string leftDia = ToDiagonalLeft(data);
            var ll = leftDia.Split('\n')[0].Count() + 1;
            for (int i = 0; i < leftDia.Length - 3 * ll; i++)
            {
                if (leftDia[i] == 'X' && leftDia[i + ll] == 'M' && leftDia[i + (2 * ll)] == 'A' && leftDia[i + (3 * ll)] == 'S')
                {
                    d++;
                }
                if (leftDia[i] == 'S' && leftDia[i + ll] == 'A' && leftDia[i + (2 * ll)] == 'M' && leftDia[i + (3 * ll)] == 'X')
                {
                    d++;
                }
            }
            string rightDia = ToDiagonalRight(data);
            for (int i = 0; i < rightDia.Length - 3 * ll; i++)
            {
                if (rightDia[i] == 'X' && rightDia[i + ll] == 'M' && rightDia[i + (2 * ll)] == 'A' && rightDia[i + (3 * ll)] == 'S')
                {
                    d++;
                }
                if (rightDia[i] == 'S' && rightDia[i + ll] == 'A' && rightDia[i + (2 * ll)] == 'M' && rightDia[i + (3 * ll)] == 'X')
                {
                    d++;
                }
            }


            Console.WriteLine($"Horizontal matches: {h}");
            Console.WriteLine($"Vertical matches: {v}");
            Console.WriteLine($"Diagonal matches: {d}");
            Console.WriteLine($"Total matches: {h + v + d}");
            return;
        }

        public static async Task DayFourPartTwo(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 4, cookieValue: cookie);

            // DIAGONAL MATCHES COUNT
            int d = 0;

            /*
            string leftDia = ToDiagonalLeft(data);
            string rightDia = ToDiagonalRight(data);
            var l = data.Split('\n')[0].Count() + 1;
            var ll = leftDia.Split('\n')[0].Count() + 1;

            for (int i = 0; i < leftDia.Length - 2 * ll; i++)
            {
                if (leftDia[i + ll] == 'M' && leftDia[i + (2 * ll)] == 'A' && leftDia[i + (3 * ll)] == 'S')
                {
                    if (rightDia[i + ll - (l - i % ll)] == 'M' && rightDia[i + (2 * ll) - (l - i % ll)] == 'A' && rightDia[i + (3 * ll) - (l - i % ll)] == 'S')
                    {
                        d++;
                    }
                    if (rightDia[i + ll - (l - i % ll)] == 'S' && rightDia[i + (2 * ll) - (l - i % ll)] == 'A' && rightDia[i + (3 * ll) - (l - i % ll)] == 'M')
                    {
                        d++;
                    }
                }
                if (leftDia[i + ll] == 'S' && leftDia[i + (2 * ll)] == 'A' && leftDia[i + (3 * ll)] == 'M')
                {
                    if (rightDia[i + ll - (l - i % ll)] == 'M' && rightDia[i + (2 * ll) - (l - i % ll)] == 'A' && rightDia[i + (3 * ll) - (l - i % ll)] == 'S')
                    {
                        d++;
                    }
                    if (rightDia[i + ll - (l - i % ll)] == 'S' && rightDia[i + (2 * ll) - (l - i % ll)] == 'A' && rightDia[i + (3 * ll) - (l - i % ll)] == 'M')
                    {
                        d++;
                    }
                }
            }*/

            var l = data.Split('\n')[0].Count() + 1;

            for (int i = 1; i < data.Length - 2 * l - 2; i++)
            {
                bool d1 = false;
                bool d2 = false;
                if (data[i + l + 1] == 'A')
                {
                    if ((data[i] == 'M' && data[i + 2 * l + 2] == 'S') || (data[i] == 'S' && data[i + 2 * l + 2] == 'M'))
                    {
                        d1 = true;
                    }
                    if ((data[i + 2] == 'M' && data[i + 2 * l] == 'S') || (data[i + 2] == 'S' && data[i + 2 * l] == 'M'))
                    {
                        d2 = true;
                    }

                    if (d1 && d2)
                    {
                        d++;
                    }
                }
            }


            Console.WriteLine($"There's {d} X-MAS in the input file");
        }

        private static string ToDiagonalLeft(string data)
        {
            var split = data.Split('\n');
            return split.Select((d, i) =>
            {
                return d.PadLeft(split.Count() + i - 1, '0').PadRight((split.Count()) * 2 - 3, '0');
            }).Aggregate((b, c) => (b + '\n' + c));

        }

        private static string ToDiagonalRight(string data)
        {
            var split = data.Split('\n');
            return split.Select((d, i) =>
            {
                return d.PadRight(split.Count() + i - 1, '0').PadLeft((split.Count()) * 2 - 3, '0');
            }).Aggregate((b, c) => (b + '\n' + c));
        }
    }
}
