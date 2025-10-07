using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Database.Models;
using Web.Service.Dto;
using Web.Service.Interfaces;
using Web.Service.src;

namespace Web.Test;

public class UnitTest1
{
    private readonly DatabaseContext _dbContext;
    private readonly IUserSerivce _userService;

    public UnitTest1()
    {
        // Setup in-memory EF Core database
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new DatabaseContext(options);

        _userService = new UserService(_dbContext);
    }

    [Fact]
    public async Task CreateShouldExists()
    {

        var user = new CreateUserDto
        {
            UserName = "1",
            Name = "1",
            Email = "1",
            Phone = "1",
            Website = "1"
        };

        await _userService.CreateAsync(user);

        Assert.Equal(1, await _dbContext.Users.CountAsync());
    }

    [Fact]
    public async Task GetShouldFind()
    {
        await _dbContext.Users.AddAsync(new User
        {
            UserName = "1",
            Name = "1",
            Email = "1",
            Phone = "1",
            Website = "1"
        });

        await _dbContext.SaveChangesAsync();

        var users = await _userService.Get();
        Assert.Single(users);
    }
    
    [Fact]
    public async Task GetShouldNotFind()
    {
        var users = await _userService.Get();
        Assert.Empty(users);
    }
}
