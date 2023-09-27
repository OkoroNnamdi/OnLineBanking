using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Utility
{
  public interface ITokenServices
   {

       public  string CreateToken(UserModel user);
      // public  string CreateToken(ManagerRequest request);
       public   RefreshToken SetRefreshToken();
    }
}
