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
    public class InputTownValidator : AbstractValidator<InputTownDto>
    {

        public readonly ITownService _ITownService;
        public InputTownValidator(ITownService TownService)
            {
            _ITownService = TownService;

            RuleSet("AddTown", () =>
               {
                   RuleFor(m => m.CityId).SetValidator(new IsCityIdExistPropertyValidator(_ITownService));
                   
                   //RuleFor(m => m.ArabicTownName).SetValidator(new IsPlaceIdUniqueAddPropertyValidator(_ITownService));
                   //Custom(m =>
                   //{
                   //    return !_ITownService.IsCityIdExist(m.CityId)
                   //        ? new ValidationFailure("CityId", CityAndTown.CityIdNotExist)
                   //        : null;
                   //});
                   CommonRules();

               });



            RuleSet("EditTown", () =>
            {
                //Custom(m =>
                //{
                //    return !_ITownService.IsCityIdExist(m.CityId)
                //        ? new ValidationFailure("CityId", CityAndTown.CityIdNotExist)
                //        : null;
                //});
                RuleFor(m => m.Id).SetValidator(new IsIdExistTownPropertyValidator(_ITownService));
                //RuleFor(m => m.ArabicTownName).SetValidator(new IsPlaceIdUniqueEditPropertyValidator(_ITownService));
                //Custom(m =>
                //{
                //    return !_ITownService.IsIdExist(m.Id)
                //        ? new ValidationFailure("Id", CityAndTown.Id_failed)
                //        : null;
                //});

                CommonRules();
            });

                CommonRules();
            }

            private void CommonRules()
            {
                RuleFor(m => m.ArabicTownName).NotEmpty().WithMessage("اسم البلدة باللغة العربية مطلوب").Length(0,40).WithMessage("الاسم طويل");
                RuleFor(m => m.EnglishTownName).NotEmpty().WithMessage("اسم البلدة باللغة العربية مطلوب").Length(0, 40).WithMessage("الاسم طويل");
                RuleFor(m => m.CityId).NotEmpty().WithMessage("معرف المدينة مطلوب");

                RuleFor(m => m.CityId).SetValidator(new IsCityIdExistPropertyValidator(_ITownService));
        }

        }
    }

