using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading;
using TransferFacts.Models;

namespace TransferFacts
{
    public class PostLike
    {
        private const string URL = "https://api.twitter.com/2/users/USERID/likes"; ////add your own USERID in the url 
        private List<UserTweetsModel> tweetsList { get; set; }
        private readonly TwitterCredentials credentials = new TwitterCredentials();
  
        public PostLike(List<UserTweetsModel> tweetsList)
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

                var jsonBody = @"{'tweet_id': 'ID'}"; //create post JsonBody to save in Object
                var myJson = JsonConvert.DeserializeObject<LikeModel>(jsonBody);

                myJson.tweet_id= tweetsList[i].id; //overwrite tweetID with ID in list 
                var json = JsonConvert.SerializeObject(myJson); //serialize Object in JSON

                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                RestResponse response = client.Execute(request);
                Thread.Sleep(1000);
            }
        }
    }
}
