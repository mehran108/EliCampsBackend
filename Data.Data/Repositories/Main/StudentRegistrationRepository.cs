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
        private const string GetAllStoredProcedureName = "GetAllStudent";
        private const string UpdateStoredProcedureName = "UpdateStudent";
        private const string ActivateStoredProcedureName = "ActivateStudent";


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

        
        private const string IDColumnName = "ID";
        private const string YearColumnName = "Year";
        private const string Reg_RefColumnName = "Reg_Ref";
        private const string GroupRefColumnName = "GroupRef";
        private const string CampsColumnName = "PCamps";
        private const string GenderColumnName = "Gender";
        private const string FirstNameColumnName = "FirstName";
        private const string LastNameColumnName = "LastName";
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
        private const string ProgrameEndDateColumnName = "rogrameEndDate";
        private const string CampusColumnName = "Campus";
        private const string FormatColumnName = "Format";
        private const string MealPlanColumnName = "MealPlan";
        private const string AddinsIDColumnName = "AddinsID";
        private const string ExtraNotesColumnName = "ExtraNotes";
        private const string ExtraNotesHTMLColumnName = "ExtraNotesHTML";


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
                    //base.GetParameter(StudentRegistrationRepository.ProgrameStartDateParameterName, student.ProgrameStartDate),
                    //base.GetParameter(StudentRegistrationRepository.ProgrameEndDateParameterName, student.ProgrameEndDate),
                    //base.GetParameter(StudentRegistrationRepository.CampusParameterName, student.Campus),
                    //base.GetParameter(StudentRegistrationRepository.FormatParameterName, student.Format),
                    //base.GetParameter(StudentRegistrationRepository.MealPlanParameterName, student.MealPlan),
                    //base.GetParameter(StudentRegistrationRepository.AddinsIDParameterName, student.AddinsID),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesParameterName, student.ExtraNotes),
                    base.GetParameter(StudentRegistrationRepository.ExtraNotesHTMLParameterName, student.ExtraNotesHTML)
                    
       
    };

            await base.ExecuteNonQuery(parameters, StudentRegistrationRepository.AddStoredProcedureName, CommandType.StoredProcedure);

            student.ID = Convert.ToInt32(studentIdParamter.Value);

            return student.ID;
        }

    }
}
