using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IELIRepository : IDisposable
    {
        Task<List<Countries>> GetAllCountriesAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Region>> GetAllRegionAsync(CancellationToken ct = default(CancellationToken));

        Task<List<ContactInfo>> GetContactInformationAsync(CancellationToken ct = default(CancellationToken));

        Task<List<Cities>> GetAllCitiesAsync(CancellationToken ct = default(CancellationToken));
        Task<List<States>> GetAllStatesAsync(int countryId, CancellationToken ct = default(CancellationToken));
        Task<Pricing> CreatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken));
        Task<ShowPricing> CreateShowPricingAsync(ShowPricing showPricing, CancellationToken ct = default(CancellationToken));
        Task<ErrorLogging> CreateLogAsync(ErrorLogging log, CancellationToken ct = default(CancellationToken));
        Task<List<Currency>> GetCurrency(CancellationToken ct = default(CancellationToken));
        Task<Region> GetRegionById(int regionId, CancellationToken ct = default(CancellationToken));
        Task<List<Cities>> GetCitiesByCountryAsync(string countryName, CancellationToken ct = default(CancellationToken));
        Task<Pricing> GetPricingByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken));
        Task UpdatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken));
        Task<ShowDiscount> CreateShowDiscountAsync(ShowDiscount showDiscount, CancellationToken ct = default(CancellationToken));
        Task<List<Discount>> GetShowDiscountByShowIdAsync(int ShowId, CancellationToken ct = default(CancellationToken));
        Task<List<ShowDiscount>> DeleteShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken));
        bool ValidateDeviceIdentifier(string deviceIdentifier);
        Task<string> AUSSuccessCase(int invoiceId, string responseCode);
        Task<string> AUSFailCase(int invoiceId, string responseCode);
    }
}
