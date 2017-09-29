using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using stock.Models;

namespace stock
{
    public class StocksExtension : IStock
    {
        //Variables
        static decimal lastTransaction = 0;
        static Dictionary<int, string> dictionary;
        string downloadedUrlString = null;
        int stocksCount;
        WebClient clientToReadUrl; // Client to read URL data.
        Stock[] totalStocksValues; 
        List<int> list;

        static StocksExtension()
        {
            //Dictionary to store name and company numbers
            dictionary = new Dictionary<int, string>();
            dictionary.Add(500180, "Hdfc Bank");
            dictionary.Add(500325, "Reliance");
            dictionary.Add(500510, "larsen & tourbo ltd");
            dictionary.Add(532401, "Vijaya Bank");
            dictionary.Add(532215, "Axis Bank");
            dictionary.Add(532461, "Pnb");
            dictionary.Add(500820, "Asian Paint");
            dictionary.Add(532885, "Central Bank");
            dictionary.Add(500875, "ITC");
            dictionary.Add(532174, "ICICI");
            dictionary.Add(532500, "Maruti Suzuki");
            dictionary.Add(570001, "Tata Motors DVR");
        }

        /// <summary>
        /// Fucntion returning Data as array of Stock class.
        /// </summary>
        /// <returns></returns>
        Stock[] IStock.getPrice()
        {
            try
            {
                stocksCount = dictionary.Count;
                totalStocksValues = new Stock[stocksCount];
                list = new List<int>(dictionary.Keys);
                //Performing parallel processing for fetching data from service
                Parallel.For(0, stocksCount, i =>
                {
                    clientToReadUrl = new WebClient();
                    downloadedUrlString = clientToReadUrl.DownloadString("Service URL" + list[i]);
                    totalStocksValues[i] = ParseStockDataString(downloadedUrlString, dictionary[list[i]]);
                });
                return totalStocksValues;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service Extension Function. Viewing Common Information has encountered a problem");
            }
        }

        private static Stock ParseStockDataString(string DownloadedString, string companyName)
        {
            try
            {
                Stock stock = new Stock();
                ///Data from Sensex comes in below form.
                // BSE##B#As on 22 Sep 17 | 16:00@C#3@P#@HL#1227.75,1228.00,1228.00,1176.80,1184.90

                //Splitting string on the basis of "# , @ | " tokens.
                string[] stringSeparatorHash = new string[] { "#", ",", "@", "|" };
                string[] resultAfterSplittingDownloadedString = DownloadedString.Split(stringSeparatorHash, StringSplitOptions.None);

                string currentPriceString = resultAfterSplittingDownloadedString[14].ToString();
                string lastDayClosingPriceString = resultAfterSplittingDownloadedString[10].ToString();

                decimal currentPriceDecimal = decimal.Parse(currentPriceString.ToString());
                decimal lastDayClosingPriceDecimal = decimal.Parse(lastDayClosingPriceString.ToString());
                decimal changeInPrice = currentPriceDecimal - lastDayClosingPriceDecimal;
                decimal percntChangeDecimal = Math.Round(decimal.Parse(((changeInPrice / lastDayClosingPriceDecimal) * 100).ToString()), 2);


                //Adding values to stock object and returning back
                stock.currentCompany = companyName;
                stock.changeInPrice = changeInPrice;
                lastTransaction = currentPriceDecimal;
                stock.currentPrice = currentPriceString;
                stock.changePercent = percntChangeDecimal.ToString("0.00");
                stock.lastDayClosingPrice = lastDayClosingPriceDecimal.ToString();
                stock.openToday = resultAfterSplittingDownloadedString[11].ToString();
                stock.high = resultAfterSplittingDownloadedString[12].ToString();
                stock.low = resultAfterSplittingDownloadedString[13].ToString();
                stock.lastUpdateDate = resultAfterSplittingDownloadedString[3].ToString();
                stock.lastUpdateTime = resultAfterSplittingDownloadedString[4].ToString();
                return stock;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in service Extension Parser. Viewing Common Information has encountered a problem");
            }
        }
    }
}