using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2023.Day2
{
    public class Game
    {
        public int Id;
        List<Hand> list;

        public Game(string input)
        {
            var split = input.Split(':');
            Id = int.Parse(split[0].Replace("Game ", ""));

            list = new List<Hand>();

            foreach (var items in split[1].Split(';'))
            {
                var hand = new Hand();

                var red = Regex.Match(items, "\\d red");
                var blue = Regex.Match(items, "\\d blue");
                var green = Regex.Match(items, "\\d green");

                foreach (var redMatches in red.Captures.ToList())
                {
                    hand.draws.Add(new Draw()
                    {
                        q = int.Parse(redMatches.Value.Replace(",", "").Replace("red", "").Trim()),
                        color = Cubes.RED
                    });
                }

                foreach (var blueMatches in blue.Captures.ToList())
                {
                    hand.draws.Add(new Draw()
                    {
                        q = int.Parse(blueMatches.Value.Replace(",", "").Replace("blue", "").Trim()),
                        color = Cubes.BLUE
                    });
                }

                foreach (var greenMatches in green.Captures.ToList())
                {
                    hand.draws.Add(new Draw()
                    {
                        q = int.Parse(greenMatches.Value.Replace(",", "").Replace("green", "").Trim()),
                        color = Cubes.GREEN
                    });
                }
                list.Add(hand);
            }
        }

        public bool IsGameValid(int r, int g, int b)
        {
            bool res = true;
            foreach (var item in list)
            {
                res = res && item.IsHandValid(r, g, b);
            }

            return res;
        }
    }
    public struct Hand
    {
        public List<Draw> draws;
        public Hand()
        {
            draws = new();
        }

        public int getRedCount()
        {
            var count = 0;
            foreach (var item in draws.Where(x => x.color == Cubes.RED))
            {
                count += item.q;
            }
            return count;
        }
        public int getBlueCount()
        {
            var count = 0;
            foreach (var item in draws.Where(x => x.color == Cubes.BLUE))
            {
                count += item.q;
            }
            return count;
        }
        public int getGreenCount()
        {
            var count = 0;
            foreach (var item in draws.Where(x => x.color == Cubes.GREEN))
            {
                count += item.q;
            }
            return count;
        }

        public bool IsHandValid(int r, int g, int b)
        {
            if (getRedCount() <= r && getGreenCount() <= g && getBlueCount() <= b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public struct Draw
    {
        public int q;
        public Cubes color;
    }
    public enum Cubes
    {
        RED,
        GREEN,
        BLUE
    }
}
