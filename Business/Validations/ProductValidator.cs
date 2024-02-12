using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
            RuleFor(x => x.Description).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                       .Length(40,200).WithMessage(ValidationMessages.ValidationMessages.HasLength);
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
            RuleFor(x => x.Price).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                 .GreaterThan(0).WithMessage(ValidationMessages.ValidationMessages.CanNotBeNegative);
            RuleFor(x => x.SalePrice).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                     .GreaterThan(0).WithMessage(ValidationMessages.ValidationMessages.CanNotBeNegative);
            RuleFor(x => x.ImgPath).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.FileCanNotBeEmpty);
            RuleFor(x => x.Model).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.FileCanNotBeEmpty);
            RuleFor(x => x.StockCount).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.FileCanNotBeEmpty)
                                      .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.ValidationMessages.CanNotBeNegative); ;
        }
    }
}
