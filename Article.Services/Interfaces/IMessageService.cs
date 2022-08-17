using Market.Common;
using Market.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Interfaces
{
    public interface IMessageService
    {

        bool Add(MessageUserInfoDto dto);
        bool Edit(MessageUserInfoDto dto);
        bool Delete(Guid UserId);
        bool Add_Update(MessageUserInfoDto dto);
        bool IsUserExist_InMessageUserInfo_table(Guid UserId);
        //bool IsUserExist_InUser_table(Guid UserId);


        #region MessageConnectionGroups

        int Add_ConnectionToGroup(MessageConnectionGroupsDto dto, Guid UserId, string ConnectionId);
        string ConnectionUpdate(MessageConnectionGroupsDto dto, Guid UserId, string groupName, string connectionId);

        #endregion

        #region Messaging

        /// <summary>
        /// this function for admin
        /// get all message that sending by "SenderId"
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3 }</param>
        /// <returns></returns>
        List<MessageDto> GetAllMessageSending(Guid SenderId, MessageState messageState);
        /// <summary>
        /// this function for admin
        /// get all message that received by "ReceiverId"
        /// </summary>
        /// <param name="ReceiverId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3 ,,all Messages= 4 }</param>
        /// <returns></returns>
        List<MessageDto> GetAllMessageReceiving(Guid ReceiverId, MessageState messageState);
        /// <summary>
        /// this function for admin
        /// get conversation between two user 
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="SecondUserId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3,all Messages= 4  }</param>
        /// <returns></returns>
        List<MessageDto> GetConversation_Admin(Guid firstUserId, Guid SecondUserId, MessageState messageState);
        /// <summary>
        /// Get conversation between two user 
        /// </summary>
        /// <param name="firstUserId">The user Id of the person requesting </param>
        /// <param name="SecondUserId">>The user Id of the another person </param>
        /// <returns></returns>
        List<MessageDto> GetConversation_User(Guid firstUserId, Guid SecondUserId);

        /// <summary>
        /// Add new message
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="SenderMobileSystem"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        int AddMessage(Guid SenderId, string SenderMobileSystem, InputMessageDto dto);
        /// <summary>
        /// Delete message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <param name="DeletedByThisPerson">The User id who wants to delete this message, if delete by admin set it null</param>
        /// <returns></returns>
        bool DeleteMessage(int messageId, string DeletedByThisPerson);
        /// <summary>
        /// Delete conversation
        /// </summary>
        /// <param name="firstUser">First one User Id</param>
        /// <param name="SecondUser">Second one User Id</param>
        /// <param name="DeletedByThisPerson">The User id who wants to delete this message, if delete by admin set it null</param>
        /// <returns></returns>
        bool DeleteConversation(Guid firstUser, Guid SecondUser, string DeletedByThisPerson);
        /// <summary>
        /// Call this function when the message is received
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="ReceiverId">The user ID that received this message</param>
        /// <returns></returns>
        MessageDto MessageReceived(int messageId, Guid ReceiverId);
        /// <summary>
        /// Call this function when the message is seen by receiver 
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="ReceiverId">The user ID that received this message</param>
        /// <returns></returns>
        MessageDto MessageSeen(int messageId, Guid ReceiverId);
        /// <summary>
        /// Call this function if you want to know if user is online or not
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        bool IsOnline(Guid UserId);

        /// <summary>
        /// Call this function when user connect
        /// Get messages that not received
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Tuple from to list
        /// first list int for count of messages 
        /// second list MessageDto for first message
        /// </returns>
        Tuple<List<int>, List<MessageDto>> GetMessageNotReceived(Guid userId);
        /// <summary>
        /// Get end conversations
        /// </summary>
        /// <param name="firstUserId">The user Id of the person requesting </param>
        /// 
        /// <returns></returns>
        List<MessageDto> GetUserConversations(Guid firstUserId);

        #endregion

    }
}
