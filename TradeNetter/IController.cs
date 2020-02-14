using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeNetter
{
    public interface IController
    {
        Tuple<List<Trade>, List<Trade>, Dictionary<string, double>> ProcessTrades(List<Trade> Trades);
    }
}
