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
    public class IsArabicNameEditUniquePropertyValidator : PropertyValidator
    {
        public readonly ICityService _cityService;
        public IsArabicNameEditUniquePropertyValidator(ICityService CityService)
            : base("هذا الاسم موجود")
        {
            _cityService = CityService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
           // string email = context.PropertyValue as string;
            InputCityDto m = context.Instance as InputCityDto;
            var c= _cityService.IsNameUnique(m.ArabicCityName, m.Id);
            return c;
        }
    }
}
