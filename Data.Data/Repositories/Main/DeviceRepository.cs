using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ELIContext _context;
        public DeviceRepository(ELIContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Device> CreateDeviceAsync(Device device, CancellationToken ct = default(CancellationToken))
        {
            await _context.Device.AddAsync(device, ct);
            await _context.SaveChangesAsync(ct);
            return device;
        }
        public async Task<Device> GetDeviceByIdAsync(int deviceId, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Device.FirstOrDefaultAsync(a => a.DeviceId == deviceId && a.IsDeleted == false && a.IsActive == true);
        }
        public async Task<Device> GetDeviceByDeviceIdentifierAsync(string deviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Device.FirstOrDefaultAsync(a => a.DeviceIdentifier == deviceIdentifier && a.IsDeleted == false && a.IsActive == true);
        }
    }

}
