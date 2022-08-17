using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Article.WebApi.Models;
using Article.WebApi.Providers;
using Article.WebApi.Results;
using Article.Services.Identity;
using Article.Common;
using Article.WebApi.ActionFilters;
using Article.Services.Dtos;
using Article.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Microsoft.Owin.Testing;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Article.WebApi.Controllers
{
    // [Authorize]

   
    [RoutePrefix("api/Account")]
    public class AccountController : ApiAuthorizeBaseController
    {
        private const string LocalLoginProvider = "Local";
        private IUserService _userService;
        //public AccountController()
        //{
        //}

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, IUserService userService) : base(userManager)
        {
            _userService = userService;
            AccessTokenFormat = accessTokenFormat;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }


        /// <summary>
        /// Получение информации о пользователе с помощью идентификатора пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        [Authorize(Roles = "AdminRole")]
        [HttpGet]
        [Route("~/api/Account/GetUserById/{id}")]
        public UserDto GetUserById(string id)
        {
               var model = _userService.GetById(getGuid(id));
            if (model != null)
                return model;
            throw new HttpResponseException(NotFoundMessage("لا يوجد مستخدم !"));
        }
        /// <summary>
        /// Получить всю информацию о пользователях
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "AdminRole")]
        [HttpGet]
        [Route("~/api/Account/GetAllUsers")]
        public List<UserDto> GetAllUsers()
        {
            var model = _userService.GetAll();
            if (model.Any())
                return model;
            throw new HttpResponseException(NotFoundMessage("لا يوجد مستخدمين !"));
        }
     

        /// <summary>
        /// Добавить обычного пользователя 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateModelAttribute]
       
        [Route("Register_User")]
        public async Task<IHttpActionResult> Register_User(RegisterUserDto model)
        {
            var user = new Article.Services.Identity.IdentityUser() {/* Email = model.Email,*/ UserName = model.UserName, CreationDate = Utils.ServerNow, FullName = model.FullName,IsActivated=true };

            IdentityResult result = await UserManager.CreateAsync(user, model.Password);

            UserManager.AddToRole(user.Id, Roles.UserRole);

            return Ok();
        }

        /// <summary>
        /// заблокировать обычного пользователя админом
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
      //  [ValidateModelAttribute]
        [Authorize(Roles = "AdminRole")]
        [Route("BlockUserd")]
        [HttpPost]
        public bool BlockUserd(Guid userId)
        {
            
            return _userService.BlockUserd(userId); ;
        }


        /// <summary>
        /// Переход от обычного пользователя к модератору 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "AdminRole")]
        [Route("ChangeUserToSupervisorRole")]
        public bool ChangeUserToSupervisorRole(Guid userId)
        {

            return _userService.ChangeUserToSupervisorRole(userId); 
        }
        /// <summary>
        /// Преобразование из обычного пользователя в писателя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "AdminRole")]
        [Route("ChangeUserToWriter")]
        public bool ChangeUserToWriter(Guid userId)
        {

            return _userService.ChangeUserToWriter(userId);
        }

        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        [Authorize]
        public async Task</*IHttpActionResult*/bool> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(NotValidMessage(ModelState));
            }

            IdentityResult result = await UserManager.ChangePasswordAsync(getCurrentUserGuid(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                //return GetErrorResult(result);
                return false;
            }
            return true;
            //return Ok();
        }

       
        /// <summary>
        ///  Удаление пользователя администратором
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        // [ValidateModelAttribute]
        [Authorize(Roles = "AdminRole")]
        [Route("DeleteUser")]
        public async Task<IHttpActionResult> DeleteUser(Guid userid)
        {
            _userService.Delete(userid);

            return Ok();
        }

        /// <summary>
        /// Авторизация пользователя по логину и паролю
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        [Route("LoginUser")]
        public async Task<JObject> LoginUser(string username,string password)
        {

            var tokenExpiration = TimeSpan.FromHours(336);
            
            IdentityUser user = await UserManager.FindAsync(username,password);
           // var managerId = _userService.GetManagerRestaurantIdByRestaurantId(RestaurantId);
            if (user == null)
                throw new HttpResponseException(NotFoundMessage("username or Password is not correct"));
            

            ///////////
            var userManager = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager;
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
              OAuthDefaults.AuthenticationType);
          

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, props);//////
            

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            int restaurantId = 0;
          
           

            JObject tokenResponse = new JObject(
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty("userName", username),
                                        new JProperty("UserId", user.Id),
                                        new JProperty("FullName", user.FullName),
                                        new JProperty("Role", user.Role.FirstOrDefault().Name),
                                        new JProperty("IsFromFacebook", false),
                                        new JProperty("issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty("expires", ticket.Properties.ExpiresUtc.ToString()),
                                        new JProperty("RestaurantId", restaurantId)
        );

            return tokenResponse;
        }

        /// <summary>
        /// Сброс пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "AdminRole")]
        [Route("SetPassword")]
        public async Task<bool> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(NotValidMessage(ModelState));
            }
            var user_ = _userService.GetByUserName(model.UserName);
            string token = UserManager.GenerateUserToken("ResetPassword", user_.UserId);

            UserManager.ResetPassword(user_.UserId, token, model.NewPassword);
            return true;
        }
        
        //
        private async Task<JObject> GenerateLocalAccessTokenResponse(string userName)
        {
            
            IdentityUser user = UserManager.FindByName(userName);
           
            var tokenExpiration = TimeSpan.FromHours(336);
            
            if (user == null)
                throw new HttpResponseException(NotFoundMessage("اسم المستخدم أو كلمة المرور خطأ"));


            ///////////
            var userManager = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager;
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
              OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);


            ///////


            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            identity.AddClaim(new Claim("role", user.Role.FirstOrDefault().Name));////////////////////

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, props);//////

            // var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty("userName", userName),
                                        new JProperty("UserId", user.Id),
                                        new JProperty("FullName", user.FullName),
                                        new JProperty("Role", user.Role.FirstOrDefault().Name),
                                        new JProperty("IsFromFacebook", true),
                                        new JProperty("issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty("expires", ticket.Properties.ExpiresUtc.ToString())
                                       // new JProperty("RestaurantManagerId", managerId)

        );

            return tokenResponse;
        }

        protected override void Dispose(bool disposing)
        {
            
            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }

  

  
}
