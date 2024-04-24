using System.Text.RegularExpressions;
using FluentValidation;
using LAB06.DTOs;

namespace LAB06.Validators
{

    public class AnimalCreateRequestValidator : AbstractValidator<CreateAnimalRequest>
    {
        public AnimalCreateRequestValidator()
        {
            RuleFor(s => s.Name).MaximumLength(50).NotNull();
            RuleFor(s => s.Description).MaximumLength(200);
            RuleFor(s => s.Category).MaximumLength(200).NotNull();
            RuleFor(s => s.Area).MaximumLength(200).NotNull();
        }
    }
}