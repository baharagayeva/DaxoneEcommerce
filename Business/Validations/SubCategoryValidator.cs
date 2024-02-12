using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class SubCategoryValidator : AbstractValidator<SubCategory>
    {
        public SubCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
            RuleFor(x => x.CategoryID).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
        }
    }
}
