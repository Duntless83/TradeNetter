using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeNetter
{
    class Controller:IController
    {
        private List<Trade> _buyTrades;
        private List<Trade> _sellTrades;
        private readonly ITradeProcessor _tradeProcessor;

        public Controller(ITradeProcessor tradeProcessor)
        {
            _buyTrades = new List<Trade>();
            _sellTrades = new List<Trade>();
            _tradeProcessor = tradeProcessor;
        }
        public Tuple<List<Trade>, List<Trade>, Dictionary<string,double>> ProcessTrades(List<Trade> Trades)
        {
            _tradeProcessor.SplitNewTrades(Trades, _buyTrades, _sellTrades);

            var pnl = _tradeProcessor.NetTradesAndGetPnL(_buyTrades, _sellTrades);

            return new Tuple<List<Trade>, List<Trade>, Dictionary<string, double>>(_buyTrades, _sellTrades, pnl);
        }
    }
}
