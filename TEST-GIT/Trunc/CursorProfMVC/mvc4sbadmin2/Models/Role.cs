using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nclprospekt.Models
{
    public class Role
    {
        protected Role() { }
        public Role(int roleid, string roleName,  string roleDescription)
        {
            RoleId = roleid;
            Name = roleName;
            Description = roleDescription;
        }
        public virtual int RoleId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual IList<Uzytkownik> Users { get; set; }
    }
}