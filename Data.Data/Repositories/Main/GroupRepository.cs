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
    public class GroupRepository : BaseRepository, IGroupRepository
    {
        public GroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public void Dispose()
        {
        }

        private const string AddStoredProcedureName = "AddGroup";
        private const string GetStoredProcedureName = "GetGroup";
        private const string GetAllStoredProcedureName = "GetAllGroup";
        private const string UpdateStoredProcedureName = "UpdateGroup";


        private const string GroupIdParameterName = "PGroupID";
        private const string YearParameterName = "PYear";
        private const string CampsParameterName = "PCamps";
        private const string RefNumberParameterName = "PRefNumber";
        private const string AgentIDParameterName = "PAgentID";
        private const string AgencyRefParameterName = "PAgencyRef";
        private const string CountryParameterName = "PCountry";
        private const string ArrivalDateParameterName = "PArrivalDate";
        private const string TerminalParameterName = "PTerminal";
        private const string FlightNumberParameterName = "PFlightNumber";
        private const string DestinationFromParameterName = "PDestinationFrom";
        private const string ArrivalTimeParameterName = "PArrivalTime";
        private const string DepartureDateParameterName = "PDepartureDate";
        private const string DepartureTerminalParameterName = "PDepartureTerminal";
        private const string DepartureFlightNumberParameterName = "PDepartureFlightNumber";
        private const string DestinationToParameterName = "PDestinationTo";
        private const string FlightDepartureTimeParameterName = "PFlightDepartureTime";


        private const string GroupIdColumnName = "GroupID";
        private const string YearColumnName = "Year";
        private const string CampsColumnName = "Camps";
        private const string RefNumberColumnName = "RefNumber";
        private const string AgentIDColumnName = "AgentID";
        private const string AgencyRefColumnName = "AgencyRef";
        private const string CountryColumnName = "Country";
        private const string ArrivalDateColumnName = "ArrivalDate";
        private const string TerminalColumnName = "Terminal";
        private const string FlightNumberColumnName = "FlightNumber";
        private const string DestinationFromColumnName = "DestinationFrom";
        private const string ArrivalTimeColumnName = "ArrivalTime";
        private const string DepartureDateColumnName = "DepartureDate";
        private const string DepartureTerminalColumnName = "DepartureTerminal";
        private const string DepartureFlightNumberColumnName = "DepartureFlightNumber";
        private const string DestinationToColumnName = "DestinationTo";
        private const string FlightDepartureTimeColumnName = "FlightDepartureTime";


        public async Task<int> AddGroupAsync(GroupViewModel group)
        {
            var groupIdParamter = base.GetParameterOut(GroupRepository.GroupIdParameterName, SqlDbType.Int, group.ID);
            var parameters = new List<DbParameter>
                {
                    groupIdParamter,
                    
                    base.GetParameter(GroupRepository.YearParameterName, group.Year),
                    base.GetParameter(GroupRepository.CampsParameterName, group.Camps),
                    base.GetParameter(GroupRepository.RefNumberParameterName, group.RefNumber),
                    base.GetParameter(GroupRepository.AgentIDParameterName, group.AgentID),
                    base.GetParameter(GroupRepository.AgencyRefParameterName, group.AgencyRef),
                    base.GetParameter(GroupRepository.CountryParameterName, group.Country),
                    base.GetParameter(GroupRepository.ArrivalDateParameterName, group.ArrivalDate),
                    base.GetParameter(GroupRepository.TerminalParameterName, group.Terminal),
                    base.GetParameter(GroupRepository.FlightNumberParameterName, group.FlightNumber),
                    base.GetParameter(GroupRepository.DestinationFromParameterName, group.DestinationFrom),
                    base.GetParameter(GroupRepository.ArrivalTimeParameterName, group.ArrivalTime),
                    base.GetParameter(GroupRepository.DepartureDateParameterName, group.DepartureDate),
                    base.GetParameter(GroupRepository.DepartureTerminalParameterName, group.DepartureTerminal),
                    base.GetParameter(GroupRepository.DepartureFlightNumberParameterName, group.DepartureFlightNumber),
                    base.GetParameter(GroupRepository.DestinationToParameterName, group.DestinationTo),
                    base.GetParameter(GroupRepository.FlightDepartureTimeParameterName, group.FlightDepartureTime)

                };

            await base.ExecuteNonQuery(parameters, GroupRepository.AddStoredProcedureName, CommandType.StoredProcedure);

            group.ID = Convert.ToInt32(groupIdParamter.Value);

            return group.ID;
        }

        public async Task<bool> UpdateGroupAsync(GroupViewModel group)
        {
             var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(GroupRepository.YearParameterName, group.Year),
                    base.GetParameter(GroupRepository.CampsParameterName, group.Camps),
                    base.GetParameter(GroupRepository.RefNumberParameterName, group.RefNumber),
                    base.GetParameter(GroupRepository.AgentIDParameterName, group.AgentID),
                    base.GetParameter(GroupRepository.AgencyRefParameterName, group.AgencyRef),
                    base.GetParameter(GroupRepository.CountryParameterName, group.Country),
                    base.GetParameter(GroupRepository.ArrivalDateParameterName, group.ArrivalDate),
                    base.GetParameter(GroupRepository.TerminalParameterName, group.Terminal),
                    base.GetParameter(GroupRepository.FlightNumberParameterName, group.FlightNumber),
                    base.GetParameter(GroupRepository.DestinationFromParameterName, group.DestinationFrom),
                    base.GetParameter(GroupRepository.ArrivalTimeParameterName, group.ArrivalTime),
                    base.GetParameter(GroupRepository.DepartureDateParameterName, group.DepartureDate),
                    base.GetParameter(GroupRepository.DepartureTerminalParameterName, group.DepartureTerminal),
                    base.GetParameter(GroupRepository.DepartureFlightNumberParameterName, group.DepartureFlightNumber),
                    base.GetParameter(GroupRepository.DestinationToParameterName, group.DestinationTo),
                    base.GetParameter(GroupRepository.FlightDepartureTimeParameterName, group.FlightDepartureTime)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.UpdateStoredProcedureName, CommandType.StoredProcedure);
            
            return returnValue > 0;
        }

        public async Task<GroupViewModel> GetGroupAsync(int groupID)
        {
            GroupViewModel groupVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, groupID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        groupVM = new GroupViewModel
                        {

                            ID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            Year = dataReader.GetIntegerValue(GroupRepository.YearColumnName),
                            Camps = dataReader.GetStringValue(GroupRepository.CampsColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            AgentID = dataReader.GetIntegerValue(GroupRepository.AgentIDColumnName),
                            AgencyRef = dataReader.GetStringValue(GroupRepository.AgencyRefColumnName),
                            Country = dataReader.GetStringValue(GroupRepository.CountryColumnName),
                            ArrivalDate = dataReader.GetDateTimeValue(GroupRepository.ArrivalDateColumnName),
                            Terminal = dataReader.GetStringValue(GroupRepository.TerminalColumnName),
                            FlightNumber = dataReader.GetStringValue(GroupRepository.FlightNumberColumnName),
                            DestinationFrom = dataReader.GetStringValue(GroupRepository.DestinationFromColumnName),
                            ArrivalTime = dataReader.GetStringValue(GroupRepository.ArrivalTimeColumnName),
                            DepartureDate = dataReader.GetDateTimeValue(GroupRepository.DepartureDateColumnName),
                            DepartureTerminal = dataReader.GetStringValue(GroupRepository.DepartureTerminalColumnName),
                            DepartureFlightNumber = dataReader.GetStringValue(GroupRepository.DepartureFlightNumberColumnName),
                            DestinationTo = dataReader.GetStringValue(GroupRepository.DestinationToColumnName),
                            FlightDepartureTime = dataReader.GetStringValue(GroupRepository.FlightDepartureTimeColumnName),
                        };
                        
	
	
             }
                }
            }

            return groupVM;
        }

        public async Task<AllResponse<GroupViewModel>> GetAllGroupList(AllRequest<GroupViewModel> groups)
        {
            GroupViewModel groupVM = null;

            var result = new AllResponse<GroupViewModel>
            {
                Data = new List<GroupViewModel>(),
                Offset = groups.Offset,
                PageSize = groups.PageSize,
                SortColumn = groups.SortColumn,
                SortAscending = groups.SortAscending
            };

            var parameters = new List<DbParameter>
            {

                
            };

            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        while (dataReader.Read())
                        {
                            groupVM = new GroupViewModel
                            {

                                ID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                                Year = dataReader.GetIntegerValue(GroupRepository.YearColumnName),
                                Camps = dataReader.GetStringValue(GroupRepository.CampsColumnName),
                                RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                                AgentID = dataReader.GetIntegerValue(GroupRepository.AgentIDColumnName),
                                AgencyRef = dataReader.GetStringValue(GroupRepository.AgencyRefColumnName),
                                Country = dataReader.GetStringValue(GroupRepository.CountryColumnName),
                                ArrivalDate = dataReader.GetDateTimeValue(GroupRepository.ArrivalDateColumnName),
                                Terminal = dataReader.GetStringValue(GroupRepository.TerminalColumnName),
                                FlightNumber = dataReader.GetStringValue(GroupRepository.FlightNumberColumnName),
                                DestinationFrom = dataReader.GetStringValue(GroupRepository.DestinationFromColumnName),
                                ArrivalTime = dataReader.GetStringValue(GroupRepository.ArrivalTimeColumnName),
                                DepartureDate = dataReader.GetDateTimeValue(GroupRepository.DepartureDateColumnName),
                                DepartureTerminal = dataReader.GetStringValue(GroupRepository.DepartureTerminalColumnName),
                                DepartureFlightNumber = dataReader.GetStringValue(GroupRepository.DepartureFlightNumberColumnName),
                                DestinationTo = dataReader.GetStringValue(GroupRepository.DestinationToColumnName),
                                FlightDepartureTime = dataReader.GetStringValue(GroupRepository.FlightDepartureTimeColumnName),
                            };
                            result.Data.Add(groupVM);
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
    }
}
