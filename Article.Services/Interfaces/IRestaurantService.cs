using Card.Common;
using Card.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface IRestaurantService
    {
        #region SearchFunction_for_user
        /// <summary>
        /// Get Restaurants for user and Restaurant manager, admin, 
        /// if you are admin please use IsAdmin parameter
        /// </summary>
        /// <param name="search"></param>
        /// <param name="IsAdmin">if you are admin set it true</param>
        /// <returns></returns>
        List<RestaurantDto> GetRestaurants(LanguageHelper lang, SearchRestaurantsDto search, bool IsAdmin = false);

        #endregion

        #region Get_RestaurantDetailed_Info


        /// <summary>
        /// By Restaurant Id
        /// This function for Admin
        /// Get all information about Restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        RestaurantInfoDto GetRestaurantDetailed_Info_forAdmin(int RestaurantId = 1);

        /// <summary>
        /// By Restaurant Id
        /// This function for user
        /// Get all information about Restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        RestaurantInfoDto GetRestaurantDetailed_Info(int RestaurantId = 1);


        #endregion


        #region  InputRestaurantDto

        /// <summary>
        /// This function for master admin
        /// Add new Restaurant
        /// </summary>
        /// <param name="dto"></param>

        /// <returns>RestaurantId</returns>
        int AddRestaurant(InputRestaurantDto dto);


        ///// <summary>
        ///// This function for restaurant manager
        ///// Update exist Restaurant
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <param name="RestManagerId"></param>
        ///// <returns>RestaurantId</returns>
        //bool EditRestaurant_forManager(InputRestaurantDto dto);

        /// <summary>
        /// This function for master admin, not for manager
        /// Update exist Restaurant
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>RestaurantId</returns>
        bool EditRestaurant_ForAdmin(InputRestaurantDto dto);
        
        /// <summary>
        /// This function for admin
        /// Increase Visitor Count
        /// </summary>
        /// <param name="RestaurantId">Restaurant id</param>
        /// <returns></returns>
        bool IncreaseVisitorCount(List<int> RestaurantId);

        /// <summary>
        /// This function for admin
        /// Delete Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns>false if this Restaurant contains catrgories</returns>
        bool Delete_Restaurant(int id);

        /// <summary>
        ///  Shows if this City exist
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        bool IsCityExist(int CityId);

        /// <summary>
        /// Shows if this Restaurant exist
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool IsIdExist(int Id);

        /// <summary>
        /// Shows if this town is exist
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        bool IsTownIdExist(int townId);


        /// <summary>
        /// Show that if this product here or no
        /// </summary>
        /// <param name="productId">Produvt Id</param>
        /// <returns></returns>
        bool IsProductIdExist(int productId);

        /// <summary>
        /// Show that if this user is exist
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns></returns>
        bool IsUserIdExist(Guid userId);

        #endregion

        #region Photos

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Add Image to Restaurant
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ManagerId"> User ID for manager of this Restaurant</param>
        /// <param name="ImageName"></param>
        /// <returns></returns>
        bool AddImageToRestaurant(string ImageName, int Id = 1);

        #endregion

        //#region Get restaurantId by Restaurant manager Id

        ///// <summary>
        ///// Get restaurant Id by restaurant manager id
        ///// </summary>
        ///// <param name="restaurantManagerId"></param>
        ///// <returns>
        ///// If there are not any restaurant return null
        ///// </returns>
        //int? GetrestaurantId(Guid restaurantManagerId);
        

        //#endregion
    }
}
