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
using Card.Domain;
using Card.Domain.Entities;
using Card.Common;
using Card.Services.Dtos;
using Card.Services.Interfaces;

namespace Card.Services
{
    public class PaidAdsService : IPaidAdsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaidAdsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        
        }



        #region AddNewPaidAds_Photo

        /// <summary>
        /// This function for admin
        /// Add New Paid ads
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool AddNewPaidAds(InputPaidAdsDto dto)
        {
            var paid_Ads = new PaidAds
            {
                Date=Utils.ServerNow,
                ImagePath=dto.ImageName,
                Link =dto.Link 
            };
            try
            {
                _unitOfWork.PaidAdsRepository.Add(paid_Ads);

                _unitOfWork.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// This function for admin
        /// Add New Paid ads
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool UpdatePaidAds(InputPaidAdsDto dto)
        {
            var model = _unitOfWork.PaidAdsRepository.FindBy(m => m.Id == dto.Id);
            if (model.Any())
            {
                var single_model = model.FirstOrDefault();
                single_model.Date = Utils.ServerNow;
                single_model.Link = dto.Link;


                try
                {
                    _unitOfWork.PaidAdsRepository.Update(single_model);

                    _unitOfWork.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        /// <summary>
        /// This function for admin
        /// Delete paid ads
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <returns>true if deleted successfully</returns>
        public bool DeletePaidAds(string ImageUrl)
        {
            var ImageName = ImageUrl.Replace(Utils.ImagePaidAdsURL, "");
            
             var model11 = _unitOfWork.PaidAdsRepository.FindBy(m =>m.ImagePath== ImageName);
            
            if (!model11.Any())
                return false;
            else
            {
               
               var model = model11.FirstOrDefault();
                if (model.ImagePath != Utils.ImageDefaultName)
                {
                    var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImagePaidAds + model.ImagePath);
                    p = p.Replace(Utils.OldPath, Utils.NewPath);

                    try
                    {
                        File.Delete(p);
                    }

                    catch
                    { }
                }
                _unitOfWork.PaidAdsRepository.Remove(model);
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

        #region GetPaidAds_Photo

        /// <summary>
        /// Get all paid ads
        /// </summary>
        /// <returns></returns>
        public List<PaidAdsDto> GetAllPaidAds()
        {
            var paid_Ads = _unitOfWork.PaidAdsRepository.GetAll().OrderByDescending(m => m.Date).ToList();
            foreach(var paid_Ads_ in paid_Ads)
            {
                paid_Ads_.ImagePath = Utils.ImagePaidAdsURL + paid_Ads_.ImagePath;
            }
            //paid_Ads.Add(new PaidAds
            //{
            //    Link = "https://www.google.com",
            //    ImagePath = "1.0001"
            //});
            return Mapper.Map<List<PaidAds>,List<PaidAdsDto>>(paid_Ads);

        }

        #endregion


    }
}
