using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using System.Linq;
using Newtonsoft.Json;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.WebApi;
using UAParser;
using System.Collections.Generic;
using Article.Data;
using Article.Domain;
using Article.WebApi.Providers;

namespace SignalRChat
{
    /// <summary>
    /// Use authorization mode by add new header with Authorization key
    /// Use User-Agent mode by add new header with User-Agent key and "a" for value
    /// </summary>
    //[Microsoft.AspNet.SignalR.Authorize]  
   // [CustomAuthorize_SignalR]  
    public class ChatHub : Hub
    {
        //private readonly IMessageService _MessageService;
        private readonly IUnitOfWork _UnitOfWork;
        //private readonly INotificationService _NotificationService;
        //private readonly IUserService _UserService;
        //public ChatHub(IMessageUserInfoService MessageUserInfoServic):base()
        //{

        //    _MessageUserInfoServic = MessageUserInfoServic;
        //}
        public ChatHub()
        {
            // var service =(IMessageService) DependencyResolver.Current.GetService(typeof(IMessageService));
            //  _MessageService = DependencyResolver.Current.GetService<MessageService>();
            //_UnitOfWork = new UnitOfWork("Data Source=server\\SQLEXPRESS;Initial Catalog=NawafizApp2;Persist Security Info=True;User ID=khalil;Password=123xxx123");
            _UnitOfWork = new UnitOfWork("DefaultConnection");

            //_MessageService = new MessageService(_UnitOfWork);
            //_NotificationService = new NotificationService(_UnitOfWork);
            //_UserService = new UserService(_UnitOfWork);
        }

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
          
        }

        public async Task say(string user)
        {
            await Clients.All.SendAsync("ReceiveMessage", user);
        }

        /// <summary>
        /// Call this function when you want to send message to user
        /// </summary>
        /// <param name="msg">This param is Json represent InputMessageDto <para name="InputMessageDto"></para> </param>
        /// <returns></returns>
        //public int Send_to_User(InputMessageDto msg)
        //{

        //       // var resInputmessage = JsonConvert.DeserializeObject<InputMessageDto>(msg);
        //       var UserIdSender = Context.User.Identity.GetUserId();
        //    System.Collections.Generic.List<string> GroupCollector = new System.Collections.Generic.List<string>();
        //    GroupCollector.Add(UserIdSender);
        //    GroupCollector.Add(msg.ReceiverId.ToString());
        //   // string s= Context.User.Identity.Name;
        //    var UserId =Guid.Parse(UserIdSender);
        //    var senderInfo= _UserService.GetById(UserId);

        //    var de=  Context.Request.Headers["User-Agent"];
        //    var uaParser = Parser.GetDefault();
        //    ClientInfo c = uaParser.Parse(de);

        //   // Guid.TryParse(User.Identity.GetUserId(), out result);
        //    var IsOnline = _MessageService.IsOnline(UserId);
        //    var messgeId= _MessageService.AddMessage(UserId, c.OS.ToString(), msg);
        //    var outmessage = new OutputMessageDto() { Id=messgeId,MessageMobileId=msg.MessageMobileId,ReceiverId=msg.ReceiverId, SendingDate=Utils.ServerNow, Body = msg.Body, IsFile = msg.IsFile, SenderId = UserId, SenderName= senderInfo.FullName };

        //    //string json = JsonConvert.SerializeObject(msg);
        //    if (IsOnline)
        //    {
        //       // var outMessageJson = JsonConvert.SerializeObject(outmessage);


        //    }
        //    else
        //    {
        //        // Send notification to this user
        //       var Restaurant_helper= _UnitOfWork.RestaurantsRepository.FindBy(m => m.UserId == UserId);
        //        if (Restaurant_helper.Any())
        //        {

        //            NotificationdictionaryDto dto = new NotificationdictionaryDto();

        //            dto.RestaurantId = Restaurant_helper.FirstOrDefault().Id;
        //            dto.Type = (int)NotificationType.Message;
        //            dto.SenderId = UserId;
        //            dto.SenderName = senderInfo.FullName;
        //            dto.Body = msg.Body;
        //            _NotificationService.Send(dto, msg.ReceiverId);
        //        }
        //        ////inpu
        //        ////_NotificationService.se
        //    }
        //    Clients.Groups(GroupCollector).testing(/*outMessageJson*/outmessage);
        //    return messgeId;
        //}
        //  [ValidateModelAttribute]
        /// <summary>
        /// Call this function one time when you connect with this hub
        /// </summary>
        /// <param name="imei"></param>
        /// /// <returns>Tuple from to list
        /// first list int for count of messages 
        /// second list MessageDto for first message
        /// </returns>
        //public async Task<Tuple<List<int>, List<MessageDto>>> JoinGroup(string imei)
        //{
        //    //MessageConnectionGroupsDto dto = new MessageConnectionGroupsDto() { IMEI = imei};
        //    // Important note
        //    // search in connection user table about this Group name and IMEI and if they present, 
        //    // Update Connection_Id field into "CContext.onnectionId" and remove old conectionId 
        //    // from Groups by this statment "await Groups.Remove(ConnectionId, group);"; 
        //    // ConnectionId : old connection Id that we got it from connection user table
        //    // and then add the new connection Id into the group by this statment " await Groups.Add(Context.ConnectionId, group);"
        //    //

        //    string UserId = Context.User.Identity.GetUserId();
        //    var _old_ConnectionId = _MessageService.ConnectionUpdate(dto, Guid.Parse(UserId), UserId, Context.ConnectionId);
        //    try
        //    {
        //        if (_old_ConnectionId != "")
        //          await  Groups.Remove(_old_ConnectionId, UserId);
        //    }
        //    catch { }
        //    await Groups.Add(Context.ConnectionId, UserId);

        //    // Send message that not received by this
        //    return _MessageService.GetMessageNotReceived(Guid.Parse(UserId));
        //    //string msg = "Hi " + "All";
        //    //Clients.Group(UserId).testing( msg);


        //}
       

        public override Task OnConnected()
        {
            
            //MessageUserInfoDto dto = new MessageUserInfoDto() { ConnectionDate = Utils.ServerNow, IsOnline = true, UserId = Guid.Parse(Context.User.Identity.GetUserId()) };
            //_MessageService.Add_Update(dto);
            var result = default(Guid);
            string UseerId = Context.User.Identity.GetUserId();
            // Guid.TryParse(User.Identity.GetUserId(), out result);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            // Important note
            // search in Connection user table about this "Context.ConnectionId" and remove it 
            //
            //MessageUserInfoDto dto = new MessageUserInfoDto() { DisconnectionDate = Utils.ServerNow, IsOnline = false, UserId = Guid.Parse(Context.User.Identity.GetUserId()) };
            //_MessageService.Add_Update(dto);

            return base.OnDisconnected(stopCalled);
        }
    }
}