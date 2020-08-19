using AspNetCoreWebApi.Application.Commands;
using FluentValidation;

namespace AspNetCoreWebApi.Application.Validations
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(request => request.UserName).NotEmpty();
            RuleFor(request => request.Password).NotEmpty();
        }
    }
}
