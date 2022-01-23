using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password can't be empty")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password requires at least 3 character")]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string GroupId = "CLIENT";
    }
}
