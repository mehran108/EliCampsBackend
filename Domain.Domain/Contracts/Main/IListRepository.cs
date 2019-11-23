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
        Task<bool> UpdateAgentAsync(AgentViewModel AgentViewModel);
        Task<AllResponse<AgentViewModel>> GetAllAgent(AgentRequestVm agent);
        Task<bool> ActivateAgentAsync(AgentViewModel agent);

        #endregion
        #region Room
        Task<RoomsViewModel> GetRomeListAsync(int roomListId);
        Task<AllResponse<RoomsList>> GetAllRomeList(RoomsRequestVm rooms);
        Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel);

        Task<bool> ActivateRoom(RoomsViewModel roomsViewModel);
        #endregion
        #region TripList
        Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        Task<TripsViewModel> GetTripAsync(int tripID);
        Task<AllResponse<TripsViewModel>> GetAllTripsList(TripsRequestVm rooms);
        Task<bool> UpdateTirpsAsync(TripsViewModel tripsViewModel);
        /// <summary>
        /// Trips
        /// </summary>
        /// <param name="trips"></param>
        /// <returns></returns>
        Task<bool> ActivateTripsAsync(TripsViewModel trips);
        #endregion

        Task<List<LookupValueViewModel>> GetListBaseonLookupTable(string lookupTable);

        #region HomeStay
        Task<int> CreateHomeStayAsync(HomeStayViewModel homeStayViewModel);

        Task<HomeStayViewModel> GetHomeStayAsync(int homeStayId);

        Task<AllResponse<HomeStayViewModel>> GetAllHomeStay(HomeStayRequestVm homeStay);

        Task<bool> UpdateHomeStayAsync(HomeStayViewModel homeStayView);

        Task<bool> ActivateHomeStayAsync(HomeStayViewModel homeStayView);


        #endregion

        #region Addins
        Task<int> CreateAddinsAsync(AddinsViewModel addinsViewModel);

        Task<AddinsViewModel> GetAddins(int addinsId);

        Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AddinsRequestVm AddinsList);

        Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel);

        Task<bool> UpdateRoomListAsync(RoomsViewModel roomsViewModel);

        Task<bool> ActivateAddinsAsync(AddinsViewModel trips);
        #endregion

        #region Campus
        Task<int> CreateCampusAsync(CampuseViewModel campusViewModel);

        Task<CampuseViewModel> GetCampus(int campusId);

        Task<AllResponse<CampuseViewModel>> GetAllCampus(AllRequest<CampuseViewModel> campusList);

        Task<bool> UpdateCampusAsync(CampuseViewModel campusViewModel);

        Task<bool> ActivateCampusAsync(CampuseViewModel campusViewModel);
        #endregion

        #region Program
        Task<int> CreateProgramAsync(ProgramViewModel programViewModel);

        Task<ProgramViewModel> GetProgramAsync(int programId);

        Task<AllResponse<ProgramViewModel>> GetAllProgramAsync(AllRequest<ProgramViewModel> programList);

        Task<bool> UpdateProgramAsync(ProgramViewModel programViewModel);

        Task<bool> ActivateProgramAsync(ProgramViewModel programViewModel);
        #endregion

        #region SubProgram
        Task<int> CreateSubProgramAsync(SubProgramViewModel subProgramViewModel);

        Task<SubProgramViewModel> GetSubProgramAsync(int subProgramId);

        Task<AllResponse<SubProgramViewModel>> GetAllSubProgramAsync(AllRequest<SubProgramViewModel> subProgramList);

        Task<bool> UpdateSubrogramAsync(SubProgramViewModel subProgramViewModel);

        Task<bool> ActivateSubProgramAsync(SubProgramViewModel subProgramViewModel);
        #endregion
    }
}
