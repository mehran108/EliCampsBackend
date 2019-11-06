using ELI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Main
{
    public interface IListRepository : IDisposable
    {
        #region AgentList
        Task<int> CreateAgentAsync(AgentViewModel agent);
        Task<AgentViewModel> GetAgentAsync(int agentID);
        Task<bool> UpdateAgentAsync(AgentViewModel agent);
        #endregion
    }
}
