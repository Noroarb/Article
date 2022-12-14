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
    public class NotificationdictionaryDtoValidator : AbstractValidator<NotificationdictionaryDto>
    {
        public readonly INotificationService _INotificationService;
        
        public NotificationdictionaryDtoValidator(INotificationService INotificationService)
        {
            _INotificationService = INotificationService;
            RuleSet("send_notify", () =>
            {
                CommonRules();
            });
            //RuleSet("EditProduct", () =>
            //{
            //    RuleFor(m => m.Id).SetValidator(new IsProductIdExistPropertyValidator(_IProductsService));
            //    CommonRules();
            //});

            CommonRules();
        }

        private void CommonRules()
        {
            RuleFor(m => m.Discription).NotEmpty().WithMessage("الوصف مطلوب").Length(1, 200).WithMessage("الوصف يجب ان يكون أقل من 200 محرف");
            RuleFor(m => m.Title).NotEmpty().WithMessage("العنوان مطلوب").Length(1, 70).WithMessage("العنوان يجب أن يكون أقل من 70 محرف");
            RuleFor(m => m.Type).NotNull().WithMessage("النوع مطلوب");//.LessThan(2).WithMessage("النوع 0 أو 1").GreaterThan(-1).WithMessage("النوع 0 أو 1");
            RuleFor(m => m.Update).Length(0, 300).WithMessage("العنوان يجب أن يكون أقل من 300 محرف");
            ////   RuleFor(m => m.ParentID).NotEmpty().WithMessage("تصنيف الفئة مطلوب").LessThan(3).WithMessage("المستوى يجب أن يكون أقل من 3");
            //RuleFor(m => m.Sort).NotEmpty().WithMessage("ترتيب الفئة مطلوب");

            //RuleFor(m => m.RestaurantId).SetValidator(new IsRestaurantIdExistPropertyValidator(_IProductsService));
            //RuleFor(m => m.Id).SetValidator(new IsProductIdExistPropertyValidator(_IProductsService));

            //RuleFor(m => m.ArabicName).NotEmpty().WithMessage("الاسم بالعربية مطلوب").Length(1, 100).WithMessage("طول السلسلة أقل من 100 حرف");
            //RuleFor(m => m.EnglishName).NotEmpty().WithMessage("الاسم بالانجليزية مطلوب").Length(1, 100).WithMessage("طول السلسلة أقل من 100 حرف");

            //RuleFor(m => m.ClassifyName).NotEmpty().WithMessage(ClassifyResource.Classify_Add_NameError_IsEmpty).Length(0, 40).WithMessage(ClassifyResource.Classify_Add_NameError_TooLarge);
            //RuleFor(m => m.Description).NotEmpty().WithMessage(ClassifyResource.Classify_Add_DescribeError_IsEmpty).Length(0, 4000).WithMessage(ClassifyResource.Classify_Add_DescribeError_TooLarge);
            //RuleFor(m => m.Email).Length(0, 70).WithMessage(ClassifyResource.Classify_Add_EmailError_NotValid).EmailAddress().WithMessage(ClassifyResource.Classify_Add_EmailError_NotCorrect);
            //RuleFor(m => m.Gps_Latitude).Length(0, 19).WithMessage(ClassifyResource.Classify_Add_GpsError_NotCorrect);
            //RuleFor(m => m.Gps_Longitude).Length(0, 19).WithMessage(ClassifyResource.Classify_Add_GpsError_NotCorrect);
            //RuleFor(m => m.Mobile).Matches(@"^[0-9]*$").WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect).Length(10).WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect);
            //RuleFor(m => m.FullName).NotEmpty().WithMessage(ClassifyResource.Classify_Add_FullNameError_IsEmpty).Length(0, 40).WithMessage(ClassifyResource.Classify_Add_FullNameError_TooLarge);
            //RuleFor(m => m.TownId).NotEmpty().When(m => m.Gps_Latitude == null).When(m => m.Gps_Longitude == null).WithMessage(ClassifyResource.GPS_And_TownId_NotEmptyTogether);
            //RuleFor(m => m.Phone).Matches(@"^[0-9]*$").WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect).Length(0, 10).WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect);
            ////.WithMessage(ClassifyResource.Classify_Add_MobileError_NotCorrect)
            //RuleFor(m => m.TownId).SetValidator(new IsTownExistPropertyValidator(_ClassifyService));


        }

    }
}
