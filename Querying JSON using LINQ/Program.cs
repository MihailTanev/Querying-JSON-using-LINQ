namespace Querying_JSON_using_LINQ
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var webClient = new WebClient())
            {
                var inputJson = webClient.DownloadString("https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/b09563bc0c4b318132c7a738e679d4f984ef0048/kings");

                var result1 = GetTotalMonarchs(inputJson);

                Console.WriteLine(result1);

                var result2 = GetMostCommonFirstName(inputJson);

                Console.WriteLine(result2);
            }
        }

        public static string GetTotalMonarchs(string inputJson)
        {

            var kingdomCollections = JsonConvert.DeserializeObject<List<KingdomCollection>>(inputJson);

            var totalMonarchs = kingdomCollections.Count;

            return $"Total number of monarchs in the list: {totalMonarchs}.";

        }

        public static string GetMostCommonFirstName(string inputJson)
        {

            var kingdom = JsonConvert.DeserializeObject<List<Kingdom>>(inputJson);

            var firstNames = kingdom
                                    .Select(v => v.Nm.Split(' ').First())
                                    .GroupBy(x => x);

            var mostCommonFirstName = firstNames
                                    .OrderByDescending(x => x.Count())
                                    .First();

            return $"The most common first name is {mostCommonFirstName.Key} and it appears {mostCommonFirstName.Count()} times.";

        }
       
    }

    public class KingdomCollection
    {
        public List<Kingdom> Kingdoms { get; set; }
    }

    public class Kingdom
    {
        public int Id { get; set; }
        public string Nm { get; set; }
        public string Cty { get; set; }
        public string Hse { get; set; }
        public string Yrs { get; set; }
    }
}
