using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Persistance.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext context) : base(context) { }

    public Task<Role?> FindByRoleNameAsync(string roleName)
        => Set
        .SingleOrDefaultAsync(x => x.Name == roleName);
}
