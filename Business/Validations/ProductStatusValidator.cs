using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class ProductStatusValidator : AbstractValidator<ProductStatus>
    {
        public ProductStatusValidator() 
        {
            RuleFor(x => x.New).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
            RuleFor(x => x.InStock).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
            RuleFor(x => x.StockOut).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
        }
    }
}
