using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username can't be empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can't be empty")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
