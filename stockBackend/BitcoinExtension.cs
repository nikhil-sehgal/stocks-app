using System;
using stock.Models;
using System.Net;

namespace stock
{
    public class BitcoinExtension : IBitcoin
    {
        //Variables
        static String currencyName = "Bitcoin";
        string downloadedUrlString = null;
        WebClient clientToReadUrl; //Client to read URL data.
        Bitcoin bitcoinValues;

        /// <summary>
        /// Fucntion returning Data as array of Stock class.
        /// </summary>
        /// <returns></returns>
        Bitcoin IBitcoin.getPrice()        
        {
            try
            {
                clientToReadUrl = new WebClient();
                downloadedUrlString = clientToReadUrl.DownloadString("https://api.coindesk.com/headerchart/history?currency=BTC");
                bitcoinValues = ParseStockDataString(downloadedUrlString, currencyName);
                return bitcoinValues;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Bitcoin service extension function. Viewing Common Information has encountered a problem");
            }
        }

        private static Bitcoin ParseStockDataString(string DownloadedString, string companyName)
        {
            try
            {
                Bitcoin bitcoin = new Bitcoin();

                //Splitting string on the basis of ", } ] " tokens.
                string[] stringSeparatorHash = new string[] { ",", "]", "}" };
                string[] resultAfterSplittingDownloadedString = DownloadedString.Split(stringSeparatorHash, StringSplitOptions.None);

                string currentPriceString = resultAfterSplittingDownloadedString[11].ToString();

                //Adding values to Bitcoin object and returning back
                bitcoin.currencyName = companyName;
                bitcoin.currentPrice = currentPriceString;
                return bitcoin;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Bitcoin service extension Parser. Viewing Common Information has encountered a problem");
            }
        }        
    }
}