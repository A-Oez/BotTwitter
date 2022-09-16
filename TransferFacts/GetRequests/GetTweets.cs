using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using TransferFacts.Models;

namespace TransferFacts.GetRequests
{
    public class GetTweets
    {
        private List<UserTweetsModel> tweetsList = new List<UserTweetsModel>();
        private List<UserDataModel> userDataList = new List<UserDataModel>();
        private List<string> DuplicateTweets = new List<string>();
        private readonly LogController controllerLog = new LogController();
        private readonly TwitterCredentials credentials = new TwitterCredentials();
        private readonly GetUser user = new GetUser();

        public List<UserTweetsModel> RequestCall(int maxResults)
        {
            userDataList = user.RequestCall(); // get userdata 

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            foreach (var UserData in userDataList) //do tweetrequests for each user in list
            {
                var URL = "https://api.twitter.com/2/users/" + UserData.id + "/tweets?tweet.fields=referenced_tweets&max_results=" + maxResults;

                var client = new RestClient(URL);
                var request = new RestRequest(URL, Method.Get);

                client.Authenticator = new JwtAuthenticator(credentials.bearerToken);
                request.AddHeader("Content-Type", "application/json");

                RestResponse response = client.Execute(request);

                var myJson = JsonConvert.DeserializeObject<UserTweetsModelInformation>(response.Content);
                DuplicateTweets = controllerLog.getDuplicateTweets();//get tweet ids from list
                var checkTweetContains = false;

                foreach (var tweet in myJson.data)
                {
                    if (DuplicateTweets.Contains(tweet.id)) //check if tweetID is in list 
                        checkTweetContains = true;

                    if (tweet.referenced_tweets == null && checkTweetContains == false) //referenced_tweets null == tweets without reply 
                    {
                        var createTweet = new UserTweetsModel(tweet.id, tweet.text);
                        tweetsList.Add(createTweet); //unique tweet without reply saved in list 
                    }

                }
            }
            Console.WriteLine("Count Tweets: {0}", tweetsList.Count);

            return tweetsList;
        }
    }

}