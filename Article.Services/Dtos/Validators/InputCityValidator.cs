using FluentValidation;
using FluentValidation.Results;
using Card.Services.Interfaces;
using Card.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Card.Common.Resources;
using Card.Services.Dtos.Validators.PropertyValidators;

namespace Card.Services.Dtos.Validators
{
    public class InputCityValidator : AbstractValidator<InputCityDto>
    {

        public readonly ICityService _lCityService;
        public InputCityValidator(ICityService cityService)
            {
            _lCityService = cityService;

            RuleSet("AddCity", () =>
               {

                   RuleFor(m => m.ArabicCityName).SetValidator(new IsArabicNameAddUniquePropertyValidator(_lCityService));
                   RuleFor(m => m.EnglishCityName).SetValidator(new IsEnglishNameAddUniquePropertyValidator(_lCityService));
                   //Custom(m =>
                   //{
                   //    return !_lCityService.IsNameUnique(m.EnglishCityName, null)
                   //       ? new ValidationFailure("EnglishCityName", CityAndTown.IsNameUnique_ValidatorError)
                   //       : null;
                   //});


                   CommonRules();

               });



            RuleSet("EditCity", () =>
            {
                RuleFor(m => m.ArabicCityName).SetValidator(new IsArabicNameEditUniquePropertyValidator(_lCityService));
                //Custom(m =>
                //{
                //    return !_lCityService.IsNameUnique(m.ArabicCityName, m.Id)
                //       ? new ValidationFailure("ArabicCityName", CityAndTown.IsNameUnique_ValidatorError)
                //       : null;
                //});
                RuleFor(m => m.EnglishCityName).SetValidator(new IsEnglishNameEditUniquePropertyValidator(_lCityService));
                //Custom(m =>
                //{
                //    return !_lCityService.IsNameUnique(m.EnglishCityName, m.Id)
                //       ? new ValidationFailure("EnglishCityName", CityAndTown.IsNameUnique_ValidatorError)
                //       : null;
                //});
                RuleFor(m => m.Id).SetValidator(new IsExistIdEditUniquePropertyValidator(_lCityService));
                //Custom(m =>
                //{
                //    return !_lCityService.IsExistId(m.Id)
                //        ? new ValidationFailure("Id", CityAndTown.Id_failed)
                //        : null;
                //});

                CommonRules();
            });

                CommonRules();
            }

            private void CommonRules()
            {
                RuleFor(m => m.ArabicCityName).NotEmpty().WithMessage("الاسم باللغة العربية مطلوب").Length(0,40).WithMessage("هذا الاسم طويل");
                RuleFor(m => m.EnglishCityName).NotEmpty().WithMessage("الاسم باللغة الانكليزية مطلوب").Length(0, 40).WithMessage("هذا الاسم طويل");
                RuleFor(m => m.Sort).NotEmpty().WithMessage("ترتيب المدينة مطلوب");
        }

    }
    }

