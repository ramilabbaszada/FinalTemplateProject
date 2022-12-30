using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserForRegisterValidator: AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator() {
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u=>u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Password.Length).GreaterThan(10);
        }
    }
}
