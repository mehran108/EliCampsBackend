using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ELI.Entity.Main;
namespace ELI.Domain.Contracts.Main
{
    public interface ISduactivationRespository : IDisposable
    {
        Task<Sduactivation> CreateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> GetSDUActivationByActivationIdAsync(int activationId, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> UpdateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> GetSDUActivationByDeviceAsync(int showId, int deviceId, CancellationToken ct = default(CancellationToken));
    }
}
