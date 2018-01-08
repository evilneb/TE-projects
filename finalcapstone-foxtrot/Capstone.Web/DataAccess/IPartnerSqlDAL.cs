using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Web.Mvc;

namespace Capstone.Web.DataAccess
{
    public interface IPartnerSqlDAL
    {
        bool AddPartner(Partner partner);

        bool PartnerExists(string username);

        //string GetPartner(string username);

        bool AssignPartnerUser(AssignPartnerUser partnerUser);

        SelectList GetUserList();

        SelectList GetPartnerList();

        
    }
}
