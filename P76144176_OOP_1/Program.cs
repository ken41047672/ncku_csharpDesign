using System.Globalization;

namespace P76144176_OOP_1
{
    internal class Program
    {
        public delegate void PriceChangeHandler(int newPrice);
        public class Stock
        {
            public Stock()
            {
                price = 500;
            }
            private int price;

            public event PriceChangeHandler OnPriceChanged;

            public int Price
            {
                get => price;
                set
                {
                    if (price != value)
                    {
                        price = value;
                        OnPriceChanged?.Invoke(value);
                    }
                }
            }
        }

        public class Trader
        {
            public static void BuyAlert(int newPrice)
            {
                Console.WriteLine("--- 價格更新 ---");
                Console.WriteLine("買入時機! 股票： 台積電， 新價格: {0,0:C}", newPrice);
            }

            public static void SellAlert(int newPrice)
            {
                Console.WriteLine("賣出時機! 股票： 台積電， 新價格: {0,0:C}\n", newPrice);
            }
        }

        static void Main(string[] args)
        {

            Stock stock = new Stock();

            stock.OnPriceChanged += Trader.BuyAlert;
            stock.OnPriceChanged += Trader.SellAlert;

            Console.WriteLine("台積電 初始價格: {0,0:C}\n", stock.Price);


            while (true)
            {
                Console.Write("請輸入新價格(或輸入 \'quit\' 離開): ");
                string input = Console.ReadLine();
                if (input == "quit") {
                    break;
                }
                try
                {
                    int newPrice = int.Parse(input);
                    stock.Price = newPrice; 
                }
                catch (Exception e) { }
            }

            Console.WriteLine("程式結束。");
        }
    }
}
