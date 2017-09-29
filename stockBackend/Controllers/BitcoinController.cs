using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using stock.Models;
using System.Runtime.Caching;

namespace stock.Controllers
{
    public class BitcoinController : ApiController
    {
        MemoryCache customBitcoinCache = MemoryCache.Default; //Cache to store Data
        static readonly object _lock = new object();        //Lock to lock process

        [HttpGet]
        public HttpResponseMessage getBitcoinStocks()
        {
            try
            {
                lock (_lock)
                {
                    ///
                    //Get stocks Data from cache and performing operations i.e
                    //Either return data from cache or call service from StocksExtension class.
                    ///
                    var BitcoinStockData = customBitcoinCache.Get("BitcoinStock");
                    if (BitcoinStockData != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, BitcoinStockData);
                    }
                    else
                    {
                        BitcoinExtension obj = new BitcoinExtension();
                        BitcoinStockData = ((IBitcoin)obj).getPrice();
                        customBitcoinCache.Add("BitcoinStock", BitcoinStockData, DateTimeOffset.UtcNow.AddMinutes(30));
                        return Request.CreateResponse(HttpStatusCode.OK, BitcoinStockData);
                    }
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
