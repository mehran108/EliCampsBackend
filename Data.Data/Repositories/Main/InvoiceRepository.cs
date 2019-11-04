using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ELI.Entity;
using ELI.Domain.Contracts.Main;
using ELI.Data.Context;
using ELI.Entity.Main;
using ELI.Domain.Helpers;
using System;

namespace ELI.Data.Repositories.Main
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ELIContext _context;
        public InvoiceRepository(ELIContext context)
        {
            _context = context;
        }
        private async Task<bool> InvoiceExists(int id, CancellationToken ct = default(CancellationToken))
        {
            return await GetByIdAsync(id, ct) != null;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice, CancellationToken ct = default(CancellationToken))
        {
            await _context.Invoice.AddAsync(invoice, ct);
            await _context.SaveChangesAsync(ct);
            return invoice;
        }
        public async Task UpdateInvoiceAsync(Invoice invoiceViewModel, CancellationToken ct = default(CancellationToken))
        {
            var invoice = await _context.Invoice.SingleOrDefaultAsync(a => a.InvoiceId == invoiceViewModel.InvoiceId && a.IsDeleted == false && a.IsActive == true);

            if (invoice == null) throw new AppException("Invoice not found");
            invoice.KeyPrice = invoiceViewModel.KeyPrice;
            invoice.PaymentId = invoiceViewModel.PaymentId;
            invoice.Quantity = invoiceViewModel.Quantity;
            invoice.RequestXml = invoiceViewModel.RequestXml;
            invoice.ResponseXml = invoiceViewModel.ResponseXml;
            invoice.RestrictedCode = invoiceViewModel.RestrictedCode;
            invoice.ShowId = invoiceViewModel.ShowId;
            invoice.SubTotal = invoiceViewModel.SubTotal;
            invoice.Tax = invoiceViewModel.Tax;
            invoice.Total = invoiceViewModel.Total;
            invoice.UpdatedDate = DateTime.Now;
            invoice.UserId = invoiceViewModel.UserId;
            invoice.Discount = invoiceViewModel.Discount;
            invoice.DiscountCode = invoiceViewModel.DiscountCode;
            _context.Invoice.Update(invoice);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Invoice>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Invoice.ToListAsync(ct);
        }
        public async Task<Invoice> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Invoice.FindAsync(id);
        }
        public async Task<Invoice> AddAsync(Invoice newInvoice, CancellationToken ct = default(CancellationToken))
        {
            _context.Invoice.Add(newInvoice);
            await _context.SaveChangesAsync(ct);
            return newInvoice;
        }
        public async Task<bool> UpdateAsync(Invoice invoice, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceExists(invoice.InvoiceId, ct))
                return false;
            _context.Invoice.Update(invoice);
            await _context.SaveChangesAsync(ct);
            return true;
        }
        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            if (!await InvoiceExists(id, ct))
                return false;
            var toRemove = _context.Invoice.Find(id);
            _context.Invoice.Remove(toRemove);
            await _context.SaveChangesAsync(ct);
            return true;
        }
        public async Task<List<Invoice>> GetByCustomerIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Invoice.Where(a => a.InvoiceId == id).ToListAsync(ct);
        }
        public async Task<Invoice> GetByActivationKeyAsync(string ActivationKey, CancellationToken ct = default(CancellationToken))
        {
            var activation =await _context.Activation.FirstOrDefaultAsync(a => a.ActivationKey == ActivationKey && a.IsDeleted == false && a.IsActive == true);
            if (activation != null)
            {
                var invoice =await _context.Invoice.Include(a => a.Show).Include(a=>a.user).Include("Show.ShowPricing.Pricing").FirstOrDefaultAsync(b => b.InvoiceId == activation.InvoiceId && b.IsActive==true && b.IsDeleted==false);
                return invoice;
            }
            else
            {
                throw new AppException("ActivationKey is not valid");
            }
        }
    }
}
