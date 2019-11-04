using ELI.Data.Context;
using ELI.Domain.Contracts;
using ELI.Domain.Contracts.Main;
using ELI.Entity;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class ActivationTypeRepository : IActivationTypeRepository
    {
        private readonly ELIContext _context;
        public ActivationTypeRepository(ELIContext context)
        {
            _context = context;
        }
        public async Task<List<ActivationType>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.ActivationType.ToListAsync(ct);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
