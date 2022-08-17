using FluentValidation;
using FluentValidation.Results;
using Article.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Article.Services.Dtos.Validators.PropertyValidators;

namespace Article.Services.Dtos.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public readonly IUserService _userService;
        //public RegisterUserValidator()
        public RegisterUserValidator(IUserService userService)
        {
            _userService = userService;
            RuleSet("Register", () =>
            {
                //RegisterRules();
                CommonRules();
            });
            RuleSet("Register_RestaurantManager", () =>
            {
                //RegisterRules();
                CommonRules();
            });
            RuleSet("Register_ArticleEmployee", () =>
            {
                //RegisterRules();
                CommonRules();
            });
            RuleSet("Register_BicycleMan", () =>
            {
                //RegisterRules();
                CommonRules();
            });
            CommonRules();
        }

        public void CommonRules()
        {
            RuleFor(m => m.UserName).NotEmpty().WithMessage("this field is requiered").Length(1, 256).WithMessage("the user name is very long")/*.Matches(@"^[a-zA-Z]*$").WithMessage("يجب ألا يحوي على أرقام")*/;
           RuleFor(m => m.Password).NotEmpty().WithMessage("this field is requiered").Length(3, 25).WithMessage("not accectable password");
            RuleFor(m => m.ConfirmPassword).NotEmpty().WithMessage("this field is requiered").Equal(x => x.Password).WithMessage("not correct");
            RuleFor(m => m.FullName).NotEmpty().WithMessage("this field is requiered");
           
            RuleFor(m => m.UserName).SetValidator(new IsUserNameUniquePropertyValidator(_userService));
        }
    }
}
