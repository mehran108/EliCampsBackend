﻿using Domain.Domain.ViewModels;
using ELI.Data.Repositories.Main.Extensions;
using ELI.Domain.Contracts.Main;
using ELI.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class ListRepository : BaseRepository, IListRepository
    {
        public ListRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public void Dispose()
        {
        }
        private const string AddStoredProcedureName = "AddAgents";
        private const string GetStoredProcedureName = "GetAgent";
        private const string GetRoomsStoredProcedureName = "GetRooms";
        private const string GetAllRoomsStoredProcedureName = "GetAllRooms";
        private const string AddRoomsStoredProcedureName = "AddRooms";
        private const string AddTripsStoredProcedureName = "AddTrips";
        private const string UpdateAgentStoredProcedureName = "UpdateAgent";

        private const string AgentIdColumnName = "AgentId";
        private const string AgentAgentColumnName = "AgentAgent";
        private const string AgentContactColumnName = "AgentContact";
        private const string AgentPhoneColumnName = "AgentPhone";
        private const string AgentEmailColumnName = "AgentEmail";
        private const string AgentWebColumnName = "AgentWeb";
        private const string AgentAddressColumnName = "AgentAddress";
        private const string AgentCountryColumnName = "AgentCountry";
        private const string AgentNotesColumnName = "AgentNotes";
        private const string AgentOtherColumnName = "AgentOther";


        private const string AgentIdParameterName = "PAgentID";
        private const string RoomIdParameterName = "PRoomID";
        private const string AgentAgentParameterName = "PAgentAgent";
        private const string AgentContactParameterName = "PAgentContact";
        private const string AgentPhoneParameterName = "PAgentPhone";
        private const string AgentEmailParameterName = "PAgentEmail";
        private const string AgentWebParameterName = "PAgentWeb";
        private const string AgentAddressParameterName = "PAgentAddress";
        private const string AgentCountryParameterName = "PAgentCountry";
        private const string AgentNotesParameterName = "PAgentNotes";
        private const string AgentOtherParameterName = "PAgentOther";



        private const string TripIdParameterName = "PID";
        private const string TripYearParameterName = "PYear";
        private const string TripNameParameterName = "PTrip";
        private const string TripCampsParameterName = "PCamps";
        private const string TripsDateParameterName = "PTripsDate";
        private const string TripsNotesParameterName = "PTripsNotes";
        private const string TripLDxParameterName = "PLdx";

        #region AgentList

        #region  RoomList
        private const string RIdParameterName = "PID";
        private const string RoomListRoomIdParameterName = "PRoomID";
        private const string RoomCampusParameterName = "PCampus";
        private const string RoomBuildingParameterName = "PBuilding";
        private const string RoomTypeParameterName = "PRoomType";
        private const string FloorParameterName = "PFloor";
        private const string LdxPhoneParameterName = "PLdx";
        private const string NotesParameterName = "PNotes";
        private const string BookedFromParameterName = "PBookedFrom";
        private const string BookedToParameterName = "PBookedTo";
        private const string AvailableParameterName = "PAvailable";
        private const string AvailableFromParameterName = "PAvailableFrom";
        private const string AvailableToParameterName = "PAvailableTo";
        private const string ImportedOneParameterName = "PImportedOne";
        private const string WeeknoParameterName = "PWeekno";
        private const string YearParameterName = "PYear";



        private const string RIdColumnName = "ID";
        private const string RoomListRoomIdColumnName = "RoomID";
        private const string RoomCampusColumnName = "Campus";
        private const string RoomBuildingColumnName = "Building";
        private const string RoomTypeColumnName = "RoomType";
        private const string FloorColumnName = "RoomFloor";
        private const string LdxPhoneColumnName = "Ldx";
        private const string NotesColumnName = "Notes";
        private const string BookedFromColumnName = "BookedFrom";
        private const string BookedToColumnName = "BookedTo";
        private const string AvailableColumnName = "Available";
        private const string AvailableFromColumnName = "AvailableFrom";
        private const string AvailableToColumnName = "AvailableTo";
        private const string ImportedOneColumnName = "ImportedOne";
        private const string WeeknoColumnName = "Weekno";
        private const string YearColumnName = "Year";
        #endregion



        public async Task<int> CreateAgentAsync(AgentViewModel agent)
        {
            var agentIdParamter = base.GetParameterOut(ListRepository.AgentIdParameterName, SqlDbType.Int, agent.ID);
            var parameters = new List<DbParameter>
            {
                agentIdParamter,


                base.GetParameter(ListRepository.AgentAgentParameterName, agent.Agent),
                base.GetParameter(ListRepository.AgentContactParameterName, agent.Contact),
                base.GetParameter(ListRepository.AgentPhoneParameterName, agent.Phone),
                base.GetParameter(ListRepository.AgentEmailParameterName, agent.Email),
                base.GetParameter(ListRepository.AgentWebParameterName, agent.Web),
                base.GetParameter(ListRepository.AgentAddressParameterName, agent.Address),
                base.GetParameter(ListRepository.AgentCountryParameterName, agent.Country),
                base.GetParameter(ListRepository.AgentNotesParameterName, agent.Notes),
                base.GetParameter(ListRepository.AgentOtherParameterName, agent.Other),

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddStoredProcedureName, CommandType.StoredProcedure);

            agent.ID = Convert.ToInt32(agentIdParamter.Value);

            return agent.ID;
        }

        public async Task<AgentViewModel> GetAgentAsync(int agentID)
        {
            AgentViewModel agentVM = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.AgentIdParameterName, agentID)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        agentVM = new AgentViewModel
                        {

                            ID = dataReader.GetIntegerValue(ListRepository.AgentIdColumnName),
                            Agent = dataReader.GetStringValue(ListRepository.AgentAgentColumnName),
                            Contact = dataReader.GetStringValue(ListRepository.AgentContactColumnName),
                            Phone = dataReader.GetStringValue(ListRepository.AgentPhoneColumnName),
                            Email = dataReader.GetStringValue(ListRepository.AgentEmailColumnName),
                            Web = dataReader.GetStringValue(ListRepository.AgentWebColumnName),
                            Address = dataReader.GetStringValue(ListRepository.AgentAddressColumnName),
                            Country = dataReader.GetStringValue(ListRepository.AgentCountryColumnName),
                            Notes = dataReader.GetStringValue(ListRepository.AgentNotesColumnName),
                            Other = dataReader.GetStringValue(ListRepository.AgentOtherColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                    }
                }
            }

            return agentVM;
        }

        public async Task<RoomsViewModel> GetRomeListAsync(int roomListID)
        {
            RoomsViewModel agentVM = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.RIdParameterName, roomListID)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetRoomsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        agentVM = new RoomsViewModel
                        {

                            ID = dataReader.GetIntegerValue(ListRepository.RIdColumnName),
                            RoomID = dataReader.GetStringValue(ListRepository.RoomListRoomIdColumnName),
                            CampusID = dataReader.GetIntegerValue(ListRepository.RoomCampusColumnName),
                            Building = dataReader.GetStringValue(ListRepository.RoomBuildingColumnName),
                            RoomType = dataReader.GetStringValue(ListRepository.RoomTypeColumnName),
                            Floor = dataReader.GetStringValue(ListRepository.FloorColumnName),
                            Ldx = dataReader.GetStringValue(ListRepository.LdxPhoneColumnName),
                            Notes = dataReader.GetStringValue(ListRepository.NotesColumnName),
                            BookedFrom = dataReader.GetDateTimeValue(ListRepository.BookedFromColumnName),
                            BookedTo = dataReader.GetDateTimeValue(ListRepository.BookedToColumnName),
                            Available = dataReader.GetBooleanValue(ListRepository.AvailableColumnName),
                            AvailableFrom = dataReader.GetDateTimeValue(ListRepository.AvailableFromColumnName),
                            AvailableTo = dataReader.GetDateTimeValue(ListRepository.AvailableToColumnName),
                            ImportedOne = dataReader.GetIntegerValue(ListRepository.ImportedOneColumnName),
                            Weekno = dataReader.GetStringValue(ListRepository.WeeknoColumnName),
                            Year = dataReader.GetIntegerValue(ListRepository.YearColumnName)

                        };
                    }
                }
            }

            return agentVM;
        }

        public async Task<AllResponse<RoomsList>> GetAllRomeList(AllRequest<RoomsList> rooms)
        {
            RoomsList roomsList = null;

            var result = new AllResponse<RoomsList>
            {
                Data = new List<RoomsList>(),
                Offset = rooms.Offset,
                PageSize = rooms.PageSize,
                SortColumn = rooms.SortColumn,
                SortAscending = rooms.SortAscending
            };
          
            var parameters = new List<DbParameter>
            {
               
                base.GetParameter(BaseRepository.OffsetParameterName, rooms.Offset),
                base.GetParameter(BaseRepository.PageSizeParameterName, rooms.PageSize),
                base.GetParameter(BaseRepository.SortColumnParameterName, rooms.SortColumn),
                base.GetParameter(BaseRepository.SortAscendingParameterName, rooms.SortAscending),
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllRoomsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        while (dataReader.Read())
                        {
                            roomsList = new RoomsList
                            {

                                ID = dataReader.GetIntegerValue(ListRepository.RIdColumnName),
                                RoomID = dataReader.GetStringValue(ListRepository.RoomListRoomIdColumnName),
                                CampusID = dataReader.GetIntegerValue(ListRepository.RoomCampusColumnName),
                                Building = dataReader.GetStringValue(ListRepository.RoomBuildingColumnName),
                                RoomType = dataReader.GetStringValue(ListRepository.RoomTypeColumnName),
                                Floor = dataReader.GetStringValue(ListRepository.FloorColumnName),
                                Ldx = dataReader.GetStringValue(ListRepository.LdxPhoneColumnName),
                                Notes = dataReader.GetStringValue(ListRepository.NotesColumnName),
                                BookedFrom = dataReader.GetDateTimeValue(ListRepository.BookedFromColumnName),
                                BookedTo = dataReader.GetDateTimeValue(ListRepository.BookedToColumnName),
                                Available = dataReader.GetBooleanValue(ListRepository.AvailableColumnName),
                                AvailableFrom = dataReader.GetDateTimeValue(ListRepository.AvailableFromColumnName),
                                AvailableTo = dataReader.GetDateTimeValue(ListRepository.AvailableToColumnName),
                                ImportedOne = dataReader.GetIntegerValue(ListRepository.ImportedOneColumnName),
                                Weekno = dataReader.GetStringValue(ListRepository.WeeknoColumnName),
                                Year = dataReader.GetIntegerValue(ListRepository.YearColumnName)

                            };
                            result.Data.Add(roomsList);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }

                    }
                }
            }

            return result;
        }


        
        public async Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel)
        {
            var agentIdParamter = base.GetParameterOut(ListRepository.RIdParameterName, SqlDbType.Int, roomsViewModel.ID);
            var parameters = new List<DbParameter>
            {
                agentIdParamter,



                base.GetParameter(ListRepository.RoomListRoomIdParameterName, roomsViewModel.RoomID),
                base.GetParameter(ListRepository.RoomCampusParameterName, roomsViewModel.CampusID),
                base.GetParameter(ListRepository.RoomBuildingParameterName, roomsViewModel.Building),
                base.GetParameter(ListRepository.RoomTypeParameterName, roomsViewModel.RoomType),
                base.GetParameter(ListRepository.FloorParameterName, roomsViewModel.Floor),
                base.GetParameter(ListRepository.LdxPhoneParameterName, roomsViewModel.Ldx),
                base.GetParameter(ListRepository.NotesParameterName, roomsViewModel.Notes),
                base.GetParameter(ListRepository.BookedFromParameterName, roomsViewModel.BookedFrom),
                base.GetParameter(ListRepository.BookedToParameterName, roomsViewModel.BookedTo),
                base.GetParameter(ListRepository.AvailableParameterName, roomsViewModel.Available),
                base.GetParameter(ListRepository.AvailableFromParameterName, roomsViewModel.AvailableFrom),
                base.GetParameter(ListRepository.AvailableToParameterName, roomsViewModel.AvailableTo),
                base.GetParameter(ListRepository.ImportedOneParameterName, roomsViewModel.ImportedOne),
                base.GetParameter(ListRepository.WeeknoParameterName, roomsViewModel.Weekno),
                base.GetParameter(ListRepository.YearParameterName, roomsViewModel.Year)

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddRoomsStoredProcedureName, CommandType.StoredProcedure);

            roomsViewModel.ID = Convert.ToInt32(agentIdParamter.Value);

            return roomsViewModel.ID;
        }
        #endregion

        #region Trips


        public async Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel)
        {
            var agentIdParamter = base.GetParameterOut(ListRepository.RIdParameterName, SqlDbType.Int, tripsViewModel.ID);
            var parameters = new List<DbParameter>
            {
                agentIdParamter,



                base.GetParameter(ListRepository.TripYearParameterName, tripsViewModel.Year),
                base.GetParameter(ListRepository.TripNameParameterName, tripsViewModel.Trips),
                base.GetParameter(ListRepository.TripCampsParameterName, tripsViewModel.Camps),
                base.GetParameter(ListRepository.TripsDateParameterName, tripsViewModel.TripsDate),
                base.GetParameter(ListRepository.TripsNotesParameterName, tripsViewModel.Notes),
                base.GetParameter(ListRepository.TripLDxParameterName, tripsViewModel.Idx)

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddTripsStoredProcedureName, CommandType.StoredProcedure);

            tripsViewModel.ID = Convert.ToInt32(agentIdParamter.Value);

            return tripsViewModel.ID;
        }

        public async Task<bool> UpdateAgentAsync(AgentViewModel agent)
        {
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.AgentIdColumnName, agent.ID),
                base.GetParameter(ListRepository.AgentAgentParameterName, agent.Agent),
                base.GetParameter(ListRepository.AgentContactParameterName, agent.Contact),
                base.GetParameter(ListRepository.AgentPhoneParameterName, agent.Phone),
                base.GetParameter(ListRepository.AgentEmailParameterName, agent.Email),
                base.GetParameter(ListRepository.AgentWebParameterName, agent.Web),
                base.GetParameter(ListRepository.AgentAddressParameterName, agent.Address),
                base.GetParameter(ListRepository.AgentCountryParameterName, agent.Country),
                base.GetParameter(ListRepository.AgentNotesParameterName, agent.Notes),
                base.GetParameter(ListRepository.AgentOtherParameterName, agent.Other),
                base.GetParameter(BaseRepository.AgentIdParameterName, agent.Active),

            };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateAgentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion

    }
}
