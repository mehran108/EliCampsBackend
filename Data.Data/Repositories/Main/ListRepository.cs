using Domain.Domain.ViewModels;
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
        private const string GetTripStoredProcedureName = "GetTripsById";
        private const string GetRoomsStoredProcedureName = "GetRooms";
        private const string GetAllRoomsStoredProcedureName = "GetAllRooms";
        private const string GetAllTripsStoredProcedureName = "GetAllTrips";
        private const string GetAllHomeStayStoredProcedureName = "GetAllHomeStay";
        private const string GetHomeStayStoredProcedureName = "GetHomeStayById";
        private const string GetAddinsTripStoredProcedureName = "GetAddinsById";

        private const string GetAllAddinsTripStoredProcedureName = "GetAllAddins";

        private const string AddRoomsStoredProcedureName = "AddRooms";
        private const string AddTripsStoredProcedureName = "AddTrips";
        private const string AddAddinsStoredProcedureName = "AddAddins";
        private const string AddHomeStayStoredProcedureName = "AddHomeStay";
        private const string UpdateAgentStoredProcedureName = "UpdateAgent";
        private const string GetLookupValueListStoredProcedureName = "GetLookupValueList";

        private const string UpdateTripsStoredProcedureName = "UpdateTrips";
        private const string UpdateRoomtblTripsStoredProcedureName = "UpdateRoomtbl";
        private const string UpdateHomeStayStoredProcedureName = "UpdateHomeStay";
        private const string UpdateAddinsStayStoredProcedureName = "UpdateAddins";

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

         //   roomsViewModel.ID = Convert.ToInt32(agentIdParamter.Value);

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
                    base.GetParameter(BaseRepository.ActiveColumnName, agent.Active),

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateAgentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
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
                            Year = dataReader.GetIntegerValue(ListRepository.TripYearColumnName)
                        };
                    }
                }
            }
            return agentVM;
        }

        public async Task<AllResponse<TripsViewModel>> GetAllTripsList(AllRequest<TripsViewModel> trips)
        {
            TripsViewModel tripsViewModel = null;

            var result = new AllResponse<TripsViewModel>
            {
                Data = new List<TripsViewModel>(),
                Offset = trips.Offset,
                PageSize = trips.PageSize,
                SortColumn = trips.SortColumn,
                SortAscending = trips.SortAscending
            };

            var parameters = new List<DbParameter>
            {

                base.GetParameter(BaseRepository.OffsetParameterName, trips.Offset),
                base.GetParameter(BaseRepository.PageSizeParameterName, trips.PageSize),
                base.GetParameter(BaseRepository.SortColumnParameterName, trips.SortColumn),
                base.GetParameter(BaseRepository.SortAscendingParameterName, trips.SortAscending),
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllTripsStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
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
                                Year = dataReader.GetIntegerValue(ListRepository.TripYearColumnName)

                            };
                            result.Data.Add(tripsViewModel);
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
                base.GetParameter(ListRepository.HomePoliceCheckParameterName, homeStayViewModel.PoliceCheck)

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

                        };
                    }
                }
            }
            return homeStayVM;
        }

        public async Task<AllResponse<HomeStayViewModel>> GetAllHomeStay(AllRequest<HomeStayViewModel> homeStay)
        {
            HomeStayViewModel homeStayViewModel = null;

            var result = new AllResponse<HomeStayViewModel>
            {
                Data = new List<HomeStayViewModel>(),
                Offset = homeStay.Offset,
                PageSize = homeStay.PageSize,
                SortColumn = homeStay.SortColumn,
                SortAscending = homeStay.SortAscending
            };

            var parameters = new List<DbParameter>
            {

                base.GetParameter(BaseRepository.OffsetParameterName, homeStay.Offset),
                base.GetParameter(BaseRepository.PageSizeParameterName, homeStay.PageSize),
                base.GetParameter(BaseRepository.SortColumnParameterName, homeStay.SortColumn),
                base.GetParameter(BaseRepository.SortAscendingParameterName, homeStay.SortAscending),
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
                        base.GetParameter(BaseRepository.ActiveParameterName, homeStayViewModel.Active)


                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateHomeStayStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }



        #endregion

        #region Addins

        private const string AddinsIdParameterName = "PID";
        private const string AddinsParameterName = "PAddins";
        private const string AddinsTypeParameterName = "PAddinsType";
        private const string AddinsCampsParameterName = "PAddinsCamps";
        private const string AddinsCostParameterName = "PAddinsCost";

        private const string AddinsIdColumnName = "ID";
        private const string AddinsColumnName = "Addins";
        private const string AddinsTypeColumnName = "AddinsType";
        private const string AddinsCampsColumnName = "AddinsCamps";
        private const string AddinsCostColumnName = "AddinsCost";


        public async Task<int> CreateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            var addinsIdParamter = base.GetParameterOut(ListRepository.AddinsIdParameterName, SqlDbType.Int, addinsViewModel.ID);
            var parameters = new List<DbParameter>
            {
                addinsIdParamter,



                base.GetParameter(ListRepository.AddinsParameterName, addinsViewModel.Addins),
                base.GetParameter(ListRepository.AddinsTypeParameterName, addinsViewModel.AddinsType),
                base.GetParameter(ListRepository.AddinsCampsParameterName, addinsViewModel.Camps),
                base.GetParameter(ListRepository.AddinsCostParameterName, addinsViewModel.Cost)

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
                            Camps = dataReader.GetStringValue(ListRepository.AddinsCampsColumnName)
                            //Cost = dataReader.GetDecimalValueNullable(ListRepository.AddinsCostColumnName)
                        };
                    }
                }
            }
            return addinsViewModel;
        }


        public async Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AllRequest<AddinsViewModel> addins)
        {
            AddinsViewModel addinsViewModel = null;

            var result = new AllResponse<AddinsViewModel>
            {
                Data = new List<AddinsViewModel>(),
                Offset = addins.Offset,
                PageSize = addins.PageSize,
                SortColumn = addins.SortColumn,
                SortAscending = addins.SortAscending
            };

            var parameters = new List<DbParameter>
            {

                base.GetParameter(BaseRepository.OffsetParameterName, addins.Offset),
                base.GetParameter(BaseRepository.PageSizeParameterName, addins.PageSize),
                base.GetParameter(BaseRepository.SortColumnParameterName, addins.SortColumn),
                base.GetParameter(BaseRepository.SortAscendingParameterName, addins.SortAscending),
            };

            using (var dataReader = await base.ExecuteReader(parameters, ListRepository.GetAllAddinsTripStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        while (dataReader.Read())
                        {
                            addinsViewModel = new AddinsViewModel
                            {

                                ID = dataReader.GetIntegerValue(ListRepository.AddinsIdColumnName),
                                Addins = dataReader.GetStringValue(ListRepository.AddinsColumnName),
                                AddinsType = dataReader.GetStringValue(ListRepository.AddinsTypeColumnName),
                                Camps = dataReader.GetStringValue(ListRepository.AddinsCampsColumnName)


                            };
                            result.Data.Add(addinsViewModel);
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

        public async Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            var parameters = new List<DbParameter>
                {
                        base.GetParameter(ListRepository.AddinsIdParameterName, addinsViewModel.ID),
                        base.GetParameter(ListRepository.AddinsParameterName, addinsViewModel.Addins),
                        base.GetParameter(ListRepository.AddinsCampsParameterName, addinsViewModel.Camps),
                        base.GetParameter(ListRepository.AddinsTypeParameterName, addinsViewModel.AddinsType),
                      //  base.GetParameter(ListRepository.AddinsCostParameterName, addinsViewModel.Cost),
                        base.GetParameter(BaseRepository.ActiveParameterName, addinsViewModel.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, ListRepository.UpdateAddinsStayStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        #endregion


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

                                Value = dataReader.GetIntegerValue(ListRepository.ValueColumnName),
                                Name = dataReader.GetStringValue(ListRepository.NameColumnName)

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



    }
}
