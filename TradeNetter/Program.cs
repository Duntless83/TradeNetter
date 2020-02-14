using Autofac;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using TradeNetter.DependencyRegistry;

namespace TradeNetter
{
    class Program
    {
        static void Main(string[] args)
        {
            var trades = new RootObject();
            using (StreamReader r = new StreamReader("InputTrades.json"))
            {
                string json = r.ReadToEnd();
                trades = JsonConvert.DeserializeObject<RootObject>(json);
            }

            if (trades.Trades.Any())
            {
                var builder = new ContainerBuilder();
                builder.RegisterModule<AutofacRegistry>();
                var container = builder.Build();
                using (var scope = container.BeginLifetimeScope())
                {
                    var controller = scope.Resolve<IController>();
                    var result = controller.ProcessTrades(trades.Trades);

                    Console.WriteLine("You Hold The Below Positions:");
                    Console.WriteLine("");
                    Console.WriteLine("Direction|Quantity|Price|Underlying");
                    foreach (var buy in result.Item1)
                    {
                        Console.WriteLine($"{buy.Direction}|{buy.Quantity}|{buy.Price}|{buy.Underlying}");
                    }

                    foreach (var sell in result.Item2)
                    {
                        Console.WriteLine($"{sell.Direction}|{sell.Quantity}|{sell.Price}|{sell.Underlying}");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Your P&L's Are:");
                    foreach(var pnl in result.Item3)
                    {
                        Console.WriteLine($"{pnl.Key} = {pnl.Value}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
