using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class ColorValidator : AbstractValidator<Color>
    {
        public ColorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                .MaximumLength(15).WithMessage(ValidationMessages.ValidationMessages.NameHasMaxLength);
            RuleFor(x => x.ColorCode).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
        }
    }
}
