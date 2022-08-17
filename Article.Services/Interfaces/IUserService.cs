using Article.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Interfaces
{
    public interface IUserService
    {
        Guid Add(UserDto dto);
       
  
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        bool Edit(UserDto dto);
        bool Delete(Guid id);
        UserDto GetById(Guid id);
        List<UserDto> GetAll();
        bool HasRole(Guid id, String role);
        List<UserDto> GetUsersByRole(string role);
        bool Exists(Guid id);
        bool IsEmailUnique(string email);
        bool IsUserNameUnique(string UserName);
        //Guid? GetManagerRestaurantIdByRestaurantId(int RestaurantId);
        UserDto GetByUserName(string Username);

        /// <summary>
        /// هذا التابع للأدمن لحظر مستخدم
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool BlockUserd(Guid userId);
        /// <summary>
        /// هذا التابع للأدمن لالغاء حظر مستخدم
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UnBlockUserd(Guid userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeUserToWriter(Guid userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool ChangeUserToSupervisorRole(Guid userId);
        
        

    }
}
