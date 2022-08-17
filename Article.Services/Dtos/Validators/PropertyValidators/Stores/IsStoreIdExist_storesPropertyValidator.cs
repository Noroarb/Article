using Card.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos.Validators.PropertyValidators
{
    public class IsRestaurantIdExist_RestaurantsPropertyValidator : PropertyValidator
    {
        public readonly IRestaurantService _IRestaurantService;
        public IsRestaurantIdExist_RestaurantsPropertyValidator(IRestaurantService RestaurantService)
            : base("هذا المتجر غير موجود")
        {
            _IRestaurantService = RestaurantService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputRestaurantDto m = context.Instance as InputRestaurantDto;
            
             return _IRestaurantService.IsIdExist(m.Id);
            
        }
    }
}
