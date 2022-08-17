using Card.Services.Dtos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface IProductsService
    {
        #region Get_ProductDetailed_Info

        /// <summary>
        /// This function for User
        /// Get product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="UserId">required for favorite</param>
        /// <returns></returns>
        ProductDetailedDto GetProductDetailed(int productId);


        #endregion

        #region SearchFunction
        /// <summary>
        /// This function for user
        /// Get products
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<ProductSimplifyDto> GetProducts(SearchProductsDto search);

        #endregion


        #region  InputProductDto

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Add new product, 
        /// if not add for any reason it return null
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Product id, or null if not add</returns>
        int? AddProduct(InputProductDto dto);


        /// <summary>
        /// This function for admin and Restaurant manager
        /// Update exist product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>RestaurantId</returns>
        bool EditProduct(InputProductDto dto);

        /// <summary>
        /// This function for admin
        /// Increase views Count
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns></returns>
        bool IncreaseViewsCount(int productId);

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete_Product(int id);

        /// <summary>
        /// Show that if this product here or no
        /// </summary>
        /// <param name="productId">Produvt Id</param>
        /// <returns></returns>
        bool IsProductIdExist(int productId);
        /// <summary>
        /// Show that if this Restaurant exist or no
        /// </summary>
        /// <param name="RestauranttId"></param>
        /// <returns></returns>
        bool IsRestaurantIdExist(int RestauranttId);
        /// <summary>
        /// Show that if this Town exist or no
        /// </summary>
        /// <param name="TownId"></param>
        /// <returns></returns>
        bool IsTownIdExist(int TownId);
        /// <summary>
        /// Show that if this category exist and not has any child
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        bool IsCategoryIdExist_And_NotHasChild(int CategoryId);
        ///// <summary>
        ///// Show that if this brand exist or no
        ///// </summary>
        ///// <param name="BrandId"></param>
        ///// <returns></returns>
        //bool IsBrandIdExist(int BrandId);
        //List<ProductSimplifyDto> GetProducts_delete();
        #endregion

        #region SearchFunction_for admin
        /// <summary>
        /// Get products for admin
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<ProductSimplifyDto> GetProducts_forAdmin(SearchProductsDto search);

        #endregion

        #region Get_ProductDetailed_Info_ for admin

        /// <summary>
        /// This function for admin
        /// Get product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ProductDetailedDto GetProductDetailed_forAdmin(int productId);


        #endregion



        #region Photos

        /// <summary>
        /// Not: Max 3 images
        /// This function for admin and Restaurant manager
        /// Add Image to product
        /// </summary>
        /// <param name="Id">Product Id</param>
        /// <param name="ImageName"></param>
        /// <param name="IsPrimary"></param>
        /// <returns></returns>
        bool AddImagesToProduct(int Id, string ImageName, bool IsPrimary);

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Delete Image product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        bool DeleteImage(int productId, string ImageUrl);

        #endregion

        void viewCountIncreas();
    }
}
