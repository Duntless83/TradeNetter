using System;
using System.Collections.Generic;

namespace TradeNetter
{
    public class Trade
    {
        public string Direction { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Underlying { get; set; }
    }

    public class RootObject
    {
        public List<Trade> Trades { get; set; }
    }
}
