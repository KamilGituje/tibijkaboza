using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TibiaModels.BL.Security;

namespace TibiaModels.BL
{
    [Table("userClaims", Schema = "Security")]
    public class UserClaim
    {
        [Key]
        public int UserClaimId { get; set; }
        public string ClaimType { get; set; }
        [NotMapped]
        public bool? ClaimValue { get; set; } = false;
        [ForeignKey("UserClaimId")]
        public List<User> Users { get; set; }
    }
}