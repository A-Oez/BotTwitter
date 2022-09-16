using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TransferFacts.GetRequests;
using TransferFacts.Models;
using TransferFacts.PostRequests;

namespace TransferFacts
{
    public class ManagerTransferFacts
    {
        private List<UserTweetsModel> listTweets = new List<UserTweetsModel>();

        public void RunTransferFacts()
        {
            var getTweets = new GetTweets();
            listTweets = getTweets.RequestCall(15); //configure max tweet results from each user 

            var postLike = new PostLike(listTweets);
            var postTweets = new PostTweets(listTweets);

            Thread t1 = new Thread(new ThreadStart(postLike.RequestCall));
            Thread t2 = new Thread(new ThreadStart(postTweets.RequestCall));

            t1.Start();
            t2.Start();
        }

    }
}