using Card.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface INotificationService
    {

        #region Send Notify
        Task<int> Send(NotificationdictionaryDto dto);


        /// <summary>
        /// This for admin !!!!  
        /// Delete Notify By id
        /// </summary>
        /// <param name="Id">Notify Id</param>
        /// <returns></returns>
        bool DeleteNotify(int Id);


        #endregion



        #region GetNotify
        /// <summary>
        /// Get notifications 
        /// </summary>
        /// <returns></returns>
        List<NotificationdictionaryDto> GetNotifications(int page, int pageSize);

        #endregion


        #region  InputMessaging

        /// <summary>
        /// Add new message from admin to user
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        Task<bool> AddMessage(MessagingDto dto);


        /// <summary>
        /// Delete Message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        bool DeleteMessage(int messageId);

      

       

        /// <summary>
        /// Get All Messages, 
        /// Note: this function for admin
        /// </summary>
        /// <returns></returns>
        List<MessagingDto> GetAllMessages( int pageSize, int page);


        /// <summary>
        /// Get All Messages for specific user, 
        /// Note: this function for user
        /// </summary>
        /// <returns></returns>
        List<MessagingDto> GetAllMessages_forUser(Guid UserId, int pageSize, int page);



        #endregion


    }
}
