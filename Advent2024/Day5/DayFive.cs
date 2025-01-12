using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2024
{
    public class DayFive
    {
        public DayFive() { }

        public static async Task DayFivePartOne(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 5, cookieValue: cookie);

            var split = data.Split("\n\n");

            var row1 = split[0].Split('\n');
            var row2 = split[1].Split('\n');

            var strules = row1.Select(x => x.Split('|')).ToList();
            var strval = row2.Select(x => x.Split(',')).SkipLast(1).ToList();
            var numRules = new List<int[]>();
            var numVal = new List<int[]>();

            strval.ForEach(x => { numVal.Add(x.Select(y => Int32.Parse(y)).ToArray()); });
            strules.ForEach(x => { numRules.Add(x.Select(y => Int32.Parse(y)).ToArray()); });



            for ( var i = 0; i < numVal.Count; i++ )
            {

                foreach (var rule in numRules)
                {
                    if (numVal[i].Contains(rule[0]) && numVal[i].Contains(rule[1]))
                    {
                        int pos1 = Array.FindIndex(numVal[i], x => x == rule[0]);
                        int pos2 = Array.FindIndex(numVal[i], x => x == rule[1]);

                        if (pos1 > pos2)
                        {
                            numVal[i] = new int[] {0};
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"The result is {numVal.Aggregate(0, (cursor, row) => { 
                return cursor = cursor + row[row.Length / 2]; 
            })}");

            return;
        }

        public static async Task DayFivePartTwo(string cookie)
        {
            string data = await AdventOfCode.Utils.DataImporter.GetData(2024, 5, cookieValue: cookie);

            var split = data.Split("\n\n");

            var row1 = split[0].Split('\n');
            var row2 = split[1].Split('\n');

            var strules = row1.Select(x => x.Split('|')).ToList();
            var strval = row2.Select(x => x.Split(',')).SkipLast(1).ToList();
            var numRules = new List<int[]>();
            var numVal = new List<int[]>();

            strval.ForEach(x => { numVal.Add(x.Select(y => Int32.Parse(y)).ToArray()); });
            strules.ForEach(x => { numRules.Add(x.Select(y => Int32.Parse(y)).ToArray()); });

            var flagArr = new bool[numVal.Count].Select(x => true).ToArray();

            for (int i = 0; i < numVal.Count; i++)
            {
                RESTART:
                foreach (var rule in numRules)
                {
                    if (numVal[i].Contains(rule[0]) && numVal[i].Contains(rule[1]))
                    {
                        int pos1 = Array.FindIndex(numVal[i], x => x == rule[0]);
                        int pos2 = Array.FindIndex(numVal[i], x => x == rule[1]);

                        if (pos1 > pos2)
                        {
                            var c = numVal[i][pos1];
                            numVal[i][pos1] = numVal[i][pos2];
                            numVal[i][pos2] = c;

                            flagArr[i] = false;

                            goto RESTART;
                        }
                    }
                }
            }

            int index = 0;
            Console.WriteLine($"The result is {numVal.Aggregate(0, (cursor, row) => {

                index++;
                return cursor = cursor + (!flagArr[index - 1] ? row[row.Length / 2] : 0);
            })}");

            return;
        }
    
    }
}
