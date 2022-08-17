using Market.Common;
using Market.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Interfaces
{
    public interface IFavoriteRestaurantService
    {
        #region InputFavoriteDto
        int Add(FavoriteRestaurantDto dto, Guid UserGuid);


        bool Delete(int ProductId, Guid UserId);

        bool IsProductIdExist_InProducts(int ProductId);
        bool IsProductIdExist_InFavoriteForThisUser(int ProductId, Guid UserId);

        #endregion


        #region FavoriteDto

        List<ProductSimplifyFavoriteDto> GetFavorites(LanguageHelper language, Guid UserGuid);

        #endregion


    }
}
