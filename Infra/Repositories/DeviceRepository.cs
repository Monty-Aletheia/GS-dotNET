using Domain.Entities;
using Infra.Data;

namespace Infra.Repositories
{
    public class DeviceRepository(FIAPDbContext context) : Repository<Device>(context)
    {
    }
}
