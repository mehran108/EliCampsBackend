using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ELI.Entity;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using ELI.Domain.Helpers;
using System;
using Domain.Domain.ViewModels;
using ELI.Data.Repositories.Main;

namespace ELI.Domain.Services
{
    public interface IELIService
    {
        Task<List<Activation>> GetAllActivationAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllShowAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllRShowAsync(CancellationToken ct = default(CancellationToken));

        Task<List<ShowByRegionViewModel>> GetAllShowByRegionAsync(int RegionId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<Show> CreateShowAsync(Show show, CancellationToken ct = default(CancellationToken));
        Task<List<Countries>> GetAllCountries(CancellationToken ct = default(CancellationToken));
        Task<List<Region>> GetAllRegions(CancellationToken ct = default(CancellationToken));
        Task<List<ContactInfo>> GetContactInformation(CancellationToken ct = default(CancellationToken));

        Task<List<Cities>> GetAllCities(string countryName, CancellationToken ct = default(CancellationToken));
        Task<List<States>> GetAllStates(int CountryId, CancellationToken ct = default(CancellationToken));
        Task<Pricing> CreatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken));
        Task<ShowPricing> CreateShowPricingAsync(ShowPricing showPricing, CancellationToken ct = default(CancellationToken));
        Task<ErrorLogging> CreateLogAsync(ErrorLogging log, CancellationToken ct = default(CancellationToken));
        Task<List<Server>> GetAllServers(CancellationToken ct = default(CancellationToken));
        Task<List<Database>> GetAllDatabase(CancellationToken ct = default(CancellationToken));
        Task<List<Currency>> GetAllCurrency(CancellationToken ct = default(CancellationToken));
        Task<List<LookupValue>> GetAllPaymentMethods(CancellationToken ct = default(CancellationToken));
        Task<List<Device>> GetAllDevices(CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetLimitedShowAsync(int key, int length, CancellationToken ct = default(CancellationToken));
        Task<Activation> CreateActivationAsync(Activation activation, CancellationToken ct = default(CancellationToken));
        LookupValue GetEncryptionKey(LookupValueEnum value, CancellationToken ct = default(CancellationToken));
        Task<Region> GetRegionById(int regionId, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> CreateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken));
        Task<Device> CreateDeviceAsync(Device device, CancellationToken ct = default(CancellationToken));
        Task<Activation> GetActivationByIdAsync(string activationKey, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> GetSDUActivationByActivationIdAsync(int activationId, CancellationToken ct = default(CancellationToken));
        Task<Device> GetDeviceByIdAsync(int deviceId, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> UpdateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken));
        bool ShowKeyDuplicationCheck(string showKey, CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetAllShowByRoleAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken));
        Task<Device> GetDeviceByDeviceIdentifierAsync(string deviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<Activation>> GetAllActivationsByRoleAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken));
        Task<ShowViewModel> GetShowByIdAsync(int id, CancellationToken ct);
        Task UpdateShowAsync(Show showViewModel, CancellationToken ct = default(CancellationToken));
        Task<Pricing> GetPricingByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken));
        Task UpdatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken));
        Task<Qualifier> CreateQualifierAsync(Qualifier qualifier, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetQualifierByShowIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetQualifierByQIdAsync(int showId,int QId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken));

        Task<Qualifier> GetQuestionsByQualifierIdAsync(string role,int Id, string userId, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierIdFromDevice(int qualifierId, int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<DiscountViewModel>> GetAllDiscountCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetAllQualifiersAsync(CancellationToken ct = default(CancellationToken));
        Task<Discount> GetDiscountByIdAsync(int Id, CancellationToken ct = default(CancellationToken));
        Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken ct = default(CancellationToken));
        Task<List<LookupValue>> GetAllDiscountTypes(CancellationToken ct = default(CancellationToken));
        bool DiscountCodeDuplicationCheck(string discountCode, CancellationToken ct = default(CancellationToken));
        Task<GetLeadsInfoViewModel> GetLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode, CancellationToken ct = default(CancellationToken));
        Task<GetLeadsInfoViewModel> UpdateLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode,string scannedDatetime ,GetLeadsInfoViewModel leadsVM , CancellationToken ct = default(CancellationToken));
        Task<CreateQuestionViewModel> CreateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken));
        Task<List<QuestionType>> GetAllQuestionTypesAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Show>> GetShowsforPurchaseActivationAsync(CancellationToken ct = default(CancellationToken));
        Task UpdateQualifierAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken));
        Task UpdateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken));
        Task<List<Discount>> GetActiveDiscountCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<Discount> GetActiveDiscountCodebyCodeAsync(string code, CancellationToken ct = default(CancellationToken));
        Task UpdateDiscountAsync(Discount showViewModel, CancellationToken ct = default(CancellationToken));
        Task<Discount> ValidateDiscountCodeAsync(string Code, int ShowId, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteQualifierAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteQuestionAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteOptionAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierId(int QualifierId, CancellationToken ct = default(CancellationToken));
        Task<bool> OrderQuestionsAsync(QuestionOrderingViewModel questionOrdering, CancellationToken ct = default(CancellationToken));
        Task<Invoice> CreateInvoiceAsync(Invoice invoice, CancellationToken ct = default(CancellationToken));
        Task UpdateInvoiceAsync(Invoice invoiceViewModel, CancellationToken ct = default(CancellationToken));
        Task<string> GetResponseAPI(string paymentId, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAllOptionsAsync(List<int> ids, CancellationToken ct = default(CancellationToken));
        Task CreateLeadsAsync(SaveLeadViewModel leads, CancellationToken ct = default(CancellationToken));
        Task<string> GenerateRACCodes(int showId, int userId, int Quantity, CancellationToken ct = default(CancellationToken));
        Task<Qualifier> CreateWebQualifierAsync(Qualifier qualifier, int userId, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionWebViewModel>> CreateQuestionsWebAsync(List<CreateQuestionWebViewModel> questions, int usedId, CancellationToken ct = default(CancellationToken));
        Task<string> GenerateActivationKeys(int showId, int userId, int Quantity, CancellationToken ct = default(CancellationToken));
        Task<Show> GetShowbyShowKey(string showKey, CancellationToken ct = default(CancellationToken));
        Task<Qualifier> UpdateQualifierWebAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken));
        Task<List<CreateQuestionWebViewModel>> UpdateQuestionsWebAsync(List<CreateQuestionWebViewModel> questions, int qualifierId, List<int> questionDelete,int userId, CancellationToken ct = default(CancellationToken));
        Task<List<Activation>> GetAllRestrictedCodesAsync(CancellationToken ct = default(CancellationToken));
        Task<List<GetAllLeadsFromDevice>> GetLeadsByShowIdDeviceIdAsync(int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken));
        Task<List<LeadsQualifier>> GetLeadsByLeadIdQId(int LeadId, int QualifierId, CancellationToken ct = default(CancellationToken));
        Task<Activation> ValidateRestrictedCodeAsync(string Code, int ShowId, CancellationToken ct = default(CancellationToken));
        Task<Sduactivation> GetSDUActivationByDeviceAsync(int showId, int deviceId, CancellationToken ct = default(CancellationToken));
        Task<Invoice> GetInvoicebyActivationKeyAsync(string activationKey, CancellationToken ct = default(CancellationToken));
        Task<List<Qualifier>> GetAllQualifiersExhibitorAsync(int userid, CancellationToken ct = default(CancellationToken));
        Task<Show> CheckShowActivationAsync(int id, CancellationToken ct);
        Task<QualifierUsers> DeleteQualifierUserRelation(int qualifierId, int userid, CancellationToken ct);
        Task<ShowDiscount> CreateShowDiscountAsync(ShowDiscount showDiscount, CancellationToken ct = default(CancellationToken));
        Task<ShowDiscount> GetShowDiscountRelationAsync(int ShowId, int DiscountId, CancellationToken ct = default(CancellationToken));
        Task<List<Discount>> GetShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken));
        Task<List<ShowDiscount>> DeleteShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken));
        Task<List<LeadsCountViewModel>> LeadCountReportAsync(string ShowKey, CancellationToken ct = default(CancellationToken));
        Task<List<AccountListReportViewModel>> AccountListReportAsync(CancellationToken ct = default(CancellationToken));
        Task<List<FinancialReconciliationReportViewModel>> FinancialReconciliationReportAsync(DateTime year, CancellationToken ct = default(CancellationToken));
        Task<List<CodeListReportViewModel>> CodeListReportAsync(CancellationToken ct = default(CancellationToken));
        Task<List<Leads>> GetLeadsByShowIdAsync(int ShowId, CancellationToken ct = default(CancellationToken));
        bool ValidateDeviceIdentifier(string deviceIdentifier);
        Task<string> AUSSuccessCase(int invoiceId, string responseCode);

        Task<string> AUSFailCase(int invoiceId, string responseCode);

        #region AgentList
        Task<int> CreateAgentAsync(AgentViewModel agent);

        Task<AgentViewModel> GetAgentAsync(int agentID);

        Task<bool> UpdateAgentAsync(AgentViewModel agent);

        Task<bool> ActivateAgentAsync(AgentViewModel agent);
        Task<AllResponse<AgentViewModel>> GetAllAgent(AgentRequestVm agent);

        #endregion

        #region Room List

        Task<RoomsViewModel> GetRomeList(int roomListId);

        Task<AllResponse<RoomsList>> GetAllRomeList(RoomsRequestVm rooms);
        Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel);

        Task<bool> UpdateRoomListAsync(RoomsViewModel roomsViewModel);

        Task<bool> ActivateRoom(RoomsViewModel roomsViewModel);

        #endregion

        #region Trips

        Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        Task<TripsViewModel> GetTirpsAsync(int tripID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        Task<AllResponse<TripsViewModel>> GetAllTripsList(TripsRequestVm rooms);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>

        Task<bool> UpdateTripsAsync(TripsViewModel trips);



        Task<bool> ActivateTripsAsync(TripsViewModel trips);
        #endregion

        #region group
        Task<int> AddGroupAsync(GroupViewModel group);
        Task<GroupViewModel> GetGroupAsync(int groupID, bool IsInvoice);
        Task<bool> UpdateGroupAsync(GroupViewModel group);
        Task<AllResponse<GroupViewModel>> GetAllGroupList(AllRequest<GroupViewModel> groups);
        Task<bool> ActivateGroup(GroupViewModel group);
        Task<bool> GroupPayment(GroupViewModel group);
        Task<bool> GroupPrograme(GroupViewModel group);
        Task<bool> GroupTrips(GroupViewModel group);
        #region PaymentsGroups
        #region PaymentsGroups
        Task<int> AddPaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);
        Task<bool> UpdatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);
        Task<PaymentsGroupsViewModel> GetPaymentGroupAsync(int paymentGroupID);
        Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupByGroupIdAsync(int groupID);
        Task<bool> ActivatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup);

        #endregion

        #region PaymentsGroupsLeader
        Task<int> AddPaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);
        Task<bool> UpdatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);
        Task<PaymentsGroupsViewModel> GetPaymentGroupLeaderAsync(int paymentGroupID);
        Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupLeaderByGroupIdAsync(int groupID);
        Task<bool> ActivatePaymentGroupLeaderAsync(PaymentsGroupsViewModel paymentGroup);

        #endregion
        #endregion
        #endregion

        Task<List<LookupValueViewModel>> GetListBaseonLookupTable(string lookupTable);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeStayViewModel"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AddinsRequestVm AddinsList);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="homeStayView"></param>
        /// <returns></returns>

        Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel);


        Task<bool> ActivateAddinsAsync(AddinsViewModel trips);
        #endregion


        #region Campus
        Task<int> CreateCampusAsync(CampuseViewModel campusViewModel);

        Task<CampuseViewModel> GetCampus(int campusId);

        Task<AllResponse<CampuseViewModel>> GetAllCampus(CampuseViewModel campusList);

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

        Task<bool> UpdateSubProgramAsync(SubProgramViewModel subProgramViewModel);

        Task<bool> ActivateSubProgramAsync(SubProgramViewModel subProgramViewModel);
        #endregion

        #region Student
        Task<int> AddStudentAsync(StudentRegistration student);
        Task<bool> UpdateStudentAsync(StudentRegistration student);
        Task<StudentRegistration> GetStudentAsync(int studentID);
        Task<StudentPDFDataVM> GetStudentFilesDataAsync(int studentID);
        Task<bool> ActivateStudentAsync(StudentRegistration student);

        Task<bool> UpdateStudentProfilePicAsync(StudentRegistration student);
        Task<AllResponse<StudentRegistration>> GetAllStudentAsync(AllRequest<StudentRegistration> student);

        #region PaymentsStudent
        Task<int> AddPaymentStudentAsync(PaymentsViewModel paymentStudent);
        Task<bool> UpdatePaymentStudentAsync(PaymentsViewModel paymentStudent);
        Task<PaymentsViewModel> GetPaymentStudentAsync(int paymentStudentID);
        Task<List<PaymentsViewModel>> GetAllPaymentStudentByStudentIdAsync(int studentID);
        Task<List<PaymentsViewModel>> GetAllPaymentStudentByGroupIdAsync(int studentID);
        Task<bool> ActivatePaymentStudentAsync(PaymentsViewModel paymentStudent);
        Task<int> UploadDocuments(StudentDocuments studentDocuments);

        #endregion
        #endregion
        #region ReportList
        Task<AllResponse<PaymentReportVM>> GetPaymentReport(string year);
        #endregion

    }
}