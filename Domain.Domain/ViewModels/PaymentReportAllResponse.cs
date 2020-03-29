using ELI.Domain.ViewModels;
using System.Collections.Generic;

namespace ELI.Data.Repositories.Main
{
    /// <summary>
    /// AllResponse class is used as base class for All Object response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaymentReportAllResponse
    {
        #region Propeties
        public List<PaymentReportVM> Data { get; set; }
        public double? TotalGrossPriceCalculated { get; set; }
        public double? TotalNetPriceCalculated { get; set; }
        public double? TotalPaidPriceCalculated { get; set; }
        public double? TotalBalanceCalculated { get; set; }

        #endregion
    }
}