using Article.Data;
using Article.Domain;
using Article.Services.Interfaces;
using Article.Services.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace Article.Services.Identity
{
    public class EmailService : IIdentityMessageService
    {
        private IUnitOfWork _UnitOfWork;
      

        public Task SendAsync(IdentityMessage message)
        {
            return null;
               
        }

          

    }
}
