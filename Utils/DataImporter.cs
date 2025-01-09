
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    public static class DataImporter
    {
        // Method to retrieve data from the AdventOfCode website
        public static async Task<string> GetData(short year, short day, string cookieValue)
        {
            if (!Directory.Exists(System.AppContext.BaseDirectory + "/Inputs"))
            {
                Directory.CreateDirectory(System.AppContext.BaseDirectory + "/Inputs");
            }
            if (File.Exists(System.AppContext.BaseDirectory + $"/Inputs/{year}_{day}.txt"))
            {
                return await File.ReadAllTextAsync(System.AppContext.BaseDirectory + $"/Inputs/{year}_{day}.txt");
            }

            var baseAddress = new Uri($"https://adventofcode.com/{year}/day/{day}/input");
            var cookieContainer = new CookieContainer();
            string content = "";

            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {

                cookieContainer.Add(baseAddress, new Cookie("session", cookieValue));
                var result = await client.GetAsync(baseAddress);
                result.EnsureSuccessStatusCode();


                content = await result.Content.ReadAsStringAsync();
            }

            File.AppendAllText(System.AppContext.BaseDirectory + $"/Inputs/{year}_{day}.txt", content );

            return content;
        }

    }
}
