using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] AllowPageSizes = [5, 10, 15, 30];

    private readonly string[] AllowSortByColumnNames =
        [nameof(RestaurantDto.Name).ToLower(),
        nameof(RestaurantDto.Description).ToLower(),
        nameof(RestaurantDto.Category).ToLower()];

    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => AllowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", AllowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => AllowSortByColumnNames.Contains(value?.ToLower()))
            .When(r => r.SortBy != null)
            .WithMessage(
                $"Sort by is optional, or must be in [{string.Join(",", AllowSortByColumnNames)}]");
    }
}
