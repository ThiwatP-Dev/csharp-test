using Web.Database.Models;
using Web.Service.Dto;

namespace Web.Service.Interfaces;

public interface IUserSerivce
{
    Task<List<User>> Get();
    Task CreateAsync(CreateUserDto request);
    Task<User?> GetByIdAsync(long id);
    Task UpdateAsync(long id, CreateUserDto request);
    Task DeleteAsync(long id);
    Task<bool> IsUsernameExists(string username, long? id = null);
}