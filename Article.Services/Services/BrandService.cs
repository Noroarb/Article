using AutoMapper;
using Market.Common;
using Market.Domain;
using Market.Domain.Entities;
using Market.Services.Dtos;
using Market.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Get_Brands

        /// <summary>
        /// Get brand By Brand Id
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns></returns>
        public BrandDto GetBrandById(int BrandId)
        {
            var messages_ = _unitOfWork.BrandsRepository.FindBy(m => m.Id == BrandId);
            if (messages_.Any())
            {
                var res = messages_.FirstOrDefault();
                
                var result_ = Mapper.Map<Brands, BrandDto>(res);
             
                return result_;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// Get All Brands
        /// </summary>
        /// <returns></returns>
        public List<BrandDto> GetAllBrands()
        {
            var messages_ = _unitOfWork.BrandsRepository.GetAll();
            //if (messages_.Any())
            //{
                var result_ = Mapper.Map<List<Brands>, List<BrandDto>>(messages_);
                return result_;
            //}
            //else
            //{
            //    return null;
            //}


        }
       

        #endregion



        #region  InputBrand

        /// <summary>
        /// For admin
        /// Add new brand
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        public int AddBrand(BrandDto dto)
        {

            var model = Mapper.Map<BrandDto, Brands>(dto);
            model.Date = Utils.ServerNow;
            _unitOfWork.BrandsRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.Id;

        }

        /// <summary>
        /// For admin
        /// Update brand
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StoreId</returns>
        public bool UpdateBrand(BrandDto dto)
        {

            var model = Mapper.Map<BrandDto, Brands>(dto);
            model.Date = Utils.ServerNow;
            _unitOfWork.BrandsRepository.Update(model);
            _unitOfWork.SaveChanges();
            return true;

        }

        /// <summary>
        /// For admin
        /// Delete brand
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns>StoreId</returns>
        public bool DeleteBrand(int BrandId)
        {

            var model = _unitOfWork.BrandsRepository.FindBy(m => m.Id == BrandId);
            if(model.Any())
            {
                var model_ = model.FirstOrDefault();
                _unitOfWork.BrandsRepository.Remove(model_);
                _unitOfWork.SaveChanges();
                return true;
            }
           
            
            return false;

        }

        /// <summary>
        /// Show that if this brand is exist
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public bool IsBrandIdExist(int brandId)
        {
            return _unitOfWork.BrandsRepository.FindBy(m => m.Id == brandId).Any();
        }

        #endregion

        
    }
}
