namespace TransferFacts.Models
{
    public class UserDataModelInformation
    {
        public UserDataModel[] data { get; set; }
    }

    public class UserDataModel
    {
        public string username { get; set; }
        public bool _protected { get; set; }
        public string name { get; set; }
        public string id { get; set; }

        public UserDataModel(string id, string name, bool @protected)
        {
            _protected = @protected;
            this.name = name;
            this.id = id;
        }
    }
}
