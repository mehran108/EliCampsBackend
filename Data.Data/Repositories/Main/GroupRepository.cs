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
        private const string ActivateStoredProcedureName = "ActivateGroup";
        private const string DeleteStoredProcedureName = "DeleteGroup";
        private const string UpdateGroupPaymentStoredProcedureName = "UpdateGroupPayment";
        private const string GroupProgrameStoredProcedureName = "GroupPrograme";
        private const string GroupTripsStoredProcedureName = "GroupTrips";
        private const string GroupPaymentStoredProcedureName = "GroupPayment";

        private const string AddPaymentGroupStoredProcedureName = "AddPaymentGroup";
        private const string UpdatePaymentGroupStoredProcedureName = "UpdatePaymentGroup";
        private const string GetPaymentGroupStoredProcedureName = "GetPaymentGroup";
        private const string GetAllPaymentGroupByGroupIDStoredProcedureName = "GetAllPaymentGroupByGroupID";
        private const string ActivatePaymentGroupStoredProcedureName = "ActivatePaymentGroup";

        private const string AddPaymentGroupLeaderStoredProcedureName = "AddPaymentGroupLeader";
        private const string UpdatePaymentGroupLeaderStoredProcedureName = "UpdatePaymentGroupLeader";
        private const string GetPaymentGroupLeaderStoredProcedureName = "GetPaymentGroupLeader";
        private const string GetAllPaymentGroupLeaderByGroupIDStoredProcedureName = "GetAllPaymentGroupLeaderByGroupID";
        private const string ActivatePaymentGroupLeaderStoredProcedureName = "ActivatePaymentGroupLeader";


        private const string GroupIdParameterName = "PGroupID";
        private const string IsInvoiceParameterName = "PIsInvoice";
        private const string YearParameterName = "PYear";
        private const string CampsParameterName = "PCamps";
        private const string RefNumberParameterName = "PRefNumber";
        private const string AgentIDParameterName = "PAgentID";
        private const string AgencyRefParameterName = "PAgencyRef";
        private const string CountryParameterName = "PCountry";
        private const string InvoiceTypeParameterName = "PInvoiceType";
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
        private const string ProgrameStartDateParameterName = "PProgrameStartDate";
        private const string ProgrameEndDateParameterName = "PProgrameEndDate";
        private const string CampusParameterName = "PCampus";
        private const string FormatParameterName = "PFormat";
        private const string MealPlanParameterName = "PMealPlan";
        private const string AddinsIDParameterName = "PAddinsID";
        private const string GroupTripsIDParameterName = "PGroupTripsID";
        private const string ChapFamilyParameterName = "PChapFamily";
        private const string ProgramIDParameterName = "PProgramID";
        private const string SubProgramIDParameterName = "PSubProgramID";
        private const string ApplyToAllStudentParameterName = "PApplyToAllStudent";

        private const string NumberOfNightsParameterName = "PNumberOfNights";
        private const string TotalGrossPriceParameterName = "PTotalGrossPrice";
        private const string PaidParameterName = "PPaid";
        private const string CommisionParameterName = "PCommision";
        private const string NetPriceParameterName = "PNetPrice";
        private const string BalanceParameterName = "PBalance";
        private const string NumOfStudentsParameterName = "PNumOfStudents";
        private const string NumOfGrpLeadersParameterName = "PNumOfGrpLeaders";
        private const string PerStudentParameterName = "PPerStudent";
        private const string PerGrpLeaderParameterName = "PPerGrpLeader";


        private const string PaymentGroupIDParameterName = "PPaymentGroupID";
        private const string PaymentGroupDateParameterName = "PPaymentGroupDate";
        private const string PaymentGroupAmountParameterName = "PPaymentGroupAmount";
        private const string PaymentGroupRemarksParameterName = "PPaymentGroupRemarks";


        private const string GroupIdColumnName = "GroupID";
        private const string YearColumnName = "Year";
        private const string CampsColumnName = "Camps";
        private const string RefNumberColumnName = "RefNumber";
        private const string AgentIDColumnName = "AgentID";
        private const string AgencyRefColumnName = "AgencyRef";
        private const string CountryColumnName = "Country";
        private const string InvoiceTypeColumnName = "InvoiceType";
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
        private const string ApplyToAllStudentColumnName = "ApplyToAllStudent";
        private const string AddinsIDColumnName = "AddinsID";
        private const string LinkIDColumnName = "LinkID";
        private const string LinkTypeIDColumnName = "LinkTypeID";
        private const string GroupTripIDColumnName = "GroupTripID";
        private const string ProgrameStartDateColumnName = "ProgrameStartDate";
        private const string ProgrameEndDateColumnName = "ProgrameEndDate";
        private const string CampusColumnName = "Campus";
        private const string FormatColumnName = "Format";
        private const string MealPlanColumnName = "MealPlan";
        private const string NumberOfNightsColumnName = "NumberOfNights";
        private const string TotalGrossPriceColumnName = "TotalGrossPrice";
        private const string PaidColumnName = "Paid";
        private const string CommisionColumnName = "Commision";
        private const string NetPriceColumnName = "NetPrice";
        private const string BalanceColumnName = "Balance";
        private const string NumOfStudentsColumnName = "NumOfStudents";
        private const string NumOfGrpLeadersColumnName = "NumOfGrpLeaders";
        private const string PerStudentColumnName = "PerStudent";
        private const string PerGrpLeaderColumnName = "PerGrpLeader";


        private const string PaymentGroupIDColumnName = "PaymentGroupID";
        private const string PaymentGroupDateColumnName = "PaymentGroupDate";
        private const string PaymentGroupAmountColumnName = "PaymentGroupAmount";
        private const string PaymentGroupRemarksColumnName = "PaymentGroupRemarks";

        private const string AgentNameColumnName = "AgentName";
        private const string CampusNameColumnName = "CampusName";
        private const string FormatNameRemarksColumnName = "FormatName";

        private const string ChapFamilyColumnName = "ChapFamily";
        private const string ProgramIDColumnName = "ProgramID";
        private const string SubProgramIDColumnName = "SubProgramID";
        private const string TotalStudentsColumnName = "TotalStudents";
        private const string TotalAddinsColumnName = "TotalAddins";
        private const string CommissionAddinsColumnName = "CommissionAddins";


        public async Task<int> AddGroupAsync(GroupViewModel group)
        {
            var groupIdParamter = base.GetParameterOut(GroupRepository.GroupIdParameterName, SqlDbType.Int, group.ID);
            var parameters = new List<DbParameter>
                {
                    groupIdParamter,

                    base.GetParameter(GroupRepository.YearParameterName, group.Year),
                    base.GetParameter(GroupRepository.CampsParameterName, group.Camps),
                    //base.GetParameter(GroupRepository.RefNumberParameterName, group.RefNumber),
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
                    base.GetParameter(GroupRepository.FlightDepartureTimeParameterName, group.FlightDepartureTime),
                    base.GetParameter(GroupRepository.InvoiceTypeParameterName, group.InvoiceType),
                    base.GetParameter(GroupRepository.ApplyToAllStudentParameterName, group.ApplyToAllStudent)

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
                    base.GetParameter(GroupRepository.FlightDepartureTimeParameterName, group.FlightDepartureTime),
                    base.GetParameter(BaseRepository.ActiveParameterName, group.Active),
                    base.GetParameter(GroupRepository.InvoiceTypeParameterName, group.InvoiceType),
                    base.GetParameter(GroupRepository.ApplyToAllStudentParameterName, group.ApplyToAllStudent)


                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<GroupViewModel> GetGroupAsync(int groupID, bool IsInvoice)
        {
            GroupViewModel groupVM = null;
            int AddinID;
            int TripID;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, groupID),
                    base.GetParameter(GroupRepository.IsInvoiceParameterName, IsInvoice)
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
                            AgentID = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.AgentIDColumnName),
                            AgencyRef = dataReader.GetStringValue(GroupRepository.AgencyRefColumnName),
                            Country = dataReader.GetStringValue(GroupRepository.CountryColumnName),
                            InvoiceType = dataReader.GetStringValue(GroupRepository.InvoiceTypeColumnName),
                            ArrivalDate = dataReader.GetDateTimeValueNullable(GroupRepository.ArrivalDateColumnName),
                            Terminal = dataReader.GetStringValue(GroupRepository.TerminalColumnName),
                            FlightNumber = dataReader.GetStringValue(GroupRepository.FlightNumberColumnName),
                            DestinationFrom = dataReader.GetStringValue(GroupRepository.DestinationFromColumnName),
                            ArrivalTime = dataReader.GetStringValue(GroupRepository.ArrivalTimeColumnName),
                            DepartureDate = dataReader.GetDateTimeValueNullable(GroupRepository.DepartureDateColumnName),
                            DepartureTerminal = dataReader.GetStringValue(GroupRepository.DepartureTerminalColumnName),
                            DepartureFlightNumber = dataReader.GetStringValue(GroupRepository.DepartureFlightNumberColumnName),
                            DestinationTo = dataReader.GetStringValue(GroupRepository.DestinationToColumnName),
                            FlightDepartureTime = dataReader.GetStringValue(GroupRepository.FlightDepartureTimeColumnName),
                            ProgrameStartDate = dataReader.GetDateTimeValueNullable(GroupRepository.ProgrameStartDateColumnName),
                            ProgrameEndDate = dataReader.GetDateTimeValueNullable(GroupRepository.ProgrameEndDateColumnName),
                            Campus = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.CampusColumnName),
                            Format = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.FormatColumnName),
                            MealPlan = dataReader.GetStringValue(GroupRepository.MealPlanColumnName),
                            NumberOfNights = dataReader.GetIntegerValue(GroupRepository.NumberOfNightsColumnName),
                            TotalGrossPrice = dataReader.GetDecimalValue(GroupRepository.TotalGrossPriceColumnName),
                            Paid = dataReader.GetDecimalValue(GroupRepository.PaidColumnName),
                            Commision = dataReader.GetDecimalValue(GroupRepository.CommisionColumnName),
                            NetPrice = dataReader.GetDecimalValue(GroupRepository.NetPriceColumnName),
                            Balance = dataReader.GetDecimalValue(GroupRepository.BalanceColumnName),
                            NumOfStudents = dataReader.GetIntegerValue(GroupRepository.NumOfStudentsColumnName),
                            NumOfGrpLeaders = dataReader.GetIntegerValue(GroupRepository.NumOfGrpLeadersColumnName),
                            PerStudent = dataReader.GetDecimalValue(GroupRepository.PerStudentColumnName),
                            PerGrpLeader = dataReader.GetDecimalValue(GroupRepository.PerGrpLeaderColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            ApplyToAllStudent = dataReader.GetBooleanValue(GroupRepository.ApplyToAllStudentColumnName),
                            ChapFamily = dataReader.GetStringValue(GroupRepository.ChapFamilyColumnName),
                            ProgramId = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.ProgramIDColumnName),
                            SubProgramId = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.SubProgramIDColumnName),
                            ProgrameAddins = new List<int>(),
                            GroupTrips = new List<int>()

                        };
                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {

                                if (dataReader.GetStringValue(GroupRepository.LinkTypeIDColumnName).Equals("AddinsID"))
                                {
                                    AddinID = dataReader.GetIntegerValue(GroupRepository.LinkIDColumnName);
                                    groupVM?.ProgrameAddins.Add(AddinID);
                                }
                                else if (dataReader.GetStringValue(GroupRepository.LinkTypeIDColumnName).Equals("GroupTripID"))
                                {
                                    TripID = dataReader.GetIntegerValue(GroupRepository.LinkIDColumnName);
                                    groupVM?.GroupTrips.Add(TripID);
                                }


                            }
                            if (dataReader.NextResult())
                            {
                                
                                if (dataReader.Read())
                                {
                                    groupVM.StudentsAgainstGroup = new StudentsAgainstGroup
                                    {
                                        TotalStudents = dataReader.GetIntegerValue(GroupRepository.TotalStudentsColumnName),
                                        TotalGrossPrice = dataReader.GetDoubleValue(GroupRepository.TotalGrossPriceColumnName),
                                        Paid = dataReader.GetDoubleValue(GroupRepository.PaidColumnName),
                                        NetPrice = dataReader.GetDoubleValue(GroupRepository.NetPriceColumnName),
                                        Commision = dataReader.GetDoubleValue(GroupRepository.CommisionColumnName),
                                        TotalAddins = dataReader.GetDoubleValue(GroupRepository.TotalAddinsColumnName),
                                        CommissionAddins = dataReader.GetDoubleValue(GroupRepository.CommissionAddinsColumnName),
                                    };

                                }

                            }

                        }
                        



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
                base.GetParameter(GroupRepository.YearParameterName, groups.Data.Year),
                base.GetParameter(BaseRepository.ActiveParameterName, groups.Data.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        groupVM = new GroupViewModel
                        {

                            ID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            Year = dataReader.GetIntegerValue(GroupRepository.YearColumnName),
                            Camps = dataReader.GetStringValue(GroupRepository.CampsColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            AgentID = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.AgentIDColumnName),
                            AgencyRef = dataReader.GetStringValue(GroupRepository.AgencyRefColumnName),
                            Country = dataReader.GetStringValue(GroupRepository.CountryColumnName),
                            InvoiceType = dataReader.GetStringValue(GroupRepository.InvoiceTypeColumnName),
                            ArrivalDate = dataReader.GetDateTimeValueNullable(GroupRepository.ArrivalDateColumnName),
                            Terminal = dataReader.GetStringValue(GroupRepository.TerminalColumnName),
                            FlightNumber = dataReader.GetStringValue(GroupRepository.FlightNumberColumnName),
                            DestinationFrom = dataReader.GetStringValue(GroupRepository.DestinationFromColumnName),
                            ArrivalTime = dataReader.GetStringValue(GroupRepository.ArrivalTimeColumnName),
                            DepartureDate = dataReader.GetDateTimeValueNullable(GroupRepository.DepartureDateColumnName),
                            DepartureTerminal = dataReader.GetStringValue(GroupRepository.DepartureTerminalColumnName),
                            DepartureFlightNumber = dataReader.GetStringValue(GroupRepository.DepartureFlightNumberColumnName),
                            DestinationTo = dataReader.GetStringValue(GroupRepository.DestinationToColumnName),
                            FlightDepartureTime = dataReader.GetStringValue(GroupRepository.FlightDepartureTimeColumnName),
                            ProgrameStartDate = dataReader.GetDateTimeValueNullable(GroupRepository.ProgrameStartDateColumnName),
                            ProgrameEndDate = dataReader.GetDateTimeValueNullable(GroupRepository.ProgrameEndDateColumnName),
                            Campus = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.CampusColumnName),
                            Format = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.FormatColumnName),
                            MealPlan = dataReader.GetStringValue(GroupRepository.MealPlanColumnName),
                            NumberOfNights = dataReader.GetIntegerValue(GroupRepository.NumberOfNightsColumnName),
                            TotalGrossPrice = dataReader.GetDecimalValue(GroupRepository.TotalGrossPriceColumnName),
                            Paid = dataReader.GetDecimalValue(GroupRepository.PaidColumnName),
                            Commision = dataReader.GetDecimalValue(GroupRepository.CommisionColumnName),
                            NetPrice = dataReader.GetDecimalValue(GroupRepository.NetPriceColumnName),
                            Balance = dataReader.GetDecimalValue(GroupRepository.BalanceColumnName),
                            NumOfStudents = dataReader.GetIntegerValue(GroupRepository.NumOfStudentsColumnName),
                            NumOfGrpLeaders = dataReader.GetIntegerValue(GroupRepository.NumOfGrpLeadersColumnName),
                            PerStudent = dataReader.GetDecimalValue(GroupRepository.PerStudentColumnName),
                            PerGrpLeader = dataReader.GetDecimalValue(GroupRepository.PerGrpLeaderColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            ApplyToAllStudent = dataReader.GetBooleanValue(GroupRepository.ApplyToAllStudentColumnName),
                            AgentName = dataReader.GetStringValue(GroupRepository.AgentNameColumnName),
                            CampusName = dataReader.GetStringValue(GroupRepository.CampusNameColumnName),
                            FormatName = dataReader.GetStringValue(GroupRepository.FormatNameRemarksColumnName),
                            ChapFamily = dataReader.GetStringValue(GroupRepository.ChapFamilyColumnName),
                            ProgramId = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.ProgramIDColumnName),
                            SubProgramId = dataReader.GetUnsignedIntegerValueNullable(GroupRepository.SubProgramIDColumnName)

                        };
                        result.Data.Add(groupVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return result;
        }

        public async Task<bool> ActivateGroup(GroupViewModel group)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, group.Active)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }
        public async Task<bool> DeleteGroup(GroupViewModel group)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(BaseRepository.DeleteParameterName, group.IsDelete)
                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.DeleteStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> GroupPrograme(GroupViewModel group)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(GroupRepository.ProgrameStartDateParameterName, group.ProgrameStartDate),
                    base.GetParameter(GroupRepository.ProgrameEndDateParameterName, group.ProgrameEndDate),
                    base.GetParameter(GroupRepository.CampusParameterName, group.Campus),
                    base.GetParameter(GroupRepository.FormatParameterName, group.Format),
                    base.GetParameter(GroupRepository.MealPlanParameterName, group.MealPlan),
                    base.GetParameter(GroupRepository.AddinsIDParameterName, group.AddinsID),
                    base.GetParameter(GroupRepository.RefNumberParameterName, group.RefNumber),
                    base.GetParameter(GroupRepository.ChapFamilyParameterName, group.ChapFamily),
                    base.GetParameter(GroupRepository.ProgramIDParameterName, group.ProgramId),
                    base.GetParameter(GroupRepository.SubProgramIDParameterName, group.SubProgramId)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.GroupProgrameStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> GroupPayment(GroupViewModel group)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(GroupRepository.NumberOfNightsParameterName, group.NumberOfNights),
                    base.GetParameter(GroupRepository.TotalGrossPriceParameterName, group.TotalGrossPrice),
                    base.GetParameter(GroupRepository.PaidParameterName, group.Paid),
                    base.GetParameter(GroupRepository.CommisionParameterName, group.Commision),
                    base.GetParameter(GroupRepository.NetPriceParameterName, group.NetPrice),
                    base.GetParameter(GroupRepository.BalanceParameterName, group.Balance),
                    base.GetParameter(GroupRepository.NumOfStudentsParameterName, group.NumOfStudents),
                    base.GetParameter(GroupRepository.NumOfGrpLeadersParameterName, group.NumOfGrpLeaders),
                    base.GetParameter(GroupRepository.PerStudentParameterName, group.PerStudent),
                    base.GetParameter(GroupRepository.PerGrpLeaderParameterName, group.PerGrpLeader),

                };
            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.GroupPaymentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> GroupTrips(GroupViewModel group)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, group.ID),
                    base.GetParameter(GroupRepository.GroupTripsIDParameterName, group.GroupTripsID),
                    base.GetParameter(GroupRepository.RefNumberParameterName, group.RefNumber)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.GroupTripsStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }


        #region PaymentsGroups
        public async Task<int> AddPaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var paymentGroupIDParamter = base.GetParameterOut(GroupRepository.PaymentGroupIDParameterName, SqlDbType.Int, paymentGroup.ID);
            var parameters = new List<DbParameter>
                {
                    paymentGroupIDParamter,

                    base.GetParameter(GroupRepository.RefNumberParameterName, paymentGroup.RefNumber),
                    base.GetParameter(GroupRepository.GroupIdParameterName, paymentGroup.GroupID),
                    base.GetParameter(GroupRepository.PaymentGroupDateParameterName, paymentGroup.Date),
                    base.GetParameter(GroupRepository.PaymentGroupAmountParameterName, paymentGroup.Amount),
                    base.GetParameter(GroupRepository.PaymentGroupRemarksParameterName, paymentGroup.Remarks)

                };

            await base.ExecuteNonQuery(parameters, GroupRepository.AddPaymentGroupStoredProcedureName, CommandType.StoredProcedure);

            paymentGroup.ID = Convert.ToInt32(paymentGroupIDParamter.Value);

            return paymentGroup.ID;
        }

        public async Task<bool> UpdatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroup.ID),
                    base.GetParameter(GroupRepository.RefNumberParameterName, paymentGroup.RefNumber),
                    base.GetParameter(GroupRepository.GroupIdParameterName, paymentGroup.GroupID),
                    base.GetParameter(GroupRepository.PaymentGroupDateParameterName, paymentGroup.Date),
                    base.GetParameter(GroupRepository.PaymentGroupAmountParameterName, paymentGroup.Amount),
                    base.GetParameter(GroupRepository.PaymentGroupRemarksParameterName, paymentGroup.Remarks),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentGroup.Active)


                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.UpdatePaymentGroupStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<PaymentsGroupsViewModel> GetPaymentGroupAsync(int paymentGroupID)
        {
            PaymentsGroupsViewModel paymentGroupVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroupID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetPaymentGroupStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        paymentGroupVM = new PaymentsGroupsViewModel
                        {
                            ID = dataReader.GetIntegerValue(GroupRepository.PaymentGroupIDColumnName),
                            GroupID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            Date = dataReader.GetDateTimeValue(GroupRepository.PaymentGroupDateColumnName),
                            Amount = dataReader.GetDecimalValue(GroupRepository.PaymentGroupAmountColumnName),
                            Remarks = dataReader.GetStringValue(GroupRepository.PaymentGroupRemarksColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };

                    }
                }
            }

            return paymentGroupVM;
        }

        public async Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupByGroupIdAsync(int groupID)
        {
            PaymentsGroupsViewModel paymentGroupVM = null;
            List<PaymentsGroupsViewModel> paymentGroupVMList = new List<PaymentsGroupsViewModel>();
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, groupID)
                };


            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetAllPaymentGroupByGroupIDStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        paymentGroupVM = new PaymentsGroupsViewModel
                        {
                            ID = dataReader.GetIntegerValue(GroupRepository.PaymentGroupIDColumnName),
                            GroupID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            Date = dataReader.GetDateTimeValue(GroupRepository.PaymentGroupDateColumnName),
                            Amount = dataReader.GetDecimalValue(GroupRepository.PaymentGroupAmountColumnName),
                            Remarks = dataReader.GetStringValue(GroupRepository.PaymentGroupRemarksColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                        paymentGroupVMList.Add(paymentGroupVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return paymentGroupVMList;
        }

        public async Task<bool> ActivatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroup.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentGroup.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.ActivatePaymentGroupStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        #endregion

        #region PaymentsGroupsLeader
        public async Task<int> AddPaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var paymentGroupIDParamter = base.GetParameterOut(GroupRepository.PaymentGroupIDParameterName, SqlDbType.Int, paymentGroup.ID);
            var parameters = new List<DbParameter>
                {
                    paymentGroupIDParamter,

                    base.GetParameter(GroupRepository.RefNumberParameterName, paymentGroup.RefNumber),
                    base.GetParameter(GroupRepository.GroupIdParameterName, paymentGroup.GroupID),
                    base.GetParameter(GroupRepository.PaymentGroupAmountParameterName, paymentGroup.Amount)

                };

            await base.ExecuteNonQuery(parameters, GroupRepository.AddPaymentGroupLeaderStoredProcedureName, CommandType.StoredProcedure);

            paymentGroup.ID = Convert.ToInt32(paymentGroupIDParamter.Value);

            return paymentGroup.ID;
        }

        public async Task<bool> UpdatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroup.ID),
                    base.GetParameter(GroupRepository.RefNumberParameterName, paymentGroup.RefNumber),
                    base.GetParameter(GroupRepository.GroupIdParameterName, paymentGroup.GroupID),
                    base.GetParameter(GroupRepository.PaymentGroupAmountParameterName, paymentGroup.Amount),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentGroup.Active)


                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.UpdatePaymentGroupLeaderStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<PaymentsGroupsViewModel> GetPaymentGroupLeaderAsync(int paymentGroupID)
        {
            PaymentsGroupsViewModel paymentGroupVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroupID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetPaymentGroupLeaderStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        paymentGroupVM = new PaymentsGroupsViewModel
                        {
                            ID = dataReader.GetIntegerValue(GroupRepository.PaymentGroupIDColumnName),
                            GroupID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            Amount = dataReader.GetDecimalValue(GroupRepository.PaymentGroupAmountColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };

                    }
                }
            }

            return paymentGroupVM;
        }

        public async Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupLeaderByGroupIdAsync(int groupID)
        {
            PaymentsGroupsViewModel paymentGroupVM = null;
            List<PaymentsGroupsViewModel> paymentGroupVMList = new List<PaymentsGroupsViewModel>();
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.GroupIdParameterName, groupID)
                };


            using (var dataReader = await base.ExecuteReader(parameters, GroupRepository.GetAllPaymentGroupLeaderByGroupIDStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        paymentGroupVM = new PaymentsGroupsViewModel
                        {
                            ID = dataReader.GetIntegerValue(GroupRepository.PaymentGroupIDColumnName),
                            GroupID = dataReader.GetIntegerValue(GroupRepository.GroupIdColumnName),
                            RefNumber = dataReader.GetStringValue(GroupRepository.RefNumberColumnName),
                            Amount = dataReader.GetDecimalValue(GroupRepository.PaymentGroupAmountColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName)
                        };
                        paymentGroupVMList.Add(paymentGroupVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return paymentGroupVMList;
        }

        public async Task<bool> ActivatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(GroupRepository.PaymentGroupIDParameterName, paymentGroup.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentGroup.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, GroupRepository.ActivatePaymentGroupLeaderStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        #endregion


    }
}
