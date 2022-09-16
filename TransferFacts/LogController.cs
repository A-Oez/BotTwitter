using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TransferFacts.Models;

namespace TransferFacts
{
    public class LogController
    {
        private string path = @"C:\"; //local txt file path

        public List<string> getDuplicateTweets() //save tweetID values from file in list
        {
            var logFile = File.ReadAllLines(path);
            List<string> DuplicateTweets = new List<string>(logFile);

            if (DuplicateTweets.Count > 5000) //clear file data 
            {
                File.WriteAllText(path, "");
            }

            return DuplicateTweets;
        }

        public async Task writeTweetID(List<UserTweetsModel> tweetList)
        {
            using StreamWriter file = new(path, append: true);

            for (int i = 0; i < tweetList.Count; i++)
            {
                await file.WriteLineAsync(tweetList[i].id);
            }

        }
    }
}
