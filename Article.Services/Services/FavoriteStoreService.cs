using AutoMapper;
using Market.Domain;
using Market.Common;
using Market.Domain.Entities;
using Market.Services.Dtos;
using Market.Services.Interfaces;
using Market.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;


namespace Market.Services.Services
{
    public class FavoriteRestaurantService : IFavoriteRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FavoriteRestaurantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        #region InputFavoriteDto
        public int Add(FavoriteRestaurantDto dto, Guid UserGuid)
        {
            var model = Mapper.Map<FavoriteRestaurantDto, FavoriteRestaurant>(dto);
            model.Date = Utils.ServerNow;
            model.UserId = UserGuid;
            _unitOfWork.FavoriteRestaurantRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.Id;
         
        }

    
        public bool Delete(int ProductId, Guid UserId)
        {
            //if (IsInGuide)
            //{
            //    var favs = _unitOfWork.GuideFavoriteRepository.FindBy(m => m.UserId == UserId).Where(m => m.ClassifyId == ClassifyId);
            //    if (favs.Any())
            //    {
            //        foreach (var fav in favs)
            //        {
            //            _unitOfWork.GuideFavoriteRepository.Remove(fav);
            //        }
            //        _unitOfWork.SaveChanges();
            //        return true;
            //    }
            //    else
            //        return false;
            //}
            //else
            //{
                var favs = _unitOfWork.FavoriteRestaurantRepository.FindBy(m => m.UserId == UserId).Where(m => m.ProductId == ProductId);
                if (favs.Any())
                {
                    foreach (var fav in favs)
                    {
                        _unitOfWork.FavoriteRestaurantRepository.Remove(fav);
                    }
                    _unitOfWork.SaveChanges();
                    return true;
                }
                else
                    return false;
          //  }
            
        }

        public bool IsProductIdExist_InProducts(int ProductId)
        {
            //if(IsInGuide)
            //{
            //    var fav = _unitOfWork.GuideClassifyRepository.FindBy(m => m.Id == ClassifyId);
            //    if (fav.Any())
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
                return _unitOfWork.ProductsRepository.FindBy(m => m.Id == ProductId && m.State == ProductState.Active).Any();
              
          //  }
            
        }

        public bool IsProductIdExist_InFavoriteForThisUser(int ProductId,Guid UserId)
        {
            //if(IsInGuide)
            //{
            //    var fav = _unitOfWork.GuideClassifyRepository.FindBy(m => m.Id == ClassifyId);
            //    if (fav.Any())
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
            return _unitOfWork.FavoriteRestaurantRepository.FindBy(m => m.ProductId == ProductId && m.UserId == UserId).Any();

            //  }

        }

        //public bool IsClassifyIdExist_ForThisUser_InFavorite(Guid UserId,int ClassifyId)
        //{
        //    //if(IsInGuide)
        //    //{
        //    //    var fav = _unitOfWork.GuideFavoriteRepository.FindBy(m => m.UserId == UserId && m.ClassifyId == ClassifyId);
        //    //    if (fav.Any())
        //    //        return true;
        //    //    else
        //    //        return false;
        //    //}
        //    //else
        //    {
        //        var fav = _unitOfWork.FavoriteRepository.FindBy(m => m.UserId == UserId && m.ClassifyId == ClassifyId);
        //        if (fav.Any())
        //            return true;
        //        else
        //            return false;
        //    }

        //}


        #endregion

        #region FavoriteDto

        public List<ProductSimplifyFavoriteDto> GetFavorites(LanguageHelper language,Guid UserGuid)
        {
            var model1 = _unitOfWork.FavoriteRestaurantRepository.FindBy(m => m.UserId == UserGuid).ToList().OrderByDescending(a=>a.Date).ToList();
           // List<ClassifySimplifyDto> ClassifySimplifyDto_first_list = new List<ClassifySimplifyDto>();
            if (model1.Any())
            {

                List<Products> C = new List<Products>();
                int index_Classify = 0;
                foreach(var fav in model1)
                {
                       
                    C.Add(fav.Product);
                    
                }
                var productdto= Mapper.Map<List<Products>,List<ProductSimplifyFavoriteDto>>(C);
                foreach(var singleProdto in productdto)
                {
                    singleProdto.IsHasDelivery= C[index_Classify].Restaurant.IsHasDelivery;
                    singleProdto.RestaurantName = C[index_Classify].Restaurant.RestaurantName;
                    var listImages = C[index_Classify].ProductsImages.Where(m => m.IsPrimary == true);
                    if (listImages.Any())
                    {
                        singleProdto.Image = Utils.ImageRestaurantProductsURL + listImages.Single().Name;
                    }
                    else
                    {
                        singleProdto.Image = Utils.ImageRestaurantProductsURL + Utils.ImageDefaultName;
                    }
                    singleProdto.IsFavorite = true;
                    //singleProdto.City=C[index_Classify].Town.City.CityDescription.Where(m => m.LanguageId == (int)language).Single().CityName;
                    //singleProdto.Town = C[index_Classify].Town.TownDescriptions.Where(m => m.LanguageId == (int)language).Single().TownName;
                    //singleProdto.IsFavorite = C[index_Classify].Favorites.Any();
                    index_Classify++;
                    
                }
                // Classdto = Classdto.OrderBy(m => m.Date).ToList();
                return productdto;

            }
            else
                return new List<ProductSimplifyFavoriteDto>();

            //var model2 = _unitOfWork.GuideFavoriteRepository.FindBy(m => m.UserId == UserGuid).ToList().OrderBy(a => a.Date).ToList();
            //List<GuideClassifySimplifyDto> GuideClassifySimplifyDto_Second_list = new List<GuideClassifySimplifyDto>();
            //if (model2.Any())
            //{

            //    List<GuideClassify> C = new List<GuideClassify>();
            //    int index_Classify = 0;
            //    foreach (var fav in model2)
            //    {

            //        C.Add(fav.GuideClassify);

            //    }
            //    var Classdto = Mapper.Map<List<GuideClassify>, List<GuideClassifySimplifyDto>>(C);
            //    foreach (var singleClassdto in Classdto)
            //    {
            //        var listImages = C[index_Classify].GuideImages.Where(m => m.IsPrimary == true);
            //        if (listImages.Any())
            //        {
            //            singleClassdto.ImagePath = Utils.ImageURL + listImages.Single().Name;
            //        }
            //        else
            //        {
            //            singleClassdto.ImagePath = Utils.ImageURL + Utils.ImageDefaultName;
            //        }
            //        singleClassdto.City = C[index_Classify].GuideTown.GuideCity.GuideCityDescriptions.Where(m => m.LanguageId == (int)language).Single().Name;
            //        singleClassdto.Town = C[index_Classify].GuideTown.GuideTownDescriptions.Where(m => m.LanguageId == (int)language).Single().Name;
            //        singleClassdto.IsFavorite = C[index_Classify].GuideFavorites.Any();
            //        index_Classify++;

            //    }
            //    // Classdto = Classdto.OrderBy(m => m.Date).ToList();
            //    GuideClassifySimplifyDto_Second_list= Classdto;

            //}
           // return Tuple.Create < List<ClassifySimplifyDto>,List <GuideClassifySimplifyDto >> (ClassifySimplifyDto_first_list, GuideClassifySimplifyDto_Second_list);

        }

        #endregion
    

    }
}

