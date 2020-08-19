using AspNetCoreWebApi.Application.Commands;
using FluentValidation;

namespace AspNetCoreWebApi.Application.Validations
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryId).NotNull();
        }
    }
}
