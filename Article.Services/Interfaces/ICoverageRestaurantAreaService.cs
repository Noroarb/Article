using Market.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Interfaces
{
    public interface ICoverageRestaurantAreaService
    {

        #region CoverageRestaurantArea

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Add New Coverage Area
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int? AddNewCoverageArea_forRestaurant(CoverageRestaurantAreaDto dto, Guid userId);

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Update Coverage Area
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UpdateCoverageArea_forRestaurant(CoverageRestaurantAreaDto dto, Guid userId);

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Delete Coverage Area
        /// </summary>
        /// <param name="CoverageAreaId"></param>
        /// <param name="userId"></param>
        /// <returns>true if deleted successfully</returns>
        bool DeleteCoverageArea(int CoverageAreaId, Guid userId);

        ///// <summary>
        ///// Show that if this brand is exist
        ///// </summary>
        ///// <param name="brandId"></param>
        ///// <returns></returns>
        //public bool IsBrandIdExist(int brandId)
        //{
        //    return _unitOfWork.BrandsRepository.FindBy(m => m.Id == brandId).Any();
        //}

        #endregion

        #region GetCoverageRestaurantAreaDto

        /// <summary>
        /// Get all areas covered by a restaurant, 
        /// for user
        /// </summary>
        /// <returns></returns>
        List<CoverageRestaurantAreaDto> GetAllCoverageRestaurantArea(int restaurantId);

        /// <summary>
        /// Get all areas covered by a restaurant, 
        /// for user
        /// </summary>
        /// <returns></returns>
        CoverageRestaurantAreaDto GetCoverageAreaBy_CoverageId(int CoverageId);


        #endregion

    }
}
