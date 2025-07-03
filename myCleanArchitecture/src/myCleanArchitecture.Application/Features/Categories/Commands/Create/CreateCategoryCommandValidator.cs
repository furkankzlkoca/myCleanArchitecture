using FluentValidation;
using myCleanArchitecture.Shared.FeatureModels.Categories.Commands;

namespace myCleanArchitecture.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandValidator(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            ApplyValidationsRules();
            ApplyCustomValidationRules();
        }
        private void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ConstantMessages.NameIsRequired)
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olabilir.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama alanı en fazla 500 karakter olabilir.");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(x => x)
                .MustAsync(async (category, cancellation) => !await _categoryRepository.AnyAsync(x => x.Name == category.Name))
                .WithMessage("Kategori ismi mevcuttur.");
        }
    }
}
