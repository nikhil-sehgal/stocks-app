using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using stock.Models;
using System.Runtime.Caching;

namespace stock.Controllers
{
    public class EthereumController : ApiController
    {
        MemoryCache customEthereumCache = MemoryCache.Default; //Cache to store Data
        static readonly object _lock = new object();        //Lock to lock process

        [HttpGet]
        public HttpResponseMessage getEthereumStocks()
        {
            try
            {
                lock (_lock)
                {
                    ///
                    //Get stocks Data from cache and performing operations i.e
                    //Either return data from cache or call service from EthereumExtension class.
                    ///
                    var EthereumStockData = customEthereumCache.Get("EthereumStock");
                    if (EthereumStockData != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, EthereumStockData);
                    }
                    else
                    {
                        EthereumExtension obj = new EthereumExtension();
                        EthereumStockData = ((IEthereum)obj).getPrice();
                        customEthereumCache.Add("EthereumStock", EthereumStockData, DateTimeOffset.UtcNow.AddMinutes(30));
                        return Request.CreateResponse(HttpStatusCode.OK, EthereumStockData);
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
