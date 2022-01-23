using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    [Serializable]
    public class UserSession
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string GroupId { get; set; }
    }
}
