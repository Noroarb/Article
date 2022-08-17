using Card.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos.Validators.PropertyValidators
{
    public class IsCategoryIdExistInputProductPropertyValidator : PropertyValidator
    {
        public readonly IProductsService _IProductsService;
        public IsCategoryIdExistInputProductPropertyValidator(IProductsService ProductsService)
            : base("هذه الفئة غير موجودة أو هذه الفئة لها اولاد")
        {
            _IProductsService = ProductsService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            //string email = context.PropertyValue as string;
            InputProductDto m = context.Instance as InputProductDto;
           
            return _IProductsService.IsCategoryIdExist_And_NotHasChild(m.CategoryId);
         
            
        }
    }
}
