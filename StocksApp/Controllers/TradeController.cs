using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Services;
using StocksApp.Models;
using System.Diagnostics;

namespace StocksApp.Controllers
{
    [Route("[controller]")]

    public class TradeController : Controller    
    {
        private readonly FinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IConfiguration _configuration;

        public TradeController(FinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
        }
    
        [Route("/")]
        [Route("[action]")]
        [Route("~/[controller]")]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(_tradingOptions.DefaultStockSymbol))
                _tradingOptions.DefaultStockSymbol = "MSFT";

            //get company profile from API server
            Dictionary<string, object>? companyProfileDictionary = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol);

            //get stock price quotes from API server
            Dictionary<string, object> stockQuoteDictionary = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol);

            StockTrade stockTrade = new StockTrade() { StockSymbol = _tradingOptions.DefaultStockSymbol };

            //load data from finnHubService into model object
            if (companyProfileDictionary != null && stockQuoteDictionary != null)
            {
                stockTrade = new StockTrade() 
                {
                    StockSymbol = Convert.ToString(companyProfileDictionary["ticker"]), 
                    StockName = Convert.ToString(companyProfileDictionary["name"]), 
                    Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString()) };
            }

            //Send Finnhub token to view
            //ViewBag.FinnhubToken = _configuration["FinnhubToken"];

            return View(stockTrade);
        }
    }
}
