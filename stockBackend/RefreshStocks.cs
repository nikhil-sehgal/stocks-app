using System;
using System.Runtime.Caching;
using stock.Models;

namespace stock
{
    public class RefreshStocks
    {
        static readonly object _lock = new object();
        static MemoryCache customStockCache = MemoryCache.Default;
        static int cacheTime = 30;

        public static void refreshBSEStock()
        {
            StocksExtension obj = new StocksExtension();
            var result = customStockCache["stocks"];
            lock (_lock)
            {
                var stocksData = ((IStock)obj).getPrice();
                if (result != null)
                {
                    customStockCache.Remove("stocks");
                    customStockCache.Add("stocks", stocksData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
                }
                else
                    customStockCache.Add("stocks", stocksData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
            }
        }
        public static void refreshBitcoin()
        {
            BitcoinExtension obj = new BitcoinExtension();
            var result = customStockCache["BitcoinStock"];
            lock (_lock)
            {
                var BitcoinData = ((IBitcoin)obj).getPrice();
                if (result != null)
                {
                    customStockCache.Remove("BitcoinStock");
                    customStockCache.Add("BitcoinStock", BitcoinData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
                }
                else
                    customStockCache.Add("BitcoinStock", BitcoinData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
            }
        }
        public static void refreshEthereum()
        {
            EthereumExtension obj = new EthereumExtension();
            var result = customStockCache["EthereumStock"];
            lock (_lock)
            {
                var EthereumData = ((IEthereum)obj).getPrice();
                if (result != null)
                {
                    customStockCache.Remove("EthereumStock");
                    customStockCache.Add("EthereumStock", EthereumData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
                }
                else
                    customStockCache.Add("EthereumStock", EthereumData, DateTimeOffset.UtcNow.AddMinutes(cacheTime));
            }
        }
    }
}