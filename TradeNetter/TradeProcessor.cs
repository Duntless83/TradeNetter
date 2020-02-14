using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeNetter
{
    class TradeProcessor : ITradeProcessor
    {
        public Dictionary<string, double> NetTradesAndGetPnL(List<Trade> buyTrades, List<Trade> sellTrades)
        {
            //will sum the quantity on each side so will iterate over the side that has the most volume
            var buySum = buyTrades.Sum(x => x.Quantity);
            var sellSum = sellTrades.Sum(x => x.Quantity);

            var pnlDic = new Dictionary<string, double>();

            if (buySum > sellSum)
            {
                foreach (var buyTrade in buyTrades)
                {
                    foreach(var sellTrade in sellTrades)
                    {
                        if (buyTrade.Quantity == 0 || sellTrade.Quantity == 0)
                            continue;

                        if (sellTrade.Underlying == buyTrade.Underlying)
                        {
                            if (buyTrade.Quantity <= sellTrade.Quantity)
                            {
                                UpdatePnlDictionary(pnlDic, -buyTrade.Quantity, buyTrade.Price, buyTrade.Quantity, sellTrade.Price, buyTrade.Underlying);
                                sellTrade.Quantity = sellTrade.Quantity - buyTrade.Quantity;
                                buyTrade.Quantity = 0;
                            }
                            else
                            {
                                UpdatePnlDictionary(pnlDic, -sellTrade.Quantity, buyTrade.Price, sellTrade.Quantity, sellTrade.Price, buyTrade.Underlying);
                                buyTrade.Quantity = buyTrade.Quantity - sellTrade.Quantity;
                                sellTrade.Quantity = 0;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach(var sellTrade in sellTrades)
                {
                    foreach(var buyTrade in buyTrades)
                    {
                        if (buyTrade.Quantity == 0 || sellTrade.Quantity == 0)
                            continue;

                        if(buyTrade.Underlying == sellTrade.Underlying)
                        {
                            if (buyTrade.Quantity <= sellTrade.Quantity)
                            {
                                UpdatePnlDictionary(pnlDic, -buyTrade.Quantity, buyTrade.Price, buyTrade.Quantity, sellTrade.Price, buyTrade.Underlying);
                                sellTrade.Quantity = sellTrade.Quantity - buyTrade.Quantity;
                                buyTrade.Quantity = 0;              
                            }
                            else
                            {
                                UpdatePnlDictionary(pnlDic, -sellTrade.Quantity, buyTrade.Price, sellTrade.Quantity, sellTrade.Price, buyTrade.Underlying);
                                buyTrade.Quantity = buyTrade.Quantity - sellTrade.Quantity;
                                sellTrade.Quantity = 0;
                            }
                        }
                    }
                }
            }

            buyTrades.RemoveAll(x => x.Quantity == 0);
            sellTrades.RemoveAll(x => x.Quantity == 0);
            return pnlDic;
        }

        private void UpdatePnlDictionary(Dictionary<string, double> pnlDic, int qty1, double prc1, int qty2, int prc2, string underlying)
        {
            var pnl = CalculatePnl(qty1, prc1, qty2, prc2);
            if (!pnlDic.ContainsKey(underlying))
                pnlDic.Add(underlying, pnl);
            else
                pnlDic[underlying] = pnlDic[underlying] + pnl;
        }

        private double CalculatePnl(int qty1, double prc1, int qty2, int prc2)
        {
            return (qty1 * prc1) + (qty2 * prc2);
        }

        public void SplitNewTrades(List<Trade> tradeList, List<Trade> buyTrades, List<Trade> sellTrades)
        {
            buyTrades.AddRange(tradeList.Where(x => x.Direction.ToLower() == "buy").ToList());
            sellTrades.AddRange(tradeList.Where(x => x.Direction.ToLower() == "sell").ToList());
        }
    }
}
