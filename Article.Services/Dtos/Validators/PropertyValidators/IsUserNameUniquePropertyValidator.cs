using Article.Services.Interfaces;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos.Validators.PropertyValidators
{
    public class IsUserNameUniquePropertyValidator : PropertyValidator
    {
        public readonly IUserService _userService;
        public IsUserNameUniquePropertyValidator(IUserService userService)
            : base("اسم المستخدم موجود")
        {
            _userService = userService;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
          //  string email = context.PropertyValue as string;
            RegisterUserDto m = context.Instance as RegisterUserDto;
            return _userService.IsUserNameUnique(m.UserName);
        }
    }
}
