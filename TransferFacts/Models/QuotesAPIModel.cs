namespace TransferFacts.Models
{
    public class QuotesAPIModel
    {
        public int id { get; set; }
        public string language_code { get; set; }
        public string content { get; set; }
        public string url { get; set; }
        public Originator originator { get; set; }
        public string[] tags { get; set; }
    }

    public class Originator
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

}
