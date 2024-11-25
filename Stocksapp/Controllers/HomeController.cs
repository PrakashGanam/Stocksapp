using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stocksapp.Models;
using Stocksapp.Services;

namespace Stocksapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly Finnhubservice _finnhubservice;
        private readonly IOptions<TradingOptions> _tradingoptions;

        public HomeController(Finnhubservice finnhubservice, IOptions<TradingOptions> tradingoptions)
        {
            _finnhubservice = finnhubservice;
            _tradingoptions = tradingoptions;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            string val = string.Empty;
            if (_tradingoptions.Value.DefaultStockSymbol == null) {
                val = "MSFT";
            }
             
            Dictionary<string, Object> retval = await _finnhubservice.GetStocksdata(val);

            Stocks stocks = new Stocks() {
            StockSymbol = _tradingoptions.Value.DefaultStockSymbol,
            CurrentPrice =Convert.ToDouble(retval["c"].ToString()),
            LowestPrice = Convert.ToDouble(retval["l"].ToString()),
            HighestPrice = Convert.ToDouble(retval["l"].ToString()),
            OpenPrice = Convert.ToDouble(retval["pc"].ToString())
            };
            return View(stocks);
        }
    }
}
