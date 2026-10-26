using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];

    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Category)
            .Must(category => validCategories.Contains(category, StringComparer.OrdinalIgnoreCase))
            .WithMessage("must contain Italian or Mexican or Japanese or American or Indian");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress();

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$");
    }
}
