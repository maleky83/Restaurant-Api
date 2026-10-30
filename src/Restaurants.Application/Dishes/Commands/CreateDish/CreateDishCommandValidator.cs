using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.KiloCalories)
                .GreaterThanOrEqualTo(0);
        }
    }
}
