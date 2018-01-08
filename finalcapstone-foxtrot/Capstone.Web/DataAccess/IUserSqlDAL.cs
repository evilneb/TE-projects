using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;


namespace Capstone.Web.DataAccess
{
    public interface IUserSqlDAL
    {
        bool SaveUser(User user);

        bool UsernameExists(string username);

        bool PasswordIsCorrect(string username, string password);

        bool IsAdmin(string username);

        bool IsResearcher(string username);

        bool IsTechnician(string username);

        bool IsPartner(string username);

        bool ChangeUsersPassword(string username, string newPassword);


        //Methods Below are Not Used

        bool RemoveAdminRole(string username);

        bool RemoveResearchRole(string username);

        bool RemoveTechRole(string username);

        bool RemovePartnerRole(string username);

        bool AddAdminRole(string username);

        bool AddResearchRole(string username);

        bool AddTechRole(string username);

        bool AddPartnerRole(string username);


    }
}
