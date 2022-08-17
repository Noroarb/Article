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
    public class IsCityIdExistPropertyValidator : PropertyValidator
    {
        public readonly ITownService _ITownService;
        public IsCityIdExistPropertyValidator(ITownService TownService)
            : base("هذه المحتفظة غير موجودة")
        {
            _ITownService = TownService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            // string email = context.PropertyValue as string;

           InputTownDto m = context.Instance as InputTownDto;
           
           return _ITownService.IsCityIdExist(m.CityId);
           
              
        }
    }
}
