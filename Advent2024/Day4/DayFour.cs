using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2024
{
    public class DayFour
    {
        public DayFour() { }

        public static async Task RunDayFourPartOne(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 4, cookieValue: cookie);
            
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
            for (int i = 0; i < data.Length - 3 * 141; i++)
            {
                if (data[i] == 'X' && data[i + 141] == 'M' && data[i + (2 * 141)] == 'A' && data[i + (3 * 141)] == 'S')
                {
                    v++;
                }
                if (data[i] == 'S' && data[i + 141] == 'A' && data[i + (2 * 141)] == 'M' && data[i + (3 * 141)] == 'X')
                {
                    v++;
                }
            }

            Console.WriteLine($"Horizontal matches: {h}");
            Console.WriteLine($"Vertical matches: {v}");
            return;
        }
    }
}
