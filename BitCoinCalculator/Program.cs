using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of your Bitcoins:");
            float userCoins = float.Parse(Console.ReadLine());
            BitCoinRate currentBitcoin = GetRates();
            Console.WriteLine("Enter you preferred currency (USD, GBP, EUR):");
            string userCurrency = Console.ReadLine().ToUpper();

            switch (userCurrency)
            {
                case "USD":
                    float resultUSD = userCoins * currentBitcoin.bpi.USD.rate_float;
                    Console.WriteLine($"Your Bitcoins are worth {resultUSD} {currentBitcoin.bpi.USD.code}.");
                    Console.WriteLine(currentBitcoin.disclaimer);
                    break;

                case "GBP":
                    float resultGBP = userCoins * currentBitcoin.bpi.GBP.rate_float;
                    Console.WriteLine($"Your Bitcoins are worth {resultGBP} {currentBitcoin.bpi.GBP.code}.");
                    Console.WriteLine(currentBitcoin.disclaimer);
                    break;

                case "EUR":
                    float resultEUR = userCoins * currentBitcoin.bpi.EUR.rate_float;
                    Console.WriteLine($"Your Bitcoins are worth {resultEUR} {currentBitcoin.bpi.EUR.code}.");
                    Console.WriteLine(currentBitcoin.disclaimer);
                    break;

                default:
                    Console.WriteLine("Entered currency not valid.");
                    break;
            }

        }

        public static BitCoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitCoinRate bitcoin;

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRate>(response);
            }

            return bitcoin;
        }
    }
}
