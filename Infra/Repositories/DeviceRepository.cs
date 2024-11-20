using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DeviceRepository : Repository<Device>
    {
        private readonly FIAPDbContext _context;

        public DeviceRepository(FIAPDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Device> FindByIdAsync(Guid id)
        {
            return await _context.Devices.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
