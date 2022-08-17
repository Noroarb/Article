using Delivery.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.Dtos.Validators.PropertyValidators
{
    public class IsBrandIdExistPaidAdsPropertyValidator : PropertyValidator
    {
        private readonly IPaidAdsService _IPaidAdsService;

        public IsBrandIdExistPaidAdsPropertyValidator(IPaidAdsService PaidAdsService)
                    : base("هذه العلامة التجارية غير موجودة")
            {
            _IPaidAdsService = PaidAdsService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputPaidAdsDto m = context.Instance as InputPaidAdsDto;
           if(m.BrandId.HasValue)
             return _IPaidAdsService.IsBrandIdExist((int)m.BrandId);
            return true;

            
        }
    }
}
