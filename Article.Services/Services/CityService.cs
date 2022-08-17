using AutoMapper;
using Card.Domain;
using Card.Common;
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
using PagedList;

/// <summary>
/// / int Add(LanguageHelper Language, InputCityDto dto);
////bool Edit(LanguageHelper Language, InputCityDto dto);
////bool Delete(int Id);
/// </summary>
namespace Card.Services.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        #region InputCityDto
        public int Add(LanguageHelper Language, InputCityDto dto)
        {
            var model = Mapper.Map<InputCityDto, City>(dto);
            model.CityDescription = new List<CityDescription>();
            CityDescription ACdes = new CityDescription();
            ACdes.CityId = model.Id;
            ACdes.LanguageId = (int)LanguageHelper.ARABIC;
            ACdes.CityName = dto.ArabicCityName;
            model.CityDescription.Add(ACdes);

            CityDescription ECdes = new CityDescription();
            ECdes.LanguageId = (int)LanguageHelper.ENGLISH;
            ECdes.CityName = dto.EnglishCityName;
            model.CityDescription.Add(ECdes);

            _unitOfWork.CityRepository.Add(model);
            _unitOfWork.SaveChanges();

            return model.Id;
        }

        public bool Edit(LanguageHelper Language, InputCityDto dto)
        {
            City c = _unitOfWork.CityRepository.FindById(dto.Id);
            if(c.CityDescription==null)
                 c.CityDescription = new List<CityDescription>();
            c.Sort = dto.Sort;
            foreach(var Cdes in c.CityDescription)
            {
                if(Cdes.LanguageId==(int)LanguageHelper.ARABIC)
                {
                    Cdes.CityName = dto.ArabicCityName;
                }
                else if(Cdes.LanguageId == (int)LanguageHelper.ENGLISH)
                {
                    Cdes.CityName = dto.EnglishCityName;
                }
            }

            _unitOfWork.CityRepository.Update(c);
            _unitOfWork.SaveChanges();

            return true;
        }

        public bool? Delete(int Id)
        {
            var c1 = _unitOfWork.CityRepository.FindBy(m=>m.Id==Id);
            if(!c1.Any())
            {
                return null;
            }
            else
            {
                if (c1.Single().Towns.Any())
                    return false;
                
                _unitOfWork.CityRepository.Remove(c1.Single());
                _unitOfWork.SaveChanges();
                return true;
            }
        }

        #endregion

        #region CityDto
        public List<CityDto> GetAllCities(LanguageHelper language)
        {
            var model = _unitOfWork.CityRepository.GetAll().OrderBy(m=>m.Sort).ToList();

            var modelDto = Mapper.Map<List<City>, List<CityDto>>(model);
            int index_m = 0;
            foreach(var city in model)
            {
                modelDto[index_m].TownsDto = new List<TownDto>();
                foreach(var CDes in city.CityDescription)
                {
                    if (CDes.LanguageId == (int)language)
                    {
                        modelDto[index_m].CityName = CDes.CityName;
                    }
                    
                }
                int index_m2 = 0;
                foreach(var Towns in city.Towns)
                {
                    TownDto towndto = new TownDto();
                    towndto.CityId = Towns.CityId;
                    towndto.Id = Towns.Id;
                    foreach (var TownDes in Towns.TownDescriptions)
                    {
                        if (TownDes.LanguageId == (int)language)
                        {

                            //towndto.Gps_Latitude = Towns.Gps_Latitude;
                            //towndto.Gps_Longitude = Towns.Gps_Longitude;
                            towndto.TownName= TownDes.TownName;
                            modelDto[index_m].TownsDto.Add(towndto);///////////////
                            
                           // modelDto[index_m].TownsDto[index_m2].TownName = TownDes.TownName;
                        }

                       
                    }
                    //int index_GuideNeighborhoodDtos = 0;
                    //towndto.GuideNeighborhoodDtos = new List<Dtos.GuideNeighborhoodDto>();

                    //foreach (var Neighborhood in Towns.GuideNeighborhoods)
                    //{
                    //    var Neighborhood_name = Neighborhood.GuideNeighborhoodDescriptions.Where(m => m.LanguageId == (int)language).ToList();
                    //    if (Neighborhood_name.Any())
                    //    {
                    //        towndto.GuideNeighborhoodDtos.Add(new GuideNeighborhoodDto() { Gps_Latitude = Neighborhood.Gps_Latitude, Gps_Longitude = Neighborhood.Gps_Longitude, TownId = Neighborhood.TownId, NeighborhoodName = Neighborhood_name[0].Name, Id= Neighborhood.Id });
                    //    //towndto.GuideNeighborhoodDtos[index_GuideNeighborhoodDtos].Gps_Latitude = Neighborhood.Gps_Latitude;
                    //    //towndto.GuideNeighborhoodDtos[index_GuideNeighborhoodDtos].Gps_Longitude = Neighborhood.Gps_Longitude;
                    //    //towndto.GuideNeighborhoodDtos[index_GuideNeighborhoodDtos].TownId = Neighborhood.TownId;
                       
                        
                    //    //    towndto.GuideNeighborhoodDtos[index_GuideNeighborhoodDtos].NeighborhoodName = Neighborhood_name[0].Name;
                    //    }
                       
                    //    index_GuideNeighborhoodDtos++;
                    //}
                    //index_m2++;
                }
                index_m++;
            }
            return modelDto;
        }

        public CityDto GetCityById(LanguageHelper language,int id)
        {
            var model1 = _unitOfWork.CityRepository.FindBy(m=>m.Id==id);

            if (model1.Any())
            {
                var model = model1[0];
                var modelDto = Mapper.Map<City, CityDto>(model);
                if (modelDto != null)
                {
                    foreach (var CDes in model.CityDescription)
                    {
                        if (CDes.LanguageId == (int)language)
                        {
                            modelDto.CityName = CDes.CityName;
                        }

                    }
                    modelDto.TownsDto = new List<TownDto>();

                    foreach (var Towns in model.Towns)
                    {
                        TownDto towndto = new TownDto();
                        towndto.CityId = Towns.CityId;
                        towndto.Id = Towns.Id;
                        foreach (var TownDes in Towns.TownDescriptions)
                        {
                            if (TownDes.LanguageId == (int)language)
                            {
                                towndto.TownName = TownDes.TownName;
                                modelDto.TownsDto.Add(towndto);

                                // modelDto[index_m].TownsDto[index_m2].TownName = TownDes.TownName;
                            }

                        }

                    }
                }

                return modelDto;
            }
            else
                return null;

        }
        #endregion

        //public IPagedList<CityDto> searchAndOrder (List<CityDto> allCities, string CurrentFilter, int? Page, string SearchString, NawafizApp.Common.Sort sortOrder = Sort.IdDown_Up)
        //{
        //    if (SearchString != null)
        //    {
        //        Page = 1;
        //    }
        //    else
        //    {
        //        SearchString = CurrentFilter;
        //    }

        //    if (!String.IsNullOrEmpty(SearchString))
        //    {
        //        allCities = allCities.Where(s => s.CityName.Contains(SearchString)).ToList();
        //    }
        //    var Cities = from city in allCities
        //                 select city;
        //    switch (sortOrder)
        //    {
        //        case Sort.IdUp_Down:
        //            Cities = Cities.OrderByDescending(s => s.Id);
        //            break;
        //        case Sort.NameDown_Up:
        //            Cities = Cities.OrderBy(s => s.CityName);
        //            break;
        //        case Sort.NameUp_Down:
        //            Cities = Cities.OrderByDescending(s => s.CityName);
        //            break;
        //        case Sort.IdDown_Up:
        //            Cities = Cities.OrderBy(s => s.Id);
        //            break;
        //        default:
        //            Cities = Cities.OrderBy(s => s.Id);
        //            break;
        //    }


        //    int pageSize = 4;
        //    int pageNumber = (Page ?? 1);

        //    return Cities.ToPagedList(pageNumber, pageSize);


        //}

        #region Validator

        public bool IsNameUnique(string name,int? id)
        {
            if (name==null)
                return true;
            else
            {
                List<CityDescription> model;

                if (id.HasValue)
                    model = _unitOfWork.CityDescriptionRepository.FindBy(m => m.CityId != id);
                else
                    model = _unitOfWork.CityDescriptionRepository.GetAll();
                return !model.Where(s => s.CityName.ToLower() == name.ToLower()).Any();

            }
        }

        public bool IsExistId(int id)
        {
            return _unitOfWork.CityRepository.FindBy(m => m.Id == id).Any();
        }

        #endregion

     
    }
}

