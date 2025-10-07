using Microsoft.EntityFrameworkCore;
using Web.Database;
using Web.Database.Models;
using Web.Service.Dto;
using Web.Service.Interfaces;

namespace Web.Service.src;

public class UserService(DatabaseContext dbContext) : IUserSerivce
{
    private readonly DatabaseContext _dbContext = dbContext;

    public async Task CreateAsync(CreateUserDto request)
    {
        var user = new User
        {
            Name = request.Name,
            UserName = request.UserName,
            Email = request.Email,
            Phone = request.Phone,
            Website = request.Website,
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            return;
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<User>> Get()
    {
        var users = await _dbContext.Users.AsNoTracking()
                                          .ToListAsync();

        return users;
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        var user = await _dbContext.Users.AsNoTracking()
                                         .SingleOrDefaultAsync(x => x.Id == id);

        return user;
    }

    public async Task UpdateAsync(long id, CreateUserDto request)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            return;
        }

        user.Name = request.Name;
        user.UserName = request.UserName;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Website = request.Website;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsUsernameExists(string username, long? id = null)
    {
        var query = _dbContext.Users.AsNoTracking()
                                    .Where(x => x.UserName.Equals(username));

        if (id.HasValue)
        {
            query = query.Where(x => x.Id != id);
        }

        return await query.AnyAsync();
    }
}