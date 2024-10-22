using KDrom.Domain.Entities;

namespace KDrom.Domain.Interfaces.IRepositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> FindByRoleNameAsync(string roleName);
}
