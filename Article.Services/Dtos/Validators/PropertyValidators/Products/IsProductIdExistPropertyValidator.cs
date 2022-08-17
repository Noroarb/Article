using Card.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos.Validators.PropertyValidators
{
    public class IsProductIdExistPropertyValidator : PropertyValidator
    {
        public readonly IProductsService _IProductsService;
        public IsProductIdExistPropertyValidator(IProductsService ProductsService)
            : base("هذا المنتج غير موجود")
        {
            _IProductsService = ProductsService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputProductDto m = context.Instance as InputProductDto;
            //if (m != null)
                return _IProductsService.IsProductIdExist(m.Id);
            //else
            //    return true;
            
        }
    }
}
