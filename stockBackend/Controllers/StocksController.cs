using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using stock.Models;
using System.Runtime.Caching;

namespace stock.Controllers
{
    public class StocksController : ApiController
    {       
        MemoryCache customStockCache = MemoryCache.Default; //Cache to store Data
        static readonly object _lock = new object();        //Lock to lock process

        [HttpGet]
        public HttpResponseMessage getStocks()
        {
            try
            {
                lock (_lock)
                {
                    ///
                    //Get stocks Data from cache and performing operations i.e
                    //Either return data from cache or call service from StocksExtension class.
                    ///
                    var stocksData = customStockCache.Get("stocks");

                    if (stocksData != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, stocksData);
                    }
                    else
                    {
                        StocksExtension obj = new StocksExtension();
                        stocksData = ((IStock)obj).getPrice();
                        customStockCache.Add("stocks", stocksData, DateTimeOffset.UtcNow.AddMinutes(30));
                        return Request.CreateResponse(HttpStatusCode.OK, stocksData);
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
