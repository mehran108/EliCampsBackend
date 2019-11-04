using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IDeviceRepository : IDisposable
    {
        Task<Device> CreateDeviceAsync(Device device, CancellationToken ct = default(CancellationToken));
        Task<Device> GetDeviceByIdAsync(int deviceId, CancellationToken ct = default(CancellationToken));
        Task<Device> GetDeviceByDeviceIdentifierAsync(string deviceIdentifier, CancellationToken ct = default(CancellationToken));
    }
}
