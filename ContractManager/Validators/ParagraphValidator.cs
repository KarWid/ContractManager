namespace ContractManager.Validators
{
    using FluentValidation;
    using ContractManager.ViewModels.Paragraphs;

    public class ParagraphValidator : AbstractValidator<ParagraphVM>
    {
        public ParagraphValidator()
        {
            RuleFor(paragraph => paragraph.Id).NotNull();
        }
    }
}
