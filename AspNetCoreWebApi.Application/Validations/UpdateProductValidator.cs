﻿using AspNetCoreWebApi.Application.Commands;
using FluentValidation;

namespace AspNetCoreWebApi.Application.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.ProductId).GreaterThan(0);
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryId).NotNull();
        }
    }
}
