using AutoMapper;
using Card.Common;
using Card.Domain;
using Card.Domain.Entities;
using Card.Services.Dtos;
using Card.Services.Interfaces;
using FirebaseNet.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Services
{
    public class NotificationService : INotificationService
    {
        //private readonly IUnitOfWork _unitOfWork;
        //private string Server_Key = "";
        //public NotificationService(IUnitOfWork unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    //   Server_Key = "qAAAc7A6ATw:APA91bGDfjOMrKALJo4dC0ldgW2f11cFumvO6_latseyb8gpNF6SsH0GJhf2XntUq4XskfqR1_tgtjq73cx0r8hhQhNzCOgs1tUPx5sX_TU_fV00HhHDJTk2XWUsKsRsGkmrpV9b1Y_m";
        //    //Server_Key = _unitOfWork.RestaurantsRepository.FindById(RestaurantId).FirebaseKey;
        //}

        //#region Send Notify
        //public async Task<int> Send(NotificationdictionaryDto dto, Guid? UserId)
        //{
        //    int returned_value = 0;
        //    //Server_Key = _unitOfWork.RestaurantsRepository.FindById(dto.RestaurantId).FirebaseKey;

        //    if (UserId.HasValue)
        //    {
        //        // var User_fireToken = _unitOfWork.UserRepository.FindBy(m => m.UserId == UserId).SelectMany(m => m.FireBases).Where(q => q.IsActive).Select(w => w.FToken).ToList();
        //        //if (User_fireToken.Any())
        //        //{
        //        //var new_notify = Mapper.Map<NotificationdictionaryDto, Notification>(dto);
        //        //new_notify.NotifyDate = Utils.ServerNow;
        //        //new_notify.UserId = UserId;
        //        //new_notify.IsSent = false;
        //        //_unitOfWork.NotificationRepository.Add(new_notify);
        //        //_unitOfWork.SaveChanges();
        //        //  dto.Id = new_notify.Id;

        //          await Send_for_one(dto, ((Guid)UserId).ToString() /*User_fireToken*/);
        //            returned_value = 0;

        //       // }
        //    }
        //    else
        //    {

        //        var new_notify = Mapper.Map<NotificationdictionaryDto, Notification>(dto);
        //        new_notify.NotifyDate = Utils.ServerNow;
        //    if(new_notify.Image=="")
        //        new_notify.Image = "ClotheNotify.png";
        //       // new_notify.UserId = null;
        //        _unitOfWork.NotificationRepository.Add(new_notify);
        //        _unitOfWork.SaveChanges();
        //        dto.Id = new_notify.Id;
        //        await send_notify_forAllUsers(dto);
        //        returned_value = new_notify.Id;

        //    }

        //    return returned_value;
        //}
        //private async Task<bool> Send_for_one(NotificationdictionaryDto dto, string firebaseToken /*List<string> firebaseToken*/)
        //{
        //    FCMClient client = new FCMClient(Server_Key);


        //    var dic = new Dictionary<string, string>();
        //    PropertyInfo[] infos = dto.GetType().GetProperties();
        //    foreach (var info in infos)
        //    {
        //        dic.Add(info.Name, info.GetValue(dto, null).ToString());
        //    }
        //    try
        //    {
        //        //foreach (var one_firebaseToken in firebaseToken)
        //        //{

        //            var message_Android = new Message()
        //            {

        //                Priority = MessagePriority.high,
        //                TimeToLive = 864000,//this 10 days
        //                To = "/topics/"+firebaseToken,

        //                Data = dic

        //            };

        //            var result = await client.SendMessageAsync(message_Android);

        //            var message_IOS = new Message()
        //            {
        //                Priority = MessagePriority.high,
        //                TimeToLive = 864000,//this 10 days
        //                To = "/topics/" + firebaseToken,
        //                Data = dic
        //            };

        //            var result2 = await client.SendMessageAsync(message_IOS);

        //        //}
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //private async Task<bool> send_notify_forAllUsers(NotificationdictionaryDto dto)
        //{
        //    var client1 = new FCMClient(Server_Key);


        //    //var message = new FirebaseNet.Messaging.Message()
        //    //{
        //    //    //DryRun = true,
        //    //    To = "/topics/news",
        //    //    //  RestrictedPackageName = "firebase_try3.firebase_try3",
        //    //    //  To = "dwxlGb9vdAA:APA91bFDXJjSOq4gvrjwc7GsGQ0Y5YN4wC8PjL1dDTGZVqxAYpn-MuKiPeuWZ1wSH8yNrGB2RWCdoZHV-MMSwGjgfomeNCOXNyIuFbm-msvAKI1NS00MTSpQfuLvA9NSE9u5aUJkcmhs",
        //    //    Notification = new AndroidNotification()
        //    //    {
        //    //        Title = Title,
        //    //        Body = Body,

        //    //    }
        //    //};

        //    var dic = new Dictionary<string, string>();
        //    PropertyInfo[] infos = dto.GetType().GetProperties();
        //    foreach (var info in infos)
        //    {
        //        dic.Add(info.Name, info.GetValue(dto, null).ToString());
        //    }

        //    try
        //    {
        //        var message_Android = new Message()
        //        {
        //            Priority = MessagePriority.high,
        //            TimeToLive = 864000,//this 10 days
        //            To = "/topics/update",
        //            //Notification = new AndroidNotification()
        //            //{
        //            //    Title = dto.Title,
        //            //    Body = dto.Body,
        //            //    //Icon = dto.Icon,
        //            //    //Sound = dto.Sound,
        //            //    //ClickAction = dto.ClickEvent.ToString(),
        //            //},

        //            //Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(dto.Body)
        //            Data = dic

        //        };

        //        await client1.SendMessageAsync(message_Android);
        //        //var client2 = new FCMClient(Server_Key);
        //        //var message_Android2 = new Message()
        //        //{
        //        //    Priority = MessagePriority.high,
        //        //    TimeToLive = 864000,//this 10 days
        //        //    To = "/topics/news",
        //        //    Notification = new AndroidNotification()
        //        //    {
        //        //        Title = dto.Title,
        //        //        Body = dto.Body,
        //        //        //Icon = dto.Icon,
        //        //        //Sound = dto.Sound,
        //        //        //ClickAction = dto.ClickEvent.ToString(),
        //        //    }

        //        //};

        //        //   await client1.SendMessageAsync(message_Android2);
        //        // var t=   new Task(() => client1.SendMessageAsync(message_Android));
        //        //  t.Wait(500);
        //        var client3 = new FCMClient(Server_Key);
        //        var message_IOS = new Message()
        //        {
        //            Priority = MessagePriority.high,
        //            TimeToLive = 864000,//this 10 days
        //            To = "/topics/update",
        //            Data = dic

        //        };

        //        await Task.Run(() => client3.SendMessageAsync(message_IOS).Wait());

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}

        ///// <summary>
        ///// This for admin !!!!  
        ///// Delete Notify By id
        ///// </summary>
        ///// <param name="Id">Notify Id</param>
        ///// <returns></returns>
        //public bool DeleteNotify(int Id)
        //{
        //    var notifications_User = _unitOfWork.NotificationRepository.FindBy(m => m.Id == Id);
        //    if (notifications_User.Any())
        //    {
        //        _unitOfWork.NotificationRepository.Remove(notifications_User.FirstOrDefault());
        //        _unitOfWork.SaveChanges();
        //        return true;
        //    }
        //    return false;

        //}
        //#endregion

        ////#region AddFireBase

        ////public async Task<int> AddFireBaseToken(FireBaseDto dto, Guid UserId, string device_system)
        ////{
        ////    var modify_Token = _unitOfWork.FireBaseRepository.GetAll();
        ////    var model_User = modify_Token.Where(m => m.UserId == UserId).ToList();
        ////    var model = model_User.Where(q => q.MobileIMEI == dto.MobileIMEI).ToList();
        ////    int returned_value = 0;
        ////    if (model.Any())
        ////    {
        ////        var single_Device = model[0];
        ////        single_Device.FToken = dto.FToken;
        ////        single_Device.MobileSystem = device_system;
        ////        single_Device.IsActive = true;
        ////        single_Device.Date = Utils.ServerNow;
        ////        _unitOfWork.FireBaseRepository.Update(single_Device);
        ////        returned_value = single_Device.Id;
        ////    }
        ////    else
        ////    {
        ////        var new_single_Device = Mapper.Map<FireBaseDto, FireBase>(dto);
        ////        new_single_Device.UserId = UserId;
        ////        new_single_Device.Date = Utils.ServerNow;
        ////        new_single_Device.IsActive = true;
        ////        new_single_Device.MobileSystem = device_system;
        ////        _unitOfWork.FireBaseRepository.Add(new_single_Device);
        ////        returned_value = new_single_Device.Id;
        ////    }


        ////    foreach (var single_modify_Token in modify_Token)
        ////    {
        ////        var single_modify_Token_date = single_modify_Token.Date.AddDays(7);
        ////        if (DateTime.Compare(single_modify_Token_date, Utils.ServerNow) < 0)
        ////        {
        ////            single_modify_Token.IsActive = false;
        ////            _unitOfWork.FireBaseRepository.Update(single_modify_Token);
        ////        }
        ////    }
        ////    //if there are notifications to this user that aren't sent
        ////    var notification_notsent = _unitOfWork.NotificationRepository.FindBy(m => m.UserId == UserId).Where(q => q.IsSent == false).ToList();
        ////    if (notification_notsent.Any())
        ////    {
        ////        var all_Firebase_acteved_User = notification_notsent[0].User.FireBases.Where(m => m.IsActive).ToList();
        ////        if (all_Firebase_acteved_User.Any())
        ////        {

        ////            var sending_notify = Mapper.Map<List<Notification>, List<NotificationdictionaryDto>>(notification_notsent);
        ////            int index_notification_notsent = 0;
        ////            foreach (var single_notification_notsent in sending_notify)
        ////            {
        ////                index_notification_notsent++;
        ////                if (await Send_for_one(single_notification_notsent, all_Firebase_acteved_User.Select(m => m.FToken).ToList()))
        ////                {
        ////                    notification_notsent[index_notification_notsent].IsSent = true;
        ////                    _unitOfWork.NotificationRepository.Update(notification_notsent[index_notification_notsent]);
        ////                }
        ////            }
        ////        }

        ////        // return 1;
        ////    }
        ////    _unitOfWork.SaveChanges();
        ////    return returned_value;


        ////}

        ////public bool IsUserIdExist(Guid userId)
        ////{
        ////    var user = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
        ////    return user.Any();
        ////}

        ////#endregion

        //#region GetNotify
        ///// <summary>
        ///// Get notifications 
        ///// if you want to get notifications for a particular user, set UserId
        ///// if you want to get notifications that sent to all user, set UserId null
        ///// </summary>
        /////// <param name="UserId"></param>
        ///// <returns></returns>
        //public List<NotificationdictionaryDto> GetNotifications(/*Guid? UserId*/)
        //{
        //    //if (UserId.HasValue)
        //    //{
        //    //    var notifications_User = _unitOfWork.NotificationRepository.FindBy(m => m.UserId == UserId).OrderByDescending(m => m.NotifyDate).ToList();
        //    //    if (notifications_User.Any())
        //    //    {
        //    //        return Mapper.Map<List<Notification>, List<NotificationdictionaryDto>>(notifications_User);
        //    //    }
        //    //    else
        //    //        return new List<NotificationdictionaryDto>();
        //    //}
        //    //else
        //    //{
        //        var notifications_All = _unitOfWork.NotificationRepository.GetAll().OrderByDescending(m => m.NotifyDate).ToList();
        //        if (notifications_All.Any())
        //        {
        //        notifications_All = notifications_All.OrderByDescending(m => m.NotifyDate).ToList();
        //        foreach (var not in notifications_All)
        //        {
        //            if(not.Type==(int)NotificationType.AdminAdvertisement)
        //                not.Image = Utils.ImageNotifyURL + not.Image;
        //            else
        //                not.Image = Utils.PhysicalImageProduct + not.Image;
        //        }
        //            return Mapper.Map<List<Notification>, List<NotificationdictionaryDto>>(notifications_All);
        //        }
        //        else
        //            return new List<NotificationdictionaryDto>();
        //   // }
        //}

        //#endregion

        ////#region UpdateNotify
        /////// <summary>
        /////// When the notify is sent from firebase set IsSent field to true
        /////// call this function from notificationApi
        /////// </summary>
        /////// <param name="id"></param>
        /////// <param name="UserId">user id who has this notification</param>
        /////// <returns></returns>
        ////public bool SetIsSent(int id, Guid UserId)
        ////{
        ////    var notify = _unitOfWork.NotificationRepository.FindBy(m => m.Id == id && m.UserId == UserId);
        ////    if (notify.Any())
        ////    {
        ////        var one_notify = notify.FirstOrDefault();
        ////        one_notify.IsSent = true;
        ////        _unitOfWork.NotificationRepository.Update(one_notify);
        ////        _unitOfWork.SaveChanges();
        ////        return true;
        ////    }
        ////    else
        ////        return false;
        ////}


        ////#endregion

        //#region DictionaryToObject

        ////private static T DictionaryToObject<T>(IDictionary<string, string> dict) where T : new()
        ////{
        ////    var t = new T();
        ////    PropertyInfo[] properties = t.GetType().GetProperties();

        ////    foreach (PropertyInfo property in properties)
        ////    {
        ////        if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
        ////            continue;

        ////        KeyValuePair<string, string> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

        ////        // Find which property type (int, string, double? etc) the CURRENT property is...
        ////        Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

        ////        // Fix nullables...
        ////        Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

        ////        // ...and change the type
        ////        object newA = Convert.ChangeType(item.Value, newT);
        ////        t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
        ////    }
        ////    return t;
        ////}


        /////this for convert oject to Dictionary
        /////
        ////////////var d = new Dictionary<string, string>();
        ////////////PropertyInfo[] infos = dto.GetType().GetProperties();
        ////////////foreach (var info in infos)
        ////////////{
        ////////////    d.Add(info.Name, info.GetValue(dto, null).ToString());
        ////////////}

        //#endregion

        private readonly IUnitOfWork _unitOfWork;
        private string Server_Key;
        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            // marwan Server_Key
            //Server_Key = "AAAArnJ4YtE:APA91bF6hs7Y0tVVtqS4eoviRl2_b688C978frNX9yDfyrS2bDtYRLln3FAxUC6Q6iX-a92K6sne5VeudAxTQ1b9H7EQ-uNGT_oMq6RlBUQcpsJqcdMVmA2j3OX23O6JWv-8b5prPzDf";

            // kiwi  Server_Key
            Server_Key = "AAAAxz5Khqc:APA91bHW0kWkSdEW5X5ODkGq63QMJe3P-L5C7awu-NIeZQUYa9_8OQ2cH6Tp4wtfJdE3zib4i_w_xlEeZb25yUFNNH8lT8Q3j5GykeylWdcMXv8MxV1ot2V-CLcBKp3a3hj_yehtIjGq";
        }

        #region Send Notify
        public async Task<int> Send(NotificationdictionaryDto dto)
        {
            int returned_value = 0;
          
            //if (UserId.HasValue)
            //{
            //    var User_fireToken = _unitOfWork.UserRepository.FindBy(m => m.UserId == UserId).SelectMany(m => m.FireBases).Where(q => q.IsActive).Select(w => w.FToken).ToList();
            //    if (User_fireToken.Any())
            //    {
            //        var new_notify = Mapper.Map<NotificationdictionaryDto, Notification>(dto);
            //        new_notify.NotifyDate = Utils.ServerNow;
            //        new_notify.UserId = UserId;
            //        new_notify.IsSent = false;
            //        _unitOfWork.NotificationRepository.Add(new_notify);
            //        _unitOfWork.SaveChanges();
            //        dto.Id = new_notify.Id;
            //        await Send_for_one(dto, User_fireToken);
            //        returned_value = new_notify.Id;

            //    }
            //}
            //else
            // {

            var new_notify = Mapper.Map<NotificationdictionaryDto, Notification>(dto);
            new_notify.NotifyDate = Utils.ServerNow;
            new_notify.Image = "ClotheNotify.png";
            // new_notify.UserId = null;
            _unitOfWork.NotificationRepository.Add(new_notify);
            _unitOfWork.SaveChanges();
            dto.Id = new_notify.Id;
            await send_notify_forAllUsers(dto);
            returned_value = new_notify.Id;

            //}

            return returned_value;
        }
        private async Task<bool> Send_for_one(NotificationdictionaryDto dto, List<string> firebaseToken)
        {
            //if (restaurantUserId.HasValue)
            //{
            //    var rests = _unitOfWork.RestaurantsRepository.FindBy(m => m.UserId == restaurantUserId);
            //    if (rests.Any())
            //    {
            //        var rest = rests.FirstOrDefault();
            //        dto.RestaurantId = rest.Id;
            //        dto.Discription = rest.RestaurantName;
            //    }
            //    else
            //        return false;
            //}
            //dto.SenderId = restaurantUserId;
            if (dto.Body == null)
                dto.Body = " ";
            FCMClient client = new FCMClient(Server_Key);
            var dic = new Dictionary<string, string>();
            PropertyInfo[] infos = dto.GetType().GetProperties();
            foreach (var info in infos)
            {
                dic.Add(info.Name, info.GetValue(dto, null).ToString());
            }
            try
            {
                foreach (var one_firebaseToken in firebaseToken)
                {
                    var message_Android = new Message()
                    {

                        Priority = MessagePriority.high,
                        TimeToLive = 864000,//this 10 days
                        To = one_firebaseToken,

                        Data = dic

                    };


                    var result = await client.SendMessageAsync(message_Android);

                    var message_IOS = new Message()
                    {
                        Priority = MessagePriority.high,
                        TimeToLive = 864000,//this 10 days
                        To = one_firebaseToken,
                        Data = dic
                    };

                    var result2 = await client.SendMessageAsync(message_IOS);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    
        private async Task<bool> send_notify_forAllUsers(NotificationdictionaryDto dto)
        {
            var client1 = new FCMClient(Server_Key);

            //if (restaurantUserId.HasValue)
            //{
            //    var rests = _unitOfWork.RestaurantsRepository.FindBy(m => m.UserId == restaurantUserId);
            //    if (rests.Any())
            //    {
            //        var rest = rests.FirstOrDefault();
            //        dto.RestaurantId = rest.Id;
            //        dto.Discription = rest.RestaurantName;
            //    }
            //    else
            //        return false;
            //}

            //var message = new FirebaseNet.Messaging.Message()
            //{
            //    //DryRun = true,
            //    To = "/topics/news",
            //    //  RestrictedPackageName = "firebase_try3.firebase_try3",
            //    //  To = "dwxlGb9vdAA:APA91bFDXJjSOq4gvrjwc7GsGQ0Y5YN4wC8PjL1dDTGZVqxAYpn-MuKiPeuWZ1wSH8yNrGB2RWCdoZHV-MMSwGjgfomeNCOXNyIuFbm-msvAKI1NS00MTSpQfuLvA9NSE9u5aUJkcmhs",
            //    Notification = new AndroidNotification()
            //    {
            //        Title = Title,
            //        Body = Body,

            //    }
            //};
            if (dto.Body == null)
                dto.Body = " ";

            var dic = new Dictionary<string, string>();
            PropertyInfo[] infos = dto.GetType().GetProperties();
            foreach (var info in infos)
            {
                dic.Add(info.Name, info.GetValue(dto, null).ToString());
            }

            try
            {
                var message_Android = new Message()
                {
                    Priority = MessagePriority.high,
                    TimeToLive = 864000,//this 10 days
                    To = "/topics/update",
                    //Notification = new AndroidNotification()
                    //{
                    //    Title = dto.Title,
                    //    Body = dto.Body,
                    //    //Icon = dto.Icon,
                    //    //Sound = dto.Sound,
                    //    //ClickAction = dto.ClickEvent.ToString(),
                    //},

                    //Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(dto.Body)
                    Data = dic

                };

              var x=  await client1.SendMessageAsync(message_Android);
                //var client2 = new FCMClient(Server_Key);
                //var message_Android2 = new Message()
                //{
                //    Priority = MessagePriority.high,
                //    TimeToLive = 864000,//this 10 days
                //    To = "/topics/news",
                //    Notification = new AndroidNotification()
                //    {
                //        Title = dto.Title,
                //        Body = dto.Body,
                //        //Icon = dto.Icon,
                //        //Sound = dto.Sound,
                //        //ClickAction = dto.ClickEvent.ToString(),
                //    }

                //};

                //   await client1.SendMessageAsync(message_Android2);
                // var t=   new Task(() => client1.SendMessageAsync(message_Android));
                //  t.Wait(500);
                var client3 = new FCMClient(Server_Key);
                var message_IOS = new Message()
                {
                    Priority = MessagePriority.high,
                    TimeToLive = 864000,//this 10 days
                    To = "/topics/update",
                    Data = dic

                };

                await Task.Run(() => client3.SendMessageAsync(message_IOS).Wait());

                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// This for admin !!!!  
        /// Delete Notify By id
        /// </summary>
        /// <param name="Id">Notify Id</param>
        /// <returns></returns>
        public bool DeleteNotify(int Id)
        {
            List<Notification> notifications_User;
           
            notifications_User = _unitOfWork.NotificationRepository.FindBy(m => m.Id == Id );
           
            if (notifications_User.Any())
            {
                _unitOfWork.NotificationRepository.Remove(notifications_User.FirstOrDefault());
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;

        }


        #endregion

        //#region AddFireBase

        //public async Task<int> AddFireBaseToken(FireBaseDto dto, Guid UserId, string device_system)
        //{
        //    var modify_Token = _unitOfWork.FireBaseRepository.GetAll();
        //    var model_User = modify_Token.Where(m => m.UserId == UserId).ToList();
        //    var model = model_User.Where(q => q.MobileIMEI == dto.MobileIMEI).ToList();
        //    int returned_value = 0;
        //    if (model.Any())
        //    {
        //        var single_Device = model[0];
        //        single_Device.FToken = dto.FToken;
        //        single_Device.MobileSystem = device_system;
        //        single_Device.IsActive = true;
        //        single_Device.Date = Utils.ServerNow;
        //        _unitOfWork.FireBaseRepository.Update(single_Device);
        //        returned_value = single_Device.Id;
        //    }
        //    else
        //    {
        //        var new_single_Device = Mapper.Map<FireBaseDto, FireBase>(dto);
        //        new_single_Device.UserId = UserId;
        //        new_single_Device.Date = Utils.ServerNow;
        //        new_single_Device.IsActive = true;
        //        new_single_Device.MobileSystem = device_system;
        //        _unitOfWork.FireBaseRepository.Add(new_single_Device);
        //        returned_value = new_single_Device.Id;
        //    }


        //    foreach (var single_modify_Token in modify_Token)
        //    {
        //        var single_modify_Token_date = single_modify_Token.Date.AddDays(7);
        //        if (DateTime.Compare(single_modify_Token_date, Utils.ServerNow) < 0)
        //        {
        //            single_modify_Token.IsActive = false;
        //            _unitOfWork.FireBaseRepository.Update(single_modify_Token);
        //        }
        //    }
        //    //if there are notifications to this user that aren't sent
        //    var notification_notsent = _unitOfWork.NotificationRepository.FindBy(m => m.UserId == UserId).Where(q => q.IsSent == false).ToList();
        //    if (notification_notsent.Any())
        //    {
        //        var all_Firebase_acteved_User = notification_notsent[0].User.FireBases.Where(m => m.IsActive).ToList();
        //        if (all_Firebase_acteved_User.Any())
        //        {

        //            var sending_notify = Mapper.Map<List<Notification>, List<NotificationdictionaryDto>>(notification_notsent);
        //            int index_notification_notsent = 0;
        //            foreach (var single_notification_notsent in sending_notify)
        //            {
        //                index_notification_notsent++;
        //                if (await Send_for_one(single_notification_notsent, all_Firebase_acteved_User.Select(m => m.FToken).ToList()))
        //                {
        //                    notification_notsent[index_notification_notsent].IsSent = true;
        //                    _unitOfWork.NotificationRepository.Update(notification_notsent[index_notification_notsent]);
        //                }
        //            }
        //        }

        //        // return 1;
        //    }
        //    _unitOfWork.SaveChanges();
        //    return returned_value;


        //}

        //public bool IsUserIdExist(Guid userId)
        //{
        //    var user = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
        //    return user.Any();
        //}

        //#endregion

        #region GetNotify
        /// <summary>
        /// Get notifications 
        /// </summary>
        /// <returns></returns>
        public List<NotificationdictionaryDto> GetNotifications(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            var notifications_All = _unitOfWork.NotificationRepository.GetAll().OrderByDescending(m => m.NotifyDate).AsParallel().Skip(skip).Take(pageSize).ToList();
            if (notifications_All.Any())
            {
                notifications_All = notifications_All.OrderByDescending(m => m.NotifyDate).ToList();
                foreach (var not in notifications_All)
                {
                    not.Image = Utils.ImageNotifyURL + not.Image;
                }
                return Mapper.Map<List<Notification>, List<NotificationdictionaryDto>>(notifications_All);
            }
            else
                return new List<NotificationdictionaryDto>();
            // }
        }

        #endregion


        #region  InputMessaging

        /// <summary>
        /// Add new message from admin to user
        /// use this function for auto reply on order
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        public async Task<bool> AddMessage(MessagingDto dto)
        {
            var model = Mapper.Map<MessagingDto, Messaging>(dto);
            var user_ = _unitOfWork.UserRepository.FindBy(x => x.UserId == dto.ReceiverId);
            if (!user_.Any())
                return false;
           // model.StoreManagerSenderId = restaurantUserId;
            model.SendingDate = Utils.ServerNow;
            NotificationdictionaryDto message_not = new Dtos.NotificationdictionaryDto();
           // var rests= _unitOfWork.RestaurantsRepository.FindBy(c=>c.UserId==restaurantUserId);
            //if (!rests.Any())
            //    return false;
            //var rest = rests.FirstOrDefault();
            message_not.Discription = "auto reply";
            message_not.Image = "a";
            message_not.ProductId = 1;
            message_not.Title = "a";
            message_not.Update = "a";
            //var orderes_ = rest.Orders.Where(a => a.OrderId == dto.OrderId);
            //if(!orderes_.Any())
            //{ return false; }
            var FirebaseToken = user_.FirstOrDefault().FirebaseToken;
            message_not.Type = NotificationType.Message;
            message_not.NotifyDate = model.SendingDate;
            message_not.Body = model.Body;
            //message_not.SenderName= rest.RestaurantName;
            var m = await Send_for_one(message_not, new List<string>() { FirebaseToken });

            //model.RestaurantName = rest.RestaurantName;
            _unitOfWork.MessagingRepository.Add(model);
            _unitOfWork.SaveChanges();
            //    await m;
            return true;

        }


        ///// <summary>
        ///// Convert Message To Seen
        ///// </summary>
        ///// <param name="messageId">Message Id</param>
        ///// <returns></returns>
        //public bool ConvertMessageToSeen(int messageId)
        //{
        //    var model = _unitOfWork.MessagingRepository.FindBy(m => m.Id == messageId);
        //    if (model.Any())
        //    {
        //        // var stores_ = model.FirstOrDefault();
        //        foreach (var message in model)
        //        {
        //            message.IsSeen = true;
        //            _unitOfWork.MessagingRepository.Update(message);
        //        }
        //        _unitOfWork.SaveChanges();
        //        return true;
        //    }
        //    return false;
        //}

        /// <summary>
        /// Delete Message
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public bool DeleteMessage(int messageId)
        {
            var model = _unitOfWork.MessagingRepository.FindBy(m => m.Id == messageId );
            if (model.Any())
            {
                _unitOfWork.MessagingRepository.Remove(model.FirstOrDefault());
                _unitOfWork.SaveChanges();

                return true;
            }
            else
                return false;
        }

        ///// <summary>
        ///// Show that if sender id exist or not
        ///// </summary>
        ///// <param name="senderId"></param>
        ///// <returns></returns>
        //public bool IsSenderIdExist(Guid senderId)
        //{
        //    return _unitOfWork.UserRepository.FindBy(m => m.UserId == senderId).Any();

        //}

        //#endregion

        //#region Get_Messages

        ///// <summary>
        ///// Get Message By message Id, 
        ///// Not: this function for admin
        ///// </summary>
        ///// <param name="messageId"></param>
        ///// <returns></returns>
        //public MessagingDto GetMessageById(int messageId)
        //{
        //    var messages_ = _unitOfWork.MessagingRepository.FindBy(m => m.Id == messageId);
        //    if (messages_.Any())
        //    {
        //        var res = messages_.FirstOrDefault();

        //        var result_ = Mapper.Map<Messaging, MessagingDto>(res);
        //        ConvertMessageToSeen(messageId);
        //        return result_;
        //    }
        //    else
        //    {
        //        return null;
        //    }


        //}

        /// <summary>
        /// Get All Messages, 
        /// Note: this function for admin
        /// </summary>
        /// <returns></returns>
        public List<MessagingDto> GetAllMessages(int pageSize,int page)
        {
            int skip = (page - 1) * pageSize;
            var messages_ = _unitOfWork.MessagingRepository.GetAll().OrderByDescending(m => m.SendingDate).AsParallel().Skip(skip).Take(pageSize).ToList();
            //if (messages_.Any())
            //{
            var result_ = Mapper.Map<List<Messaging>, List<MessagingDto>>(messages_);
            return result_;
            //}
            //else
            //{
            //    return null;
            //}

        }

        /// <summary>
        /// Get All Messages for specific user, 
        /// Note: this function for user
        /// </summary>
        /// <returns></returns>
        public List<MessagingDto> GetAllMessages_forUser(Guid UserId, int pageSize, int page)
        {
            int skip = (page - 1) * pageSize;
            var messages_ = _unitOfWork.MessagingRepository.FindBy(m => m.ReceiverId == UserId).OrderByDescending(m => m.SendingDate).AsParallel().Skip(skip).Take(pageSize).ToList();
            //if (messages_.Any())
            //{
            var result_ = Mapper.Map<List<Messaging>, List<MessagingDto>>(messages_);
            return result_;
            //}
            //else
            //{
            //    return null;
            //}

        }



        #endregion



    }
}
