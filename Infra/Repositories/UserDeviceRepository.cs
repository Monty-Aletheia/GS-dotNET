using Domain.Entities;
using Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class UserDeviceRepository : Repository<UserDevice>
{
    private readonly FIAPDbContext _context;

    public UserDeviceRepository(FIAPDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserDevice> FindByIdAndUserIdAsync(Guid userDeviceId, Guid userId)
    {
        return await _context.UserDevices
            .Include(ud => ud.Device)
            .Include(ud => ud.User) 
            .FirstOrDefaultAsync(ud => ud.Id == userDeviceId && ud.User.Id == userId);
    }

    public async Task<IEnumerable<UserDevice>> FindByUserIdAsync(Guid userId)
    {
        return await _context.UserDevices
            .Include(ud => ud.Device) 
            .Where(ud => ud.User.Id == userId)
            .ToListAsync();
    }
}
