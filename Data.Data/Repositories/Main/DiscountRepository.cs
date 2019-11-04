using AutoMapper;
using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ELIContext _context;
        private readonly IMapper _mapper;
        public DiscountRepository(ELIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<List<DiscountViewModel>> GetAllDiscountCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            var action = await DeleteExpiredDiscountCodesAsync();
            // var t = await _context.Show.Select(x=> new Show { ShowId = x.ShowId, ShowName= x.ShowName}).ToListAsync(ct);
            var discountCodes   = await _context.Discount.Where(a => a.IsDeleted == false && a.IsActive == true).Select(x=> new DiscountViewModel 
                {
                DiscountId= x.DiscountId,
                DiscountCode = x.DiscountCode,
                ExpirationDate = x.ExpirationDate,
                MinimumBuy = x.MinimumBuy,
                DiscountValue = x.DiscountValue,
                DiscountType = x.DiscountType,
                CreatedBy  = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                IsDeleted = x.IsDeleted,
                IsActive = x.IsActive,
                IsConsumed = x.IsConsumed
            }
            ).ToListAsync(ct);

            foreach (var item in discountCodes)
            {
               var timesUsed = await _context.Invoice.Where(a => a.DiscountCode == item.DiscountCode).ToListAsync();
                item.TimesUsed = timesUsed.Count();
            }
            return discountCodes;
        }
        public async Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken ct = default(CancellationToken))
        {
            await _context.Discount.AddAsync(discount, ct);
            await _context.SaveChangesAsync(ct);
            return discount;
        }
        public async Task<Discount> GetDiscountByIdAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            var discount = await _context.Discount.SingleOrDefaultAsync(a => a.DiscountId == Id && a.IsActive == true && a.IsDeleted == false);
            return discount;
        }
        public bool CheckDiscountCodeDuplication(string discountCode, CancellationToken ct = default(CancellationToken))
        {
            if (_context.Discount.Any(x => x.DiscountCode == discountCode && x.IsActive == true && x.IsDeleted == false ))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<Discount>> GetActiveDiscountCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Discount.Where(a => a.IsConsumed == false && a.IsActive == true && a.IsDeleted == false && a.ExpirationDate > DateTime.Now).ToListAsync();
        }
        public async Task<Discount> GetActiveDiscountByCodeAsync(string code, CancellationToken ct = default(CancellationToken))
        {
            var discount = await _context.Discount.SingleOrDefaultAsync(a => a.DiscountCode == code && a.IsActive == true && a.IsDeleted == false && a.IsConsumed == false);
            if (discount != null)
            {
                if (discount.IsConsumed == true)
                {
                    throw new AppException("Discount code already used.");
                }
            }
            else if (discount == null)
            {
                throw new AppException("Discount code not found");
            }
            return discount;
        }
        public async Task UpdateDiscountCodeAsync(Discount discount, CancellationToken ct = default(CancellationToken))
        {
            var code = await _context.Discount.SingleOrDefaultAsync(a => a.DiscountId == discount.DiscountId && a.IsDeleted == false && a.IsActive == true);

            if (code == null) throw new AppException("Discount Code not found");
            // code.DiscountCode = discount.DiscountCode;
            code.ExpirationDate = discount.ExpirationDate;
            code.DiscountValue = discount.DiscountValue;
            code.DiscountType = discount.DiscountType;
            code.IsConsumed = discount.IsConsumed;
            code.MinimumBuy = discount.MinimumBuy;
            code.UpdatedDate = DateTime.Now;
            _context.Discount.Update(code);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Discount>> DeleteExpiredDiscountCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            var discountCodestobeInactive = await _context.Discount.Where(a => a.ExpirationDate < DateTime.Now && a.IsActive == true && a.IsDeleted == false).ToListAsync(ct);
            foreach (var discount in discountCodestobeInactive)
            {
                discount.IsActive = false;
                _context.Discount.Update(discount);
            }
            await _context.SaveChangesAsync();
            return discountCodestobeInactive;
        }
    }
}
