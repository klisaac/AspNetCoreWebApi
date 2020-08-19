using AspNetCoreWebApi.Application.Commands;
using FluentValidation;

namespace AspNetCoreWebApi.Application.Validations
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(request => request.ProductId).GreaterThan(0);
        }
    }
}
