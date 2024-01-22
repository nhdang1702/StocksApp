using Microsoft.EntityFrameworkCore;
using StocksApp.Data;
using StocksApp.DTO;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.Services.Helpers;

namespace StocksApp.Services
{
    public class StocksService : IStocksService
    {
        private readonly StockDbContext _dbContext;

        public StocksService(StockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _dbContext.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder.ToBuyOrderResponse();
        }
        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            
            sellOrder.SellOrderID = Guid.NewGuid();

            _dbContext.Add(sellOrder);
            await _dbContext.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }


        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            List<BuyOrder> buyOrders = await _dbContext.BuyOrders
             .OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .ToListAsync();

            return buyOrders.Select(temp => temp.ToBuyOrderResponse()).ToList();
        }


        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            List<SellOrder> sellOrders = await _dbContext.SellOrders
            .OrderByDescending(temp => temp.DateAndTimeOfOrder)
            .ToListAsync();

            return sellOrders.Select(temp => temp.ToSellOrderResponse()).ToList();
        }


    }
}
