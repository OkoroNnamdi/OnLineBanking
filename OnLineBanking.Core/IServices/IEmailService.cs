using OnLineBanking.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.IServices
{
   public  interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }

   
}
