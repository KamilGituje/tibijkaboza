using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public class Item
    {
        public Item()
        {

        }

        public double DropRate { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Price { get; set; }
    }
}
