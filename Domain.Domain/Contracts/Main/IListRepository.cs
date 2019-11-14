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
        Task<RoomsViewModel> GetRomeListAsync(int roomListId);
        Task<AllResponse<RoomsList>> GetAllRomeList(AllRequest<RoomsList> rooms);
        Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel);

        #endregion

        #region TripList
        Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        Task<TripsViewModel> GetTripAsync(int tripID);
        Task<AllResponse<TripsViewModel>> GetAllTripsList(AllRequest<TripsViewModel> rooms);
        Task<bool> UpdateTirpsAsync(TripsViewModel tripsViewModel);
        #endregion

        Task<List<LookupValueViewModel>> GetListBaseonLookupTable(string lookupTable);

        #region HomeStay
        Task<int> CreateHomeStayAsync(HomeStayViewModel homeStayViewModel);

        Task<HomeStayViewModel> GetHomeStayAsync(int homeStayId);

        Task<AllResponse<HomeStayViewModel>> GetAllHomeStay(AllRequest<HomeStayViewModel> homeStay);

        Task<bool> UpdateHomeStayAsync(HomeStayViewModel homeStayView);
        #endregion

        #region Addins
        Task<int> CreateAddinsAsync(AddinsViewModel addinsViewModel);

        Task<AddinsViewModel> GetAddins(int addinsId);

        Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AllRequest<AddinsViewModel> AddinsList);

        Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel);
        #endregion
    }
}
