using Market.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Services.Dtos.Validators.PropertyValidators
{
    public class IsBrandIdExistPropertyValidator : PropertyValidator
    {
        public readonly IBrandService _IBrandService;
        public IsBrandIdExistPropertyValidator(IBrandService IBrandService)
                    : base("هذه العلامة التجارية غير موجودة")
            {
                _IBrandService = IBrandService;
            }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            BrandDto m = context.Instance as BrandDto;
           
             return _IBrandService.IsBrandIdExist(m.Id);
            
        }
    }
}
