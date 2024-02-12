using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class SeasonDiscountValidator : AbstractValidator<SeasonDiscount>
    {
        public SeasonDiscountValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                 .MaximumLength(50).WithMessage(ValidationMessages.ValidationMessages.HasMaxLength); 
            RuleFor(x => x.Description).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                       .Length(40, 200).WithMessage(ValidationMessages.ValidationMessages.HasLength);
            RuleFor(x => x.TitleDescription).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty)
                                            .Length(40, 200).WithMessage(ValidationMessages.ValidationMessages.HasLength);
            RuleFor(x => x.ImgPath).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.FileCanNotBeEmpty);
        }
    }
}
