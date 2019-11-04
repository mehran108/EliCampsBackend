using ELI.Domain.Helpers;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface ILookupTableRepository : IDisposable
    {
        LookupValue getpath(LookupValueEnum value, CancellationToken ct = default(CancellationToken));
        LookupValue getEncryptionKey(LookupValueEnum value, CancellationToken ct = default(CancellationToken));
        Task<List<LookupValue>> getPaymentMethods(CancellationToken ct = default(CancellationToken));
        Task<List<Server>> getServersAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Database>> getDatabaseAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Device>> getDeviceAsync(CancellationToken ct = default(CancellationToken));
        Task<List<LookupValue>> getDiscountTypes(CancellationToken ct = default(CancellationToken));
    }
}
