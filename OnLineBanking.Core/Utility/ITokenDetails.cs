using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
    public interface ITokenDetails
    {
       public  string GetId();
       public  string GetUserName();
       public string GetRoles();
    }
}
