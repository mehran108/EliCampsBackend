using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IQualifierRepository : IDisposable
    {
        Task<List<Qualifier>> GetAllQualifiersAsync(CancellationToken ct = default(CancellationToken));
        Task<Qualifier> CreateQualifierAsync(Qualifier qualifier, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetQualifierByShowIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetQualifierByQIdAsync(int showId, int QId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));

        Task<Qualifier> GetQuestionsByQualifierIdAsync(string role, int Id,string userId,int deviceId = 0, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierIdFromDevice(int qualifierId, int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<GetLeadsInfoViewModel> GetLeadsInfoAsync(int Id, string ShowKey,  string deviceIdentifier, string barcode, CancellationToken ct = default(CancellationToken));
        Task<GetLeadsInfoViewModel> UpdateLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode,string scannedDatetime,GetLeadsInfoViewModel leadsVM, CancellationToken ct = default(CancellationToken));
        Task UpdateQualifierAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteQualifierAsync(int Id, CancellationToken ct = default(CancellationToken));
        Task<Qualifier> CreateQualifierWebAsync(Qualifier qualifier, int userId,  CancellationToken ct = default(CancellationToken));
        void SaveQualifierWebRelationAsync(Question question, int userId, CancellationToken ct = default(CancellationToken));
        Task<Qualifier> UpdateQualifierWebAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetAllQualifiersExhibitorsAsync(int userid, CancellationToken ct = default(CancellationToken));
        Task<QualifierUsers> DeleteQualifierUserRealtion(int qualifierId, int userid, CancellationToken ct = default(CancellationToken));
        Task<List<Leads>> GetLeadsByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken));
    }
}
