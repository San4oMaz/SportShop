using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ.Models
{
    class Managers
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public override string ToString()
        {
            return $"{ManagerName}";
        }
    }
}
