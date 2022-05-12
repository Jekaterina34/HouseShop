using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseShop.Models.House
{
    public class HouseViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public string Condition { get; set; }
        public DateTime CreatedAT { get; set; }
        public DateTime ModifiedAT { get; set; }
    }
}
