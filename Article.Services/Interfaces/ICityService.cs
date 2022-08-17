using Card.Common;
using Card.Services.Dtos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface ICityService
    {
        #region InputCityDto
        /// <summary>
        /// Add City
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Add(LanguageHelper Language, InputCityDto dto);
        /// <summary>
        /// Update exist city
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool Edit(LanguageHelper Language, InputCityDto dto);
        /// <summary>
        /// Delete city by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> </returns>
        bool? Delete(int Id);

        #endregion

        #region CityDto
        List<CityDto> GetAllCities(LanguageHelper language);
        CityDto GetCityById(LanguageHelper language, int id);
        #endregion

        //IPagedList<CityDto> searchAndOrder(List<CityDto> allCities, string CurrentFilter, int? Page, string SearchString, NawafizApp.Common.Sort sortOrder = Sort.IdDown_Up);


        #region Validator

        bool IsNameUnique(string name, int? id);

        bool IsExistId(int id);

        #endregion


    }
}
