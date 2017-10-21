using System;
using System.Diagnostics;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NScrape;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace ScrapeTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //HtmlWeb web = new HtmlWeb();
            //HtmlDocument document = web.Load("http://www.c-sharpcorner.com");

            //var json = new WebClient().SendRequest(new Uri(""));

            //using (var client = new HttpClient())
            //{
            //    var response = client.GetAsync("https://feed.danskespil.dk/sportsbook/events.xml?token=07a074a5d4ce9dffbe50e377b9bb668b&typeid=DanskeSpil&sportid=21&ligaid=13270");
            //    do
            //    {
            //        Thread.Sleep(500);
            //    } while (response==null);
            //    var content = response.Result.Content.ReadAsStreamAsync();
            //    do
            //    {
            //        Thread.Sleep(500);
            //    } while (content == null);
            //    Debug.WriteLine(content);
            //}

            //JObject googleSearch = JObject.Parse("");
            //IList<JToken> results = googleSearch["responseData"]["results"].Children().ToList();
            //Debug.WriteLine("Hello World!");


            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //    {
            //        if (string.Equals(reader.Value,"hjemmehold"))
            //        {
            //            Debug.WriteLine("Token: {0}", reader.Value);
            //        }
            //    }
            //    else
            //    {
            //        //Debug.WriteLine("Token: {0}", reader.TokenType);
            //    }
            //}

            //IList<Match> matches = new List<Match>();

            //JsonTextReader reader = new JsonTextReader(new StringReader(feed));
            //reader.SupportMultipleContent = true;

            //while (true)
            //{
            //    if (!reader.Read())
            //    {
            //        break;
            //    }

            //    JsonSerializer serializer = new JsonSerializer();
            //    Match match = serializer.Deserialize<Match>(reader);
            //    matches.Add(match);
            //}

            //foreach (var match in matches)
            //{
            //    Debug.WriteLine(match.Hjemmehold);
            //}

            Stopwatch stopwatch = Stopwatch.StartNew();

            // https://www.newtonsoft.com/json/help/html/SerializingJSONFragments.htm
            string sl = "https://feed.danskespil.dk/sportsbook/events.json?token=07a074a5d4ce9dffbe50e377b9bb668b&typeid=DanskeSpil&sportid=21&ligaid=13270";
            string top10 = "https://feed.danskespil.dk/sportsbook/events.json?token=7710febf89771c43c6c0c9a6f5243da3&typeid=DanskeSpil&top=10";
            string result = "https://feed.danskespil.dk/sportsbook/events.json?token=07a074a5d4ce9dffbe50e377b9bb668b&typeid=LiveProgram&sportid=21&ligaid=13270";

            string feedResult = ProcessMatches(result).Result;

            //https://stackoverflow.com/questions/34690581/error-reading-jobject-from-jsonreader-current-jsonreader-item-is-not-an-object
            JArray matchesObjects = JArray.Parse(feedResult);
            IList<JToken> matches = matchesObjects.Children().ToList();

            IList<Match> matchesResults = new List<Match>();
            IDictionary<string,Match> matchesDic = new Dictionary<string,Match>();

            foreach (JToken match in matches)
            {
                Match matchResult = match.ToObject<Match>();
                matchesResults.Add(matchResult);
                matchesDic.Add(matchResult.Event_id,matchResult);
            }

            //foreach (Match match in matchesResults)
            //{
            //    Debug.WriteLine("");
            //    Debug.WriteLine("Event ID: " + match.Event_id);
            //    Debug.WriteLine("{0}  -  {1} -> Kickoff: {2}", match.Hjemmehold, match.Udehold, match.Kickoff);
            //    Debug.WriteLine("1: {0}   X: {1}   2: {2}", match.Odds_1, match.Odds_x, match.Odds_2);
            //    Debug.WriteLine("Marked_tekst: " + match.Marked_tekst);
            //    Debug.WriteLine("{0} - {1}", match.Hjemmescore,match.Udescore);
            //}

            foreach (string key in matchesDic.Keys)
            {
                Debug.WriteLine("");
                Match match = new Match();
                matchesDic.TryGetValue(key, out match);
                Debug.WriteLine("Key: "+key);
                Debug.WriteLine("Match: " + match.Marked_tekst);
                Debug.WriteLine("Score: {0} - {1}", match.Hjemmescore, match.Udescore);
            }

            Debug.WriteLine("");
            stopwatch.Stop();
            Debug.WriteLine("Duration: {0}", stopwatch.ElapsedMilliseconds);
            Debug.WriteLine("");
        }

        private static async Task<string> ProcessMatches(string url)
        {
            // https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
            HttpClient client = new HttpClient();

            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Task<string> stringTask = client.GetStringAsync(url);

            return await stringTask;
        }
    }
}
