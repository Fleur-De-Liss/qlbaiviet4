using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EF
{
    [Table("Credentials")]
    [Serializable]
    public partial class Credential
    {
        [Key]
        public string GroupId { get; set; }
        public string RoleId { get; set; }

        public virtual Group Group { get; set; }
        public virtual Role Role { get; set; }
    }
}
