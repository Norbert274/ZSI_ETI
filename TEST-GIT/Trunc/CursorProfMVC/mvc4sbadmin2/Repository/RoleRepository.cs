using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using nclprospekt.Models;
using nclprospekt.DAL;
using Dapper;
using Ninject;

namespace nclprospekt.Repository
{

    public class RoleRepository:IRoleRepository
    {

        #region "Query Methods"
        public Role FindById(int id)
        {
            Role rola = null;
            //w SuperZSI jest pobieranie roli po sesji

            //using (nclSQLConnection cn = Connection)
            //{
            //    cn.Open();
            //    rola = cn.SqlConn.Query<Role>("SELECT [Uzytkownik_Id],[Imie],[Nazwisko],[Nazwa],[Haslo],[Email],[IsAdmin] ,[CreatedOn],[ModifiedOn] FROM [dbo].[UZYTKOWNIK] WHERE [UZYTKOWNIK_ID]=@ID", new { ID = id }).SingleOrDefault();
            //}
            
            return rola;
        }

        public Role GetByRoleName(string username)
        {
            Role rola = null;

            //using (nclSQLConnection cn = Connection)
            //{
            //    cn.Open();
            //    rola = cn.SqlConn.Query<Role>("SELECT u.[Uzytkownik_Id],[Imie],[Nazwisko],[Nazwa],isnull(m.[Password],'') as Haslo,isnull(m.[Email],'') as Email,[IsAdmin], " +
            //    "u.[CreatedOn],u.[ModifiedOn] FROM [dbo].[UZYTKOWNIK] u LEFT JOIN [dbo].[MEMBERSHIP] m ON m.Uzytkownik_Id = u.Uzytkownik_id " + 
            //    "where u.LoweredNazwa=@NAZWA", new { NAZWA = (username).ToLower() }).SingleOrDefault();
            //}

            return rola;
        }

        public IEnumerable<Role> GetAll()
        {
            IEnumerable<Role> uzytkownicy = null;

            //using (nclSQLConnection cn = Connection)
            //{
            //    cn.Open();
            //    uzytkownicy = cn.SqlConn.Query<Role>("SELECT [Uzytkownik_Id],[Imie],[Nazwisko],[Nazwa],[Haslo],[Email],[IsAdmin] ,[CreatedOn],[ModifiedOn] FROM [dbo].[UZYTKOWNIK]");

            //}
            return uzytkownicy;
        }

        public IEnumerable<Uzytkownik> GetUsersForRole(string roleName)
        {
            return GetUsersForRole(GetByRoleName(roleName));
        }

        public IEnumerable<Uzytkownik> GetUsersForRole(int id)
        {
            return GetUsersForRole(FindById(id));
        }

        public IEnumerable<Uzytkownik> GetUsersForRole(Role role)
        {
            return null;
        }
        #endregion

    }
}