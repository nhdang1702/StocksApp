using StocksApp.Models;
using System.ComponentModel.DataAnnotations;

namespace StocksApp.DTO
{
    public class BuyOrderResponse
    {
        public Guid BuyOrderID { get; set; }
        public string StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }

        public uint Quantity { get; set; }


        public double Price { get; set; }

        public double TradeAmount { get; set; }

        /// <summary>
        /// Checks if the current object and other (parameter) object values match
        /// </summary>
        /// <param name="obj">Other object of BuyOrderResponse class, to compare</param>
        /// <returns>True or false determines whether current object and other objects match</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not BuyOrderResponse) return false;

            BuyOrderResponse other = (BuyOrderResponse)obj;
            return BuyOrderID == other.BuyOrderID && StockSymbol == other.StockSymbol && StockName == other.StockName && DateAndTimeOfOrder == other.DateAndTimeOfOrder && Quantity == other.Quantity && Price == other.Price;
        }

   
        public override int GetHashCode()
        {
            return StockSymbol.GetHashCode();
        }

       
        public override string ToString()
        {
            return $"Buy Order ID: {BuyOrderID}, Stock Symbol: {StockSymbol}, Stock Name: {StockName}, Date and Time of Buy Order: {DateAndTimeOfOrder.ToString("dd MMM yyyy hh:mm ss tt")}, Quantity: {Quantity}, Buy Price: {Price}, Trade Amount: {TradeAmount}";
        }
    }


    public static class BuyOrderExtensions
    {
        
        public static BuyOrderResponse ToBuyOrderResponse(this BuyOrder buyOrder)
        {
            return new BuyOrderResponse() 
            {
                BuyOrderID = buyOrder.BuyOrderID, 
                StockSymbol = buyOrder.StockSymbol, 
                StockName = buyOrder.StockName, 
                Price = buyOrder.Price, 
                DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder, 
                Quantity = buyOrder.Quantity, 
                TradeAmount = buyOrder.Price * buyOrder.Quantity };
        }
    }
}
