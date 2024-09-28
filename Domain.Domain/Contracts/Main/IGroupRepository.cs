using ELI.Data.Repositories.Main;
using ELI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IGroupRepository : IDisposable
    {
        Task<int> AddGroupAsync(GroupViewModel group);
        Task<GroupViewModel> GetGroupAsync(int groupID,bool IsInvoice);
        Task<bool> UpdateGroupAsync(GroupViewModel group);
        Task<AllResponse<GroupViewModel>> GetAllGroupList(AllRequest<GroupViewModel> groups);
        Task<bool> ActivateGroup(GroupViewModel group);
        Task<bool> DeleteGroup(GroupViewModel group);
        Task<bool> GroupPayment(GroupViewModel group);
        Task<bool> GroupPrograme(GroupViewModel group);
        Task<bool> GroupTrips(GroupViewModel group);

        #region PaymentsGroups
        Task<int> AddPaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);
        Task<bool> UpdatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);
        Task<PaymentsGroupsViewModel> GetPaymentGroupAsync(int paymentGroupID);
        Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupByGroupIdAsync(int groupID);
        Task<bool> ActivatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);

        #endregion

        #region PaymentsGroupsLeader
        Task<int> AddPaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);
        Task<bool> UpdatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);
        Task<PaymentsGroupsViewModel> GetPaymentGroupLeaderAsync(int paymentGroupID);
        Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupLeaderByGroupIdAsync(int groupID);
        Task<bool> ActivatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);

        #endregion
    }
}
