using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCsvParser
{
    public class CurrencyProvider
    {

        public CurrencyProvider()
        {
           var bittrexCurrencyDbContext = new BittrexCurrencyDbContext();

        }

        public void SaveCurrencyRecords(List<Currency> currencies)
        {
            var bittrexCurrencyDbContext = new BittrexCurrencyDbContext();

            bittrexCurrencyDbContext.CurrencyDatas.AddRange(currencies.ToArray());
            bittrexCurrencyDbContext.SaveChanges();

            bittrexCurrencyDbContext.Dispose();
        }

        public void ClearBase()
        {

        }
    }
}
