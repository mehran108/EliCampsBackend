using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
   public interface ILeadsRepository : IDisposable
    {
        Task CreateLeadsAsync(SaveLeadViewModel leads, CancellationToken ct = default(CancellationToken));
        Task<List<GetAllLeadsFromDevice>> GetLeadsByShowIdDeviceIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<LeadsQualifier>> GetLeadsByLeadIdQId(int LeadId, int QualifierId, CancellationToken ct = default(CancellationToken));

    }
}
