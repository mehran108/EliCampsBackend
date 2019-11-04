using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ELI.Entity.Main;
namespace ELI.Domain.Contracts.Main
{
    public interface IActivationRepository : IDisposable
    {
        Task<List<Activation>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Activation> CreateActivationAsync(Activation activation, CancellationToken ct = default(CancellationToken));
        Task<Activation> GetActivationByIdAsync(string activationKey, CancellationToken ct = default(CancellationToken));
        Task<List<Activation>> GetAllActivationsByUserIdAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken));
        Task<List<Activation>> GetAllRestrictedCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<Activation> ValidateRestrictedCode(string code, int showId, CancellationToken ct = default(CancellationToken));
    }
}
