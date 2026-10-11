using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities;

public class User : IdentityUser
{
    public DateOnly DateOfBirth { get; set; }
    public string? Nationality { get; set; }
}
