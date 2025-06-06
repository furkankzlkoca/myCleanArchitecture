
using FluentValidation;
using myCleanArchitecture.Domain.Models;
using myCleanArchitecture.Shared.FeatureModels.Products.Commands;

namespace myCleanArchitecture.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public UpdateProductCommandValidator(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
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
            RuleFor(x => x.CategoryId)
                .MustAsync(async (categoryId, cancellation) => await _categoryRepository.AnyAsync(x => x.Id == categoryId)).WithMessage("Kategori bulunamadı.")
                .MustAsync(async (categoryId, cancellation) =>
                {
                    var category = await _categoryRepository.Get(x => x.Id == categoryId);
                    return category.IsActive;
                }).WithMessage("Pasif bir kategoriye ürün eklenemez.");

        }
    }
}
