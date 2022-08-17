using Card.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface IPaidAdsService
    {


        #region AddNewPaidAds_Photo

        /// <summary>
        /// This function for admin
        /// Add New Paid ads
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool AddNewPaidAds(InputPaidAdsDto dto);

        /// <summary>
        /// This function for admin
        /// Delete paid ads
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <returns>true if deleted successfully</returns>
        bool DeletePaidAds(string ImageUrl);

        /// <summary>
        /// This function for admin
        /// Add New Paid ads
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool UpdatePaidAds(InputPaidAdsDto dto);

        ///// <summary>
        ///// Show that if this brand is exist
        ///// </summary>
        ///// <param name="brandId"></param>
        ///// <returns></returns>
        //bool IsBrandIdExist(int brandId);
        #endregion

        #region GetPaidAds_Photo

        /// <summary>
        /// Get all paid ads
        /// </summary>
        /// <returns></returns>
        List<PaidAdsDto> GetAllPaidAds();

        #endregion

    }
}
