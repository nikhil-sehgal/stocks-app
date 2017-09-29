using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock.Models
{
    interface IBitcoin
    {
        Bitcoin getPrice();
    }
    public class Bitcoin
    {
        public string currencyName { get; set; }
        public string currentPrice { get; set; }
    }
}
