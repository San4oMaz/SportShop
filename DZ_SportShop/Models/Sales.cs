using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Models
{
    class Sales
    {
        public int Id { get; set; }
        public string BayerName { get; set; }
        public int GoodId { get; set; }
        public int CountPurchasedGoods { get; set; }
        public double OverPrice { get; set; }
        public DateTime DateSale { get; set; }
        public int SoldManagerId { get; set; }
        
    }
}
