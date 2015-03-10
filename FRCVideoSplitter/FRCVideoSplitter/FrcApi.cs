using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FRCVideoSplitter
{
    class FrcApi
    {
        private string baseUrl = "https://frc-api.usfirst.org/api/v1.0/";
        Communicator communicator = new Communicator();

        public FrcApi() { }

        public List<MatchResult> getMatchResults(int season, string eventKey)
        {
            string uri = baseUrl + "/matches/" + season.ToString() + "/" + eventKey;
            string api_response = communicator.sendAndGetRawResponse(uri);

            List<MatchResult> results = JsonConvert.DeserializeObject<MatchResultsList>(api_response).Matches;

            return results;
        }


        public class MatchResultsList
        {
            public List<MatchResult> Matches { get; set; }

            public MatchResultsList() { }
        }

        public class MatchResult
        {
            public string autoStartTime { get; set; }
            public string description { get; set; }
            public string level { get; set; }
            public string matchNumber { get; set; }
            public string scoreRedFinal { get; set; }
            public string scoreRedFoul { get; set; }
            public string scoreRedAuto { get; set; }
            public string scoreBlueFinal { get; set; }
            public string scoreBlueFoul { get; set; }
            public string scoreBlueAuto { get; set; }
            public List<MatchResultsTeam> teams { get; set; }

            public MatchResult() { }
        }

        public class MatchResultsTeam
        {
            public int? teamNumber { get; set; }
            public string station { get; set; }
            public bool dq { get; set; }

            public MatchResultsTeam() { }
        }

        private class Communicator
        {
            public string sendAndGetRawResponse(string uri)
            {
                var request = System.Net.WebRequest.Create(uri) as System.Net.HttpWebRequest;
                request.KeepAlive = true;

                string token = "TYTREMBLAY:C272D991-944E-49D7-B10E-27BA5EBB598B";

                string encodedToken = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(token));

                request.Headers.Add("Authorization: Basic " + encodedToken);

                request.Method = "GET";

                request.Accept = "application/json";
                request.ContentLength = 0;

                string responseContent = null;

                try
                {
                    using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                    {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                    }

                    return responseContent;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    
                }
                return responseContent;
            }
        }
    }
}
