using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
namespace nclprospekt.Repository
{ 
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role GetByRoleName(string username);
    }
}