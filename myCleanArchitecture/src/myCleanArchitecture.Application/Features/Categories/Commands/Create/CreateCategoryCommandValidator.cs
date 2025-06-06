

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
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(x => x.Name)
                .MustAsync(async (name, cancellation) => !await _categoryRepository.IsNameExist(name))
                .WithMessage("Category with this name already exists.");
        }
    }
}
