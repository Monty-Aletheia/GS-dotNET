using Domain.Entities;
using Infra.Data;

namespace Infra.Repositories
{
    public class UserDeviceRepository(FIAPDbContext context) : Repository<UserDevice>(context)
    {
    }
}
