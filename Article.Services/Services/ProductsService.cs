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
using PagedList;

namespace Card.Services.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRestaurantService _RestaurantService;
        public ProductsService(IUnitOfWork unitOfWork,IRestaurantService RestaurantService)
        {
            _unitOfWork = unitOfWork;
            _RestaurantService = RestaurantService;
        }



        #region  InputProductDto

        /// <summary>
        /// This function for admin
        /// Add new product, 
        /// if not add for any reason it return null
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Product id, or null if not add</returns>
        public int? AddProduct(InputProductDto dto)
        {
            
            var category_ = _unitOfWork.CategoryRepository.FindBy(m => m.Id == dto.CategoryId).FirstOrDefault();
           
            var model = Mapper.Map<InputProductDto, Products>(dto);
            model.RestaurantId = category_.RestaurantID;
            //model.RestaurantId = _unitOfWork.RestaurantsRepository.FindBy(m => m.TownId == dto.TownId).FirstOrDefault().Id;
            //model.UserId = model.UserId;
            model.Date = Utils.ServerNow;
           // model.NewPrice = (decimal)(Convert.ToDouble(dto.OldPrice) * ((100 - dto.DiscountPercentage) / 100));

           // model.State = dto.State;
           
           // model.OrdersCount = 0;
            model.ViewsCount = 0;
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
          
                _unitOfWork.ProductsRepository.Add(model);
         
            _unitOfWork.SaveChanges();

            return model.Id;

        }


        /// <summary>
        /// This function for admin
        /// Update exist product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>RestaurantId</returns>
        public bool EditProduct(InputProductDto dto)
        {

            var model = _unitOfWork.ProductsRepository.FindById(dto.Id);
           
            if (!model.Restaurant.Categories.Where(m => m.Id == dto.CategoryId).Any())
                return false;
            // var model = Mapper.Map<InputClassifyDto, Classify>(dto);
            // Classify model1 = new Classify();
            // model1 = model;


            // model.Time = DateTime.Now.TimeOfDay;
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
            //    model.Gps_Latitude = dto.Gps_Latitude;
            //    model.Gps_Longitude = dto.Gps_Longitude;
            //}
            //
            model.RestaurantId = model.RestaurantId;
            model.State = dto.State;
            model.Description = dto.Description;
            model.Name = dto.Name;
            model.OldPrice = dto.OldPrice;
            model.NewPrice = dto.NewPrice;
            
            if(dto.productOffer=="")
                model.productOffer = null;
            else
                model.productOffer = dto.productOffer;
            model.MinAmount = dto.MinAmount;
            //model.BrandId = dto.BrandId;
           // model.NewPrice = (decimal)(Convert.ToDouble(dto.OldPrice) * ((100 - dto.DiscountPercentage) / 100));
            model.Date = Utils.ServerNow;
           
            _unitOfWork.ProductsRepository.Update(model);
            _unitOfWork.SaveChanges();
            return true;

        }

        /// <summary>
        /// This function for admin
        /// Increase views Count
        /// </summary>
        /// <param name="productId">product id</param>
        /// <returns></returns>
        public bool IncreaseViewsCount(int productId)
        {
            var model = _unitOfWork.ProductsRepository.FindBy(m => m.Id == productId);
            if (model.Any())
            {
                var product_ = model.FirstOrDefault();
                product_.ViewsCount++;
                _unitOfWork.ProductsRepository.Update(product_);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// This function for admin
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete_Product(int id)
        {
            List<Products> model;

            model = _unitOfWork.ProductsRepository.FindBy(m => m.Id == id);

            if (model.Any())
            {

                var model2 = model.SingleOrDefault();
               
                List<ProductsImages> imgs = model2.ProductsImages.ToList();
               // List<FavoriteRestaurant> favs = model2.FavoriteRestaurants.ToList();
                //foreach (var fav in favs)
                //{
                //    _unitOfWork.FavoriteRestaurantRepository.Remove(fav);
                //}
                    foreach (var img in imgs)
                {
                    string ImageName = img.Name;

                    if (img.Name != Utils.ImageDefaultName)
                    {
                        var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImageProduct + img.Name);
                        p = p.Replace(Utils.OldPath, Utils.NewPath);

                        try
                        {
                            File.Delete(p);
                        }
                        catch (Exception e1) { }

                    }
                    _unitOfWork.ProductsImagesRepository.Remove(img);
                }
                    
                    _unitOfWork.ProductsRepository.Remove(model2);
                _unitOfWork.SaveChanges();

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Show that if this product here or no
        /// </summary>
        /// <param name="productId">Produvt Id</param>
        /// <returns></returns>
        public bool IsProductIdExist(int productId)
        {
            return _unitOfWork.ProductsRepository.FindBy(m => m.Id == productId ).Any();
        }
        /// <summary>
        /// Show that if this Restaurant exist or no
        /// </summary>
        /// <param name="RestauranttId"></param>
        /// <returns></returns>
        public bool IsRestaurantIdExist(int RestauranttId)
        {
            return _unitOfWork.RestaurantsRepository.FindBy(m => m.Id == RestauranttId&&m.State==RestaurantState.ACTIVE).Any();
        }

        /// <summary>
        /// Show that if this Town exist or no
        /// </summary>
        /// <param name="TownId"></param>
        /// <returns></returns>
        public bool IsTownIdExist(int TownId)
        {
            return _unitOfWork.RestaurantsRepository.FindBy(m => m.TownId == TownId).Any();
        }

        /// <summary>
        /// Show that if this brand exist or no
        /// </summary>
        /// <param name="BrandId"></param>
        /// <returns></returns>
        //public bool IsBrandIdExist(int BrandId)
        //{
        //    return _unitOfWork.BrandsRepository.FindBy(m => m.Id == BrandId).Any();
        //}

        /// <summary>
        /// Show that if this category exist and not has any child
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        public bool IsCategoryIdExist_And_NotHasChild(int CategoryId)
        {
            var category= _unitOfWork.CategoryRepository.FindBy(m => (m.Id == CategoryId)&& m.IsActive);
            if (category.Any())
            {
                return !category.FirstOrDefault().Children.Any();
            }
            else
                return false;
        }
        #endregion

        #region Get_ProductDetailed_Info

        /// <summary>
        /// This function for User
        /// Get product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="UserId">required for favorite</param>
        /// <returns></returns>
        public ProductDetailedDto GetProductDetailed(int productId/*, Guid? UserId*/)
        {
            //  var Restaurants_1 = _unitOfWork.ProductsRepository.FindBy(m=>(m.Id==RestaurantId)&&(m.State == (int)ClassifyStateHelper.ACTIVE));
            var Products_1 = _unitOfWork.ProductsRepository.FindBy(m => m.Id == productId && m.State==ProductState.Active);
            if (Products_1.Any())
            {
                var res = Products_1.FirstOrDefault();
                var result_ = Mapper.Map<Products, ProductDetailedDto>(res);
                result_.RestaurantName = res.Restaurant.RestaurantName;
                result_.Images = new List<string>();
                foreach (var img in res.ProductsImages)
                {
                    result_.Images.Add(Utils.ImageRestaurantProductsURL + img.Name);
                }
                if (!result_.Images.Any())
                {
                    result_.Images.Add(Utils.ImageRestaurantProductsURL + Utils.ImageDefaultName);
                }
                //bool res_favo = false;
                //if (UserId.HasValue)
                //{
                //    result_.IsFavorite = res.FavoriteRestaurants.Where(m => m.UserId == UserId).Any();
                //}
                 IncreaseViewsCount(productId);
                //if (res.BrandId.HasValue)
                //{
                //    SearchProductsDto sameBrand_Search = new SearchProductsDto()
                //    {
                //        BrandId = new List<int?> { res.BrandId },
                //        page = 1,
                //        pageSize = 10,
                //        sorting = ProductSearchSortingHelper.ViewsCount,
                //        TownId = res.Restaurant.TownId
                //    };
                
                //result_.ProductsInSameBrand = GetProducts(sameBrand_Search,UserId).AsParallel().Skip(0).Take(10).ToList();
                //    if (result_.ProductsInSameBrand.Any())
                //    {
                //        try
                //        {
                //            result_.ProductsInSameBrand.Remove(result_.ProductsInSameBrand.Where(m => m.Id == result_.Id).FirstOrDefault());
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}
                SearchProductsDto similarProducts_ = new SearchProductsDto()
                {
                    page = 1,
                    pageSize = 6,
                    sorting = ProductSearchSortingHelper.ViewsCount,
                    TownId = res.Restaurant.TownId,
                    RestaurantId=res.RestaurantId,
                    CategoryId=res.CategoryId
                };
                result_.SimilarProducts= GetProducts(similarProducts_/*, UserId*/).AsParallel().Skip(0).Take(6).ToList();
                if (result_.SimilarProducts.Any())
                {
                    try
                    {
                        result_.SimilarProducts.Remove(result_.SimilarProducts.Where(m => m.Id == result_.Id).FirstOrDefault());
                    }
                    catch
                    {

                    }
                }
                return result_;
            }
            else
            {
                return null;
            }

        }


        #endregion

        #region SearchFunction
        /// <summary>
        /// Get products for user
        /// </summary>
        /// <param name="search"></param>
        /// <param name="UserId">required for favorite</param>
        /// <returns></returns>
        public List<ProductSimplifyDto> GetProducts(SearchProductsDto search/*,Guid? UserId*/)
        {
            
            //to know when it access to database 
            bool DB_Access = false;
            List<Products> Products_db = new List<Products>();
            if (search.RestaurantId.HasValue)
            {
                Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.RestaurantId == search.RestaurantId && m.State == ProductState.Active);
                DB_Access = true;
            }
            if (search.MaxPrice.HasValue)
            {
                Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.NewPrice <= search.MaxPrice && m.State != ProductState.Active);
                DB_Access = true;
            }
            if(search.MinPrice.HasValue)
            {
                if(DB_Access)
                {
                    Products_db = Products_db.Where(m => m.NewPrice >= search.MinPrice).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.NewPrice >= search.MinPrice && m.State != ProductState.Active);
                    DB_Access = true;
                }
            }
            //if(search.BrandId!=null)
            //{
            //    List<int?> br_id_list = search.BrandId;
            //    foreach (var br_id in br_id_list)
            //    {
            //        if(!br_id.HasValue)
            //        {
            //            search.BrandId.Remove(br_id);
            //        }
            //    }
            //    if (DB_Access)
            //    {
            //        Products_db = Products_db.Where(m => search.BrandId.Contains(m.BrandId)).ToList();
            //    }
            //    else
            //    {
            //        Products_db = _unitOfWork.ProductsRepository.FindBy(m => search.BrandId.Contains(m.BrandId) && m.availability != (int)ProductsAvailability.NotExist);
            //        DB_Access = true;
            //    }
            //}
           
            if (search.CategoryId.HasValue)
            {
                if (DB_Access)
                {
                    Products_db = Products_db.Where(m => m.CategoryId==search.CategoryId).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.CategoryId == search.CategoryId && m.State != ProductState.Active);
                    DB_Access = true;
                }
            }
            if (search.TownId.HasValue)
            {
                if (DB_Access)
                {
                    Products_db = Products_db.Where(m => m.Restaurant.TownId == search.TownId).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.Restaurant.TownId == search.TownId && m.State != ProductState.Active);
                    DB_Access = true;
                }
            }
            if (!string.IsNullOrEmpty(search.Text))
            {
                // start searching algrithem

                List<string> words = search.Text/*.Replace(",", "").Replace(":", "").Replace(".", "")*/.Split(' ').ToList();
                for (int index = 0; index < words.Count; ++index)
                {
                    words[index] = words[index].ToLower();
                    if (words[index].Length < 3)
                        words.Remove(words[index]);
                }

                
                if (DB_Access)
                {
                    List<Products> Products_db_temp = new List<Products>();
                    foreach (var w in words)
                        Products_db_temp.AddRange(Products_db.Where(m => m.Name.ToLower().Contains(w) || m.Description.ToLower().Contains(w)).ToList());
                    Products_db = Products_db_temp;
                }
                else
                {
                    foreach (var w in words)
                        Products_db.AddRange(_unitOfWork.ProductsRepository.FindBy(m => (m.Name.ToLower().Contains(w) && m.State == ProductState.Active) || (m.Description.ToLower().Contains(w) && m.State == ProductState.Active)));
                    DB_Access = true;
                }
                //   Products_db = Products_db.OrderByDescending(m => m.Id).ToList();
               // double procounter = 0;
                Products_db = Products_db.Distinct().ToList();
                //for (int i= 0; i< Products_db.Count-1;i++)
                //{
                //    for(int j=i+1;j< Products_db.Count;j++)
                //    {
                //        //if (Products_db[i].Id == Products_db[j].Id)
                //        //{
                //        //  //  Products_db.Remove(Products_db[j]);
                //        //    Products_db.RemoveAt(j);
                //        //}
                //        procounter++;
                //    }
                //    //if (Products_db[i].Id == Products_db[i + 1].Id)
                //    //    Products_db.Remove(Products_db[i + 1]);
                //   // if(Products_db.Contains()
                //}
                //List<Products> Products_db_result = new List<Products>();
                //var grouped_ = Products_db.GroupBy(m => m.Id);
                //var sorted_ = grouped_.OrderByDescending(group => group.Count()).ToList();
                //Parallel.For(0, sorted_.Count(), i => 
                //{
                //    Products_db_result.Add(Products_db.Where(m => m.Id == sorted_[i].Key).FirstOrDefault());
                //});
                //for (int index_sorted_ = 0; index_sorted_ < sorted_.Count(); ++index_sorted_)
                //{
                //    Products_db_result.Add(Products_db.Where(m => m.Id == sorted_[index_sorted_].Key).FirstOrDefault());
                //}
               // Products_db = Products_db_result;
                // end searching algorithem
            }
            else
            if(!DB_Access)
            {
                Products_db = _unitOfWork.ProductsRepository.FindBy( m=> m.State == ProductState.Active);
                DB_Access = true;
            }

            
            //returned_list = Mapper.Map<List<Products>, List<RestaurantProductSimplifyDto>>(products_);
            List<int> Restaurant_id = new List<int>();
            int skip = ((int)search.page - 1) * search.pageSize;
            var returned_list = new List<ProductSimplifyDto>();
            //  products_ = products_.Skip(skip).Take(search.pageSize).ToList();
            if (string.IsNullOrEmpty(search.Text))
            {
                switch (search.sorting)
                {
                    case ProductSearchSortingHelper.FromOldestToNewest:
                        Products_db = Products_db.AsParallel().OrderBy(m => m.Date).ToList();
                        break;
                    case ProductSearchSortingHelper.FromTheLatestToTheOldest:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.Date).ToList();
                        break;
                    case ProductSearchSortingHelper.From_the_cheapest_to_the_most_expensive:
                        Products_db = Products_db.AsParallel().OrderBy(m => m.NewPrice).ToList();
                        break;
                    case ProductSearchSortingHelper.From_the_most_expensive_to_the_cheapest:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.NewPrice).ToList();
                        break;
                    case ProductSearchSortingHelper.ViewsCount:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.ViewsCount).ToList();
                        break;
                    default:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.Date).ToList();
                        break;
                }
            }
           
            List<Products> products_2 = Products_db.AsParallel().Skip(skip).Take(search.pageSize).ToList();

            foreach (var res in products_2)
            {
                Products_db.Remove(res);
                if (!Restaurant_id.Contains(res.RestaurantId))
                {
                    Restaurant_id.Add(res.RestaurantId);
                }
                var img_ = res.ProductsImages.AsParallel().Where(m => m.IsPrimary).FirstOrDefault();
                string img_path;
                if (img_ != null)
                    img_path = Utils.ImageRestaurantProductsURL + img_.Name;
                else
                    img_path = Utils.ImageRestaurantProductsURL + Utils.ImageDefaultName;
                //bool res_favo = false;
                //if(UserId.HasValue)
                //{
                //    res_favo = res.FavoriteRestaurants.Where(m => m.UserId == UserId).Any();
                //}
                returned_list.Add(new Dtos.ProductSimplifyDto()
                {
                    Id = res.Id,
                    Description = res.Description,
                    Image = img_path,
                    Name = res.Name,
                    //availability = 1,
                    NewPrice = res.NewPrice,
                    productOffer = res.productOffer,
                    Date = res.Date,
                    CategoryId=res.CategoryId,
                   // DiscountPercentage=res.DiscountPercentage,
                    OldPrice=res.OldPrice,
                    MinAmount=res.MinAmount,
                    //IsFavorite= res_favo,
                    RestaurantId = res.RestaurantId,
                    RestaurantName=res.Restaurant.RestaurantName
                   // IsHasCard= res.Restaurant.IsHasCard
                   
                //NumberOfPieces = 0
            });
            }
            //_RestaurantService.IncreaseVisitorCount(Restaurant_id);
            var ret = Mapper.Map<List<Products>, List<ProductSimplifyDto>>(Products_db);
            ret.InsertRange(skip, returned_list);
            // _RestaurantService.IncreaseVisitorCount(products_.Select(m => m.RestaurantId).ToList());
            // returned_list = returned_list.OrderByDescending(m => m.Date).ToList();
            //   return ret.ToPagedList((int)search.page, search.pageSize);
            return ret;

        }

        //public List<ProductSimplifyDto> GetProducts_delete()
        //{

        //    var products_ = _unitOfWork.ProductsRepository.PageAll(0, 2);

        //    //returned_list = Mapper.Map<List<Products>, List<RestaurantProductSimplifyDto>>(products_);
        //    List<int> Restaurant_id = new List<int>();
        //    List<ProductSimplifyDto> returned_list = new List<Dtos.ProductSimplifyDto>();
        //    foreach (Products res in products_)
        //    {
        //        if (!Restaurant_id.Contains(res.RestaurantId))
        //        {
        //            Restaurant_id.Add(res.RestaurantId);
        //        }
        //        var img_ = res.ProductsImages.Where(m => m.IsPrimary).FirstOrDefault();
        //        string img_path;
        //        if (img_ != null)
        //            img_path = Utils.ImageRestaurantProductsURL + img_.Name;
        //        else
        //            img_path = Utils.ImageRestaurantProductsURL + "index.png";
        //        returned_list.Add(new Dtos.ProductSimplifyDto()
        //        {
        //            Id = res.Id,
        //            Description = res.Description,
        //            Image = img_path,
        //            Name = res.Name,
        //            availability = res.availability,
        //            NewPrice = res.NewPrice,
        //            productOffer = res.productOffer,
        //            Date = res.Date,
        //            NumberOfPieces = res.NumberOfPieces
        //        });
        //    }
        //    _RestaurantService.IncreaseVisitorCount(Restaurant_id);
        //    _RestaurantService.IncreaseVisitorCount(products_.Select(m => m.RestaurantId).ToList());
        //    returned_list = returned_list.OrderByDescending(m => m.Date).ToList();
        //    return returned_list;

        //}

        #endregion


        #region SearchFunction_for Admin
        /// <summary>
        /// Get products for Admin
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<ProductSimplifyDto> GetProducts_forAdmin(SearchProductsDto search)
        {

            //to know when it access to database 
            bool DB_Access = false;
            List<Products> Products_db = new List<Products>();
            if (search.RestaurantId.HasValue)
            {
                Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.RestaurantId == search.RestaurantId);
                DB_Access = true;
            }
            if (search.MaxPrice.HasValue)
            {
                Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.NewPrice <= search.MaxPrice);
                DB_Access = true;
            }
            if (search.MinPrice.HasValue)
            {
                if (DB_Access)
                {
                    Products_db = Products_db.Where(m => m.NewPrice >= search.MinPrice).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.NewPrice >= search.MinPrice);
                    DB_Access = true;
                }
            }
            //if (search.BrandId != null)
            //{
            //    List<int?> br_id_list = search.BrandId;
            //    foreach (var br_id in br_id_list)
            //    {
            //        if (!br_id.HasValue)
            //        {
            //            search.BrandId.Remove(br_id);
            //        }
            //    }
            //    if (DB_Access)
            //    {
            //        Products_db = Products_db.Where(m => search.BrandId.Contains(m.BrandId)).ToList();
            //    }
            //    else
            //    {
            //        Products_db = _unitOfWork.ProductsRepository.FindBy(m => search.BrandId.Contains(m.BrandId));
            //        DB_Access = true;
            //    }
            //}

            if (search.CategoryId.HasValue)
            {
                if (DB_Access)
                {
                    Products_db = Products_db.Where(m => m.CategoryId == search.CategoryId).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.CategoryId == search.CategoryId);
                    DB_Access = true;
                }
            }
            if (search.TownId.HasValue)
            {
                if (DB_Access)
                {
                    Products_db = Products_db.Where(m => m.Restaurant.TownId == search.TownId).ToList();
                }
                else
                {
                    Products_db = _unitOfWork.ProductsRepository.FindBy(m => m.Restaurant.TownId == search.TownId);
                    DB_Access = true;
                }
            }
            if (!string.IsNullOrEmpty(search.Text))
            {
                // start searching algrithem

                List<string> words = search.Text.Replace(",", "").Replace(":", "").Replace(".", "").Split(' ').ToList();
                for (int index = 0; index < words.Count; ++index)
                {
                    words[index] = words[index].ToLower();
                    if (words[index].Length < 3)
                        words.Remove(words[index]);
                }


                if (DB_Access)
                {
                    List<Products> Products_db_temp = new List<Products>();
                    foreach (var w in words)
                        Products_db_temp.AddRange(Products_db.Where(m => m.Name.ToLower().Contains(w) || m.Description.ToLower().Contains(w)).ToList());
                    Products_db = Products_db_temp;
                }
                else
                {
                    foreach (var w in words)
                        Products_db.AddRange(_unitOfWork.ProductsRepository.FindBy(m => (m.Name.ToLower().Contains(w) && m.State == ProductState.Active) || (m.Description.ToLower().Contains(w) && m.State == ProductState.Active)));
                    DB_Access = true;
                }
                //   Products_db = Products_db.OrderByDescending(m => m.Id).ToList();
               // double procounter = 0;
                Products_db = Products_db.Distinct().ToList();
                //for (int i= 0; i< Products_db.Count-1;i++)
                //{
                //    for(int j=i+1;j< Products_db.Count;j++)
                //    {
                //        //if (Products_db[i].Id == Products_db[j].Id)
                //        //{
                //        //  //  Products_db.Remove(Products_db[j]);
                //        //    Products_db.RemoveAt(j);
                //        //}
                //        procounter++;
                //    }
                //    //if (Products_db[i].Id == Products_db[i + 1].Id)
                //    //    Products_db.Remove(Products_db[i + 1]);
                //   // if(Products_db.Contains()
                //}
                //List<Products> Products_db_result = new List<Products>();
                //var grouped_ = Products_db.GroupBy(m => m.Id);
                //var sorted_ = grouped_.OrderByDescending(group => group.Count()).ToList();
                //Parallel.For(0, sorted_.Count(), i => 
                //{
                //    Products_db_result.Add(Products_db.Where(m => m.Id == sorted_[i].Key).FirstOrDefault());
                //});
                //for (int index_sorted_ = 0; index_sorted_ < sorted_.Count(); ++index_sorted_)
                //{
                //    Products_db_result.Add(Products_db.Where(m => m.Id == sorted_[index_sorted_].Key).FirstOrDefault());
                //}
                // Products_db = Products_db_result;
                // end searching algorithem
            }
            else
            if (!DB_Access)
            {
                Products_db = _unitOfWork.ProductsRepository.GetAll();
                DB_Access = true;
            }


            //returned_list = Mapper.Map<List<Products>, List<RestaurantProductSimplifyDto>>(products_);
            List<int> Restaurant_id = new List<int>();
            int skip = ((int)search.page - 1) * search.pageSize;
            var returned_list = new List<ProductSimplifyDto>();
            //  products_ = products_.Skip(skip).Take(search.pageSize).ToList();
            if (string.IsNullOrEmpty(search.Text))
            {
                switch (search.sorting)
                {
                    case ProductSearchSortingHelper.FromOldestToNewest:
                        Products_db = Products_db.AsParallel().OrderBy(m => m.Date).ToList();
                        break;
                    case ProductSearchSortingHelper.FromTheLatestToTheOldest:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.Date).ToList();
                        break;
                    case ProductSearchSortingHelper.From_the_cheapest_to_the_most_expensive:
                        Products_db = Products_db.AsParallel().OrderBy(m => m.NewPrice).ToList();
                        break;
                    case ProductSearchSortingHelper.From_the_most_expensive_to_the_cheapest:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.NewPrice).ToList();
                        break;
                    case ProductSearchSortingHelper.ViewsCount:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.ViewsCount).ToList();
                        break;
                    default:
                        Products_db = Products_db.AsParallel().OrderByDescending(m => m.Date).ToList();
                        break;
                }
            }

            List<Products> products_2 = Products_db.AsParallel().Skip(skip).Take(search.pageSize).ToList();

            foreach (var res in products_2)
            {
                Products_db.Remove(res);
                if (!Restaurant_id.Contains(res.RestaurantId))
                {
                    Restaurant_id.Add(res.RestaurantId);
                }
                var img_ = res.ProductsImages.AsParallel().Where(m => m.IsPrimary).FirstOrDefault();
                string img_path;
                if (img_ != null)
                    img_path = Utils.ImageRestaurantProductsURL + img_.Name;
                else
                    img_path = Utils.ImageRestaurantProductsURL + Utils.ImageDefaultName;

                returned_list.Add(new Dtos.ProductSimplifyDto()
                {
                    Id = res.Id,
                    Description = res.Description,
                    Image = img_path,
                    Name = res.Name,
                    //availability = 1,
                    NewPrice = res.NewPrice,
                    productOffer = res.productOffer,
                    Date = res.Date,
                    CategoryId = res.CategoryId,
                   // DiscountPercentage = res.DiscountPercentage,
                    OldPrice = res.OldPrice,
                    MinAmount = res.MinAmount,
                  //  IsFavorite = false,
                    RestaurantId = res.RestaurantId,
                    RestaurantName = res.Restaurant.RestaurantName
                    // IsHasCard = res.Restaurant.IsHasCard
                    //NumberOfPieces = 0
                });
            }
            //_RestaurantService.IncreaseVisitorCount(Restaurant_id);
            var ret = Mapper.Map<List<Products>, List<ProductSimplifyDto>>(Products_db);
            ret.InsertRange(skip, returned_list);
            // _RestaurantService.IncreaseVisitorCount(products_.Select(m => m.RestaurantId).ToList());
            // returned_list = returned_list.OrderByDescending(m => m.Date).ToList();
            //   return ret.ToPagedList((int)search.page, search.pageSize);
            return ret;

        }

        #endregion

        #region Get_ProductDetailed_Info_ for Admin

        /// <summary>
        /// This function for Admin
        /// Get product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductDetailedDto GetProductDetailed_forAdmin(int productId)
        {
            //  var Restaurants_1 = _unitOfWork.ProductsRepository.FindBy(m=>(m.Id==RestaurantId)&&(m.State == (int)ClassifyStateHelper.ACTIVE));
            var Products_1 = _unitOfWork.ProductsRepository.FindBy(m => m.Id == productId);
            if (Products_1.Any())
            {
                var res = Products_1.FirstOrDefault();
                var result_ = Mapper.Map<Products, ProductDetailedDto>(res);
                result_.RestaurantName = res.Restaurant.RestaurantName;
                result_.Images = new List<string>();
                foreach (var img in res.ProductsImages)
                {
                    result_.Images.Add(Utils.ImageRestaurantProductsURL + img.Name);
                }
                if (!result_.Images.Any())
                {
                    result_.Images.Add(Utils.ImageRestaurantProductsURL + Utils.ImageDefaultName);
                }
                IncreaseViewsCount(productId);
                //if (res.BrandId.HasValue)
                //{
                //    SearchProductsDto sameBrand_Search = new SearchProductsDto()
                //    {
                //        BrandId = new List<int?> { res.BrandId },
                //        page = 1,
                //        pageSize = 10,
                //        sorting = ProductSearchSortingHelper.ViewsCount,
                //        TownId = res.Restaurant.TownId
                //    };

                //    result_.ProductsInSameBrand = GetProducts_forManager(sameBrand_Search).AsParallel().Skip(0).Take(10).ToList();
                //    if (result_.ProductsInSameBrand.Any())
                //    {
                //        try
                //        {
                //            result_.ProductsInSameBrand.Remove(result_.ProductsInSameBrand.Where(m => m.Id == result_.Id).FirstOrDefault());
                //        }
                //        catch
                //        {

                //        }
                //    }
                //}
                SearchProductsDto similarProducts_ = new SearchProductsDto()
                {
                    page = 1,
                    pageSize = 6,
                    sorting = ProductSearchSortingHelper.ViewsCount,
                    TownId = res.Restaurant.TownId,
                    RestaurantId = res.RestaurantId,
                    CategoryId = res.CategoryId
                };
                result_.SimilarProducts = GetProducts_forAdmin(similarProducts_).AsParallel().Skip(0).Take(6).ToList();
                if (result_.SimilarProducts.Any())
                {
                    try
                    {
                        result_.SimilarProducts.Remove(result_.SimilarProducts.Where(m => m.Id == result_.Id).FirstOrDefault());
                    }
                    catch
                    {

                    }
                }
                return result_;
            }
            else
            {
                return null;
            }

        }


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
        /// <param name="RestaurantManagerId"></param>
        /// <returns></returns>
        public bool AddImagesToProduct(int Id, string ImageName, bool IsPrimary/*, Guid RestaurantManagerId*/)
        {
            List<Products> model11;

            model11 = _unitOfWork.ProductsRepository.FindBy(m => m.Id == Id);
            
            if (!model11.Any())
                return false;
            else
            {
                var model = model11.FirstOrDefault();
                //if (model.Restaurant.UserId != RestaurantManagerId)
                //    return false;
                if (model.ProductsImages != null)
                {
                    List<ProductsImages> images_db = model.ProductsImages.ToList();
                    if(images_db.Count>2)
                    {
                        return false;
                    }
                    if(IsPrimary)
                    {
                        foreach(var im in images_db)
                        {
                            im.IsPrimary = false;
                        }
                    }
                    if (images_db.Count == 0)
                    {
                        model.ProductsImages = new List<ProductsImages>()
                        {
                            new ProductsImages()
                            {
                               IsPrimary = true,
                               Date = Utils.ServerNow,
                               Name = ImageName
                            }
                        };
                    }
                    else
                    {
                        model.ProductsImages.Add(new ProductsImages()

                        {
                            IsPrimary = IsPrimary,
                            Date = Utils.ServerNow,
                            Name = ImageName
                        });
                        
                    }
                    //foreach (var single_model in images_db)
                    //{
                    //    try
                    //    {
                    //        var path = System.Web.HttpContext.Current.Server.MapPath("~/RestaurantsImages/Products/");
                    //        path = path.Replace("\\web\\", "\\");
                    //        var path_name = path + single_model.Name;
                    //        File.Delete(path_name);
                    //        //    File.Delete(System.Web.HttpContext.Current.Server.MapPath("~/RestaurantsImages/Restaurants/" + model.ImagePath));
                    //    }
                    //    catch (Exception e)
                    //    {

                    //    }
                    //    _unitOfWork.ProductsImagesRepository.Remove(single_model);
                    //}
                    //model.ProductsImages.Add(new ProductsImages()
                    //{
                    //    IsPrimary = true,
                    //    Date = Utils.ServerNow,
                    //    Name = ImageName
                    //});

                }
                else
                {

                    model.ProductsImages = new List<ProductsImages>()
                        {
                            new ProductsImages()
                            {
                               IsPrimary = true,
                               Date = Utils.ServerNow,
                               Name = ImageName
                            }
                        };

                }


                //if (!model.ProductsImages.Any())
                //    {
                //        model.ProductsImages = new List<ProductsImages>()
                //        {
                //            new ProductsImages()
                //            {
                //               IsPrimary = true,
                //               Date = Utils.ServerNow,
                //               Name = ImageName
                //           }
                //        };


                //    }
                //    else
                //    {
                //        if (IsPrimary)
                //        {
                //            var img_primary = model.ProductsImages.Where(m => m.IsPrimary);
                //            if (img_primary.Any())
                //            {
                //                foreach (var single_img_primary in img_primary)
                //                {
                //                    single_img_primary.IsPrimary = false;
                //                    _unitOfWork.ProductsImagesRepository.Update(single_img_primary);
                //                }
                //            }
                //        }
                //        model.ProductsImages.Add(new ProductsImages()
                //        {
                //            IsPrimary = IsPrimary,
                //            Date = Utils.ServerNow,
                //            Name = ImageName
                //        });

                //    }
                _unitOfWork.ProductsRepository.Update(model);
                    _unitOfWork.SaveChanges();
                    return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
        }

        /// <summary>
        /// This function for admin and Restaurant manager
        /// Delete Image product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="ImageUrl"></param>
        /// <param name="RestaurantManagerId"></param>
        /// <returns></returns>
        public bool DeleteImage(int productId, string ImageUrl/*, Guid RestaurantManagerId*/)
        {
            var ImageName = ImageUrl.Replace(Utils.ImageRestaurantProductsURL, "");
            List<ProductsImages> model11;
            string pa = "";
            var model11_temp = _unitOfWork.ProductsImagesRepository.FindBy(m => (m.ProductId == productId));
            model11 = model11_temp.Where(m=>m.Name == ImageName).ToList();
            // var model11 = model101.Where(v => v.Name == ImageName);
          //var  p = ImageUrl.Replace(Utils.ImageRestaurantProductsURL, System.Web.HttpContext.Current.Server.MapPath("RestaurantsImages\\Products\\"));
          //  p = p.Replace("web\\AdminProducts\\DeletePhoto", "");
            if (!model11.Any())
                return false;
            else
            {
                ProductsImages model;
                model = model11.FirstOrDefault();
                //if (model.Product.Restaurant.UserId != RestaurantManagerId)
                //    return false;
                if (model.Name != Utils.ImageDefaultName)
                {
                    var p = System.Web.HttpContext.Current.Server.MapPath(Utils.PhysicalImageProduct + model.Name);
                    p = p.Replace(Utils.OldPath, Utils.NewPath);

                    try
                    {
                        File.Delete(p);
                    }
                    catch (Exception e1) { }
                }
                   
                if (model.IsPrimary && (model11_temp.Count() > 1))
                {
                    var Convert_oldToTrue = model11_temp.Where(m => m.IsPrimary != true).ToList()[0];
                    Convert_oldToTrue.IsPrimary = true;
                    _unitOfWork.ProductsImagesRepository.Update(Convert_oldToTrue);
                }
                _unitOfWork.ProductsImagesRepository.Remove(model);
                _unitOfWork.SaveChanges();
                return true;

            }

        }

        #endregion

        public void viewCountIncreas()
        {
            var products = _unitOfWork.ProductsRepository.GetAll();
            Random r = new Random();
            foreach(var p in products)
            {

                p.ViewsCount += r.Next(2, 5);
                _unitOfWork.ProductsRepository.Update(p);
            }
            _unitOfWork.SaveChanges();
        }


    }
}
