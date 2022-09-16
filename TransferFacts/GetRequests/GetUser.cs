using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferFacts.Models;

namespace TransferFacts.GetRequests
{
    public class GetUser
    {
        private readonly List<UserDataModel> userDataList = new List<UserDataModel>();
        private readonly TwitterCredentials credentials = new TwitterCredentials();
        private const string URL = "https://api.twitter.com/2/users/USERID/following?user.fields=protected"; ////add your own USERID in the url 


        public List<UserDataModel> RequestCall()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var client = new RestClient(URL);
            var request = new RestRequest(URL, Method.Get);

            client.Authenticator = new JwtAuthenticator(credentials.bearerToken);
            request.AddHeader("Content-Type", "application/json");

            RestResponse response = client.Execute(request);

            var myJson = JsonConvert.DeserializeObject<UserDataModelInformation>(response.Content);

            foreach (var user in myJson.data)
            {
                if (user._protected == false) //check if user is privat
                {
                    var createUser = new UserDataModel(user.id, user.name, user._protected);
                    userDataList.Add(createUser); //public user saved in list 
                }
            }
            return userDataList;
        }
    }
}