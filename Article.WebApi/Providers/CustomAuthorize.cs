using Article.Data;
using Article.Domain;
using Article.Services.Interfaces;
using Article.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Article.WebApi.Providers
{
    public class CustomAuthorize_SignalR:   Microsoft.AspNet.SignalR.AuthorizeAttribute
    {

      //  private readonly ITokensService _ITokensService;
        private readonly IUnitOfWork _UnitOfWork;
        private string Resource { get; set; } = null;
        private string Action { get; set; } = null;
        public CustomAuthorize_SignalR(string resource = null, string action = null)
        {
            Resource = resource;
            Action = action;
            _UnitOfWork = new UnitOfWork("DefaultConnection");
           // _ITokensService = new TokensService(_UnitOfWork);
        }


        public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
       {
            var x = request.Headers.Where(m => m.Key == "Authorization").FirstOrDefault().Value;
            x = x.Replace("Bearer ", "");
            //var y = x.Parameter;
            //if (!_ITokensService.IsTokenExist(x))
            //{
            //    return false;
            //    //request.GetHttpContext().Error.HResult = 1;
            //    //re.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
            //    //{
            //    //    ReasonPhrase = "Unauthorized Database",
            //    //    Content = new StringContent(" \"Message\": \"Authorization has been denied for this request.\"")

            //    //};
            //}

            //Check your post authorization logic using Resource and Action
            //Your logic here to return authorize or unauthorized response 
            return base.AuthorizeHubConnection(hubDescriptor, request);
        }

    }
}
    //public class CustomAuthorize : System.Web.Http.AuthorizeAttribute
    //{
    //    private readonly ITokensService _ITokensService;
    //    private readonly IUnitOfWork _UnitOfWork;
    //    private string Resource { get; set; } = null;
    //    private string Action { get; set; } = null;
    //    public CustomAuthorize(string resource = null, string action = null)
    //    {
    //        Resource = resource;
    //        Action = action;
    //        _UnitOfWork = new UnitOfWork("DefaultConnection");
    //        _ITokensService = new TokensService(_UnitOfWork);
    //    }
    //    public override void OnAuthorization(
    //           System.Web.Http.Controllers.HttpActionContext actionContext)
    //    {
    //       var x= actionContext.Request.Headers.Authorization;
    //       var y= x.Parameter;
    //        if(!_ITokensService.IsTokenExist(y))
    //        {
    //            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
    //            {
    //                ReasonPhrase = "Unauthorized Database",
    //                Content = new StringContent(" \"Message\": \"Authorization has been denied for this request.\"")

    //            };
    //        }
    //        base.OnAuthorization(actionContext);
    //        //Check your post authorization logic using Resource and Action
    //        //Your logic here to return authorize or unauthorized response 
    //    }
    //}
