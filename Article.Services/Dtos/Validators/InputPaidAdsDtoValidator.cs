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
    public class InputPaidAdsDtoValidator : AbstractValidator<InputPaidAdsDto>
    {
        private readonly IPaidAdsService _IPaidAdsService;
        
        public InputPaidAdsDtoValidator(IPaidAdsService PaidAdsService)
        {
            _IPaidAdsService = PaidAdsService;
            RuleSet("UploadPaidAds", () =>
            {
                CommonRules();
            });
            //RuleSet("Edit", () =>
            //{
            //    //RuleFor(m => m.Id).SetValidator(new IsTownExistPropertyValidator(_ClassifyService));
            //    CommonRules();
            //});

            CommonRules();
        }

        private void CommonRules()
        {
            //RuleFor(m => m.Body).NotEmpty().WithMessage("محتوى الرسالة مطلوب").Length(1,4000).WithMessage("الرسالة طويلة جدا");
            //RuleFor(m => m.SenderName).NotEmpty().WithMessage("اسم المرسل مطلوب").Length(3, 75).WithMessage("الاسم طويل جدا");
            //RuleFor(m => m.SenderPhoneNumber).Matches(@"^[0-9]*$").WithMessage("الرقم غير صحيح").Length(10).WithMessage("الرقم غير صحيح");
            //RuleFor(m => m.SenderId).SetValidator(new IsSenderIdExistPropertyValidator(_IMessagingService));
            
           // RuleFor(m => m.BrandId).SetValidator(new IsBrandIdExistPaidAdsPropertyValidator(_IPaidAdsService));
           

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
