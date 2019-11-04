using ELI.Entity;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IInvoiceRepository : IDisposable
    {
        Task<Invoice> CreateInvoiceAsync(Invoice invoice, CancellationToken ct = default(CancellationToken));
        Task UpdateInvoiceAsync(Invoice invoiceViewModel, CancellationToken ct = default(CancellationToken));
        Task<Invoice> GetByActivationKeyAsync(string ActivationKey, CancellationToken ct = default(CancellationToken));
        Task<List<Invoice>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Invoice> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<Invoice>> GetByCustomerIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Invoice> AddAsync(Invoice newInvoice, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(Invoice invoice, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken));
    }
}
