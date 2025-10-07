namespace Web.Service.Dto;

public class CreateUserDto
{
    public required string Name { get; set; }

    public required string UserName { get; set; }

    public required string Email { get; set; }

    public string? Phone { get; set; }

    public string? Website { get; set; }
}