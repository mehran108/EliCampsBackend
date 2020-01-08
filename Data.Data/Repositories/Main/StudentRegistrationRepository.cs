using ELI.Data.Repositories.Main.Extensions;
using ELI.Domain.Contracts.Main;
using ELI.Domain.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class StudentRegistrationRepository : BaseRepository, IStudentRegistrationRepository
    {
        public StudentRegistrationRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public void Dispose()
        {
        }


        private const string AddStoredProcedureName = "AddStudent";
        private const string GetStoredProcedureName = "GetStudent";
        private const string GetStudentPDFDataStoredProcedureName = "GetStudentPDFData";
        private const string GetAllStoredProcedureName = "GetAllStudent";
        private const string UpdateStoredProcedureName = "UpdateStudent";
        private const string ActivateStoredProcedureName = "ActivateStudent";


        private const string AddPaymentStudentStoredProcedureName = "AddPaymentStudent";
        private const string UpdatePaymentStudentStoredProcedureName = "UpdatePaymentStudent";
        private const string GetPaymentStudentStoredProcedureName = "GetPaymentStudent";
        private const string GetAllPaymentStudentByStudentIDStoredProcedureName = "GetAllPaymentStudentByStudentID";
        private const string ActivatePaymentStudentStoredProcedureName = "ActivatePaymentStudent";
        private const string AddDocumentStoredProcedureName = "AddDocuments";

        private const string IDParameterName = "PID";
        private const string YearParameterName = "PYear";
        private const string Reg_RefParameterName = "PReg_Ref";
        private const string GroupRefParameterName = "PGroupRef";
        private const string CampsParameterName = "PCamps";
        private const string GenderParameterName = "PGender";
        private const string FirstNameParameterName = "PFirstName";
        private const string LastNameParameterName = "PLastName";
        private const string HomeAddressParameterName = "PHomeAddress";
        private const string CityParameterName = "PCity";
        private const string StateParameterName = "PState";
        private const string CountryParameterName = "PCountry";
        private const string PostCodeParameterName = "PPostCode";
        private const string EmergencyContactParameterName = "PEmergencyContact";
        private const string EmailParameterName = "PEmail";
        private const string PhoneParameterName = "PPhone";
        private const string DOBParameterName = "PDOB";
        private const string AgeParameterName = "PAge";
        private const string PassportNumberParameterName = "PPassportNumber";
        private const string AgencyIDParameterName = "PAgencyID";
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
        private const string MedicalInformationParameterName = "PMedicalInformation";
        private const string DietaryNeedsParameterName = "PDietaryNeeds";
        private const string AllergiesParameterName = "PAllergies";
        private const string MedicalNotesParameterName = "PMedicalNotes";
        private const string ProgrameStartDateParameterName = "PProgrameStartDate";
        private const string ProgrameEndDateParameterName = "PProgrameEndDate";
        private const string CampusParameterName = "PCampus";
        private const string FormatParameterName = "PFormat";
        private const string MealPlanParameterName = "PMealPlan";
        private const string AddinsIDParameterName = "PAddinsID";
        private const string ExtraNotesParameterName = "PExtraNotes";
        private const string ExtraNotesHTMLParameterName = "PExtraNotesHTML";
        private const string StatusParameterName = "PStatus";
        private const string GroupIDParameterName = "PGroupID";

        private const string HomestayOrResiParameterName = "PHomestayOrResi";
        private const string HomestayIDParameterName = "PHomestayID";
        private const string RoomIDParameterName = "PRoomID";
        private const string RoomSearchCampusParameterName = "PRoomSearchCampus";
        private const string RoomSearchFromParameterName = "PRoomSearchFrom";
        private const string RoomSearchToParameterName = "PRoomSearchTo";
        private const string NumberOfNightsParameterName = "PNumberOfNights";
        private const string TotalGrossPriceParameterName = "PTotalGrossPrice";
        private const string TotalAddinsParameterName = "PTotalAddins";
        private const string PaidParameterName = "PPaid";
        private const string CommisionParameterName = "PCommision";
        private const string CommissionAddinsParameterName = "PCommissionAddins";
        private const string NetPriceParameterName = "PNetPrice";
        private const string BalanceParameterName = "PBalance";
        private const string StudentTripsIDParameterName = "PStudentTripsID";
        private const string StudentDocumentIdParameterName = "PStudentDocumentId";

        private const string PaymentStudentIDParameterName = "PPaymentStudentID";
        private const string PaymentStudentDateParameterName = "PPaymentStudentDate";
        private const string PaymentStudentAmountParameterName = "PPaymentStudentAmount";
        private const string PaymentStudentRemarksParameterName = "PPaymentStudentRemarks";

        private const string ChapFamilyParameterName = "PChapFamily";
        private const string ProgramIDParameterName = "PProgramID";
        private const string SubProgramIDParameterName = "PSubProgramID";




        private const string IDColumnName = "ID";
        private const string YearColumnName = "Year";
        private const string Reg_RefColumnName = "Reg_Ref";
        private const string GroupRefColumnName = "GroupRef";
        private const string CampsColumnName = "Camps";
        private const string GenderColumnName = "Gender";
        private const string FirstNameColumnName = "FirstName";
        private const string LastNameColumnName = "LastName";
        private const string CampusNameColumnName = "CampusName";
        private const string HomeAddressColumnName = "HomeAddress";
        private const string CityColumnName = "City";
        private const string StateColumnName = "State";
        private const string CountryColumnName = "Country";
        private const string PostCodeColumnName = "PostCode";
        private const string EmergencyContactColumnName = "EmergencyContact";
        private const string EmailColumnName = "Email";
        private const string PhoneColumnName = "Phone";
        private const string DOBColumnName = "DOB";
        private const string AgeColumnName = "Age";
        private const string PassportNumberColumnName = "PassportNumber";
        private const string AgencyIDColumnName = "AgencyID";
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
        private const string MedicalInformationColumnName = "MedicalInformation";
        private const string DietaryNeedsColumnName = "DietaryNeeds";
        private const string AllergiesColumnName = "Allergies";
        private const string MedicalNotesColumnName = "MedicalNotes";
        private const string ProgrameStartDateColumnName = "ProgrameStartDate";
        private const string ProgrameEndDateColumnName = "ProgrameEndDate";
        private const string CampusColumnName = "Campus";
        private const string FormatColumnName = "Format";
        private const string MealPlanColumnName = "MealPlan";
        private const string AddinsIDColumnName = "AddinsID";
        private const string ExtraNotesColumnName = "ExtraNotes";
        private const string ExtraNotesHTMLColumnName = "ExtraNotesHTML";
        private const string AgencyRefColumnName = "AgencyRef";

        private const string StatusColumnName = "Status";

        private const string HomestayOrResiColumnName = "HomestayOrResi";
        private const string HomestayIDColumnName = "HomestayID";
        private const string RoomIDColumnName = "RoomID";
        private const string RoomSearchCampusColumnName = "RoomSearchCampus";
        private const string RoomSearchFromColumnName = "RoomSearchFrom";
        private const string RoomSearchToColumnName = "RoomSearchTo";
        private const string NumberOfNightsColumnName = "NumberOfNights";
        private const string TotalGrossPriceColumnName = "TotalGrossPrice";
        private const string TotalAddinsColumnName = "TotalAddins";
        private const string PaidColumnName = "Paid";
        private const string CommisionColumnName = "Commision";
        private const string CommissionAddinsColumnName = "CommissionAddins";
        private const string NetPriceColumnName = "NetPrice";
        private const string BalanceColumnName = "Balance";
        private const string StudentTripsIDColumnName = "StudentTripsID";

        private const string LinkTypeIDColumnName = "LinkTypeID";
        private const string LinkIDColumnName = "LinkID";
        private const string AgentNameColumnName = "AgentName";
        private const string FormatNameColumnName = "FormatName";

        private const string AddinNameColumnName = "AddinName";
        private const string AddinsTypeColumnName = "AddinsType";


        private const string PaymentStudentIDColumnName = "PaymentStudentID";
        private const string PaymentStudentDateColumnName = "PaymentStudentDate";
        private const string PaymentStudentAmountColumnName = "PaymentStudentAmount";
        private const string PaymentStudentRemarksColumnName = "PaymentStudentRemarks";

        private const string ChapFamilyColumnName = "ChapFamily";
        private const string ProgramIDColumnName = "ProgramID";
        private const string SubProgramIDColumnName = "SubProgramID";
        private const string DocumentPathColumnName = "documentPath";
        private const string DocumentIdParameterName = "PDocumentId";
        private const string DocumentNameParameterName = "PDocumentName";
        private const string DocumentPathParameterName = "PDocumentPath";
        private const string RegistrantionIdParameterName = "PRegistrantionId";

        private const string HomestayNameColumnName = "HomestayName";
        private const string RoomNameColumnName = "RoomName";
        private const string ProgramNameColumnName = "ProgramName";
        private const string SubProgramNameColumnName = "SubProgramName";
        private const string GroupIDColumnName = "GroupID";
        private const string AgentAddressColumnName = "AgentAddress";
        private const string AgentCountryColumnName = "AgentCountry";
        private const string CampusAddressOnReportsColumnName = "CampusAddressOnReports";




        public async Task<int> AddStudentAsync(StudentRegistration student)
        {
            var studentIdParamter = base.GetParameterOut(StudentRegistrationRepository.IDParameterName, SqlDbType.Int, student.ID);
            var parameters = new List<DbParameter>
                {
                    studentIdParamter,
                    base.GetParameter(StudentRegistrationRepository.YearParameterName, student.Year),
                    base.GetParameter(StudentRegistrationRepository.GroupRefParameterName, student.GroupRef),
                    base.GetParameter(StudentRegistrationRepository.CampsParameterName, student.Camps),
                    base.GetParameter(StudentRegistrationRepository.GenderParameterName, student.Gender),
                    base.GetParameter(StudentRegistrationRepository.FirstNameParameterName, student.FirstName),
                    base.GetParameter(StudentRegistrationRepository.LastNameParameterName, student.LastName),
                    base.GetParameter(StudentRegistrationRepository.HomeAddressParameterName, student.HomeAddress),
                    base.GetParameter(StudentRegistrationRepository.CityParameterName, student.City),
                    base.GetParameter(StudentRegistrationRepository.StateParameterName, student.State),
                    base.GetParameter(StudentRegistrationRepository.CountryParameterName, student.Country),
                    base.GetParameter(StudentRegistrationRepository.PostCodeParameterName, student.PostCode),
                    base.GetParameter(StudentRegistrationRepository.EmergencyContactParameterName, student.EmergencyContact),
                    base.GetParameter(StudentRegistrationRepository.EmailParameterName, student.Email),
                    base.GetParameter(StudentRegistrationRepository.PhoneParameterName, student.Phone),
                    base.GetParameter(StudentRegistrationRepository.DOBParameterName, student.DOB),
                    base.GetParameter(StudentRegistrationRepository.AgeParameterName, student.Age),
                    base.GetParameter(StudentRegistrationRepository.PassportNumberParameterName, student.PassportNumber),
                    base.GetParameter(StudentRegistrationRepository.AgencyIDParameterName, student.AgencyID),
                    base.GetParameter(StudentRegistrationRepository.ArrivalDateParameterName, student.ArrivalDate),
                    base.GetParameter(StudentRegistrationRepository.TerminalParameterName, student.Terminal),
                    base.GetParameter(StudentRegistrationRepository.FlightNumberParameterName, student.FlightNumber),
                    base.GetParameter(StudentRegistrationRepository.DestinationFromParameterName, student.DestinationFrom),
                    base.GetParameter(StudentRegistrationRepository.ArrivalTimeParameterName, student.ArrivalTime),
                    base.GetParameter(StudentRegistrationRepository.DepartureDateParameterName, student.DepartureDate),
                    base.GetParameter(StudentRegistrationRepository.DepartureTerminalParameterName, student.DepartureTerminal),
                    base.GetParameter(StudentRegistrationRepository.DepartureFlightNumberParameterName, student.DepartureFlightNumber),
                    base.GetParameter(StudentRegistrationRepository.DestinationToParameterName, student.DestinationTo),
                    base.GetParameter(StudentRegistrationRepository.FlightDepartureTimeParameterName, student.FlightDepartureTime),
                    base.GetParameter(StudentRegistrationRepository.MedicalInformationParameterName, student.MedicalInformation),
                    base.GetParameter(StudentRegistrationRepository.DietaryNeedsParameterName, student.DietaryNeeds),
                    base.GetParameter(StudentRegistrationRepository.AllergiesParameterName, student.Allergies),
                    base.GetParameter(StudentRegistrationRepository.MedicalNotesParameterName, student.MedicalNotes),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesParameterName, student.ExtraNotes),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesHTMLParameterName, student.ExtraNotesHTML),
                    base.GetParameter(StudentRegistrationRepository.StatusParameterName, student.Status),
                    base.GetParameter(StudentRegistrationRepository.ProgrameStartDateParameterName, student.ProgrameStartDate),
                    base.GetParameter(StudentRegistrationRepository.ProgrameEndDateParameterName, student.ProgrameEndDate),
                    base.GetParameter(StudentRegistrationRepository.CampusParameterName, student.Campus),
                    base.GetParameter(StudentRegistrationRepository.FormatParameterName, student.Format),
                    base.GetParameter(StudentRegistrationRepository.MealPlanParameterName, student.MealPlan),
                    base.GetParameter(StudentRegistrationRepository.AddinsIDParameterName, student.AddinsID),
                    base.GetParameter(StudentRegistrationRepository.GroupIDParameterName, student.GroupID)


    };

            await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.AddStoredProcedureName, CommandType.StoredProcedure);

            student.ID = Convert.ToInt32(studentIdParamter.Value);

            return student.ID;
        }


        public async Task<bool> UpdateStudentAsync(StudentRegistration student)
        {
            var parameters = new List<DbParameter>
                {

                    base.GetParameter(StudentRegistrationRepository.IDParameterName, student.ID),
                    base.GetParameter(StudentRegistrationRepository.YearParameterName, student.Year),
                    base.GetParameter(StudentRegistrationRepository.GroupRefParameterName, student.GroupRef),
                    base.GetParameter(StudentRegistrationRepository.CampsParameterName, student.Camps),
                    base.GetParameter(StudentRegistrationRepository.GenderParameterName, student.Gender),
                    base.GetParameter(StudentRegistrationRepository.FirstNameParameterName, student.FirstName),
                    base.GetParameter(StudentRegistrationRepository.LastNameParameterName, student.LastName),
                    base.GetParameter(StudentRegistrationRepository.HomeAddressParameterName, student.HomeAddress),
                    base.GetParameter(StudentRegistrationRepository.CityParameterName, student.City),
                    base.GetParameter(StudentRegistrationRepository.StateParameterName, student.State),
                    base.GetParameter(StudentRegistrationRepository.CountryParameterName, student.Country),
                    base.GetParameter(StudentRegistrationRepository.PostCodeParameterName, student.PostCode),
                    base.GetParameter(StudentRegistrationRepository.EmergencyContactParameterName, student.EmergencyContact),
                    base.GetParameter(StudentRegistrationRepository.EmailParameterName, student.Email),
                    base.GetParameter(StudentRegistrationRepository.PhoneParameterName, student.Phone),
                    base.GetParameter(StudentRegistrationRepository.DOBParameterName, student.DOB),
                    base.GetParameter(StudentRegistrationRepository.AgeParameterName, student.Age),
                    base.GetParameter(StudentRegistrationRepository.PassportNumberParameterName, student.PassportNumber),
                    base.GetParameter(StudentRegistrationRepository.AgencyIDParameterName, student.AgencyID),
                    base.GetParameter(StudentRegistrationRepository.ArrivalDateParameterName, student.ArrivalDate),
                    base.GetParameter(StudentRegistrationRepository.TerminalParameterName, student.Terminal),
                    base.GetParameter(StudentRegistrationRepository.FlightNumberParameterName, student.FlightNumber),
                    base.GetParameter(StudentRegistrationRepository.DestinationFromParameterName, student.DestinationFrom),
                    base.GetParameter(StudentRegistrationRepository.ArrivalTimeParameterName, student.ArrivalTime),
                    base.GetParameter(StudentRegistrationRepository.DepartureDateParameterName, student.DepartureDate),
                    base.GetParameter(StudentRegistrationRepository.DepartureTerminalParameterName, student.DepartureTerminal),
                    base.GetParameter(StudentRegistrationRepository.DepartureFlightNumberParameterName, student.DepartureFlightNumber),
                    base.GetParameter(StudentRegistrationRepository.DestinationToParameterName, student.DestinationTo),
                    base.GetParameter(StudentRegistrationRepository.FlightDepartureTimeParameterName, student.FlightDepartureTime),
                    base.GetParameter(StudentRegistrationRepository.MedicalInformationParameterName, student.MedicalInformation),
                    base.GetParameter(StudentRegistrationRepository.DietaryNeedsParameterName, student.DietaryNeeds),
                    base.GetParameter(StudentRegistrationRepository.AllergiesParameterName, student.Allergies),
                    base.GetParameter(StudentRegistrationRepository.MedicalNotesParameterName, student.MedicalNotes),
                    base.GetParameter(StudentRegistrationRepository.ProgrameStartDateParameterName, student.ProgrameStartDate),
                    base.GetParameter(StudentRegistrationRepository.ProgrameEndDateParameterName, student.ProgrameEndDate),
                    base.GetParameter(StudentRegistrationRepository.CampusParameterName, student.Campus),
                    base.GetParameter(StudentRegistrationRepository.FormatParameterName, student.Format),
                    base.GetParameter(StudentRegistrationRepository.MealPlanParameterName, student.MealPlan),
                    base.GetParameter(StudentRegistrationRepository.AddinsIDParameterName, student.AddinsID),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesParameterName, student.ExtraNotes),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesHTMLParameterName, student.ExtraNotesHTML),
                    base.GetParameter(StudentRegistrationRepository.StatusParameterName, student.Status),
                    base.GetParameter(StudentRegistrationRepository.HomestayOrResiParameterName, student.HomestayOrResi),
                    base.GetParameter(StudentRegistrationRepository.HomestayIDParameterName, student.HomestayID),
                    base.GetParameter(StudentRegistrationRepository.RoomIDParameterName, student.RoomID),
                    base.GetParameter(StudentRegistrationRepository.RoomSearchCampusParameterName, student.RoomSearchCampus),
                    base.GetParameter(StudentRegistrationRepository.RoomSearchFromParameterName, student.RoomSearchFrom),
                    base.GetParameter(StudentRegistrationRepository.RoomSearchToParameterName, student.RoomSearchTo),
                    base.GetParameter(StudentRegistrationRepository.NumberOfNightsParameterName, student.NumberOfNights),
                    base.GetParameter(StudentRegistrationRepository.TotalGrossPriceParameterName, student.TotalGrossPrice),
                    base.GetParameter(StudentRegistrationRepository.TotalAddinsParameterName, student.TotalAddins),
                    base.GetParameter(StudentRegistrationRepository.PaidParameterName, student.Paid),
                    base.GetParameter(StudentRegistrationRepository.CommisionParameterName, student.Commision),
                    base.GetParameter(StudentRegistrationRepository.CommissionAddinsParameterName, student.CommissionAddins),
                    base.GetParameter(StudentRegistrationRepository.NetPriceParameterName, student.NetPrice),
                    base.GetParameter(StudentRegistrationRepository.BalanceParameterName, student.Balance),
                    base.GetParameter(StudentRegistrationRepository.StudentTripsIDParameterName, student.StudentTripsID),
                    base.GetParameter(StudentRegistrationRepository.StudentDocumentIdParameterName, student.DocumentId),
                    base.GetParameter(BaseRepository.ActiveParameterName, student.Active),
                    base.GetParameter(StudentRegistrationRepository.ChapFamilyParameterName, student.ChapFamily),
                    base.GetParameter(StudentRegistrationRepository.ProgramIDParameterName, student.ProgramID),
                    base.GetParameter(StudentRegistrationRepository.SubProgramIDParameterName, student.SubProgramID),
                    base.GetParameter(StudentRegistrationRepository.GroupIDParameterName, student.GroupID)



    };

            var returnValue = await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.UpdateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<bool> ActivateStudentAsync(StudentRegistration student)
        {
            var parameters = new List<DbParameter>
                {

                    base.GetParameter(StudentRegistrationRepository.IDParameterName, student.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, student.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.ActivateStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<StudentRegistration> GetStudentAsync(int studentID)
        {
            StudentRegistration studentVM = null;
            int AddinID;
            int TripID;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.IDParameterName, studentID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, StudentRegistrationRepository.GetStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        studentVM = new StudentRegistration
                        {

                            ID = dataReader.GetIntegerValue(StudentRegistrationRepository.IDColumnName),
                            Year = dataReader.GetIntegerValue(StudentRegistrationRepository.YearColumnName),
                            Reg_Ref = dataReader.GetStringValue(StudentRegistrationRepository.Reg_RefColumnName),
                            GroupRef = dataReader.GetStringValue(StudentRegistrationRepository.GroupRefColumnName),
                            Camps = dataReader.GetStringValue(StudentRegistrationRepository.CampsColumnName),
                            Gender = dataReader.GetStringValue(StudentRegistrationRepository.GenderColumnName),
                            FirstName = dataReader.GetStringValue(StudentRegistrationRepository.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(StudentRegistrationRepository.LastNameColumnName),
                            HomeAddress = dataReader.GetStringValue(StudentRegistrationRepository.HomeAddressColumnName),
                            City = dataReader.GetStringValue(StudentRegistrationRepository.CityColumnName),
                            State = dataReader.GetStringValue(StudentRegistrationRepository.StateColumnName),
                            Country = dataReader.GetStringValue(StudentRegistrationRepository.CountryColumnName),
                            PostCode = dataReader.GetStringValue(StudentRegistrationRepository.PostCodeColumnName),
                            EmergencyContact = dataReader.GetStringValue(StudentRegistrationRepository.EmergencyContactColumnName),
                            Email = dataReader.GetStringValue(StudentRegistrationRepository.EmailColumnName),
                            Phone = dataReader.GetStringValue(StudentRegistrationRepository.PhoneColumnName),
                            DOB = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.DOBColumnName),
                            Age = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.AgeColumnName),
                            PassportNumber = dataReader.GetStringValue(StudentRegistrationRepository.PassportNumberColumnName),
                            AgencyID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.AgencyIDColumnName),
                            ArrivalDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ArrivalDateColumnName),
                            Terminal = dataReader.GetStringValue(StudentRegistrationRepository.TerminalColumnName),
                            FlightNumber = dataReader.GetStringValue(StudentRegistrationRepository.FlightNumberColumnName),
                            DestinationFrom = dataReader.GetStringValue(StudentRegistrationRepository.DestinationFromColumnName),
                            ArrivalTime = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ArrivalTimeColumnName),
                            DepartureDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.DepartureDateColumnName),
                            DepartureTerminal = dataReader.GetStringValue(StudentRegistrationRepository.DepartureTerminalColumnName),
                            DepartureFlightNumber = dataReader.GetStringValue(StudentRegistrationRepository.DepartureFlightNumberColumnName),
                            DestinationTo = dataReader.GetStringValue(StudentRegistrationRepository.DestinationToColumnName),
                            FlightDepartureTime = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.FlightDepartureTimeColumnName),
                            MedicalInformation = dataReader.GetStringValue(StudentRegistrationRepository.MedicalInformationColumnName),
                            DietaryNeeds = dataReader.GetStringValue(StudentRegistrationRepository.DietaryNeedsColumnName),
                            Allergies = dataReader.GetStringValue(StudentRegistrationRepository.AllergiesColumnName),
                            MedicalNotes = dataReader.GetStringValue(StudentRegistrationRepository.MedicalNotesColumnName),
                            ProgrameStartDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameStartDateColumnName),
                            ProgrameEndDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameEndDateColumnName),
                            Campus = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.CampusColumnName),
                            Format = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.FormatColumnName),
                            MealPlan = dataReader.GetStringValue(StudentRegistrationRepository.MealPlanColumnName),
                            ExtraNotes = dataReader.GetStringValue(StudentRegistrationRepository.ExtraNotesColumnName),
                            ExtraNotesHTML = dataReader.GetStringValue(StudentRegistrationRepository.ExtraNotesHTMLColumnName),
                            Status = dataReader.GetStringValue(StudentRegistrationRepository.StatusColumnName),
                            HomestayOrResi = dataReader.GetStringValue(StudentRegistrationRepository.HomestayOrResiColumnName),
                            HomestayID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.HomestayIDColumnName),
                            RoomID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.RoomIDColumnName),
                            RoomSearchCampus = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.RoomSearchCampusColumnName),
                            GroupID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.GroupIDColumnName),
                            RoomSearchFrom = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.RoomSearchFromColumnName),
                            RoomSearchTo = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.RoomSearchToColumnName),
                            NumberOfNights = dataReader.GetIntegerValue(StudentRegistrationRepository.NumberOfNightsColumnName),
                            TotalGrossPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.TotalGrossPriceColumnName),
                            TotalAddins = dataReader.GetDoubleValue(StudentRegistrationRepository.TotalAddinsColumnName),
                            Paid = dataReader.GetDoubleValue(StudentRegistrationRepository.PaidColumnName),
                            Commision = dataReader.GetDoubleValue(StudentRegistrationRepository.CommisionColumnName),
                            CommissionAddins = dataReader.GetDoubleValue(StudentRegistrationRepository.CommissionAddinsColumnName),
                            NetPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.NetPriceColumnName),
                            Balance = dataReader.GetDoubleValue(StudentRegistrationRepository.BalanceColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            ChapFamily = dataReader.GetStringValue(StudentRegistrationRepository.ChapFamilyColumnName),
                            ProgramID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.ProgramIDColumnName),
                            SubProgramID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.SubProgramIDColumnName),
                            AgentName = dataReader.GetStringValue(StudentRegistrationRepository.AgentNameColumnName),
                            FormatName = dataReader.GetStringValue(StudentRegistrationRepository.FormatNameColumnName),
                            CampusName = dataReader.GetStringValue(StudentRegistrationRepository.CampusNameColumnName),
                            HomestayName = dataReader.GetStringValue(StudentRegistrationRepository.HomestayNameColumnName),
                            RoomName = dataReader.GetStringValue(StudentRegistrationRepository.RoomNameColumnName),
                            ProgramName = dataReader.GetStringValue(StudentRegistrationRepository.ProgramNameColumnName),
                            SubProgramName = dataReader.GetStringValue(StudentRegistrationRepository.SubProgramNameColumnName),
                            DocumentPath = dataReader.GetStringValue(StudentRegistrationRepository.DocumentPathColumnName),
                            ProgrameAddins = new List<int>(),
                            StudentTrips = new List<int>()
                        };
                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {

                                if (dataReader.GetStringValue(StudentRegistrationRepository.LinkTypeIDColumnName).Equals("AddinsID"))
                                {
                                    AddinID = dataReader.GetIntegerValue(StudentRegistrationRepository.LinkIDColumnName);
                                    studentVM?.ProgrameAddins.Add(AddinID);
                                }
                                else
                                {
                                    TripID = dataReader.GetIntegerValue(StudentRegistrationRepository.LinkIDColumnName);
                                    studentVM?.StudentTrips.Add(TripID);
                                }


                            }

                        }


                    }
                }
            }

            return studentVM;
        }
        public async Task<StudentPDFDataVM> GetStudentFilesDataAsync(int studentID)
        {
            StudentPDFDataVM studentVM = null;
           
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.IDParameterName, studentID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, StudentRegistrationRepository.GetStudentPDFDataStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        studentVM = new StudentPDFDataVM
                        {

                            Reg_Ref = dataReader.GetStringValue(StudentRegistrationRepository.Reg_RefColumnName),
                            FirstName = dataReader.GetStringValue(StudentRegistrationRepository.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(StudentRegistrationRepository.LastNameColumnName),
                            DOB = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.DOBColumnName),
                            ProgrameStartDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameStartDateColumnName),
                            ProgrameEndDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameEndDateColumnName),
                            MealPlan = dataReader.GetStringValue(StudentRegistrationRepository.MealPlanColumnName),
                            TotalGrossPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.TotalGrossPriceColumnName),
                            Paid = dataReader.GetDoubleValue(StudentRegistrationRepository.PaidColumnName),
                            Commision = dataReader.GetDoubleValue(StudentRegistrationRepository.CommisionColumnName),
                            CommissionAddins = dataReader.GetDoubleValue(StudentRegistrationRepository.CommissionAddinsColumnName),
                            Balance = dataReader.GetDoubleValue(StudentRegistrationRepository.BalanceColumnName),
                            NetPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.NetPriceColumnName),
                            AgentName = dataReader.GetStringValue(StudentRegistrationRepository.AgentNameColumnName),
                            FormatName = dataReader.GetStringValue(StudentRegistrationRepository.FormatNameColumnName),
                            ProgramName = dataReader.GetStringValue(StudentRegistrationRepository.ProgramNameColumnName),
                            SubProgramName = dataReader.GetStringValue(StudentRegistrationRepository.SubProgramNameColumnName),
                            AgentAddress = dataReader.GetStringValue(StudentRegistrationRepository.AgentAddressColumnName),
                            AgentCountry = dataReader.GetStringValue(StudentRegistrationRepository.AgentCountryColumnName),
                            CampusAddressOnReports = dataReader.GetStringValue(StudentRegistrationRepository.CampusAddressOnReportsColumnName),
                            Email = dataReader.GetStringValue(StudentRegistrationRepository.EmailColumnName),
                            StudentPDFAddinInc = new List<string>(),
                            StudentPDFAddinAdd = new List<string>()

                        };
                        if (dataReader.NextResult())
                        {
                            while (dataReader.Read())
                            {

                                if (dataReader.GetStringValue(StudentRegistrationRepository.AddinsTypeColumnName).Equals("Additional services"))
                                {
                                    studentVM?.StudentPDFAddinAdd.Add(dataReader.GetStringValue(StudentRegistrationRepository.AddinNameColumnName));
                                }
                                else
                                {
                                    studentVM?.StudentPDFAddinInc.Add(dataReader.GetStringValue(StudentRegistrationRepository.AddinNameColumnName));
                                }
                                
                            }

                        }


                    }
                }
            }

            return studentVM;
        }


        
        public async Task<AllResponse<StudentRegistration>> GetAllStudentAsync(AllRequest<StudentRegistration> student)
        {
            StudentRegistration studentVM = null;

            var result = new AllResponse<StudentRegistration>
            {
                Data = new List<StudentRegistration>(),
                Offset = student.Offset,
                PageSize = student.PageSize,
                SortColumn = student.SortColumn,
                SortAscending = student.SortAscending
            };

            var parameters = new List<DbParameter>
            {
                base.GetParameter(BaseRepository.ActiveParameterName, student.Data.Active)
            };

            using (var dataReader = await base.ExecuteReader(parameters, StudentRegistrationRepository.GetAllStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        studentVM = new StudentRegistration
                        {

                            ID = dataReader.GetIntegerValue(StudentRegistrationRepository.IDColumnName),
                            Year = dataReader.GetIntegerValue(StudentRegistrationRepository.YearColumnName),
                            Reg_Ref = dataReader.GetStringValue(StudentRegistrationRepository.Reg_RefColumnName),
                            GroupRef = dataReader.GetStringValue(StudentRegistrationRepository.GroupRefColumnName),
                            Camps = dataReader.GetStringValue(StudentRegistrationRepository.CampsColumnName),
                            Gender = dataReader.GetStringValue(StudentRegistrationRepository.GenderColumnName),
                            FirstName = dataReader.GetStringValue(StudentRegistrationRepository.FirstNameColumnName),
                            LastName = dataReader.GetStringValue(StudentRegistrationRepository.LastNameColumnName),
                            CampusName = dataReader.GetStringValue(StudentRegistrationRepository.CampusNameColumnName),
                            HomeAddress = dataReader.GetStringValue(StudentRegistrationRepository.HomeAddressColumnName),
                            City = dataReader.GetStringValue(StudentRegistrationRepository.CityColumnName),
                            State = dataReader.GetStringValue(StudentRegistrationRepository.StateColumnName),
                            Country = dataReader.GetStringValue(StudentRegistrationRepository.CountryColumnName),
                            PostCode = dataReader.GetStringValue(StudentRegistrationRepository.PostCodeColumnName),
                            EmergencyContact = dataReader.GetStringValue(StudentRegistrationRepository.EmergencyContactColumnName),
                            Email = dataReader.GetStringValue(StudentRegistrationRepository.EmailColumnName),
                            Phone = dataReader.GetStringValue(StudentRegistrationRepository.PhoneColumnName),
                            DOB = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.DOBColumnName),
                            Age = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.AgeColumnName),
                            PassportNumber = dataReader.GetStringValue(StudentRegistrationRepository.PassportNumberColumnName),
                            AgencyID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.AgencyIDColumnName),
                            ArrivalDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ArrivalDateColumnName),
                            Terminal = dataReader.GetStringValue(StudentRegistrationRepository.TerminalColumnName),
                            FlightNumber = dataReader.GetStringValue(StudentRegistrationRepository.FlightNumberColumnName),
                            DestinationFrom = dataReader.GetStringValue(StudentRegistrationRepository.DestinationFromColumnName),
                            ArrivalTime = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ArrivalTimeColumnName),
                            DepartureDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.DepartureDateColumnName),
                            DepartureTerminal = dataReader.GetStringValue(StudentRegistrationRepository.DepartureTerminalColumnName),
                            DepartureFlightNumber = dataReader.GetStringValue(StudentRegistrationRepository.DepartureFlightNumberColumnName),
                            DestinationTo = dataReader.GetStringValue(StudentRegistrationRepository.DestinationToColumnName),
                            FlightDepartureTime = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.FlightDepartureTimeColumnName),
                            MedicalInformation = dataReader.GetStringValue(StudentRegistrationRepository.MedicalInformationColumnName),
                            DietaryNeeds = dataReader.GetStringValue(StudentRegistrationRepository.DietaryNeedsColumnName),
                            Allergies = dataReader.GetStringValue(StudentRegistrationRepository.AllergiesColumnName),
                            MedicalNotes = dataReader.GetStringValue(StudentRegistrationRepository.MedicalNotesColumnName),
                            ProgrameStartDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameStartDateColumnName),
                            ProgrameEndDate = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.ProgrameEndDateColumnName),
                            Campus = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.CampusColumnName),
                            Format = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.FormatColumnName),
                            MealPlan = dataReader.GetStringValue(StudentRegistrationRepository.MealPlanColumnName),
                            ExtraNotes = dataReader.GetStringValue(StudentRegistrationRepository.ExtraNotesColumnName),
                            ExtraNotesHTML = dataReader.GetStringValue(StudentRegistrationRepository.ExtraNotesHTMLColumnName),
                            Status = dataReader.GetStringValue(StudentRegistrationRepository.StatusColumnName),
                            HomestayOrResi = dataReader.GetStringValue(StudentRegistrationRepository.HomestayOrResiColumnName),
                            HomestayID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.HomestayIDColumnName),
                            RoomID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.RoomIDColumnName),
                            RoomSearchCampus = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.RoomSearchCampusColumnName),
                            RoomSearchFrom = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.RoomSearchFromColumnName),
                            RoomSearchTo = dataReader.GetDateTimeValueNullable(StudentRegistrationRepository.RoomSearchToColumnName),
                            NumberOfNights = dataReader.GetIntegerValue(StudentRegistrationRepository.NumberOfNightsColumnName),
                            GroupID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.GroupIDColumnName),
                            TotalGrossPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.TotalGrossPriceColumnName),
                            TotalAddins = dataReader.GetDoubleValue(StudentRegistrationRepository.TotalAddinsColumnName),
                            Paid = dataReader.GetDoubleValue(StudentRegistrationRepository.PaidColumnName),
                            Commision = dataReader.GetDoubleValue(StudentRegistrationRepository.CommisionColumnName),
                            CommissionAddins = dataReader.GetDoubleValue(StudentRegistrationRepository.CommissionAddinsColumnName),
                            NetPrice = dataReader.GetDoubleValue(StudentRegistrationRepository.NetPriceColumnName),
                            Balance = dataReader.GetDoubleValue(StudentRegistrationRepository.BalanceColumnName),
                            Active = dataReader.GetBooleanValue(BaseRepository.ActiveColumnName),
                            AgentName = dataReader.GetStringValue(StudentRegistrationRepository.AgentNameColumnName),
                            FormatName = dataReader.GetStringValue(StudentRegistrationRepository.FormatNameColumnName),
                            ChapFamily = dataReader.GetStringValue(StudentRegistrationRepository.ChapFamilyColumnName),
                            AgencyRef = dataReader.GetStringValue(StudentRegistrationRepository.AgencyRefColumnName),
                            ProgramID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.ProgramIDColumnName),
                            SubProgramID = dataReader.GetUnsignedIntegerValueNullable(StudentRegistrationRepository.SubProgramIDColumnName),
                            ProgrameAddins = new List<int>(),
                            StudentTrips = new List<int>()
                        };
                        result.Data.Add(studentVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }
                }
            }

            return result;
        }


        #region PaymentsStudent
        public async Task<int> AddPaymentStudentAsync(PaymentsViewModel paymentStudent)
        {
            var paymentStudentIDParamter = base.GetParameterOut(StudentRegistrationRepository.PaymentStudentIDParameterName, SqlDbType.Int, paymentStudent.ID);
            var parameters = new List<DbParameter>
                {
                    paymentStudentIDParamter,

                    base.GetParameter(StudentRegistrationRepository.IDParameterName, paymentStudent.StudentRegID),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentDateParameterName, paymentStudent.Date),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentAmountParameterName, paymentStudent.Amount),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentRemarksParameterName, paymentStudent.Remarks)

                };
            
            await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.AddPaymentStudentStoredProcedureName, CommandType.StoredProcedure);

            paymentStudent.ID = Convert.ToInt32(paymentStudentIDParamter.Value);

            return paymentStudent.ID;
        }

        public async Task<bool> UpdatePaymentStudentAsync(PaymentsViewModel paymentStudent)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentIDParameterName, paymentStudent.ID),
                    base.GetParameter(StudentRegistrationRepository.IDParameterName, paymentStudent.StudentRegID),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentDateParameterName, paymentStudent.Date),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentAmountParameterName, paymentStudent.Amount),
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentRemarksParameterName, paymentStudent.Remarks),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentStudent.Active)


                };

            var returnValue = await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.UpdatePaymentStudentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<PaymentsViewModel> GetPaymentStudentAsync(int paymentStudentID)
        {
            PaymentsViewModel paymentStudentVM = null;
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentIDParameterName, paymentStudentID)
                };

            using (var dataReader = await base.ExecuteReader(parameters, StudentRegistrationRepository.GetPaymentStudentStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {
                    if (dataReader.Read())
                    {
                        paymentStudentVM = new PaymentsViewModel
                        {
                            ID = dataReader.GetIntegerValue(StudentRegistrationRepository.PaymentStudentIDColumnName),
                            StudentRegID = dataReader.GetIntegerValue(StudentRegistrationRepository.IDColumnName),
                            Date = dataReader.GetDateTimeValue(StudentRegistrationRepository.PaymentStudentDateColumnName),
                            Amount = dataReader.GetDecimalValue(StudentRegistrationRepository.PaymentStudentAmountColumnName),
                            Remarks = dataReader.GetStringValue(StudentRegistrationRepository.PaymentStudentRemarksColumnName),
                            Active = dataReader.GetBooleanValue(StudentRegistrationRepository.ActiveColumnName)
                        };

                    }
                }
            }
            return paymentStudentVM;
        }

        public async Task<List<PaymentsViewModel>> GetAllPaymentStudentByStudentIdAsync(int studentID)
        {
            PaymentsViewModel paymentStudentVM = null;
            List<PaymentsViewModel> paymentStudentVMList = new List<PaymentsViewModel>();
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.IDParameterName, studentID)
                };


            using (var dataReader = await base.ExecuteReader(parameters, StudentRegistrationRepository.GetAllPaymentStudentByStudentIDStoredProcedureName, CommandType.StoredProcedure))
            {
                if (dataReader != null && dataReader.HasRows)
                {

                    while (dataReader.Read())
                    {
                        paymentStudentVM = new PaymentsViewModel
                        {
                            ID = dataReader.GetIntegerValue(StudentRegistrationRepository.PaymentStudentIDColumnName),
                            StudentRegID = dataReader.GetIntegerValue(StudentRegistrationRepository.IDColumnName),
                            Date = dataReader.GetDateTimeValue(StudentRegistrationRepository.PaymentStudentDateColumnName),
                            Amount = dataReader.GetDecimalValue(StudentRegistrationRepository.PaymentStudentAmountColumnName),
                            Remarks = dataReader.GetStringValue(StudentRegistrationRepository.PaymentStudentRemarksColumnName),
                            Active = dataReader.GetBooleanValue(StudentRegistrationRepository.ActiveColumnName)
                        };
                        paymentStudentVMList.Add(paymentStudentVM);
                    }

                    if (!dataReader.IsClosed)
                    {
                        dataReader.Close();
                    }

                }
            }

            return paymentStudentVMList;
        }

        public async Task<bool> ActivatePaymentStudentAsync(PaymentsViewModel paymentStudent)
        {
            var parameters = new List<DbParameter>
                {
                    base.GetParameter(StudentRegistrationRepository.PaymentStudentIDParameterName, paymentStudent.ID),
                    base.GetParameter(BaseRepository.ActiveParameterName, paymentStudent.Active)

                };

            var returnValue = await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.ActivatePaymentStudentStoredProcedureName, CommandType.StoredProcedure);

            return returnValue > 0;
        }

        public async Task<int> UploadDocuments(UploadDocuments uploadDocuments)
        {
            var uploadDocumentIdParamter = base.GetParameterOut(StudentRegistrationRepository.DocumentIdParameterName, SqlDbType.Int, uploadDocuments.DocumentId);
            var parameters = new List<DbParameter>
                {
                    uploadDocumentIdParamter,

                    base.GetParameter(StudentRegistrationRepository.DocumentPathParameterName, uploadDocuments.FilePath),
                    base.GetParameter(StudentRegistrationRepository.DocumentNameParameterName, uploadDocuments.FileName)

                };

            await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.AddDocumentStoredProcedureName, CommandType.StoredProcedure);

            uploadDocuments.DocumentId = Convert.ToInt32(uploadDocumentIdParamter.Value);

            return uploadDocuments.DocumentId;
        }

        #endregion

    }
}
