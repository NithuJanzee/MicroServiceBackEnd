
using eCommerce.Core.DTO;
using FluentValidation;

namespace eCommerce.Core.Validators;

public class UserLoginRequestValidation : AbstractValidator<LoginRequestDTO>
{
    public UserLoginRequestValidation()
    {
        //Email Ruels 
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");

        //Password Rules
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
    }

}

