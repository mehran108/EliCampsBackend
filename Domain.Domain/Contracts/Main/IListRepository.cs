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

        Task<bool> UpdateAgentAsync(AgentViewModel agent);

        #endregion

        #region RoomsList
        Task<RoomsViewModel> GetRomeListAsync(int roomListId);
        Task<AllResponse<RoomsList>> GetAllRomeList(AllRequest<RoomsList> rooms);
        Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel);

        #endregion

        #region TripList
        Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        Task<TripsViewModel> GetTripAsync(int tripID);
        Task<AllResponse<TripsViewModel>> GetAllTripsList(AllRequest<TripsViewModel> rooms);
        Task<int> UpdateTirpsAsync(TripsViewModel tripsViewModel);
        #endregion
    }
}
