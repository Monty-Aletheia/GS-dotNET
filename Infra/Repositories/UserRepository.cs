using Domain.Entities;
using Infra.Data;

namespace Infra.Repositories
{
    public class UserRepository(FIAPDbContext context) : Repository<User>(context)
    {

    }
}
