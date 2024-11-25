namespace Stocksapp.ServiceContracts
{
    public interface IFinnhubservice
    {
        public Dictionary<string, object> GetStockobj();

        Task<Dictionary<string, object>> GetStocksdata(string key);
    }
}
