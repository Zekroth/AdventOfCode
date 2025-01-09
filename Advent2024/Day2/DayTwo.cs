using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2024
{
    public class DayTwo
    {
        public static async Task DayTwoPartOne(string cookie)
        {
            var data = await DataImporter.GetData(2024, 2, cookie);

            var split = data.Split("\n");

            var reports = new List<Report>();

            foreach (var row in split.SkipLast(1))
            {
                reports.Add(new Report(row, new() 
                    { 
                        // IF LEVELS ARE ALL DECREASING OR ALL INCREASING
                        (r) => 
                        {
                            int previous = -1;
                            bool direction = true;

                            for (var i = 0; i < r.Length; i++)
                            {
                                #region if-setup
                                
                                if(i == 0)
                                {
                                    // SETUP FIRST VAL
                                    previous = r[i];
                                    continue;
                                } 
                                else if (i == 1)
                                {
                                    // SETUP DIRECTION
                                    if (r[i] > previous)
                                        direction = true;
                                    else if (r[i] < previous)
                                        direction = false;
                                    else
                                        return false;

                                    continue;
                                }
                                
                                #endregion

                                if (r[i] > r[i - 1] && direction)
                                    continue;
                                else if (r[i] < r[i - 1] && !direction)
                                    continue;
                                else {
                                    return false;
                                }
                            }

                            return true; 
                        },

                        // AND
                        // IF ANY TWO ADIACENT LEVELS DIFFER FROM AT LEAST 1 AND AT MOST 3
                        (r) =>
                        {
                            int previous = -1;

                            for (var i = 0; i < r.Length; i++)
                            {
                                #region if-setup
                                
                                if(i == 0)
                                {
                                    // SETUP FIRST VAL
                                    previous = r[i];
                                    continue;
                                }

                                #endregion

                                if (r[i] != previous && Math.Abs(r[i] - previous) <= 3)
                                {
                                    previous = r[i];
                                    continue;
                                }
                                else
                                    return false;
                            }

                            return true;
                        }
                    }
                ));
            }

            var safeR = reports.Count((r) => r.IsSafe );

            Console.WriteLine($"The total number of safe reports is {safeR}");
        }

        public static async Task DayTwoPartTwo(string cookie)
        {
            var data = await DataImporter.GetData(2024, 2, cookie);

            var split = data.Split("\n");

            var reports = new List<Report>();

            foreach (var row in split.SkipLast(1))
            {
                reports.Add(new Report(row, new()
                    { 
                        // IF LEVELS ARE ALL DECREASING OR ALL INCREASING
                        (r) =>
                        {
                            int previous = -1;
                            bool direction = true;

                            for (var i = 0; i < r.Length; i++)
                            {
                                #region if-setup
                                
                                if(i == 0)
                                {
                                    // SETUP FIRST VAL
                                    previous = r[i];
                                    continue;
                                }
                                else if (i == 1)
                                {
                                    // SETUP DIRECTION
                                    if (r[i] > previous)
                                        direction = true;
                                    else if (r[i] < previous)
                                        direction = false;
                                    else
                                        return false;

                                    continue;
                                }
                                
                                #endregion

                                if (r[i] > r[i - 1] && direction)
                                    continue;
                                else if (r[i] < r[i - 1] && !direction)
                                    continue;
                                else {
                                    return false;
                                }
                            }

                            return true;
                        },

                        // AND
                        // IF ANY TWO ADIACENT LEVELS DIFFER FROM AT LEAST 1 AND AT MOST 3
                        (r) =>
                        {
                            int previous = -1;

                            for (var i = 0; i < r.Length; i++)
                            {
                                #region if-setup
                                
                                if(i == 0)
                                {
                                    // SETUP FIRST VAL
                                    previous = r[i];
                                    continue;
                                }

                                #endregion

                                if (r[i] != previous && Math.Abs(r[i] - previous) <= 3)
                                {
                                    previous = r[i];
                                    continue;
                                }
                                else
                                    return false;
                            }

                            return true;
                        }
                    }
                ));
            }

            var safeR = reports.Count((r) => r.IsSafeV2);

            Console.WriteLine($"The total number of safe reports is {safeR}");
        }


        protected internal class Report
        {
            public int[] Levels { get; set; } = new int[0];

            public List<Func<int[], bool>> Patterns { get; set; } = new List<Func<int[], bool>> { };

            public bool IsSafe
            {
                get {
                    
                    // Executes all pattern matching functions
                    foreach (var item in Patterns)
                    {
                        // If even one pattern is not matching, the evaluation stops and the result is false
                        if (!item.Invoke(this.Levels))
                            return false;
                    }

                    // If all the patterns are matched the result is true
                    return true; 
                }
            }

            public bool IsSafeV2 
            {
                get {
                    if (IsSafe)
                        return true;

                    var temp = Levels;
                    for (var i = 0; i < temp.Length; i ++ )
                    {
                        var t2 = temp.ToList();
                        t2.RemoveAt(i);
                        Levels = t2.ToArray();
                        if (IsSafe)
                            return true;
                    }

                    return false;
                } 
            }
            public Report(string Raw)
            {
                var split = Raw.Split(" ");
                Levels = split.Select(x => 
                    { 
                        return Int32.Parse(x.ToString()); 
                    }).ToArray();


            }

            public Report(string Raw, List<Func<int[], bool>> Patterns) : this(Raw)
            {
                this.Patterns = Patterns;
            }
        }
    }
}
