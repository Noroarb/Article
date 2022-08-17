using AutoMapper;
using Market.Domain;
using Market.Common;
using Market.Domain.Entities;
using Market.Services.Dtos;
using Market.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Xml;
using System.Net;
using Newtonsoft.Json;
using FirebaseNet.Exceptions;
using FirebaseNet.Messaging;
using FirebaseNet.Serialization;

namespace Market.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
     
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MessageService()
        {
        }



        #region MessageUserInfo
        
        public bool Add(MessageUserInfoDto dto)
        {
            var new_userInfo = Mapper.Map<MessageUserInfoDto, MessageUserInfo>(dto);
            //new_userInfo.ConnectionDate = DateTime.Parse("2/2/2017");
            //new_userInfo.DisconnectionDate = DateTime.Parse("2/2/2017");
            //new_userInfo.IsOnline = true;
            //new_userInfo.UserId = Guid.Parse("8520EEED-DE32-47E2-A31F-6DF47C004D80");
            
            _unitOfWork.MessageUserInfoRepository.Add(new_userInfo);
            _unitOfWork.SaveChanges();
            return true;

        }

        public bool Edit(MessageUserInfoDto dto)
        {
            //var _userInfo = Mapper.Map<MessageUserInfoDto, MessageUserInfo>(dto);
            var _userInfo = _unitOfWork.MessageUserInfoRepository.FindBy(m => m.UserId == dto.UserId).SingleOrDefault();
            _userInfo.ConnectionDate = dto.ConnectionDate;
            _userInfo.DisconnectionDate = dto.DisconnectionDate;
            _userInfo.IsOnline = dto.IsOnline;
           // _userInfo.UserId = dto.UserId;
            _unitOfWork.MessageUserInfoRepository.Update(_userInfo);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool Delete(Guid UserId)
        {
            var _userInfo = _unitOfWork.MessageUserInfoRepository.FindBy(m=>m.UserId==UserId).SingleOrDefault();
            _unitOfWork.MessageUserInfoRepository.Remove(_userInfo);
            _unitOfWork.SaveChanges();
            return true;
        }

        /// <summary>
        /// this function Check if this user is in "MessageUserInfoDto" 
        ///  if there is, the value occurs, otherwise a new user is created
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool Add_Update(MessageUserInfoDto dto)
        {
            var IsUserExist_InMessageUserInfo_table_var = _unitOfWork.MessageUserInfoRepository.FindBy(m => m.UserId == dto.UserId);
            if (IsUserExist_InMessageUserInfo_table_var.Any())
            {
                //Edit(dto);
                var _userInfo = IsUserExist_InMessageUserInfo_table_var.SingleOrDefault();
                _userInfo.ConnectionDate = dto.ConnectionDate;
                _userInfo.DisconnectionDate = dto.DisconnectionDate;
                _userInfo.IsOnline = dto.IsOnline;
                // _userInfo.UserId = dto.UserId;
                _unitOfWork.MessageUserInfoRepository.Update(_userInfo);
                _unitOfWork.SaveChanges();

            }
            else
            {
                Add(dto);
            }

            return true;
        }

        public bool IsUserExist_InMessageUserInfo_table(Guid UserId)
        {
            return _unitOfWork.MessageUserInfoRepository.FindBy(m => m.UserId == UserId).Any();       
        }

        //public bool IsUserExist_InUser_table(Guid UserId)
        //{
        //    return _unitOfWork.MessageUserInfoRepository.FindBy(m => m.UserId == UserId).Any();
        //}

        #endregion



        #region MessageConnectionGroups

        public int Add_ConnectionToGroup(MessageConnectionGroupsDto dto, Guid UserId, string ConnectionId)
        {
            var _connectionGroup = Mapper.Map<MessageConnectionGroupsDto, MessageConnectionGroups>(dto);
            _connectionGroup.UserId = UserId;
            _connectionGroup.ConnectionId = ConnectionId;
            _connectionGroup.GroupName = UserId.ToString();

            _unitOfWork.MessageConnectionGroupsRepository.Add(_connectionGroup);
            _unitOfWork.SaveChanges();

            return _connectionGroup.Id;

        }


        public string ConnectionUpdate(MessageConnectionGroupsDto dto, Guid UserId, string groupName, string connectionId)
        {
            var _connectionGroup = _unitOfWork.MessageConnectionGroupsRepository.FindBy(m => m.IMEI == dto.IMEI);
            if (_connectionGroup.Any())
            {
                var one_connectionGroup = _connectionGroup[0];
                var ConncetionIdReturned = one_connectionGroup.ConnectionId;
                one_connectionGroup.ConnectionId = connectionId;
                _unitOfWork.MessageConnectionGroupsRepository.Update(one_connectionGroup);

                _unitOfWork.SaveChanges();

                return ConncetionIdReturned;
            }
            else
            {
                Add_ConnectionToGroup(dto, UserId, connectionId);
                return "";
            }
        }

        #endregion



        #region Messaging

        /// <summary>
        /// this function for admin ,not use this now
        /// get all messages that sending by "SenderId"
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3 }</param>
        /// <returns></returns>
        public List<MessageDto> GetAllMessageSending(Guid SenderId,MessageState messageState)
        {
            List<Messaging> messages = new List<Messaging>();
            if (messageState ==MessageState.Present)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.SenderId == SenderId).Where(q => q.DeletedByAdmin == false && q.DeletedByReciever == false && q.DeletedBySender == false).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByAdmin)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.SenderId == SenderId).Where(q => q.DeletedByAdmin).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByReceiver)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.SenderId == SenderId).Where(q => q.DeletedByReciever).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedBySender)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.SenderId == SenderId).Where(q => q.DeletedBySender).OrderByDescending(m => m.ReceivedDate).ToList();
            else
                return new List<MessageDto>();

            var returned_value= Mapper.Map<List<Messaging>, List<MessageDto>>(messages);

            int index_help = 0;
            foreach(var one_returned_value in returned_value)
            {
               
                one_returned_value.SenderName = messages[index_help].Sender.FullName;
                one_returned_value.ReceiverName = messages[index_help].Receiver.FullName;
                one_returned_value.Receiverstatus = messages[index_help].Receiver.MessagingUserInfo.IsOnline;
                if (!one_returned_value.Receiverstatus)
                    one_returned_value.DisconnectDate = messages[index_help].Receiver.MessagingUserInfo.DisconnectionDate;
                else
                    one_returned_value.DisconnectDate = new DateTime();

            }

            return returned_value;

        }

        /// <summary>
        /// this function for admin ,not use this now
        /// get all message that received by "ReceiverId"
        /// </summary>
        /// <param name="ReceiverId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3 ,all Messages= 4  }</param>
        /// <returns></returns>
        public List<MessageDto> GetAllMessageReceiving(Guid ReceiverId, MessageState messageState)
        {
            List<Messaging> messages = new List<Messaging>();
            if (messageState == MessageState.Present)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == ReceiverId).Where(q => q.DeletedByAdmin == false && q.DeletedByReciever == false && q.DeletedBySender == false).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByAdmin)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == ReceiverId).Where(q => q.DeletedByAdmin).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByReceiver)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == ReceiverId).Where(q => q.DeletedByReciever).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedBySender)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == ReceiverId).Where(q => q.DeletedBySender).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.AllMessages)
                messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == ReceiverId).OrderByDescending(m => m.ReceivedDate).ToList();

            else
                return new List<MessageDto>();

            var returned_value = Mapper.Map<List<Messaging>, List<MessageDto>>(messages);

            int index_help = 0;
            foreach (var one_returned_value in returned_value)
            {
                one_returned_value.SenderName = messages[index_help].Sender.FullName;
                one_returned_value.ReceiverName = messages[index_help].Receiver.FullName;
                one_returned_value.Receiverstatus = messages[index_help].Receiver.MessagingUserInfo.IsOnline;
                if (!one_returned_value.Receiverstatus)
                    one_returned_value.DisconnectDate = messages[index_help].Receiver.MessagingUserInfo.DisconnectionDate;
                else
                    one_returned_value.DisconnectDate = new DateTime();

            }

            return returned_value;
        }

        /// <summary>
        /// this function for admin
        /// get conversation between two user 
        /// </summary>
        /// <param name="firstUserId"></param>
        /// <param name="SecondUserId"></param>
        /// <param name="messageState">MessageState { Present = 0, DeletedByAdmin = 1, DeletedBySender = 2, DeletedByReceiver = 3,all Messages= 4 }</param>
        /// <returns></returns>
        public List<MessageDto> GetConversation_Admin(Guid firstUserId, Guid SecondUserId, MessageState messageState)
        {
            List<Messaging> messages = new List<Messaging>();
            
            if (messageState == MessageState.Present)
                messages = _unitOfWork.MessagingRepository.FindBy(m =>( m.ReceiverId == firstUserId && m.SenderId== SecondUserId) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId)).Where(q => q.DeletedByAdmin == false && q.DeletedByReciever == false && q.DeletedBySender == false).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByAdmin)
                messages = _unitOfWork.MessagingRepository.FindBy(m => (m.ReceiverId == firstUserId && m.SenderId == SecondUserId) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId)).Where(q => q.DeletedByAdmin).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedByReceiver)
                messages = _unitOfWork.MessagingRepository.FindBy(m => (m.ReceiverId == firstUserId && m.SenderId == SecondUserId) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId)).Where(q => q.DeletedByReciever).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.DeletedBySender)
                messages = _unitOfWork.MessagingRepository.FindBy(m => (m.ReceiverId == firstUserId && m.SenderId == SecondUserId) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId)).Where(q => q.DeletedBySender).OrderByDescending(m => m.ReceivedDate).ToList();
            else if (messageState == MessageState.AllMessages)
                messages = _unitOfWork.MessagingRepository.FindBy(m => (m.ReceiverId == firstUserId && m.SenderId == SecondUserId) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId)).OrderByDescending(m => m.ReceivedDate).ToList();

            else
                return new List<MessageDto>();

            var returned_value = Mapper.Map<List<Messaging>, List<MessageDto>>(messages);

            int index_help = 0;
            foreach (var one_returned_value in returned_value)
            {
                one_returned_value.SenderName = messages[index_help].Sender.FullName;
                one_returned_value.ReceiverName = messages[index_help].Receiver.FullName;
                one_returned_value.Receiverstatus = messages[index_help].Receiver.MessagingUserInfo.IsOnline;
                if (!one_returned_value.Receiverstatus)
                    one_returned_value.DisconnectDate = messages[index_help].Receiver.MessagingUserInfo.DisconnectionDate;
                else
                    one_returned_value.DisconnectDate = new DateTime();

            }

            return returned_value;
        }

        /// <summary>
        /// Get conversation between two user 
        /// </summary>
        /// <param name="firstUserId">The user Id of the person requesting </param>
        /// <param name="SecondUserId">>The user Id of the another person </param>
        /// <returns></returns>
        public List<MessageDto> GetConversation_User(Guid firstUserId, Guid SecondUserId)
        {
           
             var messages = _unitOfWork.MessagingRepository.FindBy(m =>( m.ReceiverId == firstUserId && m.SenderId == SecondUserId && !m.DeletedByReciever ) || (m.SenderId == firstUserId && m.ReceiverId == SecondUserId && !m.DeletedBySender)).OrderByDescending(m=>m.ReceivedDate).ToList();

            var returned_value = Mapper.Map<List<Messaging>, List<MessageDto>>(messages);

            int index_help = 0;
            foreach (var one_returned_value in returned_value)
            {
                one_returned_value.SenderName = messages[index_help].Sender.FullName;
                one_returned_value.ReceiverName = messages[index_help].Receiver.FullName;
                one_returned_value.Receiverstatus = messages[index_help].Receiver.MessagingUserInfo.IsOnline;
                if (!one_returned_value.Receiverstatus)
                    one_returned_value.DisconnectDate = messages[index_help].Receiver.MessagingUserInfo.DisconnectionDate;
                else
                    one_returned_value.DisconnectDate = new DateTime();

            }

            return returned_value;
        }

        /// <summary>
        /// Add new message
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="SenderMobileSystem"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int AddMessage(Guid SenderId,string SenderMobileSystem, InputMessageDto dto)
        {
        
               var new_message = Mapper.Map<InputMessageDto, Messaging>(dto);
            new_message.DeletedByAdmin = new_message.DeletedByReciever= new_message.IsReceived= new_message.IsSeen = new_message.DeletedBySender = false;
            new_message.SenderId = SenderId;
            new_message.SenderMobileSystem = SenderMobileSystem;
            new_message.SendingDate = new_message.ReceivedDate= Utils.ServerNow;

            _unitOfWork.MessagingRepository.Add(new_message);
            _unitOfWork.SaveChanges();

            return new_message.Id;
        }


        /// <summary>
        /// Delete message
        /// </summary>
        /// <param name="messageId">message id</param>
        /// <param name="DeletedByThisPerson">The User id who wants to delete this message, if delete by admin set it null</param>
        /// <returns></returns>
        public bool DeleteMessage(int messageId,string DeletedByThisPerson)
        {
            var new_message = _unitOfWork.MessagingRepository.FindBy(m=>m.Id==messageId);
            if (new_message.Any())
            {
                var old_message = new_message.FirstOrDefault();

                if (DeletedByThisPerson != null && DeletedByThisPerson != "")
                {
                    
                    if (old_message.SenderId == Guid.Parse(DeletedByThisPerson))
                    {
                        old_message.DeletedBySender = true;
                    }
                    else if(old_message.ReceiverId == Guid.Parse(DeletedByThisPerson))
                    {
                        old_message.DeletedByReciever = true;
                    }
                    else
                    {
                        return false;
                    }
                    if (old_message.DeletedByReciever && old_message.DeletedBySender )
                    {
                        _unitOfWork.MessagingRepository.Remove(old_message);
                        _unitOfWork.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    //old_message.DeletedByAdmin = true;
                    _unitOfWork.MessagingRepository.Remove(old_message);
                    _unitOfWork.SaveChanges();
                    return true;
                }
                _unitOfWork.MessagingRepository.Update(old_message);
                _unitOfWork.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
          

           
        }

        /// <summary>
        /// Delete conversation
        /// </summary>
        /// <param name="firstUser">First one User Id, I ,Sender person</param>
        /// <param name="SecondUser">Second one User Id, another person, Receiver person</param>
        /// <param name="DeletedByThisPerson">The User id who wants to delete this message, if delete by admin set it null</param>
        /// <returns></returns>
        public bool DeleteConversation(Guid firstUser, Guid SecondUser, string DeletedByThisPerson)
        {
            var messages = _unitOfWork.MessagingRepository.FindBy(m => m.SenderId == firstUser && m.ReceiverId== SecondUser);
            if (DeletedByThisPerson == "" || DeletedByThisPerson == null)
            {
                for (int index = 0; index < messages.Count; ++index)
                {
                    // one_message.DeletedByAdmin = true;
                    _unitOfWork.MessagingRepository.Remove(messages[index]);
                }
            }
           else if (messages.Where(m=>m.SenderId== Guid.Parse(DeletedByThisPerson)).Any())
            {
                for(int index=0;index< messages.Count;++index)
                {
                    messages[index].DeletedBySender = true;
                    if (messages[index].DeletedByReciever && messages[index].DeletedBySender)
                    {
                        _unitOfWork.MessagingRepository.Remove(messages[index]);
                        //_unitOfWork.SaveChanges();
                        //return true;
                    }
                    else
                       _unitOfWork.MessagingRepository.Update(messages[index]);
                }
            }
            else if (messages.Where(m => m.ReceiverId == Guid.Parse(DeletedByThisPerson)).Any())
            {
                for (int index = 0; index < messages.Count; ++index)
                {
                    messages[index].DeletedByReciever = true;
                    if (messages[index].DeletedByReciever && messages[index].DeletedBySender)
                    {
                        _unitOfWork.MessagingRepository.Remove(messages[index]);
                        //_unitOfWork.SaveChanges();
                        //return true;
                    }
                    else
                        _unitOfWork.MessagingRepository.Update(messages[index]);
                }
            }
            //else if (DeletedByThisPerson=="" || DeletedByThisPerson ==null )
            //{
            //    for (int index = 0; index < messages.Count; ++index)
            //    {
            //        // one_message.DeletedByAdmin = true;
            //        _unitOfWork.MessagingRepository.Remove(messages[index]);
            //    }
            //}
            else
            {
                return false;
            }

            messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == firstUser && m.SenderId == SecondUser);
            if (DeletedByThisPerson == "" || DeletedByThisPerson == null)
            {
                for (int index = 0; index < messages.Count; ++index)
                {
                    // one_message.DeletedByAdmin = true;
                    _unitOfWork.MessagingRepository.Remove(messages[index]);
                }
            }
            else
            if (messages.Where(m => m.SenderId == Guid.Parse(DeletedByThisPerson)).Any())
            {
                for (int index = 0; index < messages.Count; ++index)
                {
                    messages[index].DeletedBySender = true;
                    if (messages[index].DeletedByReciever && messages[index].DeletedBySender)
                    {
                        _unitOfWork.MessagingRepository.Remove(messages[index]);
                        //_unitOfWork.SaveChanges();
                        //return true;
                    }
                    else
                        _unitOfWork.MessagingRepository.Update(messages[index]);
                }
            }
            else if (messages.Where(m => m.ReceiverId == Guid.Parse(DeletedByThisPerson)).Any())
            {
                for (int index = 0; index < messages.Count; ++index)
                {
                    messages[index].DeletedByReciever = true;
                    if (messages[index].DeletedByReciever && messages[index].DeletedBySender)
                    {
                        _unitOfWork.MessagingRepository.Remove(messages[index]);
                        //_unitOfWork.SaveChanges();
                        //return true;
                    }
                    else
                        _unitOfWork.MessagingRepository.Update(messages[index]);
                }
            }
           
            else
            {
                return false;
            }

            _unitOfWork.SaveChanges();
            return true;

        }

        /// <summary>
        /// Call this function when the message is received
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="ReceiverId">The user ID that received this message</param>
        /// <returns></returns>
        public MessageDto MessageReceived(int messageId,Guid ReceiverId)
        {
            
            var message = _unitOfWork.MessagingRepository.FindBy(m => m.Id == messageId && m.ReceiverId == ReceiverId);
            if(message.Any())
            {
                var one_message = message.FirstOrDefault();
                one_message.IsReceived = true;
                one_message.ReceivedDate = Utils.ServerNow;

                _unitOfWork.MessagingRepository.Update(one_message);

                _unitOfWork.SaveChanges();
                return Mapper.Map<Messaging,MessageDto>(one_message);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Call this function when the message is seen by receiver 
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="ReceiverId">The user ID that received this message</param>
        /// <returns></returns>
        public MessageDto MessageSeen(int messageId, Guid ReceiverId)
        {
            var message = _unitOfWork.MessagingRepository.FindBy(m => m.Id == messageId && m.ReceiverId == ReceiverId);
            if (message.Any())
            {
                var one_message = message.FirstOrDefault();
                if (!one_message.IsReceived)
                {
                    one_message.IsReceived = true;
                    one_message.ReceivedDate = Utils.ServerNow;
                }
                one_message.IsSeen = true;
                one_message.ShowingDate = Utils.ServerNow;
                _unitOfWork.MessagingRepository.Update(one_message);

                _unitOfWork.SaveChanges();
                return Mapper.Map<Messaging, MessageDto>(one_message);
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// Call this function if you want to know if user is online or not
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public bool IsOnline(Guid UserId)
        {
           
            var messageUserInfo = _unitOfWork.MessageUserInfoRepository.FindBy(m => m.UserId ==UserId);
            if (messageUserInfo.Any())
                return messageUserInfo.FirstOrDefault().IsOnline;
            else
                return false;
        }

        /// <summary>
        /// Call this function when user connect
        /// Get messages that not received
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Tuple from to list
        /// first list int for count of messages 
        /// second list MessageDto for first message
        /// </returns>
        public Tuple<List<int>, List<MessageDto>> GetMessageNotReceived(Guid userId)
        {
            var messages = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == userId && !m.IsReceived)/*.Where(m => !m.DeletedByAdmin)*/.OrderBy(m=>m.ReceivedDate).ToList();
            
          //  return Mapper.Map<List<Messaging>,List<MessageDto>>(messages);
            if(messages.Any())
            {
                List<Guid> senders = new List<Guid>();
                foreach(var one_message in messages)
                {
                    if (!senders.Contains(one_message.SenderId))
                    {
                        senders.Add(one_message.SenderId);
                    }
                }

                List<MessageDto> message_sending = new List<MessageDto>();
                List<int> messages_count = new List<int>();
                foreach(var one_sender in senders)
                {
                    var messsages_thisSender = messages.Where(m => m.SenderId == one_sender).ToList();
                    messages_count.Add(messsages_thisSender.Count);
                    message_sending.Add(new MessageDto()
                    {
                        Body = messsages_thisSender[0].Body,

                        Id = messsages_thisSender[0].Id,
                        //IsFile = messsages_thisSender[0].IsFile,
                        ReceiverName = "",
                        SenderId = messsages_thisSender[0].SenderId,
                        SenderName = messsages_thisSender[0].Sender.FullName,
                        SendingDate = messsages_thisSender[0].SendingDate,
                        MessageMobileId= messsages_thisSender[0].MessageMobileId

                    });
                }
                return Tuple.Create(messages_count, message_sending);

            }
            return Tuple.Create(new List<int>() { 0 }, new List<MessageDto>() { new Dtos.MessageDto() });
        }

        /// <summary>
        /// Get end conversations
        /// </summary>
        /// <param name="firstUserId">The user Id of the person requesting </param>
        /// 
        /// <returns></returns>
        public List<MessageDto> GetUserConversations(Guid firstUserId)
        {

           var messages = _unitOfWork.MessagingRepository.FindBy(m => (m.ReceiverId == firstUserId ) || (m.SenderId == firstUserId)).OrderByDescending(m => m.ReceivedDate).ToList();
            var target_messages = new List<Messaging>();
            foreach (var single_message in messages)
            {
                if ((single_message.SenderId == firstUserId && (!single_message.DeletedBySender )))
                {
                    if (!target_messages.Where(m => ((m.SenderId == single_message.SenderId) && (m.ReceiverId == single_message.ReceiverId))
                    || ((m.SenderId == single_message.ReceiverId) && (m.ReceiverId == single_message.SenderId))).Any())
                    {
                        target_messages.Add(single_message);
                    }
                }
                else
                if (single_message.ReceiverId == firstUserId && (!single_message.DeletedByReciever))
                {
                    if (!target_messages.Where(m => ((m.SenderId == single_message.SenderId) && (m.ReceiverId == single_message.ReceiverId))
                    || ((m.SenderId == single_message.ReceiverId) && (m.ReceiverId == single_message.SenderId))).Any())
                    {
                        target_messages.Add(single_message);
                    }
                }
               
            }
            var returned_value = Mapper.Map<List<Messaging>, List<MessageDto>>(target_messages);

            int index_help = 0;
            foreach (var one_returned_value in returned_value)
            {
                one_returned_value.SenderName = target_messages[index_help].Sender.FullName;
                one_returned_value.ReceiverName = target_messages[index_help].Receiver.FullName;
                one_returned_value.Receiverstatus = target_messages[index_help].Receiver.MessagingUserInfo.IsOnline;
                if (!one_returned_value.Receiverstatus)
                    one_returned_value.DisconnectDate = target_messages[index_help].Receiver.MessagingUserInfo.DisconnectionDate;
                else
                    one_returned_value.DisconnectDate = new DateTime();

                index_help++;

            }

            return returned_value;
        }

        #endregion

    }
}
