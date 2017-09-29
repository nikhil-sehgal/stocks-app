using System;

namespace stock.Models
{
    interface IStock
    {
        Stock[] getPrice();
    }
    public class Stock
    {
        public String currentCompany { get; set; }
        public string currentPrice { get; set; }           
        public string lastDayClosingPrice { get; set; }     
        public decimal changeInPrice { get; set; }          
        public string changePercent { get; set; }           
        public string openToday { get; set; }               
        public string high { get; set; }                    
        public string low { get; set; }                     
        public string lastUpdateTime { get; set; }          
        public string lastUpdateDate { get; set; }          
    }
}