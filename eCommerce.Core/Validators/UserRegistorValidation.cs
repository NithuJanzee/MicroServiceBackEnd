using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class UserRegistorValidation : AbstractValidator<RegistorRequestDTO>
{
    public UserRegistorValidation()
    {
        //email
        RuleFor(e => e.Email).EmailAddress().WithMessage("Email is not valid")
            .NotEmpty().WithMessage("Email is required");

        //Password
        RuleFor(p => p.Password).NotEmpty().WithMessage("Password is Required")
            .Length(6, 20).WithMessage("Password must be between 6 and 20 characters");

        RuleFor(n => n.PersonName).NotEmpty().WithMessage("Name is Required");

        RuleFor(g => g.Gender).IsInEnum();
    }
}

