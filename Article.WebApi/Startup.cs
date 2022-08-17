using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Hangfire;
using System.Threading;
using System.Threading.Tasks;
using Article.Services.Services;
using Article.Services.Interfaces;
using Article.Data;
using Article.Domain;
using Article.WebApi.Controllers;
using Article.Services.Identity;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(Article.WebApi.Startup))]

namespace Article.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);           
        }

       
    }
}
