using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ELI.Entity.Main;
namespace ELI.Domain.Contracts.Main
{
    public interface IActivationTypeRepository : IDisposable
    {
        Task<List<ActivationType>> GetAllAsync(CancellationToken ct = default(CancellationToken));
    }
}
