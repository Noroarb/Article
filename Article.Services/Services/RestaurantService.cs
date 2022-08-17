using AutoMapper;
using Card.Common;
using Card.Domain;
using Card.Domain.Entities;
using Card.Services.Dtos;
using Card.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryService _CategoryService;
        public RestaurantService(IUnitOfWork unitOfWork, ICategoryService CategoryService)
        {
            _unitOfWork = unitOfWork;
            _CategoryService = CategoryService;
        }

        #region SearchFunction_for_user
        /// <summary>
        /// Get Restaurants for user and Restaurant manager, admin, 
        /// if you are admin please use IsAdmin parameter
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="search"></param>
        /// <param name="IsAdmin">if you are admin set it true</param>
        /// <returns></returns>
        public List<RestaurantDto> GetRestaurants(LanguageHelper lang, SearchRestaurantsDto search,bool IsAdmin=false)
        {
            List<Restaurants> Restaurants_;
            if (IsAdmin)
                Restaurants_ = _unitOfWork.RestaurantsRepository.GetAll();
            else
                Restaurants_ = _unitOfWork.RestaurantsRepository.FindBy(m => m.State == RestaurantState.ACTIVE);

            var returned_list = new List<RestaurantDto>();
            if (search.ID != null)
            {
                Restaurants_ = Restaurants_.Where(m => m.Id == search.ID).ToList();

            }

            if (search.TownID.HasValue)
            {
                Restaurants_ = Restaurants_.Where(m => m.TownId == search.TownID).ToList();
            }
            //if (search.NeighborhoodId.HasValue)
            //{
            //    Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.NeighborhoodId == search.NeighborhoodId).ToList();
            //}
            if (search.CityID.HasValue)
            {
                Restaurants_ = Restaurants_.Where(m => m.Town.CityId == search.CityID).ToList();
            }
            //if (search.CategoryID.HasValue)
            //{
            //    Restaurants_ = Restaurants_.Where(m => m.RestaurantCategoryId == search.CategoryID).ToList();
            //}
            if (search.UserId.HasValue)
            {
                Restaurants_ = Restaurants_.Where(m => m.UserId == search.UserId).ToList();
            }

            int skip = ((int)search.page - 1) * search.pageSize;
            List<Restaurants> restaurants_2 = Restaurants_.OrderBy(m=>(m.Date.Day*Utils.ServerNow.Day)%40).OrderBy(m => m.Sort).Skip(skip).Take(search.pageSize).ToList();

            foreach (var res in restaurants_2)
            {
                if (res.ImagePath == "" || res.ImagePath == null)
                {
                    res.ImagePath = Utils.ImageDefaultName;
                }
                //if (res.OnlineUpdateDate.AddMinutes(1) < Utils.ServerNow)
                //    res.IsOnline = false;
                string townName = res.Town.TownDescriptions.Where(m => m.LanguageId == (int)lang).FirstOrDefault().TownName;
                returned_list.Add(new Dtos.RestaurantDto()
                {
                    Id = res.Id,
                    Description = res.Description,
                    ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,
                    State = (RestaurantState)res.State,
                    //NeighborhoodId = res.RestaurantInfo.NeighborhoodId,
                    //  ProductsCount = res.Products.Count(),
                    Sort = res.Sort,
                    RestaurantName = res.RestaurantName,
                    //CardCost = res.CardCost,
                    //GreaterThanPriceCardIsFree = res.GreaterThanPriceCardIsFree,
                    //IsHasCard = res.IsHasCard,
                    //IsOnline = res.IsOnline,
                    LocationText=res.LocationText,
                    RestaurantOffer=res.RestaurantOffer,
                    TownName= townName
                });
            }
            if (!IsAdmin)
            {
                Random r = new Random(Utils.ServerNow.Second);

                var x = returned_list.OrderBy(q => (r.Next()));
                returned_list = x.OrderBy(m => m.Sort).ToList();
            }
            return returned_list;

        }

      

        #endregion

        #region Get_RestaurantDetailed_Info


        /// <summary>
        /// By Restaurant Id
        /// This function for Admin
        /// Get all information about Restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        public RestaurantInfoDto GetRestaurantDetailed_Info_forAdmin(int RestaurantId = 1)
        {
            var Restaurants_1 = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == RestaurantId);
            if (Restaurants_1.Any())
            {
                var res = Restaurants_1.FirstOrDefault();

                if (res.ImagePath == "" || res.ImagePath == null)
                {
                    res.ImagePath = Utils.ImageDefaultName;
                }
                //if (res.OnlineUpdateDate.AddMinutes(1) < Utils.ServerNow)
                //    res.IsOnline = false;
                var result_ = new Dtos.RestaurantInfoDto
                {
                    Id = res.Id,
                    Description = res.Description,
                    ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,
                    LocationText = res.LocationText,
                    Mobile1 = res.Mobile1,
                    Mobile2 = res.Mobile2,
                    Mobile3 = res.Mobile3,
                    Phone1 = res.Phone1,
                    Phone2 = res.Phone2,
                    Phone3 = res.Phone3,
                    State = (RestaurantState)res.State,

                    ProductsCount = res.Products.Count(),
                    Sort = res.Sort,
                    Email1 = res.Email1,
                    Facebook = res.Facebook,
                    Fax1 = res.Fax1,
                    Gps_Latitude = res.Gps_Latitude,
                    Gps_Longitude = res.Gps_Longitude,
                    Instagram = res.Instagram,
                    //IsMain = res.IsMain,
                    LinkedIn = res.LinkedIn,
                    Snapchat = res.Snapchat,
                    Twitter = res.Twitter,
                    Website = res.Website,
                    RestaurantName = res.RestaurantName,
                    UserId = res.UserId,
                    TownId= res.TownId
                    //CardCost = res.CardCost,
                    //GreaterThanPriceCardIsFree = res.GreaterThanPriceCardIsFree,
                    //IsHasCard = res.IsHasCard,
                    //IsOnline = res.IsOnline

                };
                return result_;
            }
            else
            {
                return null;
            }


        }

        /// <summary>
        /// By Restaurant Id
        /// This function for user
        /// Get all information about Restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns></returns>
        public RestaurantInfoDto GetRestaurantDetailed_Info(int RestaurantId=1)
        {
            var Restaurants_1 = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == RestaurantId && m.State==RestaurantState.ACTIVE);
            if (Restaurants_1.Any())
            {
                var res = Restaurants_1.FirstOrDefault();
               
                if (res.ImagePath == "" || res.ImagePath == null)
                {
                    res.ImagePath = Utils.ImageDefaultName;
                }
                //if (res.OnlineUpdateDate.AddMinutes(1) < Utils.ServerNow)
                //    res.IsOnline = false;
                var result_ = new Dtos.RestaurantInfoDto
                {
                    Id = res.Id,
                    Description = res.Description,
                    ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,
                    LocationText = res.LocationText,
                    Mobile1 = res.Mobile1,
                    Mobile2 = res.Mobile2,
                    Mobile3 = res.Mobile3,
                    Phone1 = res.Phone1,
                    Phone2 = res.Phone2,
                    Phone3 = res.Phone3,
                    State = (RestaurantState)res.State,

                    ProductsCount = res.Products.Count(),
                    Sort = res.Sort,
                    Email1 = res.Email1,
                    Facebook = res.Facebook,
                    Fax1 = res.Fax1,
                    Gps_Latitude = res.Gps_Latitude,
                    Gps_Longitude = res.Gps_Longitude,
                    Instagram = res.Instagram,
                   // IsMain = res.IsMain,
                    LinkedIn = res.LinkedIn,
                    Snapchat = res.Snapchat,
                    Twitter = res.Twitter,
                    Website = res.Website,
                    RestaurantName = res.RestaurantName,
                    UserId = res.UserId,
                    //CardCost = res.CardCost,
                    //GreaterThanPriceCardIsFree = res.GreaterThanPriceCardIsFree,
                    //IsHasCard = res.IsHasCard,
                    //IsOnline = res.IsOnline,
                    TownId = res.TownId
                   // CoverageRestaurantAreas = Mapper.Map<List<CoverageRestaurantArea>, List<CoverageRestaurantAreaDto>>(res.CoverageRestaurantAreas.ToList())
                };
                return result_;
            }
            else
            {
                return null;
            }


        }

        ///// <summary>
        ///// By town Id
        ///// This function for user and admin
        ///// Get all information about Restaurant by town
        ///// </summary>
        ///// <param name="townId"></param>
        ///// <returns></returns>
        //public RestaurantDto GetRestaurantDetailed_Info_By_TownId(int townId = 1)
        //{
        //    var Restaurants_1 = _unitOfWork.RestaurantsRepository.FindBy(m => m.TownId == townId);
        //    if (Restaurants_1.Any())
        //    {
        //        var res = Restaurants_1.FirstOrDefault();

        //        if (res.ImagePath == "" || res.ImagePath == null)
        //        {
        //            res.ImagePath = Utils.ImageDefaultName;
        //        }

        //        var result_ = new Dtos.RestaurantDto
        //        {
        //            Id = res.Id,
        //            Description = res.Description,
        //            ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,
        //            LocationText = res.LocationText,
        //            Mobile1 = res.Mobile1,
        //            Mobile2 = res.Mobile2,
        //            Mobile3 = res.Mobile3,
        //            Phone1 = res.Phone1,
        //            Phone2 = res.Phone2,
        //            Phone3 = res.Phone3,

        //            ProductsCount = res.Products.Count(),
        //            Sort = res.Sort,
        //            Email1 = res.Email1,
        //            Facebook = res.Facebook,
        //            Fax1 = res.Fax1,
        //            Gps_Latitude = res.Gps_Latitude,
        //            Gps_Longitude = res.Gps_Longitude,
        //            Instagram = res.Instagram,
        //            IsMain = res.IsMain,
        //            LinkedIn = res.LinkedIn,
        //            Snapchat = res.Snapchat,
        //            Twitter = res.Twitter,
        //            Website = res.Website,
        //            RestaurantName = res.RestaurantName
        //        };
        //        return result_;
        //    }
        //    else
        //    {
        //        return null;
        //    }


        //}

        #endregion

        //#region SearchFunction_Search_for_Admin
        ///// <summary>
        ///// Get Restaurants for Admin 
        ///// </summary>
        ///// <param name="search"></param>
        ///// <returns></returns>
        //public List<AdminGetRestaurantDto> GetRestaurants_for_Admin(SearchRestaurantsDto search)
        //{
        //    var Restaurants_ = _unitOfWork.RestaurantsRepository.GetAll();
        //    var returned_list = new List<AdminGetRestaurantDto>();
        //    if (search.ID != null)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.Id == search.ID).ToList();

        //    }

        //    if (search.TownID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.Neighborhood.TownId == search.TownID).ToList();
        //    }
        //    if (search.NeighborhoodId.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.NeighborhoodId == search.NeighborhoodId).ToList();
        //    }
        //    if (search.CityID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.Neighborhood.TownId == search.TownID).ToList();
        //    }
        //    if (search.CategoryID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantCategoryId == search.CategoryID).ToList();
        //    }
        //    if (search.UserId.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.UserId == search.UserId).ToList();
        //    }

        //    foreach (var res in Restaurants_)
        //    {
        //        if (res.ImagePath == "" || res.ImagePath == null)
        //        {
        //            res.ImagePath = Utils.ImageDefaultName;
        //        }
        //        returned_list.Add(new Dtos.AdminGetRestaurantDto()
        //        {
        //            Id = res.Id,
        //            Description = res.Description,
        //            ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,
        //            LocationText = res.RestaurantInfo.LocationText,
        //            Mobile = res.RestaurantInfo.Mobile1,
        //            Phone = res.RestaurantInfo.Phone1,
        //            NeighborhoodId = res.RestaurantInfo.NeighborhoodId,
        //            ProductsCount = res.Products.Count(),
        //            Sort = res.Sort,
        //            State = res.State,
        //            RestaurantName = res.RestaurantName
        //        });
        //    }
        //    returned_list = returned_list.OrderBy(m => m.Sort).ToList();
        //    return returned_list;

        //}

        //#endregion

        //#region SearchFunction_for_user
        ///// <summary>
        ///// Get Restaurants for user and Restaurant manager
        ///// </summary>
        ///// <param name="search"></param>
        ///// <returns></returns>
        //public List<SimplifyRestaurantDto> GetRestaurants_for_user(SearchRestaurantsDto search)
        //{
        //    var Restaurants_ = _unitOfWork.RestaurantsRepository.FindBy(m => m.State == (int)ClassifyStateHelper.ACTIVE);
        //    var returned_list = new List<SimplifyRestaurantDto>();
        //    if (search.ID != null)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.Id == search.ID).ToList();

        //    }

        //    if (search.TownID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.Neighborhood.TownId == search.TownID).ToList();
        //    }
        //    if (search.NeighborhoodId.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.NeighborhoodId == search.NeighborhoodId).ToList();
        //    }
        //    if (search.CityID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantInfo.Neighborhood.TownId == search.TownID).ToList();
        //    }
        //    if (search.CategoryID.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.RestaurantCategoryId == search.CategoryID).ToList();
        //    }
        //    if (search.UserId.HasValue)
        //    {
        //        Restaurants_ = Restaurants_.Where(m => m.UserId == search.UserId).ToList();
        //    }
        //    foreach (var res in Restaurants_)
        //    {
        //        if (res.ImagePath == "" || res.ImagePath == null)
        //        {
        //            res.ImagePath = Utils.ImageDefaultName;
        //        }
        //        returned_list.Add(new Dtos.SimplifyRestaurantDto()
        //        {
        //            Id = res.Id,
        //            Description = res.Description,
        //            ImagePath = Utils.ImageRestaurantProfileURL + res.ImagePath,

        //            NeighborhoodId = res.RestaurantInfo.NeighborhoodId,
        //            ProductsCount = res.Products.Count(),
        //            Sort = res.Sort,
        //            RestaurantName = res.RestaurantName
        //        });
        //    }
        //    returned_list = returned_list.OrderBy(m => m.Sort).ToList();
        //    return returned_list;

        //}

        //#endregion

        #region  InputRestaurantDto

        /// <summary>
        /// This function for master admin
        /// Add new Restaurant
        /// </summary>
        /// <param name="dto"></param>

        /// <returns>RestaurantId</returns>
        public int AddRestaurant(InputRestaurantDto dto)
        {
            var model = Mapper.Map<InputRestaurantDto, Restaurants>(dto);

            //model.UserId = model.UserId;
            //model.Date = Utils.ServerNow;
            //model.RestaurantInfo = new RestaurantInfo();
            //model.RestaurantInfo.IsMain = true;
            //model.RestaurantInfo.LocationText = dto.LocationText;
            //model.RestaurantInfo.Mobile1 = dto.Mobile;
            //model.RestaurantInfo.Phone1 = dto.Phone;

            //model.RestaurantInfo.NeighborhoodId = (int)dto.NeighborhoodId;
            //model.Time = DateTime.Now.TimeOfDay;
            //if (dto.Gps_Latitude == null || dto.Gps_Latitude == "" || dto.Gps_Longitude == null || dto.Gps_Longitude == "")
            //{
            //    model.TownId = dto.TownId;
            //    var Town_Location = _unitOfWork.TownRepository.FindBy(m => m.Id == dto.TownId)[0];
            //    model.Gps_Latitude = Town_Location.Gps_Latitude;
            //    model.Gps_Longitude = Town_Location.Gps_Longitude;
            //}
            //else
            //{
            //    int? town_id = GetTownId(dto);
            //    if (town_id == null)
            //    { return null; }

            //    model.TownId = (int)town_id;
            //}
            //model.State =(int) ClassifyStateHelper.ACTIVE;

            

            //Image image = new Image();
            //image.IsPrimary = true;
            //image.Name = "index.png";
            //model.Images = new List<Image>();
            //model.Images.Add(image);

            _unitOfWork.RestaurantsRepository.Add(model);
            _unitOfWork.SaveChanges();

            return model.Id;
        }


        ///// <summary>
        ///// This function for restaurant manager
        ///// Update exist Restaurant
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns>RestaurantId</returns>
        //public bool EditRestaurant_forManager(InputRestaurantDto dto)
        //{

        //    var models = _unitOfWork.RestaurantsRepository.FindBy(m=>m.Id==dto.Id);
        //    if (models.Any())
        //    {
        //        var model = models.FirstOrDefault();
        //        model.Date = Utils.ServerNow;
        //        model.Description = dto.Description;
        //        model.Mobile1 = dto.Mobile1;
        //        model.Mobile2 = dto.Mobile2;
        //        model.Mobile3 = dto.Mobile3;
        //        model.Phone1 = dto.Phone1;
        //        model.Phone2 = dto.Phone2;
        //        model.Phone3 = dto.Phone3;
        //        model.Sort = dto.Sort;
        //        model.Email1 = dto.Email1;
        //        model.Facebook = dto.Facebook;
        //        model.Fax1 = dto.Fax1;
        //        model.Gps_Latitude = dto.Gps_Latitude;
        //        model.Gps_Longitude = dto.Gps_Longitude;
        //        model.Instagram = dto.Instagram;
        //        model.LinkedIn = dto.LinkedIn;
        //        model.Snapchat = dto.Snapchat;
        //        model.Twitter = dto.Twitter;
        //        model.Website = dto.Website;
        //        model.RestaurantName = dto.RestaurantName;
        //        model.IsMain = true;
        //        model.LocationText = dto.LocationText;
        //        // model.Sort = dto.Sort;
        //        //model.GreaterThanPriceCardIsFree = dto.GreaterThanPriceCardIsFree;
        //        //model.IsOnline = dto.IsOnline;
        //        //model.IsHasCard = dto.IsHasCard;
        //        //model.CardCost = dto.CardCost;
        //        model.RestaurantOffer = dto.RestaurantOffer;
        //        //model.State = dto.State;

        //        _unitOfWork.RestaurantsRepository.Update(model);
        //        _unitOfWork.SaveChanges();
        //        return true;
        //    }
        //    else
        //        return false;

        //}

        /// <summary>
        /// This function for master admin, not for manager
        /// Update exist Restaurant
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>RestaurantId</returns>
        public bool EditRestaurant_ForAdmin(InputRestaurantDto dto)
        {

            var model = _unitOfWork.RestaurantsRepository.FindById(dto.Id);

            model.Date = Utils.ServerNow;
            model.Description = dto.Description;
            model.Mobile1 = dto.Mobile1;
            model.Mobile2 = dto.Mobile2;
            model.Mobile3 = dto.Mobile3;
            model.Phone1 = dto.Phone1;
            model.Phone2 = dto.Phone2;
            model.Phone3 = dto.Phone3;
            model.Sort = dto.Sort;
            model.Email1 = dto.Email1;
            model.Facebook = dto.Facebook;
            model.Fax1 = dto.Fax1;
            model.Gps_Latitude = dto.Gps_Latitude;
            model.Gps_Longitude = dto.Gps_Longitude;
            model.Instagram = dto.Instagram;
            model.LinkedIn = dto.LinkedIn;
            model.Snapchat = dto.Snapchat;
            model.Twitter = dto.Twitter;
            model.Website = dto.Website;
            model.RestaurantName = dto.RestaurantName;
            model.IsMain = true;
            model.LocationText = dto.LocationText;
            model.Sort = dto.Sort;
            //model.GreaterThanPriceCardIsFree = dto.GreaterThanPriceCardIsFree;
            //model.IsOnline = dto.IsOnline;
            //model.IsHasCard = dto.IsHasCard;
            //model.CardCost = dto.CardCost;
            model.UserId = dto.UserId;
            model.RestaurantOffer = dto.RestaurantOffer;
            //model.State = dto.State;
            if (model.State==Common.RestaurantState.STOP && dto.State== Common.RestaurantState.ACTIVE)
            {
                model.State = Common.RestaurantState.ACTIVE;
                for(int index=0;index<model.Categories.Count;++index)
                {
                    model.Categories.ToList()[index] = ChangeCategoryState(model.Categories.ToList()[index], true);
                }
            }
            else if (model.State == Common.RestaurantState.ACTIVE && dto.State == Common.RestaurantState.STOP)
            {
                model.State = Common.RestaurantState.STOP;
                for (int index = 0; index < model.Categories.Count; ++index)
                {
                    model.Categories.ToList()[index] = ChangeCategoryState(model.Categories.ToList()[index], false);
                }
            }

            _unitOfWork.RestaurantsRepository.Update(model);
            _unitOfWork.SaveChanges();
            return true;

        }

        /// <summary>
        /// Change Category State to active or not active
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        private Category ChangeCategoryState(Category model1, bool IsActive)
        {
            if (IsActive)
            {
                foreach (var classify in model1.Products)
                {
                    if (classify.State == ProductState.StopByAdminAfterActive)
                        classify.State = ProductState.Active;
                    else if (classify.State == ProductState.StopByAdminAfterStop)
                        classify.State = ProductState.Stop;
                }
            }
            else
            {
                foreach (var classify in model1.Products)
                {
                    if (classify.State == ProductState.Active)
                        classify.State = ProductState.StopByAdminAfterActive;
                    else if (classify.State == ProductState.Stop)
                        classify.State = ProductState.StopByAdminAfterStop;
                }
            }

            model1.IsActive = IsActive;
            if (model1.Children.Any())
            {
                int index = 0;
                foreach (var child_Active in model1.Children)
                {
                    child_Active.IsActive = IsActive;
                    model1.Children.ToList()[index] = ChangeCategoryState(child_Active, IsActive);
                }
            }
            return model1;
        }
       

        /// <summary>
        /// This function for admin
        /// Increase Visitor Count
        /// </summary>
        /// <param name="RestaurantId">Restaurant id</param>
        /// <returns></returns>
        public bool IncreaseVisitorCount(List<int> RestaurantId)
        {
            var model = _unitOfWork.RestaurantsRepository.FindBy(m => RestaurantId.Contains(m.Id));
            if (model.Any())
            {
                // var Restaurants_ = model.FirstOrDefault();
                foreach (var Restaurants_ in model)
                {
                    Restaurants_.visitorsCount++;
                    _unitOfWork.RestaurantsRepository.Update(Restaurants_);
                }
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function for admin
        /// Delete Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns>false if this Restaurant contains catrgories</returns>
        public bool Delete_Restaurant(int id)
        {
            var model = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == id);
            

            if (model.Any())
            {

                var model2 = model[0];
                if (model2.Categories.Any())
                    return false;
                //var favo = model2.FavoriteRestaurants;

                //foreach (var f in favo)
                //{
                //    _unitOfWork.FavoriteRestaurantRepository.Remove(f);
                //}
                //var Proiducts_ = model2.Products;

                //foreach (var p in Proiducts_)
                //{
                //    _unitOfWork.ProductsRepository.Remove(p);
                //}
                //var Prop_ = model2.RestaurantProperties;

                //foreach (var pr in Prop_)
                //{
                //    _unitOfWork.RestaurantPropertiesRepository.Remove(pr);
                //}
               
                if (model2.ImagePath != null && model2.ImagePath != Utils.ImageDefaultName)
                {
                    var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImageProduct + model2.ImagePath);
                    p = p.Replace(Utils.OldPath, Utils.NewPath);

                    try
                    {
                        File.Delete(p);
                    }

                    catch
                    { }
                   
                }
               
                _unitOfWork.RestaurantsRepository.Remove(model2);
                _unitOfWork.SaveChanges();

                return true;
            }
            else
                return false;
        }

        ///// <summary>
        ///// Shows if this town exist
        ///// </summary>
        ///// <param name="TownId"></param>
        ///// <returns></returns>
        //public bool IsTownExist(int TownId)
        //{
        //    var model = _unitOfWork.TownRepository.FindBy(m => m.Id == TownId);
        //    if (model.Any())
        //        return true;
        //    else
        //        return false;
        //}
        /// <summary>
        ///  Shows if this City exist
        /// </summary>
        /// <param name="CityId"></param>
        /// <returns></returns>
        public bool IsCityExist(int CityId)
        {
            var model = _unitOfWork.CityRepository.FindBy(m => m.Id == CityId);
            if (model.Any())
                return true;
            else
                return false;
        }
        ///// <summary>
        ///// Shows if this NeighborhoodId exist
        ///// </summary>
        ///// <param name="NeighborhoodId"></param>
        ///// <returns></returns>
        //public bool IsNeighborhoodExist(int NeighborhoodId)
        //{
        //    var model = _unitOfWork.GuideNeighborhoodRepository.FindBy(m => m.Id == NeighborhoodId);
        //    if (model.Any())
        //        return true;
        //    else
        //        return false;
        //}


        /// <summary>
        /// Shows if this Restaurant exist
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool IsIdExist(int Id)
        {
            var model = _unitOfWork.RestaurantsRepository.FindBy(m => (m.Id == Id));
            if (model.Any())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Shows if this town is exist
        /// </summary>
        /// <param name="townId"></param>
        /// <returns></returns>
        public bool IsTownIdExist(int townId)
        {
            var model = _unitOfWork.TownRepository.FindBy(m => (m.Id == townId));
            if (model.Any())
                return true;
            else
                return false;
        }

        ///// <summary>
        ///// Shows if this category exist
        ///// </summary>
        ///// <param name="CategoryId"></param>
        ///// <returns></returns>
        //public bool IsCategoryIdExist(int CategoryId)
        //{
        //    var model = _unitOfWork.RestaurantsCategoryRepository.FindBy(m => m.Id == CategoryId).Where(c => c.IsActive);
        //    if (model.Any())
        //        return true;
        //    else
        //        return false;
        //}

        ///// <summary>
        ///// Shows if this user exist
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <returns></returns>
        //public bool IsUserIdExist(Guid userId)
        //{
        //    var model = _unitOfWork.UserRepository.FindBy(m => (m.UserId == userId) && (m.Roles.Where(q => q.Name == Roles.RestaurantManager).Any()));
        //    if (model.Any())
        //        return true;
        //    else
        //        return false;
        //}


        /// <summary>
        /// Show that if this product here or no
        /// </summary>
        /// <param name="productId">Produvt Id</param>
        /// <returns></returns>
        public bool IsProductIdExist(int productId)
        {
            return _unitOfWork.ProductsRepository.FindBy(m => m.Id == productId && m.State==ProductState.Active).Any();
        }

        /// <summary>
        /// Show that if this user is exist
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns></returns>
        public bool IsUserIdExist(Guid userId)
        {
            return _unitOfWork.UserRepository.FindBy(m => (m.UserId == userId) && (m.Roles.Where(x=>x.Name==Roles.CardManagerRole).Any())).Any();
        }

        #endregion

        #region Photos

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Add Image to Restaurant
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <param name="ImageName"></param>
        /// <returns></returns>
        public bool AddImageToRestaurant(string ImageName, int RestaurantId = 1)
        {
            var model11 = _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == RestaurantId);
            if (!model11.Any())
                return false;
            else
            {
                var model = model11.FirstOrDefault();
                
                //var p = System.Web.HttpContext.Current.Server.MapPath("~/RestaurantsImages/Restaurants/" + model.ImagePath);
                //p = p.Replace("\\web\\", "\\");
                if (model.ImagePath != null&& model.ImagePath != Utils.ImageDefaultName)
                    {
                        var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImageCategory + model.ImagePath);
                        p = p.Replace(Utils.OldPath, Utils.NewPath);

                        try
                        {
                            File.Delete(p);
                        }
                        catch (Exception e1) { }
                    
                    }
                model.ImagePath = ImageName;
                _unitOfWork.RestaurantsRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
               

            }
        }



        #endregion


        //#region Get restaurantId by Restaurant manager Id

        ///// <summary>
        ///// Get restaurant Id by restaurant manager id
        ///// </summary>
        ///// <param name="restaurantManagerId"></param>
        ///// <returns>
        ///// If there are not any restaurant return null
        ///// </returns>
        //public int? GetrestaurantId(Guid restaurantManagerId)
        //{
        //    var rests = _unitOfWork.RestaurantsRepository.FindBy(m => m.UserId == restaurantManagerId);
        //    if (rests.Any())
        //    {
        //        return rests.FirstOrDefault().Id;
        //    }
        //    else
        //        return null;
        //}

        //#endregion

    }
}
