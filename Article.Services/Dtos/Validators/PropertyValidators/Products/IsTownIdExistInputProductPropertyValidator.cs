using Delivery.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Services.Dtos.Validators.PropertyValidators
{
    public class IsTownIdExistInputProductPropertyValidator : PropertyValidator
    {
        public readonly IProductsService _IProductsService;
        public IsTownIdExistInputProductPropertyValidator(IProductsService ProductsService)
            : base("هذه المنطقة غير مدعومة غير موجود")
        {
            _IProductsService = ProductsService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputProductDto m = context.Instance as InputProductDto;
            //if (m != null)
                return _IProductsService.IsTownIdExist(m.TownId);
            //else
            //    return true;
            
        }
    }
}
