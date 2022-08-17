using Delivery.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.Dtos.Validators.PropertyValidators
{
    public class IsBrandIdExistInputProductPropertyValidator : PropertyValidator
    {
        public readonly IProductsService _IProductsService;
        public IsBrandIdExistInputProductPropertyValidator(IProductsService ProductsService)
            : base("هذه العلامة التجارية غير موجودة")
        {
            _IProductsService = ProductsService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputProductDto m = context.Instance as InputProductDto;
            if (m.BrandId.HasValue)
                return _IProductsService.IsBrandIdExist((int)m.BrandId);
            else
                return true;

        }
    }
}
