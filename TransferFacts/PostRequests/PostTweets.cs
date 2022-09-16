using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using TransferFacts.GetRequests;
using TransferFacts.Models;

namespace TransferFacts.PostRequests
{
    public class PostTweets
    {
        private const string URL = "https://api.twitter.com/2/tweets";
        private List<UserTweetsModel> tweetsList { get; set; }
        private CallAPI requestCallAPI = new CallAPI();
        private LogController controllerLog = new LogController();
        private readonly TwitterCredentials credentials = new TwitterCredentials();

        public PostTweets(List<UserTweetsModel> tweetsList)
        {
            this.tweetsList = tweetsList;
        } 

        public void RequestCall()
        {
            for (int i = 0; i < tweetsList.Count; i++)
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var client = new RestClient(URL);
                client.Authenticator = OAuth1Authenticator.ForAccessToken(credentials.consumer_key, credentials.consumer_secret, credentials.access_token, credentials.token_secret,
                    RestSharp.Authenticators.OAuth.OAuthSignatureMethod.HmacSha1);
                var request = new RestRequest(URL, Method.Post);

                var jsonBody = @"{'text': 'x', 'reply': {'in_reply_to_tweet_id': 'x'}}"; //create post JsonBody to save in Object
                var myJson = JsonConvert.DeserializeObject<PostTweetModel>(jsonBody);

                myJson.text = requestCallAPI.RequestCall(); //overwrite text value with API call value
                myJson.reply.in_reply_to_tweet_id = tweetsList[i].id; //overwrite tweetID with ID in list 
                var json = JsonConvert.SerializeObject(myJson); //serialize Object in JSON

                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);

                Console.WriteLine("post {0} done", i + 1);
                controllerLog.writeTweetID(tweetsList); //write tweetID in file to avoid double answering
            }
        }      
        
    }
}
