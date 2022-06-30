using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibia
{
    public static class Utility
    {
        public static bool Drop(double dropRate)
        {
            bool drop = false;
            Random rng = new Random();
            double rngNumber = rng.NextDouble();
            if(rngNumber <= dropRate)
            {
                drop = true;
            }
            return drop;
        }
    }
}
