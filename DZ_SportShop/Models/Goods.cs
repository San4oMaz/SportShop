using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Models
{
    class Goods
    {
        public int Id { get; set; }
        public string GoodsName { get; set; }
        public int CountGoods { get; set; }
        public double PriceStart { get; set; }
        public int TypeId { get; set; }
    }
}
