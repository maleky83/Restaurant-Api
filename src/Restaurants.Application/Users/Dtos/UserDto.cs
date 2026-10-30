namespace Restaurants.Application.Users.Dtos
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Nationality { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
