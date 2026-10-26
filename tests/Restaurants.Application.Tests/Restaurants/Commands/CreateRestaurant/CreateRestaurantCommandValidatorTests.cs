using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        var command = new CreateRestaurantCommand
        {
            Name = "test",
            ContactEmail = "test@gmail.com",
            Category = "italian",
            PostalCode = "12-345"
        };

        var validator = new CreateRestaurantCommandValidator();

        //act 
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand
        {
            Name = "a",
            ContactEmail = "testgmail.com",
            Category = "ita",
            PostalCode = "12345"
        };

        var validator = new CreateRestaurantCommandValidator();

        // act

        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrors();
    }

    [Theory]
    [InlineData("italian")]
    [InlineData("Mexican")]
    [InlineData("Indian")]
    [InlineData("American")]
    [InlineData("Japanese")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange
        var commnad = new CreateRestaurantCommand { Category = category };
        var validator = new CreateRestaurantCommandValidator();

        // act 
        var result = validator.TestValidate(commnad);

        // assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory]
    [InlineData("12333")]
    [InlineData("23-3 33")]
    [InlineData("123-33")]
    [InlineData("22 223")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange
        var command = new CreateRestaurantCommand { PostalCode = postalCode };
        var validator = new CreateRestaurantCommandValidator();

        // act
        var result = validator.TestValidate(command);

        // assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}
