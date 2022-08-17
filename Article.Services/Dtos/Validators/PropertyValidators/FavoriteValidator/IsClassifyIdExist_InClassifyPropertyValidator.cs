using Market.Services.Interfaces;
using FluentValidation.Validators;
using Market.Services.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Market.Common.Resources;

namespace Market.Services.Dtos.Validators.PropertyValidators
{
    public class IsClassifyIdExist_InClassifyPropertyValidator : PropertyValidator
    {
        public readonly IFavoriteRestaurantService _FavoriteService;
      
        public IsClassifyIdExist_InClassifyPropertyValidator(IFavoriteRestaurantService favoriteService)
            : base("هذا المنتج غير موجود")
        {
            _FavoriteService = favoriteService;
           
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
           // string email = context.PropertyValue as string;
            FavoriteRestaurantDto m = context.Instance as FavoriteRestaurantDto;
           
               return  _FavoriteService.IsProductIdExist_InProducts(m.ProductId);
          
        }
    }
}
