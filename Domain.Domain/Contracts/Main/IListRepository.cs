using Domain.Domain.ViewModels;
using ELI.Data.Repositories.Main;
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
        Task<RoomsViewModel> GetRomeListAsync(int roomListId);
        Task<AllResponse<RoomsList>> GetAllRomeList(AllRequest<RoomsList> rooms);
        Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel);


        #endregion

        #region RoomsList
        Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        #endregion
    }
}
