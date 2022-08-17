using Card.Common;
using Card.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface ITownService
    {
        #region InputTownDto

        int Add(LanguageHelper Language, InputTownDto dto);
        bool? Edit(LanguageHelper Language, InputTownDto dto);
        bool? Delete(int Id);

        #endregion

        #region TownDto

        TownDto GetTownById(LanguageHelper language, int id);
        CityDto GetCityByTownId(LanguageHelper language, int id);
        List<TownDto> GetAllTowns(LanguageHelper language);

        #endregion

        #region Validator

        bool IsIdExist(int id);
        bool IsCityIdExist(int CityId);
        //bool ISPlaceIdUnique(string ArabicTownName, int CityId, int? editedId);

        #endregion

    }
}
