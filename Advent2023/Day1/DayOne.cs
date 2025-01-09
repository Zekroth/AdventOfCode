using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Advent2023.Day1
{
    public class DayOne
    {
        public DayOne() { }

        public static async Task RunDayOnePartOne()
        {
            var baseAddress = new Uri("https://adventofcode.com/2023/day/1/input");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                cookieContainer.Add(baseAddress, new Cookie("session", "53616c7465645f5f0cd9f01cb9fffbee56229070d641fee554205d01d5db6ad1c821fc7da87380697ac3614d606d496990028726ab8bd8286e377e319d658d9f"));
                var result = await client.GetAsync(baseAddress);
                result.EnsureSuccessStatusCode();


                var content = await result.Content.ReadAsStringAsync();
                // Console.WriteLine(content);
                var sum = 0;

                foreach (var item in content.Split('\n'))
                {

                    //Console.WriteLine(match.Captures.FirstOrDefault()?.Value);

                    string line = "";
                    foreach (var capture in Regex.Replace(item, @"[^\d]", ""))
                    {
                        line = line + capture;
                    }

                    string value = "";
                    switch (line.Length)
                    {
                        case 0:
                            value = "0";
                            break;
                        default:
                            value = "" + line.First() + line.Last();
                            break;
                    }
                    sum += int.Parse(value);

                }

                Console.WriteLine($"Result is {sum}");


            }

        }

        public static async Task RunDayOnePartTwo()
        {
            var baseAddress = new Uri("https://adventofcode.com/2023/day/1/input");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                cookieContainer.Add(baseAddress, new Cookie("session", "53616c7465645f5f0cd9f01cb9fffbee56229070d641fee554205d01d5db6ad1c821fc7da87380697ac3614d606d496990028726ab8bd8286e377e319d658d9f"));
                var result = await client.GetAsync(baseAddress);
                result.EnsureSuccessStatusCode();


                var content = await result.Content.ReadAsStringAsync();
                // Console.WriteLine(content);
                var sum = 0;

                content = content.Replace("one", "on1e");
                content = content.Replace("two", "t2wo");
                content = content.Replace("three", "thr3ee");
                content = content.Replace("four", "fo4ur");
                content = content.Replace("five", "fi5ve");
                content = content.Replace("six", "si6x");
                content = content.Replace("seven", "se7ven");
                content = content.Replace("eight", "eig8ht");
                content = content.Replace("nine", "ni9ne");
                content = content.Replace("zero", "ze0ro");

                foreach (var item in content.Split('\n'))
                {

                    //Console.WriteLine(match.Captures.FirstOrDefault()?.Value);

                    string line = "";
                    foreach (var capture in Regex.Replace(item, @"[^\d]", ""))
                    {
                        line = line + capture;
                    }

                    string value = "";
                    switch (line.Length)
                    {
                        case 0:
                            value = "0";
                            break;
                        default:
                            value = "" + line.First() + line.Last();
                            break;
                    }
                    sum += int.Parse(value);

                }

                Console.WriteLine($"Result is {sum}");


            }
        }
    }
}
