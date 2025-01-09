using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2023.Day2
{
    public class DayTwo
    {
        public static async Task RunDayTwoPartOne()
        {
            var baseAddress = new Uri("https://adventofcode.com/2023/day/2/input");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                cookieContainer.Add(baseAddress, new Cookie("session", "53616c7465645f5f0cd9f01cb9fffbee56229070d641fee554205d01d5db6ad1c821fc7da87380697ac3614d606d496990028726ab8bd8286e377e319d658d9f"));
                var result = await client.GetAsync(baseAddress);
                result.EnsureSuccessStatusCode();


                var content = await result.Content.ReadAsStringAsync();

                var board = new List<Game>();

                foreach (var item in content.Split('\n'))
                {
                    if (item != "" && item.StartsWith("Game "))
                        board.Add(new Game(item));
                }

                var count = 0;

                foreach (var item in board)
                {
                    count += item.IsGameValid(12, 13, 14) ? item.Id : 0;
                }

                Console.WriteLine($"The result is {count}");
            }
        }
    }
}
