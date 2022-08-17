using Article.Common;
using Article.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Article.WebApi.Controllers
{
    [RoutePrefix("api/Validation")]
    public class ValidationController : ApiController
    {
        //private readonly IRestaurantService _iRestaurantService;
        //private readonly IProductsService _iproductService;
        private readonly IUserService _userService;

        public ValidationController(/*IRestaurantService iRestaurantService, IProductsService iproductService, */IUserService userService)
        {
            //_iRestaurantService = iRestaurantService;
            //_iproductService = iproductService;
            _userService = userService;
        }

        //[HttpGet]
        //public async Task<IHttpActionResult> IsEmailUnique(string email)
        //{
        //    bool validationResult = true;

        //    if (!String.IsNullOrEmpty(email))
        //    {
        //        validationResult = _userService.IsEmailUnique(email);
        //    }

        //    return Ok(validationResult);
        //}
        [HttpGet]
        public async Task<IHttpActionResult> IsUserNameUnique(string UserName)
        {
            bool validationResult = true;

            if (!String.IsNullOrEmpty(UserName))
            {
                validationResult = _userService.IsUserNameUnique(UserName);
            }

            return Ok(validationResult);
        }

        //[HttpGet]
        //public async Task<IHttpActionResult> IsProductIdExist(int id)
        //{
        //    bool validationResult = false;

        //    if (id != null)
        //    {
        //        validationResult = _iproductService.IsProductIdExist(id);
        //    }

        //    return Ok(validationResult);
        //}


        //[HttpGet]
        //public async Task<IHttpActionResult> IsRestaurantIdExist(int id)
        //{
        //    bool validationResult = false;

        //    if (id != null)
        //    {
        //        validationResult = _iproductService.IsRestaurantIdExist(id);
        //    }

        //    return Ok(validationResult);
        //}

        [Route("getserverTime")]
        [HttpGet]
        public string getserverTime()
        {

            return Utils.ServerNow.ToString();
        }




    }
}
