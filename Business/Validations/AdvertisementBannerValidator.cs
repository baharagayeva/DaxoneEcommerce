using Entities.Concrete.TableModels;
using FluentValidation;

namespace Business.Validations
{
    public class AdvertisementBannerValidator : AbstractValidator<AdvertisementBanner>
    {
        public AdvertisementBannerValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("dhjjjjjjjjjjfk")
                                 .MaximumLength(50).WithMessage(ValidationMessages.ValidationMessages.HasMaxLength);
            RuleFor(x => x.Description).NotEmpty().WithMessage("sfsfjjjjjjjjjjfk")
                                       .Length(10, 200).WithMessage(ValidationMessages.ValidationMessages.HasLength);
            RuleFor(x => x.ImgPath).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.FileCanNotBeEmpty);
            RuleFor(x => x.Discount).NotEmpty().WithMessage(ValidationMessages.ValidationMessages.CanNotBeEmpty);
        }
    }
}
