using Entities.Concrete.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validations
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
          RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);  
        }
    }
}
