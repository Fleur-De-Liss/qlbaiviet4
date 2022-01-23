using Models.Common;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDAO
    {
        QLBaiVietDbContext db = null;
        public UserDAO()
        {
            db = new QLBaiVietDbContext();
        }
        public User GetByUsername(string username)
        {
            return db.Users.SingleOrDefault(x => x.Username == username);
        }
        public List<string> GetListCredential(string username)
        {
            var user = db.Users.Single(x => x.Username == username);
            var data = (from a in db.Credentials
                       join b in db.Groups on a.GroupId equals b.GroupId
                       join c in db.Roles on a.RoleId equals c.RoleId
                       where b.GroupId == user.GroupId
                       select new
                       {
                           GroupId = a.GroupId,
                           RoleId = a.RoleId
                       }).AsEnumerable().Select(x => new Credential()
                       {
                           GroupId = x.GroupId,
                           RoleId = x.RoleId
                       });
            return data.Select(x => x.RoleId).ToList();
        }
        public int Login(string username, string password, bool isAdmin = false)
        {
            var res = db.Users.SingleOrDefault(x => x.Username == username);
            if (res == null)
            {
                return 0;
            }
            else
            {
                if(isAdmin == true)
                {
                    if(res.GroupId == CommonConstants.ADMIN_GROUP || res.GroupId == CommonConstants.MOD_GROUP)
                    {
                        if (res.Password == password)
                        {
                            return 1;
                        }
                        else return -1;
                    }
                    else
                    {
                        return -2;
                    }
                }
                else
                {
                    if (res.Password == password)
                    {
                        return 1;
                    }
                    else return -1;
                }
            }
        }
        public int Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.UserId;
        }
        public bool checkUsername(string username)
        {
            return db.Users.Count(x => x.Username == username) > 0;
        }
        public bool checkEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
