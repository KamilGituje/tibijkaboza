using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TibiaModels.BL.Security
{
    public class UserAuthBase
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string BearerToken { get; set; }
    }
}