using System;
using stock.Models;
using System.Net;

namespace stock
{
    public class EthereumExtension : IEthereum
    {

        //Variables
        static String currencyName = "Ethereum";
        string downloadedUrlString = null;
        WebClient clientToReadUrl; //Client to read URL data.
        Ethereum ethereumValues;

        /// <summary>
        /// Fucntion returning Data as array of Stock class.
        /// </summary>
        /// <returns></returns>
        Ethereum IEthereum.getPrice()
        {
            try
            {
                clientToReadUrl = new WebClient();
                downloadedUrlString = clientToReadUrl.DownloadString("https://api.coindesk.com/headerchart/history?currency=ETH");
                ethereumValues = ParseStockDataString(downloadedUrlString, currencyName);
                return ethereumValues;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Ethereum service extension function. Viewing Common Information has encountered a problem");
            }
        }

        private static Ethereum ParseStockDataString(string DownloadedString, string companyName)
        {
            try
            {
                Ethereum ethereum = new Ethereum();

                //Splitting string on the basis of ", } ] " tokens.
                string[] stringSeparatorHash = new string[] { ",", "]", "}" };
                string[] resultAfterSplittingDownloadedString = DownloadedString.Split(stringSeparatorHash, StringSplitOptions.None);

                string currentPriceString = resultAfterSplittingDownloadedString[11].ToString();

                //Adding values to Ethereum object and returning back
                ethereum.currencyName = companyName;
                ethereum.currentPrice = currentPriceString;
                return ethereum;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Ethereum service extension Parser. Viewing Common Information has encountered a problem");
            }
        }        
    }
}