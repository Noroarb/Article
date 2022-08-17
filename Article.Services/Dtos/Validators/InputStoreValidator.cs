using Card.Domain;
using Card.Services.Dtos.Validators.PropertyValidators;
using Card.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Dtos.Validators
{
    public class InputRestaurantValidator : AbstractValidator<InputRestaurantDto>
    {
        public readonly IRestaurantService _IRestaurantService;
        
        public InputRestaurantValidator(IRestaurantService RestaurantService)
        {
            _IRestaurantService = RestaurantService;
            //RuleSet("AddProduct", () =>
            //{
            //    CommonRules();
            //});
            RuleSet("EditRestaurant_Info", () =>
            {
                RuleFor(m => m.Id).SetValidator(new IsRestaurantIdExist_RestaurantsPropertyValidator(_IRestaurantService));
                CommonRules();
            });
            RuleSet("AddRestaurant", () =>
            {
                CommonRules();
            });
            CommonRules();
        }

        private void CommonRules()
        {
            RuleFor(m => m.RestaurantName).NotEmpty().WithMessage("اسم المطعم مطلوب").Length(0, 40).WithMessage("الاسم طويل جدا");
            RuleFor(m => m.Mobile1).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Mobile2).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Mobile3).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Phone1).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(3,10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Phone2).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(3,10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Phone3).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(3,10).WithMessage("الرقم مرفوض");
            RuleFor(m => m.Fax1).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(3, 10).WithMessage("الرقم مرفوض");
            //RuleFor(m => m.Phone3).Matches(@"^[0-9]*$").WithMessage("الرقم مرفوض").Length(3, 10).WithMessage("الرقم مرفوض");
            
            //RuleFor(m => m.NeighborhoodId).NotEmpty().WithMessage(ClassifyResource.Classify_Search_TownIDError_IsEmpty).SetValidator(new IsIdExistNeighborhood_RestaurantsPropertyValidator(_RestaurantService));

            RuleFor(m => m.Description).Length(0, 500).WithMessage("الوصف طويل جدا");
            RuleFor(m => m.LocationText).Length(0, 200).WithMessage("الموقع طويل جدا");
            RuleFor(m => m.Sort).NotEmpty().WithMessage("الترتيب مطلوب");

            //RuleFor(m => m.ArabicName).NotEmpty().WithMessage("الاسم بالعربية مطلوب").Length(1, 100).WithMessage("طول السلسلة أقل من 100 حرف");
            //RuleFor(m => m.EnglishName).NotEmpty().WithMessage("الاسم بالانجليزية مطلوب").Length(1, 100).WithMessage("طول السلسلة أقل من 100 حرف");

            //RuleFor(m => m.ClassifyName).NotEmpty().WithMessage(ClassifyResource.Classify_Add_NameError_IsEmpty).Length(0, 40).WithMessage(ClassifyResource.Classify_Add_NameError_TooLarge);
            //RuleFor(m => m.Description).NotEmpty().WithMessage(ClassifyResource.Classify_Add_DescribeError_IsEmpty).Length(0, 4000).WithMessage(ClassifyResource.Classify_Add_DescribeError_TooLarge);
            RuleFor(m => m.Email1).Length(0, 70).WithMessage("الايميل طويل جدا").EmailAddress().WithMessage("تأكد من صحة الايميل");
            RuleFor(m => m.Gps_Latitude).Length(0, 19).WithMessage("الموقع طويل جدا");
            RuleFor(m => m.Gps_Longitude).Length(0, 19).WithMessage("الموقع طويل جدا");
            //RuleFor(m => m.Mobile).Matches(@"^[0-9]*$").WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect).Length(10).WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect);
            //RuleFor(m => m.FullName).NotEmpty().WithMessage(ClassifyResource.Classify_Add_FullNameError_IsEmpty).Length(0, 40).WithMessage(ClassifyResource.Classify_Add_FullNameError_TooLarge);
            //RuleFor(m => m.TownId).NotEmpty().When(m => m.Gps_Latitude == null).When(m => m.Gps_Longitude == null).WithMessage(ClassifyResource.GPS_And_TownId_NotEmptyTogether);
            //RuleFor(m => m.Phone).Matches(@"^[0-9]*$").WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect).Length(0, 10).WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect);
            ////.WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect)
            //RuleFor(m => m.TownId).SetValidator(new IsTownExistPropertyValidator(_ClassifyService));
            RuleFor(m => m.Website).Length(0, 200).WithMessage("هذا الحقل غير صحيح");
            RuleFor(m => m.Instagram).Length(0, 200).WithMessage("هذا الحقل غير صحيح");
            RuleFor(m => m.Facebook).Length(0, 200).WithMessage("هذا الحقل غير صحيح");
            RuleFor(m => m.Twitter).Length(0, 200).WithMessage("هذا الحقل غير صحيح");
            RuleFor(m => m.LinkedIn).Length(0, 200).WithMessage("هذا الحقل غير صحيح");
            RuleFor(m => m.Snapchat).Length(0, 200).WithMessage("هذا الحقل غير صحيح");

            RuleFor(m => m.TownId).SetValidator(new IsTownIdExist_RestaurantsPropertyValidator(_IRestaurantService));
            RuleFor(m => m.UserId).SetValidator(new IsUserIdExist_RestaurantsPropertyValidator(_IRestaurantService));

        }

    }
}
