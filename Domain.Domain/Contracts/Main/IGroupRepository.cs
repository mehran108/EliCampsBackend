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
        Task<GroupViewModel> GetGroupAsync(int groupID);
        Task<bool> UpdateGroupAsync(GroupViewModel group);
        Task<AllResponse<GroupViewModel>> GetAllGroupList(AllRequest<GroupViewModel> groups);
        Task<bool> ActivateGroup(GroupViewModel group);

        Task<bool> GroupPayment(GroupViewModel group);
    }
}
