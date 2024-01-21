using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StocksApp.DTO;
using StocksApp.ServiceContracts;
using StocksApp.Services;


namespace Tests
{
    public class StocksServiceTest
    {
        private readonly IStocksService _stocksService;

        public StocksServiceTest()
        {
            _stocksService = new StocksService();
        }
        [Fact]
        public void CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

    }
}
