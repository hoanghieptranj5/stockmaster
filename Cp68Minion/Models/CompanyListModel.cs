using System.Collections.Generic;
using StockMaster.Models.CoPhieu69;

namespace Cp68Minion.Models
{
    public class CompanyListModel
    {
        public KeyValuePair<int, string> Stc { get; set; }
        public List<Company> Companies { get; set; }
    }
}