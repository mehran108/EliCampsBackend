using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IReportRepository : IDisposable
    {
        Task<List<LeadsCountViewModel>> LeadsCountReportAsync(String showkey, CancellationToken ct = default(CancellationToken));
        Task<List<AccountListReportViewModel>> AccountListReportAsync(CancellationToken ct = default(CancellationToken));
        Task<List<FinancialReconciliationReportViewModel>> FinancialReconciliationReportAsync(DateTime year, CancellationToken ct = default(CancellationToken));
        Task<List<CodeListReportViewModel>> CodeListReportAsync(CancellationToken ct = default(CancellationToken));

    }
}
