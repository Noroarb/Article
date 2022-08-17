using Card.Services.Interfaces;
using FluentValidation.Validators;
using Card.Services.Dtos;
using Card.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using NawafizApp.Common.Resources;

namespace Card.Services.Dtos.Validators.PropertyValidators
{
    public class IsEnglishNameAddUniquePropertyValidator : PropertyValidator
    {
        public readonly ICityService _cityService;
        public IsEnglishNameAddUniquePropertyValidator(ICityService CityService)
            : base("هذا الاسم موجود")
        {
            _cityService = CityService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
           // string email = context.PropertyValue as string;
            InputCityDto m = context.Instance as InputCityDto;
            return _cityService.IsNameUnique(m.EnglishCityName,null);
        }
    }
}
