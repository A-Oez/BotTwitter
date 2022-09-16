using TransferFacts;
using TransferFacts.GetRequests;
using TransferFacts.Models;
using TransferFacts.PostRequests;

namespace BotTwitter 
{
    internal class Program
    {     
        public static void Main(string[] args)
        {
            runTransfer();
        }      

        private static void runTransfer()
        {
            ManagerTransferFacts ManagerFacts = new ManagerTransferFacts();
            ManagerFacts.RunTransferFacts();
        }

    }
}