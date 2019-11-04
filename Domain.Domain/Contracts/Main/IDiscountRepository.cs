using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
   public interface IDiscountRepository : IDisposable
    {
        Task<List<DiscountViewModel>> GetAllDiscountCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken ct = default(CancellationToken));
        Task<Discount> GetDiscountByIdAsync(int Id, CancellationToken ct = default(CancellationToken));
        bool CheckDiscountCodeDuplication(string discountCode, CancellationToken ct = default(CancellationToken));
        Task<List<Discount>> GetActiveDiscountCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<Discount> GetActiveDiscountByCodeAsync(string code, CancellationToken ct = default(CancellationToken));
        Task UpdateDiscountCodeAsync(Discount discount, CancellationToken ct = default(CancellationToken));
    }
}
