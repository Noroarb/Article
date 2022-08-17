using Market.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Interfaces
{
    public interface IBrandService
    {
        #region Get_Brands

        /// <summary>
        /// Get brand By Brand Id
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns></returns>
        BrandDto GetBrandById(int BrandId);

        /// <summary>
        /// Get All Brands
        /// </summary>
        /// <returns></returns>
        List<BrandDto> GetAllBrands();


        #endregion



        #region  InputBrand

        /// <summary>
        /// For admin
        /// Add new brand
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        int AddBrand(BrandDto dto);

        /// <summary>
        /// For admin
        /// Update brand
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        bool UpdateBrand(BrandDto dto);

        /// <summary>
        /// For admin
        /// Delete brand
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns>StoreId</returns>
        bool DeleteBrand(int BrandId);

        /// <summary>
        /// Show that if this brand is exist
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        bool IsBrandIdExist(int brandId);

        #endregion
    }
}
