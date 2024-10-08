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
        private const string AddLookupValueStoredProcedureName = "AddLookupValue";
        private const string AddStoredProcedureName = "AddAgents";
        private const string GetStoredProcedureName = "GetAgent";
        private const string GetAllAgentProcedureName = "GetAllAgent";
        private const string GetTripStoredProcedureName = "GetTripsById";
        private const string GetRoomsStoredProcedureName = "GetRooms";
        private const string GetAllRoomsStoredProcedureName = "GetAllRooms";
        private const string GetAllTripsStoredProcedureName = "GetAllTrips";
        private const string GetAllHomeStayStoredProcedureName = "GetAllHomeStay";
        private const string GetHomeStayStoredProcedureName = "GetHomeStayById";
        private const string GetAddinsTripStoredProcedureName = "GetAddinsById";
        private const string ActivateRoomStoredProcedureName = "ActivateRoom";
        private const string ActivateTripsStoredProcedureName = "ActivateTrips";
        private const string ActivateHomeStayStoredProcedureName = "ActivateHomeStay";
        private const string ActivateAddinsStoredProcedureName = "ActivateAddins";
        private const string GetAllAddinsTripStoredProcedureName = "GetAllAddins";

        private const string AddRoomsStoredProcedureName = "AddRooms";
        private const string AddTripsStoredProcedureName = "AddTrips";
        private const string AddAddinsStoredProcedureName = "AddAddins";
        private const string AddHomeStayStoredProcedureName = "AddHomeStay";
        private const string UpdateAgentStoredProcedureName = "UpdateAgent";
        private const string ActivateAgentStoredProcedureName = "ActivateAgent";
        private const string GetLookupValueListStoredProcedureName = "GetLookupValueList";

        private const string UpdateTripsStoredProcedureName = "UpdateTrips";
        private const string UpdateRoomtblTripsStoredProcedureName = "UpdateRoomtbl";
        private const string UpdateHomeStayStoredProcedureName = "UpdateHomeStay";
        private const string UpdateAddinsStayStoredProcedureName = "UpdateAddins";

        private const string AddCampusStoredProcedureName = "AddCampus";
        private const string UpdateCampusStoredProcedureName = "UpdateCampus";
        private const string GetCampusStoredProcedureName = "GetCampus";
        private const string GetAllCampusStoredProcedureName = "GetAllCampus";
        private const string ActivateCampusStoredProcedureName = "ActivateCampus";
        private const string UpdateLookupValueStoredProcedureName = "UpdateLookupValue";
        private const string DeleteLookupValueStoredProcedureName = "DeleteLookupValue";

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
                            Year = dataReader.GetIntegerValue(ListRepository.YearColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)

                        };
                    }
                }
            }

            return agentVM;
        }

        public async Task<AllResponse<RoomsList>> GetAllRomeList(RoomsRequestVm rooms)
        {
            RoomsList roomsList = null;

            var result = new AllResponse<RoomsList>
            {
                Data = new List<RoomsList>()
            };

            var parameters = new List<DbParameter>
            {

               
                base.GetParameter(BaseRepository.ActiveParameterName, rooms.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllRoomsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    
                        while (dataReader.Read())
                        {
                            roomsList = new RoomsList
                            {

                                ID = dataReader.GetIntegerValue(ListRepository.RIdColumnName),
                                RoomID = dataReader.GetStringValue(ListRepository.RoomListRoomIdColumnName),
                                Campus = dataReader.GetStringValue(ListRepository.RoomCampusColumnName),
                                CampusID = dataReader.GetIntegerValueNullable(ListRepository.CampusIDColumnName),
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
                                Year = dataReader.GetIntegerValue(ListRepository.YearColumnName),
                                Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)

                            };
                            result.Data.Add(roomsList);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
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


        public async Task<bool> UpdateRoomListAsync(RoomsViewModel roomsViewModel)
        {

            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.RIdParameterName, roomsViewModel.ID),
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
                base.GetParameter(ListRepository.YearParameterName, roomsViewModel.Year),
                base.GetParameter(ListRepository.ActiveParameterName, roomsViewModel.Active)

            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateRoomtblTripsStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        public async Task<bool> ActivateRoom(RoomsViewModel roomsViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.RIdParameterName, roomsViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, roomsViewModel.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateRoomStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        #endregion

        #region Agent
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

        private const string LookupTableParameterName = "PLookupTable";

        private const string ValueColumnName = "Value";
        private const string NameColumnName = "Name";
        private const string DescriptionColumnName = "Description";
        private const string LookupTableIdColumnName = "LookupTableId";
        private const string IdColumnName = "Id";

        private const string ValueParameterName = "PValue";
        private const string NameParameterName = "PName";
        private const string DescriptionParameterName = "PDescription";
        private const string LookupTableIdParameterName = "PLookupTableId";


        private const string IDParameterName = "PID";
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
        public async Task<bool> UpdateAgentAsync(AgentViewModel agent)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.AgentIdParameterName, agent.ID),
                    base.GetParameter(ListRepository.AgentAgentParameterName, agent.Agent),
                    base.GetParameter(ListRepository.AgentContactParameterName, agent.Contact),
                    base.GetParameter(ListRepository.AgentPhoneParameterName, agent.Phone),
                    base.GetParameter(ListRepository.AgentEmailParameterName, agent.Email),
                    base.GetParameter(ListRepository.AgentWebParameterName, agent.Web),
                    base.GetParameter(ListRepository.AgentAddressParameterName, agent.Address),
                    base.GetParameter(ListRepository.AgentCountryParameterName, agent.Country),
                    base.GetParameter(ListRepository.AgentNotesParameterName, agent.Notes),
                    base.GetParameter(ListRepository.AgentOtherParameterName, agent.Other),
                    base.GetParameter(BaseRepository.ActiveParameterName, agent.Active),

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateAgentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateAgentAsync(AgentViewModel agent)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.AgentIdParameterName, agent.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, agent.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateAgentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<AllResponse<AgentViewModel>> GetAllAgent(AgentRequestVm agent)
        {
            AgentViewModel agentVM = null;

            var result = new AllResponse<AgentViewModel>
            {
                Data = new List<AgentViewModel>()
            };

            var parameters = new List<DbParameter>
            {

                base.GetParameter(BaseRepository.ActiveParameterName, agent.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllAgentProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    
                        while (dataReader.Read())
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
                        result.Data.Add(agentVM);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }

                    
                }
            }

            return result;
        }


        #endregion


        #region Trips

        private const string TripIdParameterName = "PID";
        private const string TripYearParameterName = "PYear";
        private const string TripNameParameterName = "PTrip";
        private const string TripCampsParameterName = "PCamps";
        private const string TripsDateParameterName = "PTripsDate";
        private const string TripsNotesParameterName = "PTripsNotes";
        private const string TripLDxParameterName = "PLdx";


        private const string TripIdColumnName = "Trips_ID";
        private const string TripYearColumnName = "TripYear";
        private const string TripNameColumnName = "TripName";
        private const string TripCampsColumnName = "Camps";
        private const string TripsDateColumnName = "TripDate";
        private const string TripsNotesColumnName = "TripNotes";
        private const string TripLDxColumnName = "TripLdx";
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
                base.GetParameter(ListRepository.TripLDxParameterName, tripsViewModel.ldx)

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddTripsStoredProcedureName, CommandType.StoredProcedure);

            tripsViewModel.ID = Convert.ToInt32(agentIdParamter.Value);

            return tripsViewModel.ID;
        }

        public async Task<TripsViewModel> GetTripAsync(int tripID)
        {
            TripsViewModel agentVM = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.TripIdParameterName, tripID)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetTripStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        agentVM = new TripsViewModel
                        {

                            ID = dataReader.GetIntegerValue(ListRepository.TripIdColumnName),
                            ldx = dataReader.GetStringValue(ListRepository.TripLDxColumnName),
                            Trips = dataReader.GetStringValue(ListRepository.TripNameColumnName),
                            TripsDate = dataReader.GetDateTimeValue(ListRepository.TripsDateColumnName),
                            Camps = dataReader.GetStringValue(ListRepository.TripCampsColumnName),
                            Year = dataReader.GetIntegerValue(ListRepository.TripYearColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            Notes = dataReader.GetStringValue(ListRepository.NotesColumnName)
                        };
                    }
                }
            }
            return agentVM;
        }

        public async Task<AllResponse<TripsViewModel>> GetAllTripsList(TripsRequestVm trips)
        {
            TripsViewModel tripsViewModel = null;

            var result = new AllResponse<TripsViewModel>
            {
                Data = new List<TripsViewModel>()
                
            };

            var parameters = new List<DbParameter>
            {

       
                base.GetParameter(BaseRepository.ActiveParameterName, trips.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllTripsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    
                        while (dataReader.Read())
                        {
                            tripsViewModel = new TripsViewModel
                            {

                                ID = dataReader.GetIntegerValue(ListRepository.TripIdColumnName),
                                ldx = dataReader.GetStringValue(ListRepository.TripLDxColumnName),
                                Trips = dataReader.GetStringValue(ListRepository.TripNameColumnName),
                                TripsDate = dataReader.GetDateTimeValue(ListRepository.TripsDateColumnName),
                                Camps = dataReader.GetStringValue(ListRepository.TripCampsColumnName),
                                Year = dataReader.GetIntegerValue(ListRepository.TripYearColumnName),
                                Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                                Notes = dataReader.GetStringValue(ListRepository.NotesColumnName)

                            };
                            result.Data.Add(tripsViewModel);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }

                }
            }

            return result;
        }

        public async Task<bool> UpdateTirpsAsync(TripsViewModel tripsViewModel)
        {

            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.TripIdParameterName, tripsViewModel.ID),
                base.GetParameter(ListRepository.TripYearParameterName, tripsViewModel.Year),
                base.GetParameter(ListRepository.TripNameParameterName, tripsViewModel.Trips),
                base.GetParameter(ListRepository.TripCampsParameterName, tripsViewModel.Camps),
                base.GetParameter(ListRepository.TripsDateParameterName, tripsViewModel.TripsDate),
                base.GetParameter(ListRepository.TripsNotesParameterName, tripsViewModel.Notes),
                base.GetParameter(ListRepository.TripLDxParameterName, tripsViewModel.ldx),
                 base.GetParameter(ListRepository.ActiveParameterName, tripsViewModel.Active)

            };
            //TODO: Add other parameters.

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateTripsStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateTripsAsync(TripsViewModel tripsViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.RIdParameterName, tripsViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, tripsViewModel.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateTripsStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }


        #endregion

        #region Home Stay

        private const string HomeIDParameterName = "PHomeID";
        private const string HomeRefrenaceParameterName = "PHomeRefrenance";
        private const string HomeNameParameterName = "PHomeName";
        private const string HomeCellNumberParameterName = "PHomeCellNumber";
        private const string HomeEmailParameterName = "PHomeEmail";
        private const string HomeAddressParameterName = "PHomeAddress";
        private const string HomeRegionParameterName = "PHomeRegion";
        private const string HomeIntersectionParameterName = "PHomeIntersection";
        private const string HomeDistanceParameterName = "PHomeDistance";
        private const string HomeMealsParameterName = "PHomeMeals";
        private const string HomePreferParameterName = "PHomePrefer";
        private const string HomeRoomsParameterName = "PHomeRooms";
        private const string HomeAgreementParameterName = "PHomeAggrement";
        private const string HomePoliceCheckParameterName = "PHomePoliceCheck";
        private const string HomeStayLocationURLParameterName = "PHomeStayLocationURL";


        private const string HomeIDColumnName = "HomeID";
        private const string HomeRefrenaceColumnName = "HomeRefrenance";
        private const string HomeNameColumnName = "HomeName";
        private const string HomeCellNumberColumnName = "HomeCellNumber";
        private const string HomeEmailColumnName = "HomeEmail";
        private const string HomeAddressColumnName = "HomeAddress";
        private const string HomeRegionColumnName = "HomeRegion";
        private const string HomeIntersectionColumnName = "HomeIntersection";
        private const string HomeDistanceColumnName = "HomeDistance";
        private const string HomeMealsColumnName = "HomeMeals";
        private const string HomePreferColumnName = "HomePrefer";
        private const string HomeRoomsColumnName = "HomeRooms";
        private const string HomeAgreementColumnName = "HomeAggrement";
        private const string HomePoliceCheckColumnName = "HomePoliceCheck";
        private const string HomeStayLocationURLColumnName = "HomeStayLocationURL";
        public async Task<int> CreateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            var homeIDParameterName = base.GetParameterOut(ListRepository.HomeIDParameterName, SqlDbType.Int, homeStayViewModel.HomeId);
            var parameters = new List<DbParameter>
            {
                homeIDParameterName,



                base.GetParameter(ListRepository.HomeRefrenaceParameterName, homeStayViewModel.Reference),
                base.GetParameter(ListRepository.HomeNameParameterName, homeStayViewModel.Name),
                base.GetParameter(ListRepository.HomeCellNumberParameterName, homeStayViewModel.CellNumber),
                base.GetParameter(ListRepository.HomeEmailParameterName, homeStayViewModel.Email),
                base.GetParameter(ListRepository.HomeAddressParameterName, homeStayViewModel.Address),
                base.GetParameter(ListRepository.HomeRegionParameterName, homeStayViewModel.Region),
                base.GetParameter(ListRepository.HomeIntersectionParameterName, homeStayViewModel.Intersection),
                base.GetParameter(ListRepository.HomeDistanceParameterName, homeStayViewModel.Distance),
                base.GetParameter(ListRepository.HomeMealsParameterName, homeStayViewModel.Meals),
                base.GetParameter(ListRepository.HomePreferParameterName, homeStayViewModel.Prefer),
                base.GetParameter(ListRepository.HomeRoomsParameterName, homeStayViewModel.Rooms),
                base.GetParameter(ListRepository.HomeAgreementParameterName, homeStayViewModel.Aggrements),
                base.GetParameter(ListRepository.HomePoliceCheckParameterName, homeStayViewModel.PoliceCheck),
                base.GetParameter(ListRepository.HomeStayLocationURLParameterName, homeStayViewModel.HomeStayLocationURL)

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddHomeStayStoredProcedureName, CommandType.StoredProcedure);

            homeStayViewModel.HomeId = Convert.ToInt32(homeIDParameterName.Value);

            return homeStayViewModel.HomeId;
        }


        public async Task<HomeStayViewModel> GetHomeStayAsync(int homeStayId)
        {
            HomeStayViewModel homeStayVM = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.HomeIDParameterName, homeStayId)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetHomeStayStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        homeStayVM = new HomeStayViewModel
                        {

                            HomeId = dataReader.GetIntegerValue(ListRepository.HomeIDColumnName),
                            Reference = dataReader.GetStringValue(ListRepository.HomeRefrenaceColumnName),
                            Name = dataReader.GetStringValue(ListRepository.HomeNameColumnName),
                            CellNumber = dataReader.GetStringValue(ListRepository.HomeCellNumberColumnName),
                            Email = dataReader.GetStringValue(ListRepository.HomeEmailColumnName),
                            Address = dataReader.GetStringValue(ListRepository.HomeAddressColumnName),
                            Rooms = dataReader.GetStringValue(ListRepository.HomeRoomsColumnName),
                            Region = dataReader.GetStringValue(ListRepository.HomeRegionColumnName),
                            Intersection = dataReader.GetStringValue(ListRepository.HomeIntersectionColumnName),
                            Distance = dataReader.GetStringValue(ListRepository.HomeDistanceColumnName),
                            Meals = dataReader.GetStringValue(ListRepository.HomeMealsColumnName),
                            Prefer = dataReader.GetStringValue(ListRepository.HomePreferColumnName),
                            Aggrements = dataReader.GetStringValue(ListRepository.HomeAgreementColumnName),
                            PoliceCheck = dataReader.GetStringValue(ListRepository.HomePoliceCheckColumnName),
                            HomeStayLocationURL = dataReader.GetStringValue(ListRepository.HomeStayLocationURLColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)

                        };
                    }
                }
            }
            return homeStayVM;
        }

        public async Task<AllResponse<HomeStayViewModel>> GetAllHomeStay(HomeStayRequestVm homeStay)
        {
            HomeStayViewModel homeStayViewModel = null;

            var result = new AllResponse<HomeStayViewModel>
            {
                Data = new List<HomeStayViewModel>()
            };

            var parameters = new List<DbParameter>
            {

                
                base.GetParameter(BaseRepository.ActiveParameterName, homeStay.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllHomeStayStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                   
                        while (dataReader.Read())
                        {
                            homeStayViewModel = new HomeStayViewModel
                            {

                                HomeId = dataReader.GetIntegerValue(ListRepository.HomeIDColumnName),
                                Reference = dataReader.GetStringValue(ListRepository.HomeRefrenaceColumnName),
                                Name = dataReader.GetStringValue(ListRepository.HomeNameColumnName),
                                CellNumber = dataReader.GetStringValue(ListRepository.HomeCellNumberColumnName),
                                Email = dataReader.GetStringValue(ListRepository.HomeEmailColumnName),
                                Address = dataReader.GetStringValue(ListRepository.HomeAddressColumnName),
                                Rooms = dataReader.GetStringValue(ListRepository.HomeRoomsColumnName),
                                Region = dataReader.GetStringValue(ListRepository.HomeRegionColumnName),
                                Intersection = dataReader.GetStringValue(ListRepository.HomeIntersectionColumnName),
                                Distance = dataReader.GetStringValue(ListRepository.HomeDistanceColumnName),
                                Meals = dataReader.GetStringValue(ListRepository.HomeMealsColumnName),
                                Prefer = dataReader.GetStringValue(ListRepository.HomePreferColumnName),
                                Aggrements = dataReader.GetStringValue(ListRepository.HomeAgreementColumnName),
                                PoliceCheck = dataReader.GetStringValue(ListRepository.HomePoliceCheckColumnName),
                                Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)


                            };
                            result.Data.Add(homeStayViewModel);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }

                  
                }
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        public async Task<bool> UpdateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            var parameters = new List<DbParameter>
                {
                        base.GetParameter(ListRepository.HomeIDParameterName, homeStayViewModel.HomeId),
                        base.GetParameter(ListRepository.HomeRefrenaceParameterName, homeStayViewModel.Reference),
                        base.GetParameter(ListRepository.HomeNameParameterName, homeStayViewModel.Name),
                        base.GetParameter(ListRepository.HomeCellNumberParameterName, homeStayViewModel.CellNumber),
                        base.GetParameter(ListRepository.HomeEmailParameterName, homeStayViewModel.Email),
                        base.GetParameter(ListRepository.HomeAddressParameterName, homeStayViewModel.Address),
                        base.GetParameter(ListRepository.HomeRegionParameterName, homeStayViewModel.Region),
                        base.GetParameter(ListRepository.HomeIntersectionParameterName, homeStayViewModel.Intersection),
                        base.GetParameter(ListRepository.HomeDistanceParameterName, homeStayViewModel.Distance),
                        base.GetParameter(ListRepository.HomeMealsParameterName, homeStayViewModel.Meals),
                        base.GetParameter(ListRepository.HomePreferParameterName, homeStayViewModel.Prefer),
                        base.GetParameter(ListRepository.HomeRoomsParameterName, homeStayViewModel.Rooms),
                        base.GetParameter(ListRepository.HomeAgreementParameterName, homeStayViewModel.Aggrements),
                        base.GetParameter(ListRepository.HomePoliceCheckParameterName, homeStayViewModel.PoliceCheck),
                        base.GetParameter(ListRepository.HomeStayLocationURLParameterName, homeStayViewModel.HomeStayLocationURL),
                        base.GetParameter(BaseRepository.ActiveParameterName, homeStayViewModel.Active)
                        

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateHomeStayStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.RIdParameterName, homeStayViewModel.HomeId),
                    base.GetParameter(BaseRepository.ActiveParameterName, homeStayViewModel.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateHomeStayStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        #endregion

        #region Addins

        private const string AddinsIdParameterName = "PID";
        private const string AddinsParameterName = "PAddins";
        private const string AddinsTypeParameterName = "PAddinsType";
        private const string AddinsCampsParameterName = "PAddinsCamps";
        private const string AddinsCostParameterName = "PAddinsCost";
        private const string IsDefaultParameterName = "PIsDefault";

        private const string AddinsIdColumnName = "ID";
        private const string AddinsColumnName = "Addins";
        private const string AddinsTypeColumnName = "AddinsType";
        private const string AddinsCampsColumnName = "AddinsCamps";
        private const string AddinsCostColumnName = "AddinsCost";
        private const string IsDefaultColumnName = "IsDefault";


        public async Task<int> CreateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            var addinsIdParamter = base.GetParameterOut(ListRepository.AddinsIdParameterName, SqlDbType.Int, addinsViewModel.ID);
            var parameters = new List<DbParameter>
            {
                addinsIdParamter,



                base.GetParameter(ListRepository.AddinsParameterName, addinsViewModel.Addins),
                base.GetParameter(ListRepository.AddinsTypeParameterName, addinsViewModel.AddinsType),
                base.GetParameter(ListRepository.AddinsCampsParameterName, addinsViewModel.Camps),
                base.GetParameter(ListRepository.AddinsCostParameterName, addinsViewModel.Cost),
                base.GetParameter(ListRepository.IsDefaultParameterName, addinsViewModel.IsDefault)

            };

            await base.ExecuteNonQuery(parameters, ListRepository.AddAddinsStoredProcedureName, CommandType.StoredProcedure);

            addinsViewModel.ID = Convert.ToInt32(addinsIdParamter.Value);

            return addinsViewModel.ID;
        }

        public async Task<AddinsViewModel> GetAddins(int tripID)
        {
            AddinsViewModel addinsViewModel = null;
            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.TripIdParameterName, tripID)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAddinsTripStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        addinsViewModel = new AddinsViewModel
                        {

                            ID = dataReader.GetIntegerValue(ListRepository.AddinsIdColumnName),
                            Addins = dataReader.GetStringValue(ListRepository.AddinsColumnName),
                            AddinsType = dataReader.GetStringValue(ListRepository.AddinsTypeColumnName),
                            Camps = dataReader.GetStringValue(ListRepository.AddinsCampsColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            IsDefault = dataReader.GetBooleanValue(ListRepository.IsDefaultColumnName)
                            //Cost = dataReader.GetDecimalValueNullable(ListRepository.AddinsCostColumnName)
                        };
                    }
                }
            }
            return addinsViewModel;
        }


        public async Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AddinsRequestVm addins)
        {
            AddinsViewModel addinsViewModel = null;

            var result = new AllResponse<AddinsViewModel>
            {
                Data = new List<AddinsViewModel>()
               
            };

            var parameters = new List<DbParameter>
            {

               
                base.GetParameter(BaseRepository.ActiveParameterName, addins.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllAddinsTripStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    
                        while (dataReader.Read())
                        {
                            addinsViewModel = new AddinsViewModel
                            {

                                ID = dataReader.GetIntegerValue(ListRepository.AddinsIdColumnName),
                                Addins = dataReader.GetStringValue(ListRepository.AddinsColumnName),
                                AddinsType = dataReader.GetStringValue(ListRepository.AddinsTypeColumnName),
                                Camps = dataReader.GetStringValue(ListRepository.AddinsCampsColumnName),
                                Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                                IsDefault = dataReader.GetBooleanValue(ListRepository.IsDefaultColumnName)


                            };
                            result.Data.Add(addinsViewModel);
                        }

                        if (!dataReader.IsClosed)
                        {
                            dataReader.Close();
                        }
                        
                }
            }

            return result;
        }

        public async Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            var parameters = new List<DbParameter>
                {
                        base.GetParameter(ListRepository.AddinsIdParameterName, addinsViewModel.ID),
                        base.GetParameter(ListRepository.AddinsParameterName, addinsViewModel.Addins),
                        base.GetParameter(ListRepository.AddinsCampsParameterName, addinsViewModel.Camps),
                        base.GetParameter(ListRepository.AddinsTypeParameterName, addinsViewModel.AddinsType),
                      //  base.GetParameter(ListRepository.AddinsCostParameterName, addinsViewModel.Cost),
                        base.GetParameter(BaseRepository.ActiveParameterName, addinsViewModel.Active),
                        base.GetParameter(ListRepository.IsDefaultParameterName, addinsViewModel.IsDefault)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateAddinsStayStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }


        public async Task<bool> ActivateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.RIdParameterName, addinsViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, addinsViewModel.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateAddinsStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        #endregion


        #region Campus


        private const string CampusIDParameterName = "PCampusID";
        private const string CampusParameterName = "PCampus";
        private const string CampusCampsParameterName = "PCampusCamps";
        private const string CampusAddressOnReportsParameterName = "PCampusAddressOnReports";
        private const string CampusCompleteNameParameterName = "PCampusCompleteName";
        private const string CampusOnelineaddressParameterName = "PCampusOnelineaddress";


        private const string LookupNameParameterName = "PName";
        private const string LookupValueParameterName = "PValue";


        private const string CampusIDColumnName = "CampusID";
        private const string CampusColumnName = "Campus";
        private const string CampusCampsColumnName = "CampusCamps";
        private const string CampusAddressOnReportsColumnName = "CampusAddressOnReports";
        private const string CampusCompleteNameColumnName = "CampusCompleteName";
        private const string CampusOnelineaddressColumnName = "CampusOnelineaddress";


        public async Task<int> CreateCampusAsync(CampuseViewModel campusViewModel)
        {
            var campusIdParamter = base.GetParameterOut(ListRepository.CampusIDParameterName, SqlDbType.Int, campusViewModel.ID);
            var parameters = new List<DbParameter>
                {
                    campusIdParamter,


                    base.GetParameter(ListRepository.CampusParameterName, campusViewModel.Campus),
                    base.GetParameter(ListRepository.CampusCampsParameterName, campusViewModel.Camps),
                    base.GetParameter(ListRepository.CampusAddressOnReportsParameterName, campusViewModel.AddressOnReports),
                    base.GetParameter(ListRepository.CampusCompleteNameParameterName, campusViewModel.CompleteName),
                    base.GetParameter(ListRepository.CampusOnelineaddressParameterName, campusViewModel.Onelineaddress)

                };

            await base.ExecuteNonQuery(parameters, ListRepository.AddCampusStoredProcedureName, CommandType.StoredProcedure);

            campusViewModel.ID = Convert.ToInt32(campusIdParamter.Value);

            return campusViewModel.ID;
        }
        
        public async Task<bool> UpdateCampusAsync(CampuseViewModel campusViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.CampusIDParameterName, campusViewModel.ID),
                    base.GetParameter(ListRepository.CampusParameterName, campusViewModel.Campus),
                    base.GetParameter(ListRepository.CampusCampsParameterName, campusViewModel.Camps),
                    base.GetParameter(ListRepository.CampusAddressOnReportsParameterName, campusViewModel.AddressOnReports),
                    base.GetParameter(ListRepository.CampusCompleteNameParameterName, campusViewModel.CompleteName),
                    base.GetParameter(ListRepository.CampusOnelineaddressParameterName, campusViewModel.Onelineaddress),
                    base.GetParameter(BaseRepository.ActiveParameterName, campusViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateCampusStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }    

        public async Task<bool> ActivateCampusAsync(CampuseViewModel campusViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.CampusIDParameterName, campusViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, campusViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateCampusStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<CampuseViewModel> GetCampus(int campusId)
        {
            CampuseViewModel campusVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.CampusIDParameterName, campusId)
                };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetCampusStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        campusVM = new CampuseViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.CampusIDColumnName),
                            Campus = dataReader.GetStringValue(ListRepository.CampusColumnName),
                            Camps = dataReader.GetStringValue(ListRepository.CampusCampsColumnName),
                            AddressOnReports = dataReader.GetStringValue(ListRepository.CampusAddressOnReportsColumnName),
                            CompleteName = dataReader.GetStringValue(ListRepository.CampusCompleteNameColumnName),
                            Onelineaddress = dataReader.GetStringValue(ListRepository.CampusOnelineaddressColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                    }
                }
            }
            return campusVM;
        }

        public async Task<AllResponse<CampuseViewModel>> GetAllCampus(CampuseViewModel campusList)
        {
            CampuseViewModel campusVM = null;

            var result = new AllResponse<CampuseViewModel>
            {
                Data = new List<CampuseViewModel>()
            };

            var parameters = new List<DbParameter>
            {

                base.GetParameter(BaseRepository.ActiveParameterName, campusList.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllCampusStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                         campusVM = new CampuseViewModel
                            {
                                ID = dataReader.GetIntegerValue(ListRepository.CampusIDColumnName),
                                Campus = dataReader.GetStringValue(ListRepository.CampusColumnName),
                                Camps = dataReader.GetStringValue(ListRepository.CampusCampsColumnName),
                                AddressOnReports = dataReader.GetStringValue(ListRepository.CampusAddressOnReportsColumnName),
                                CompleteName = dataReader.GetStringValue(ListRepository.CampusCompleteNameColumnName),
                                Onelineaddress = dataReader.GetStringValue(ListRepository.CampusOnelineaddressColumnName),
                                Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                            };
                        result.Data.Add(campusVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }


                }
            }

            return result;
        }

        #endregion


        #region Program


        private const string AddProgramStoredProcedureName = "AddProgram";
        private const string UpdateProgramStoredProcedureName = "UpdateProgram";
        private const string GetProgramStoredProcedureName = "GetProgram";
        private const string GetAllProgramStoredProcedureName = "GetAllProgram";
        private const string ActivateProgramStoredProcedureName = "ActivateProgram";

        private const string ProgramIDParameterName = "PProgramID";
        private const string ProgramNameParameterName = "PName";


        private const string ProgramIDColumnName = "ProgramID";
        private const string ProgramNameColumnName = "ProgramName";


        public async Task<int> CreateProgramAsync(ProgramViewModel programViewModel)
        {
            var programIdParamter = base.GetParameterOut(ListRepository.ProgramIDParameterName, SqlDbType.Int, programViewModel.ID);
            var parameters = new List<DbParameter>
                {
                    programIdParamter,
                    
                    base.GetParameter(ListRepository.ProgramNameParameterName, programViewModel.ProgramName),
                    base.GetParameter(ListRepository.IsDefaultParameterName, programViewModel.IsDefault)

                };

            await base.ExecuteNonQuery(parameters, ListRepository.AddProgramStoredProcedureName, CommandType.StoredProcedure);

            programViewModel.ID = Convert.ToInt32(programIdParamter.Value);

            return programViewModel.ID;
        }

        public async Task<bool> UpdateProgramAsync(ProgramViewModel programViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.ProgramIDParameterName, programViewModel.ID),
                    base.GetParameter(ListRepository.ProgramNameParameterName, programViewModel.ProgramName),
                    base.GetParameter(BaseRepository.ActiveParameterName, programViewModel.Active),
                    base.GetParameter(ListRepository.IsDefaultParameterName, programViewModel.IsDefault)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateProgramStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateProgramAsync(ProgramViewModel programViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.ProgramIDParameterName, programViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, programViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateProgramStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<ProgramViewModel> GetProgramAsync(int programId)
        {
            ProgramViewModel programVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.ProgramIDParameterName, programId)
                };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetProgramStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        programVM = new ProgramViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.ProgramIDColumnName),
                            ProgramName = dataReader.GetStringValue(ListRepository.ProgramNameColumnName),
                            IsDefault = dataReader.GetBooleanValue(ListRepository.IsDefaultColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                    }
                }
            }
            return programVM;
        }

        public async Task<AllResponse<ProgramViewModel>> GetAllProgramAsync(AllRequest<ProgramViewModel> programList)
        {
            ProgramViewModel programVM = null;

            var result = new AllResponse<ProgramViewModel>
            {
                Data = new List<ProgramViewModel>(),
                Offset = programList.Offset,
                PageSize = programList.PageSize,
                SortColumn = programList.SortColumn,
                SortAscending = programList.SortAscending
            };

            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseRepository.ActiveParameterName, programList.Data.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllProgramStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        programVM = new ProgramViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.ProgramIDColumnName),
                            ProgramName = dataReader.GetStringValue(ListRepository.ProgramNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            IsDefault = dataReader.GetBooleanValue(ListRepository.IsDefaultColumnName)
                        };
                        result.Data.Add(programVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }


                }
            }

            return result;
        }

        #endregion

        #region SubProgram


        private const string AddSubProgramStoredProcedureName = "AddSubProgram";
        private const string UpdateSubProgramStoredProcedureName = "UpdateSubProgram";
        private const string GetSubProgramStoredProcedureName = "GetSubProgram";
        private const string GetAllSubProgramStoredProcedureName = "GetAllSubProgram";
        private const string ActivateSubProgramStoredProcedureName = "ActivateSubProgram";

        private const string SubProgramIDParameterName = "PSubProgramID";
        private const string SubProgramProgramIDParameterName = "PProgramID";
        private const string SubProgramNameParameterName = "PSubProgramName";


        private const string SubProgramIDColumnName = "SubProgramID";
        private const string SubProgramProgramIDColumnName = "ProgramID";
        private const string SubProgramProgramNameColumnName = "ProgramName";
        private const string SubProgramNameColumnName = "SubProgramName";


        public async Task<int> CreateSubProgramAsync(SubProgramViewModel subProgramViewModel)
        {
            var subProgramIdParamter = base.GetParameterOut(ListRepository.SubProgramIDParameterName, SqlDbType.Int, 0);
            var parameters = new List<DbParameter>
                {
                    subProgramIdParamter,

                    base.GetParameter(ListRepository.SubProgramProgramIDParameterName, subProgramViewModel.ProgramID),
                    base.GetParameter(ListRepository.SubProgramNameParameterName, subProgramViewModel.SubProgramName)

                };

            await base.ExecuteNonQuery(parameters, ListRepository.AddSubProgramStoredProcedureName, CommandType.StoredProcedure);

            subProgramViewModel.ID = Convert.ToInt32(subProgramIdParamter.Value);

            return subProgramViewModel.ID;
        }

        public async Task<bool> UpdateSubrogramAsync(SubProgramViewModel subProgramViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.SubProgramIDParameterName, subProgramViewModel.ID),
                    base.GetParameter(ListRepository.SubProgramProgramIDParameterName, subProgramViewModel.ProgramID),
                    base.GetParameter(ListRepository.SubProgramNameParameterName, subProgramViewModel.SubProgramName),
                    base.GetParameter(BaseRepository.ActiveParameterName, subProgramViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateSubProgramStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateSubProgramAsync(SubProgramViewModel subProgramViewModel)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.SubProgramIDParameterName, subProgramViewModel.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, subProgramViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.ActivateSubProgramStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<SubProgramViewModel> GetSubProgramAsync(int subProgramId)
        {
            SubProgramViewModel subProgramVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.SubProgramIDParameterName, subProgramId)
                };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetSubProgramStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        subProgramVM = new SubProgramViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.SubProgramIDColumnName),
                            ProgramID = dataReader.GetIntegerValue(ListRepository.SubProgramProgramIDColumnName),
                            SubProgramName = dataReader.GetStringValue(ListRepository.SubProgramNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                    }
                }
            }
            return subProgramVM;
        }

        public async Task<AllResponse<SubProgramViewModel>> GetAllSubProgramAsync(AllRequest<SubProgramViewModel> subProgramList)
        {
            SubProgramViewModel subProgramVM = null;

            var result = new AllResponse<SubProgramViewModel>
            {
                Data = new List<SubProgramViewModel>(),
                Offset = subProgramList.Offset,
                PageSize = subProgramList.PageSize,
                SortColumn = subProgramList.SortColumn,
                SortAscending = subProgramList.SortAscending
            };

            var parameters = new List<DbParameter>
            {
                base.GetParameter(ListRepository.ProgramIDParameterName, subProgramList.Data.ProgramID),
                base.GetParameter(BaseRepository.ActiveParameterName, subProgramList.Data.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllSubProgramStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        subProgramVM = new SubProgramViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.SubProgramIDColumnName),
                            ProgramID = dataReader.GetIntegerValue(ListRepository.SubProgramProgramIDColumnName),
                            SubProgramName = dataReader.GetStringValue(ListRepository.SubProgramNameColumnName),
                            ProgramName = dataReader.GetStringValue(ListRepository.ProgramNameColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                        result.Data.Add(subProgramVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }


                }
            }

            return result;
        }

        #endregion

        #region LookupValue

        public async Task<int> CreateLookupValueAsync(LookupValueViewModel lookup)
        {
            var statusIdParamter = base.GetParameterOut(ListRepository.IDParameterName, SqlDbType.Int, lookup.ID);
            var parameters = new List<DbParameter>
                {
                    statusIdParamter,
                    base.GetParameter(ListRepository.DescriptionParameterName,lookup.Description),
                    base.GetParameter(ListRepository.NameParameterName, lookup.Name),
                    base.GetParameter(ListRepository.LookupTableIdParameterName, lookup.LookupTableId)

                };
            await base.ExecuteNonQuery(parameters, ListRepository.AddLookupValueStoredProcedureName, CommandType.StoredProcedure);

            lookup.ID = Convert.ToInt32(statusIdParamter.Value);

            return lookup.ID;
        }
        public async Task<int> DeleteLookupValue(LookupValueViewModel lookup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.IDParameterName,lookup.ID)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.DeleteLookupValueStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0 ? 1 : 0;
        }
        
        public async Task<bool> UpdateLookupValue(LookupValueViewModel lookup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(ListRepository.IDParameterName,lookup.ID),
                    base.GetParameter(ListRepository.DescriptionParameterName,lookup.Description),
                    base.GetParameter(ListRepository.NameParameterName, lookup.Name),
                    base.GetParameter(ListRepository.LookupTableIdParameterName, lookup.LookupTableId)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateLookupValueStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0 ? true : false;
        }

        public async Task<List<LookupValueViewModel>> GetListBaseonLookupTable(string lookupTable)
        {
            LookupValueViewModel lookupValue = null;
            List<LookupValueViewModel> list = new List<LookupValueViewModel>();



            var parameters = new List<DbParameter>
            {

                base.GetParameter(ListRepository.LookupTableParameterName, lookupTable)
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetLookupValueListStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        lookupValue = new LookupValueViewModel
                        {
                            ID = dataReader.GetIntegerValue(ListRepository.IdColumnName),
                            Value = dataReader.GetIntegerValue(ListRepository.ValueColumnName),
                            Name = dataReader.GetStringValue(ListRepository.NameColumnName),
                            Description = dataReader.GetStringValue(ListRepository.DescriptionColumnName),
                            LookupTableId = dataReader.GetIntegerValue(ListRepository.LookupTableIdColumnName)


                        };
                        list.Add(lookupValue);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return list;
        }

        #endregion
    }
}
