using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TibiaModels.BL.Security
{
    public class AppClaim
    {
        public bool CanAccessChars { get; set; }
        public bool CanAccessBackpack { get; set; }
        public bool CanAccessExp { get; set; }
        public bool CanAccessSell { get; set; }
    }
}