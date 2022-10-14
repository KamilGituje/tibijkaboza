using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TibiaModels.BL
{
    [Table("users", Schema = "Security")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [ForeignKey("UserId")]
        public List<UserClaim> UserClaims { get; set; }
        public List<Character> Characters { get; set; }
    }
}
