using AutoMapper;
using Card.Domain;
using Card.Domain.Entities;
using Card.Services.Dtos;
using Card.Services.Interfaces;
using Card.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Card.Common;

namespace Card.Services.Services
{
    public class TownService : ITownService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TownService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        #region InputTownDto
        public int Add(LanguageHelper Language, InputTownDto dto)
        {
            var model = Mapper.Map<InputTownDto, Town>(dto);
            model.TownDescriptions = new List<TownDescription>();
            TownDescription ATdes = new TownDescription();
            ATdes.TownId = model.Id;
            ATdes.LanguageId = (int)LanguageHelper.ARABIC;
            ATdes.TownName = dto.ArabicTownName;
            model.TownDescriptions.Add(ATdes);
          
            TownDescription ETdes = new TownDescription();
            ETdes.LanguageId = (int)LanguageHelper.ENGLISH;
            ETdes.TownName = dto.EnglishTownName;
            
            //var cityName = _unitOfWork.CityDescriptionRepository.FindBy(m => m.CityId == model.CityId).Where(v => v.LanguageId == (int)LanguageHelper.ARABIC).Single().CityName;
            //GPS.GetGps townGps = new GPS.GetGps(cityName, dto.ArabicTownName);
            //if(townGps.latitude==0|| townGps.longitude==0)
            //{ return null; }
            //try
            //{
                //model.Gps_Latitude = townGps.latitude.ToString();
                //model.Gps_Longitude = townGps.longitude.ToString();
                //model.Place_Id = GPS.GetGps.Get_PlaceId_And_Town(model.Gps_Latitude.ToString() + "," + model.Gps_Longitude.ToString()).ToList()[0];
                model.TownDescriptions.Add(ETdes);
            //model.Restaurants = new List<Restaurants>();
            //Restaurants Restaurant_ = new Restaurants()
            //{
            //    Date = Utils.ServerNow,
            //    IsMain = true,
            //    Sort = 1,
            //    RestaurantName = "Marwan",
            //    visitorsCount = 0,
            //    State=1
            //};
            //model.Restaurants.Add(Restaurant_);
                
            
                _unitOfWork.TownRepository.Add(model);
                _unitOfWork.SaveChanges();
            
                return model.Id;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public bool? Edit(LanguageHelper Language, InputTownDto dto)
        {
            Town T = _unitOfWork.TownRepository.FindById(dto.Id);
            if(T.TownDescriptions==null)
                  T.TownDescriptions = new List<TownDescription>();
            T.CityId = dto.CityId;
            foreach (var Tdes in T.TownDescriptions)
            {
                if (Tdes.LanguageId == (int)LanguageHelper.ARABIC)
                {
                    Tdes.TownName = dto.ArabicTownName;
                }
                else if (Tdes.LanguageId == (int)LanguageHelper.ENGLISH)
                {
                    Tdes.TownName = dto.EnglishTownName;
                }
            }
            var cityName = _unitOfWork.CityDescriptionRepository.FindBy(m => m.CityId == T.CityId).Where(v => v.LanguageId == (int)LanguageHelper.ARABIC).Single().CityName;
            //GPS.GetGps townGps = new GPS.GetGps(cityName, dto.ArabicTownName);
            //if (townGps.latitude == 0 || townGps.longitude == 0)
            //{ return null; }
            //try
            //{
            //    T.Gps_Latitude = townGps.latitude.ToString();
            //    T.Gps_Longitude = townGps.longitude.ToString();
            //    T.Place_Id = GPS.GetGps.Get_PlaceId_And_Town(T.Gps_Latitude.ToString() + "," + T.Gps_Longitude.ToString()).ToList()[0];
            //    //  T.TownDescriptions.Add(ETdes);

                _unitOfWork.TownRepository.Update(T);
                _unitOfWork.SaveChanges();

                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public bool? Delete(int Id)
        {
            var T = _unitOfWork.TownRepository.FindBy(m=>m.Id==Id);
            if (!T.Any())
            {
                return null;
            }
            else
            {
                //if (T.Single().Restaurants.Any())
                //{
                //    return false;
                //}
                _unitOfWork.TownRepository.Remove(T.Single());
                _unitOfWork.SaveChanges();
                return true;
            }
        }

        #endregion

        #region TownDto
        public List<TownDto> GetAllTowns(LanguageHelper language)
        {
            var model = _unitOfWork.TownRepository.GetAll();
            var modelDto = Mapper.Map<List<Town>, List<TownDto>>(model);
            int index_m = 0;
            foreach (var k in model)
            {

                foreach (var TDes in k.TownDescriptions)
                {
                    if (TDes.LanguageId == (int)language)
                    {
                        modelDto[index_m].TownName = TDes.TownName;
                    }

                }
                index_m++;
            }
            return modelDto;
        }




        public TownDto GetTownById(LanguageHelper language, int id)
        {
        
               var model_ = _unitOfWork.TownRepository.FindBy(m=>m.Id==id);
            if (model_.Any())
            {
                var model = model_.FirstOrDefault();
                var modelDto = Mapper.Map<Town, TownDto>(model);
                if (modelDto != null)
                {
                    foreach (var TDes in model.TownDescriptions)
                    {
                        if (TDes.LanguageId == (int)language)
                        {
                            modelDto.TownName = TDes.TownName;
                        }

                    }
                }
                return modelDto;
            }
            else
                return null;
        }

        public CityDto GetCityByTownId(LanguageHelper language,int id)
        {
            var model1 = _unitOfWork.TownRepository.GetAll().Where(m => m.Id == id);
            if (model1.Any())
            {
                var modelCity = model1.Single().City.CityDescription.Where(m => m.LanguageId == (int)language).Single();
                CityDto c = new CityDto();
                c.Id = modelCity.CityId;
                c.CityName = modelCity.CityName;
                return c;
            }
            else
              return null;

        }
        #endregion

        #region Validator

        public bool IsIdExist(int id)
        {
            return _unitOfWork.TownRepository.FindBy(m => m.Id == id).Any();
        }
        public bool IsCityIdExist(int Cityid)
        {
            return _unitOfWork.CityRepository.FindBy(m => m.Id == Cityid).Any();
        }
        
        //public bool ISPlaceIdUnique(string ArabicTownName,int CityId, int? editedId)
        //{
        //    List<Town> towns;
        //    string Place_Id;
        //    if (editedId.HasValue)
        //        towns = _unitOfWork.TownRepository.FindBy(m => m.Id != editedId);
        //    else
        //        towns = _unitOfWork.TownRepository.GetAll();
        //    try
        //    {
        //        var cities = _unitOfWork.CityRepository.FindBy(m => m.Id == CityId);
        //        if (cities.Any())
        //        {

        //            GPS.GetGps townGps = new GPS.GetGps(ArabicTownName, cities[0].CityDescription.Where(m => m.LanguageId == (int)LanguageHelper.ARABIC).ToList()[0].CityName);

        //            Place_Id = GPS.GetGps.Get_PlaceId_And_Town(townGps.latitude.ToString() + "," + townGps.longitude.ToString()).ToList()[0];

        //        }
        //        else
        //            return true;
        //    }
        //    catch(Exception qw)
        //    {
        //        return false;
        //    }
           
         
        //    return !towns.Where(m => m.Place_Id.ToLower() == Place_Id.ToLower()).Any();
        //}


        #endregion

    }
}

