using AutoMapper;
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
using Market.Domain;
using Market.Domain.Entities;
using Market.Common;
using Market.Services.Dtos;
using Market.Services.Interfaces;

namespace Market.Services
{
    public class CoverageRestaurantAreaService : ICoverageRestaurantAreaService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverageRestaurantAreaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        
        }



        #region CoverageRestaurantArea

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Add New Coverage Area
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int? AddNewCoverageArea_forRestaurant(CoverageRestaurantAreaDto dto, Guid userId)
        {
            var model = Mapper.Map<CoverageRestaurantAreaDto, CoverageRestaurantArea>(dto);
            var restaurantId = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == dto.RestaurantId);
            if(restaurantId.Any())
            {
                if (restaurantId[0].UserId != userId)
                    return null; 
            }
            else
                return null;
            _unitOfWork.CoverageRestaurantAreaRepository.Add(model);

            _unitOfWork.SaveChanges();
            return model.Id;

        }

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Update Coverage Area
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UpdateCoverageArea_forRestaurant(CoverageRestaurantAreaDto dto, Guid userId)
        {
            var model = Mapper.Map<CoverageRestaurantAreaDto, CoverageRestaurantArea>(dto);
            var restaurantId = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == dto.RestaurantId);
            if (restaurantId.Any())
            {
                if (restaurantId[0].UserId != userId)
                    return false;
            }
            else
                return false;
            _unitOfWork.CoverageRestaurantAreaRepository.Update(model);

            _unitOfWork.SaveChanges();
            return true;

        }

        /// <summary>
        /// This function for restaurant Manager and admin
        /// Delete Coverage Area
        /// </summary>
        /// <param name="CoverageAreaId"></param>
        /// <param name="userId"></param>
        /// <returns>true if deleted successfully</returns>
        public bool DeleteCoverageArea(int CoverageAreaId,Guid userId)
        {
            
             var model11 = _unitOfWork.CoverageRestaurantAreaRepository.FindBy(m =>m.Id== CoverageAreaId && m.Restaurant.UserId== userId);
            
            if (!model11.Any())
                return false;
            else
            {
               
               var model = model11.FirstOrDefault();
               
                _unitOfWork.CoverageRestaurantAreaRepository.Remove(model);
                _unitOfWork.SaveChanges();
                return true;

            }

        }

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
        public List<CoverageRestaurantAreaDto> GetAllCoverageRestaurantArea(int restaurantId)
        {
            var paid_Ads = _unitOfWork.CoverageRestaurantAreaRepository.FindBy(m=>m.RestaurantId== restaurantId);
            
            return Mapper.Map<List<CoverageRestaurantArea>,List<CoverageRestaurantAreaDto>>(paid_Ads);

        }

        /// <summary>
        /// Get all areas covered by a restaurant, 
        /// for user
        /// </summary>
        /// <returns></returns>
        public CoverageRestaurantAreaDto GetCoverageAreaBy_CoverageId(int CoverageId)
        {
            var paid_Ads = _unitOfWork.CoverageRestaurantAreaRepository.FindBy(m => m.Id == CoverageId);
            if (paid_Ads.Any())
                return Mapper.Map<CoverageRestaurantArea, CoverageRestaurantAreaDto>(paid_Ads[0]);
            else
                return null;


        }


        #endregion


    }
}
