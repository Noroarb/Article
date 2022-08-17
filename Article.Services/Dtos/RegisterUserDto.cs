using Article.Services.Dtos.Validators;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Dtos
{
    //[Validator(typeof(RegisterUserValidator))]
    public class RegisterUserDto
    {
        //public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        public string PhoneNumber { get; set; }
    }
}
