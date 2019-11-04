using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ELI.Domain.Helpers;
using ELI.Entity.Main;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace ELI.Data.Repositories.Main
{
    public class LookupTableRepository : ILookupTableRepository
    {
        private readonly ELIContext _context;
        public LookupTableRepository(ELIContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public LookupValue getpath(LookupValueEnum value, CancellationToken ct = default(CancellationToken))
        {
            var result = _context.LookupValue.FirstOrDefault(x => x.Name == value.ToString());
            return result;
        }
        public LookupValue getEncryptionKey(LookupValueEnum value, CancellationToken ct = default(CancellationToken))
        {
            var result = _context.LookupValue.FirstOrDefault(x => x.Name == value.ToString());
            return result;
        }
        public async Task<List<LookupValue>> getPaymentMethods(CancellationToken ct = default(CancellationToken))
        {
            var paymentMehthods = await _context.LookupValue.Where(x => x.LookupTableId == 4).ToListAsync(ct);
            return paymentMehthods;
        }
        public async Task<List<LookupValue>> getDiscountTypes(CancellationToken ct = default(CancellationToken))
        {
            var discountTypes = await _context.LookupValue.Where(x => x.LookupTableId == 5).ToListAsync(ct);
            return discountTypes;
        }
        public async Task<List<Server>>   getServersAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Server.ToListAsync(ct);
        }
        public async Task<List<Database>> getDatabaseAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Database.ToListAsync(ct);
        }
        public async Task<List<Device>> getDeviceAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Device.Where(a=>a.IsDeleted == false && a.IsActive == true).ToListAsync(ct);
        }
    }
}
