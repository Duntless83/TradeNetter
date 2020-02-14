using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeNetter
{
    public interface ITradeProcessor
    {
        void SplitNewTrades(List<Trade> tradeList, List<Trade> buyTrades, List<Trade> sellTrades);

        Dictionary<string, double> NetTradesAndGetPnL(List<Trade> buyTrades, List<Trade> sellTrades);
    }
}
