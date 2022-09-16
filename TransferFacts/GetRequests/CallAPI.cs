using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading;
using TransferFacts.Models;

namespace TransferFacts.GetRequests
{
    public class CallAPI
    {
        private Random random = new Random();
        private readonly TwitterCredentials credentials = new TwitterCredentials();

        public string RequestCall() //calls the apis 
        {
            var randomValue = random.Next(1, 3);

            if (randomValue == 1)
                return CallUselessFactsAPI();
            

            return CallQuotesAPI();
        }

        private string CallUselessFactsAPI()
        {
            var randomValue = random.Next(1, 3); //get content in de ore en 
            var isoCode = "de";

            if (randomValue == 1)
                isoCode = "en";

            string URLAPI = "https://uselessfacts.jsph.pl/random.json?language=" + isoCode;
            string value = string.Empty;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient(URLAPI);
            var request = new RestRequest(URLAPI, Method.Get);

            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);
            Thread.Sleep(1000);
            var myJson = JsonConvert.DeserializeObject<UselessFactsAPIModel>(response.Content);
            return myJson.text;
        }

        private string CallQuotesAPI()
        {
            var randomValue = random.Next(1, 3);
            var isoCode = "de";

            if (randomValue == 1)
                isoCode = "en";

            string URLAPI = "https://quotes15.p.rapidapi.com/quotes/random/?language_code="+ isoCode;

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient(URLAPI);
            var request = new RestRequest(URLAPI, Method.Get);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-RapidAPI-Key", credentials.quotesToken);

            RestResponse response = client.Execute(request);
            Thread.Sleep(1000);
            var myJson = JsonConvert.DeserializeObject<QuotesAPIModel>(response.Content);
            return myJson.content + " ~" + myJson.originator.name;
        }
    }
}