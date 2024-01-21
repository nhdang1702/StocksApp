﻿using StocksApp.DTO;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using StocksApp.Services.Helpers;

namespace StocksApp.Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
        }

        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null)
                throw new ArgumentNullException(nameof(buyOrderRequest));

            ValidationHelper.ModelValidation(buyOrderRequest);

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            buyOrder.BuyOrderID = Guid.NewGuid();

            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }
        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null)
                throw new ArgumentNullException(nameof(sellOrderRequest));

            ValidationHelper.ModelValidation(sellOrderRequest);

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            
            sellOrder.SellOrderID = Guid.NewGuid();

            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }


        public List<BuyOrderResponse> GetBuyOrders()
        {
            return _buyOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .Select(temp => temp.ToBuyOrderResponse()).ToList();
        }


        public List<SellOrderResponse> GetSellOrders()
        {
            return _sellOrders.OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .Select(temp => temp.ToSellOrderResponse()).ToList();
        }


    }
}
