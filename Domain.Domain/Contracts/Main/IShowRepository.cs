using ELI.Domain.ViewModels;
using ELI.Entity;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IShowRepository : IDisposable
    {
        Task<Show> CreateShowAsync(Show show, CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllRAsync(CancellationToken ct = default(CancellationToken));
        //Task<List<Show>> GetAllShowsAsyncByRegion(int regionId, int UserId, CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetLimitednumberShowsAsync(int key, int length, CancellationToken ct = default(CancellationToken));
        Task<List<ShowByRegionViewModel>> GetAllShowsByRegion(int regionId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        bool CheckShowKeyDuplication(string showKey, CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllShowsByUserIdAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken));
        Task<ShowViewModel> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task UpdateShowAsync(Show showViewModel, CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetShowsforPurchaseActivationAsync(CancellationToken ct = default(CancellationToken));
        Task<Discount> ValidateDiscountCode(string code, int showId, CancellationToken ct = default(CancellationToken));
        Task<string> GetRequestResponse(string PaymentId, CancellationToken ct = default(CancellationToken));
        string GenerateKeys(int showId, int userId, int Quantity, int activationTypeId, int? invoiceId);
        Task<bool> ValidateRestrictedCode(string code, int showId, CancellationToken ct = default(CancellationToken));
        Task<Show> GetShowByShowKey(string ShowKey, CancellationToken ct = default(CancellationToken));
        Task<Show> CheckShowActivationAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<ShowDiscount> GetShowDiscountRelationAsync(int ShowId, int DiscountId, CancellationToken ct = default(CancellationToken));
    }
}
