namespace TransferFacts.Models
{

     public class PostTweetModel
     {
            public string text { get; set; }
            public Reply reply { get; set; }
     }

     public class Reply
     {
        public string in_reply_to_tweet_id { get; set; }      
     }

    
}
