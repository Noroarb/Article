using FluentValidation;
using FluentValidation.Results;
using Market.Services.Interfaces;
using Market.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Market.Common.Resources;
using Market.Services.Dtos.Validators.PropertyValidators;

namespace Market.Services.Dtos.Validators
{
    public class FavoriteValidator : AbstractValidator<FavoriteRestaurantDto>
    {

        public readonly IFavoriteRestaurantService _FavoriteService;
       
        public FavoriteValidator(IFavoriteRestaurantService favoriteService)
        {
            _FavoriteService = favoriteService;
            

            RuleSet("Add", () =>
             {
                

                 CommonRules();
                
             });

       
        
           

            CommonRules();
        }

        private void CommonRules()
        {
            //RuleFor(m => m.ArabicName).NotEmpty().WithMessage("اسم الفئة مطلوب").Length(0, 24).WithMessage("الاسم مرفوض");
            //RuleFor(m => m.EnglishName).NotEmpty().WithMessage("اسم الفئة مطلوب").Length(0, 24).WithMessage("الاسم مرفوض");
            RuleFor(m => m.ProductId).NotEmpty().WithMessage("هذا الحقل مطلوب");
           

            RuleFor(m => m.ProductId).SetValidator(new IsClassifyIdExist_InClassifyPropertyValidator(_FavoriteService));

            ////   RuleFor(m => m.ParentID).NotEmpty().WithMessage("تصنيف الفئة مطلوب").LessThan(3).WithMessage("المستوى يجب أن يكون أقل من 3");
            //RuleFor(m => m.Sort).NotEmpty().WithMessage("ترتيب الفئة مطلوب");
        }

    }
}
