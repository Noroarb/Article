using Card.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos.Validators.PropertyValidators
{
    public class IsUserIdExist_RestaurantsPropertyValidator : PropertyValidator
    {
        public readonly IRestaurantService _IRestaurantService;
        public IsUserIdExist_RestaurantsPropertyValidator(IRestaurantService RestaurantService)
            : base("هذا المستخدم غير موجود")
        {
            _IRestaurantService = RestaurantService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
             InputRestaurantDto m = context.Instance as InputRestaurantDto;
           // if (m.UserId.HasValue)
                return _IRestaurantService.IsUserIdExist((Guid)m.UserId);
            return true;
            
        }
    }
}
