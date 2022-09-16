namespace TransferFacts.Models
{
    public class UserTweetsModelInformation
    {
        public UserTweetsModel[] data { get; set; }
    }

    public class UserTweetsModel
    {
        public string id { get; set; }
        public Referenced_Tweets[] referenced_tweets { get; set; }
        public string text { get; set; }

        public UserTweetsModel(string id, string text)
        {
            this.id = id;
            this.text = text;
        }
    }

    public class Referenced_Tweets
    {
        protected string type { get; set; }
        protected string id { get; set; }
    }
    
}
