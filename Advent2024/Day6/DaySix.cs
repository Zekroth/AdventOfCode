using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdventOfCode.Advent2024
{
    public class DaySix
    {
        public DaySix() { }

        public static async Task DaySixPartOne(string cookie)
        {
            string data = await DataImporter.GetData(2024, 6, cookie);
            int l = data.Split('\n')[0].Count();
            char[] map = data.Split('\n').Aggregate("", (x, y) => x = x + y).ToCharArray();
            int h = map.Length / l;
            int guardPosX = 0, guardPosY = 0;
            int guardPos = Array.FindIndex(map, x => x == '^' || x == '<' || x == '>' || x == 'v');
            while (true)
            {
                
                guardPosX = guardPos % l;
                guardPosY = guardPos / l;

                // STEP

                try
                {
                    switch (map[guardPos])
                    {
                        case '^':
                            if (guardPosY - 1 >= 0)
                                if (map[guardPos - l] == '#')
                                    map[guardPos] = '>';
                                else
                                {
                                    map[guardPos - l] = '^';
                                    map[guardPos] = 'X';
                                    guardPos = guardPos - l;
                                }
                            else
                            {
                                map[guardPos] = 'X';
                                throw new IndexOutOfRangeException();
                            }
                            break;
                        case '<':
                            if (guardPosX - 1 >= 0)
                                if (map[guardPos - 1] == '#')
                                    map[guardPos] = '^';
                                else
                                {
                                    map[guardPos - 1] = '<';
                                    map[guardPos] = 'X';
                                    guardPos = guardPos - 1;
                                }
                            else
                            {
                                map[guardPos] = 'X';
                                throw new IndexOutOfRangeException();
                            }
                            break;
                        case 'v':
                            if (guardPosY + 1 != h)
                                if (map[guardPos + l] == '#')
                                    map[guardPos] = '<';
                                else
                                {
                                    map[guardPos + l] = 'v';
                                    map[guardPos] = 'X';
                                    guardPos = guardPos + l;
                                }
                            else
                            {
                                map[guardPos] = 'X';
                                throw new IndexOutOfRangeException();
                            }
                            break;
                        case '>':
                            if (guardPosX + 1 != l)
                                if (map[guardPos + 1] == '#')
                                    map[guardPos] = 'v';
                                else
                                {
                                    map[guardPos + 1] = '>';
                                    map[guardPos] = 'X';
                                    guardPos = guardPos + 1;
                                }
                            else
                            {
                                map[guardPos] = 'X';
                                throw new IndexOutOfRangeException();
                            }
                            break;
                    }
                } catch (IndexOutOfRangeException e)
                {
                    break;
                }
            }
            var o = "";
            for (int i = 0; i < map.Length; i++)
            {
                if (i % l == 0){
                    Console.Write('\n');
                    if (i != 0)
                        o += "\n";
                }
                o += map[i];
                Console.Write(map[i]);
            }
            File.AppendAllText(System.AppContext.BaseDirectory + $"/Outputs/{2024}_{6}.txt", o);
            Console.WriteLine();
            Console.WriteLine($"The guard covered {map.Count(x => x == 'X')} steps.");
        }

        private static bool CheckForward(char[] map, int start, Direction d, int l, int h, List<EncounteredWall> path)
        {
            var fw = d switch
            {
                Direction.NORTH => -l,
                Direction.EAST => 1,
                Direction.SOUTH => l,
                Direction.WEST => -1,
            };

            var lim = d switch
            {
                Direction.NORTH => 0,
                Direction.EAST => l,
                Direction.SOUTH => h,
                Direction.WEST => 0,
            };

            while (lim == 0 ? start + fw > lim : start + fw < lim)
            {
                if (map[start + fw] == '#')
                {
                    return false;
                }

                EncounteredWall wall = new EncounteredWall();
                wall.wallPos = start + fw;
                wall.dir = d;

                if (path.Contains(wall))
                    return true;

                start += fw;
            }

            return false;
        }
        private enum Direction
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        private static Direction RotateRight(Direction d)
        {
            return d switch
            {
                Direction.NORTH => Direction.EAST,
                Direction.EAST => Direction.SOUTH,
                Direction.SOUTH => Direction.WEST,
                Direction.WEST => Direction.NORTH,
            };
        }
        private struct EncounteredWall
        {
            public int wallPos;
            public Direction dir;
        }

        private struct MarchStatus
        {
            public int guardPos;
            public Direction dir;
            public char[] image;
            public List<EncounteredWall> encWalls;
            public int l;
            public int h;

            /// <summary>
            /// FicticiousWallPosition for the '0' wall
            /// </summary>
            public int fWP;
            
            public char SP {
                get
                {
                    return image[guardPos];
                }
            }
            public int guardPosX
            {
                get
                {
                    return this.guardPos % l;
                }
            }
            public int guardPosY
            {
                get
                {
                    return this.guardPos / l;
                }
            }
        }
    }
}
