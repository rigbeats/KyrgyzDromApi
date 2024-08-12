using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}
