using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ELI.Domain.ViewModels;
using ELI.Entity;
using ELI.Domain.Contracts;
using ELI.Domain.Helpers;
using ELI.Domain.Contracts.Main;
using ELI.Entity.Main;
using ELI.Domain.Common;
using Domain.Domain.ViewModels;
using ELI.Data.Repositories.Main;

namespace ELI.Domain.Services
{
    public class ELIService : IELIService
    {
        private readonly ILookupTableRepository _lookupTableRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IShowRepository _showRepository;
        private readonly IActivationRepository _activationRepository;
        private readonly IActivationTypeRepository _activationTypeRepository;
        private readonly IELIRepository _ELIRepository;
        private readonly ISduactivationRespository _sduActivationRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IQualifierRepository _qualifierRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly ILeadsRepository _leadsRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IListRepository _listRepository;
        private readonly IGroupRepository _groupRepository;


        public ELIService(
            ILookupTableRepository lookupTableRepository,
            IInvoiceRepository invoiceRepository,
            IShowRepository showRepository,
            IActivationTypeRepository activationTypeRepository,
            IActivationRepository activationRepository,
            IELIRepository ELIRepository,
              ISduactivationRespository sduActivationRepository,
              IDeviceRepository deviceRepository,
              IQualifierRepository qualifierRepository,
              IDiscountRepository discountRepository,
               IQuestionRepository questionRepository,
               ILeadsRepository leadsRepository,
               IReportRepository reportRepository,
               IListRepository listRepository,
               IGroupRepository groupRepository

            )
        {
            _lookupTableRepository = lookupTableRepository;
            _invoiceRepository = invoiceRepository;
            _showRepository = showRepository;
            _activationTypeRepository = activationTypeRepository;
            _activationRepository = activationRepository;
            _ELIRepository = ELIRepository;
            _sduActivationRepository = sduActivationRepository;
            _deviceRepository = deviceRepository;
            _qualifierRepository = qualifierRepository;
            _discountRepository = discountRepository;
            _questionRepository = questionRepository;
            _leadsRepository = leadsRepository;
            _reportRepository = reportRepository;
            _listRepository = listRepository;
            _groupRepository = groupRepository;

        }
        public async Task<bool> DeleteInvoiceAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _invoiceRepository.DeleteAsync(id, ct);
        }
        public Task<List<Show>> GetAllShowAsync(CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetAllAsync(ct);
        }
        public Task<List<Show>> GetAllRShowAsync(CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetAllRAsync(ct);
        }
        public Task<List<Show>> GetAllShowByRoleAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetAllShowsByUserIdAsync(role, userid, isDashboard, ct);
        }
        public Task<List<Activation>> GetAllActivationsByRoleAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            return _activationRepository.GetAllActivationsByUserIdAsync(role, userid, isDashboard, ct);
        }
        public Task<List<Show>> GetLimitedShowAsync(int key, int length, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetLimitednumberShowsAsync(key, length, ct);
        }
        public Task<List<ShowByRegionViewModel>> GetAllShowByRegionAsync(int RegionId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetAllShowsByRegion(RegionId, DeviceIdentifier, ct);
        }
        public async Task<Show> CreateShowAsync(Show show, CancellationToken ct = default(CancellationToken))
        {
            return await _showRepository.CreateShowAsync(show, ct);
        }
        public async Task<Qualifier> CreateQualifierAsync(Qualifier qualifier, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.CreateQualifierAsync(qualifier, DeviceIdentifier, ct);
        }
        public async Task<Pricing> CreatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.CreatePricingAsync(pricing, ct);
        }
        public async Task<Pricing> GetPricingByShowIdAsync(int showId, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.GetPricingByShowIdAsync(showId, ct);
        }
        public async Task<List<Qualifier>> GetQualifierByShowIdAsync(int showId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetQualifierByShowIdAsync(showId, DeviceIdentifier, ct);
        }
        public async Task<List<Qualifier>> GetQualifierByQIdAsync(int showId, int QId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetQualifierByQIdAsync(showId, QId, DeviceIdentifier, ct);
        }
        public async Task<Qualifier> GetQuestionsByQualifierIdAsync(string role, int Id, string userId, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetQuestionsByQualifierIdAsync(role, Id, userId, 0, ct);
        }
        public async Task<Activation> CreateActivationAsync(Activation activation, CancellationToken ct = default(CancellationToken))
        {
            return await _activationRepository.CreateActivationAsync(activation, ct);
        }
        public async Task<Sduactivation> CreateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken))
        {
            return await _sduActivationRepository.CreateSduActivationAsync(sduactivation, ct);
        }
        public async Task<Sduactivation> UpdateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken))
        {
            return await _sduActivationRepository.UpdateSduActivationAsync(sduactivation, ct);
        }
        public async Task<Device> CreateDeviceAsync(Device device, CancellationToken ct = default(CancellationToken))
        {
            return await _deviceRepository.CreateDeviceAsync(device, ct);
        }
        public async Task<ErrorLogging> CreateLogAsync(ErrorLogging log, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.CreateLogAsync(log, ct);
        }
        public async Task<ShowPricing> CreateShowPricingAsync(ShowPricing showPricing, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.CreateShowPricingAsync(showPricing, ct);
        }
        public Task<List<Countries>> GetAllCountries(CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetAllCountriesAsync(ct);
        }
        public Task<List<Region>> GetAllRegions(CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetAllRegionAsync(ct);
        }
        public Task<List<ContactInfo>> GetContactInformation(CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetContactInformationAsync(ct);
        }
        public Task<Region> GetRegionById(int regionId, CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetRegionById(regionId);
        }
        public Task<List<Cities>> GetAllCities(string countryName, CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetCitiesByCountryAsync(countryName, ct);
        }
        public Task<List<States>> GetAllStates(int CountryId, CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetAllStatesAsync(CountryId, ct);
        }
        public Task<List<Server>> GetAllServers(CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getServersAsync();
        }
        public Task<List<Database>> GetAllDatabase(CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getDatabaseAsync();
        }
        public Task<List<Currency>> GetAllCurrency(CancellationToken ct = default(CancellationToken))
        {
            return _ELIRepository.GetCurrency();
        }
        public Task<List<LookupValue>> GetAllPaymentMethods(CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getPaymentMethods();
        }
        public Task<List<LookupValue>> GetAllDiscountTypes(CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getDiscountTypes();
        }
        public Task<List<Device>> GetAllDevices(CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getDeviceAsync();
        }
        public async Task<List<Activation>> GetAllActivationAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _activationRepository.GetAllAsync(ct);
        }
        public async Task<Activation> GetActivationByIdAsync(string activationKey, CancellationToken ct = default(CancellationToken))
        {
            return await _activationRepository.GetActivationByIdAsync(activationKey, ct);
        }
        public async Task<Sduactivation> GetSDUActivationByActivationIdAsync(int activationId, CancellationToken ct = default(CancellationToken))
        {
            return await _sduActivationRepository.GetSDUActivationByActivationIdAsync(activationId, ct);
        }
        public async Task<Device> GetDeviceByIdAsync(int deviceId, CancellationToken ct = default(CancellationToken))
        {
            return await _deviceRepository.GetDeviceByIdAsync(deviceId, ct);
        }
        public async Task<Device> GetDeviceByDeviceIdentifierAsync(string deviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _deviceRepository.GetDeviceByDeviceIdentifierAsync(deviceIdentifier, ct);
        }
        public LookupValue GetEncryptionKey(LookupValueEnum value, CancellationToken ct = default(CancellationToken))
        {
            return _lookupTableRepository.getEncryptionKey(value);
        }
        public bool ShowKeyDuplicationCheck(string showKey, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.CheckShowKeyDuplication(showKey, ct);
        }
        public async Task<ShowViewModel> GetShowByIdAsync(int id, CancellationToken ct)
        {
            return await _showRepository.GetByIdAsync(id, ct);
        }
        public async Task UpdateShowAsync(Show showViewModel, CancellationToken ct = default(CancellationToken))
        {
            await _showRepository.UpdateShowAsync(showViewModel, ct);
        }
        public async Task UpdatePricingAsync(Pricing pricing, CancellationToken ct = default(CancellationToken))
        {
            await _ELIRepository.UpdatePricingAsync(pricing, ct);
        }
        public Task<List<DiscountViewModel>> GetAllDiscountCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            return _discountRepository.GetAllDiscountCodesAsync(ct);
        }
        public Task<List<Qualifier>> GetAllQualifiersAsync(CancellationToken ct = default(CancellationToken))
        {
            return _qualifierRepository.GetAllQualifiersAsync(ct);
        }
        public Task<List<Qualifier>> GetAllQualifiersExhibitorAsync(int userid, CancellationToken ct = default(CancellationToken))
        {
            return _qualifierRepository.GetAllQualifiersExhibitorsAsync(userid, ct);
        }
        public async Task<Discount> GetDiscountByIdAsync(int Id, CancellationToken ct = default(CancellationToken))
        {
            return await _discountRepository.GetDiscountByIdAsync(Id, ct);
        }
        public async Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken ct = default(CancellationToken))
        {
            return await _discountRepository.CreateDiscountAsync(discount, ct);
        }
        public bool DiscountCodeDuplicationCheck(string discountCode, CancellationToken ct = default(CancellationToken))
        {
            return _discountRepository.CheckDiscountCodeDuplication(discountCode, ct);
        }
        public async Task<GetLeadsInfoViewModel> GetLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetLeadsInfoAsync(Id, ShowKey, deviceIdentifier, barcode, ct);
        }
        public async Task<GetLeadsInfoViewModel> UpdateLeadsInfoAsync(int Id, string ShowKey, string deviceIdentifier, string barcode, string scannedDatetime, GetLeadsInfoViewModel leadsVM, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.UpdateLeadsInfoAsync(Id, ShowKey, deviceIdentifier, barcode, scannedDatetime, leadsVM, ct);
        }
        public async Task<List<Leads>> GetLeadsByShowIdAsync(int ShowId, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetLeadsByShowIdAsync(ShowId, ct);
        }
        public async Task<CreateQuestionViewModel> CreateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.CreateQuestionAsync(question, ct);
        }
        public Task<List<QuestionType>> GetAllQuestionTypesAsync(CancellationToken ct = default(CancellationToken))
        {
            return _questionRepository.GetQuestionTypesAsync(ct);
        }
        public Task<List<Show>> GetShowsforPurchaseActivationAsync(CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GetShowsforPurchaseActivationAsync(ct);
        }
        public async Task UpdateQualifierAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken))
        {
            await _qualifierRepository.UpdateQualifierAsync(qualifier, ct);
        }
        public async Task UpdateQuestionAsync(CreateQuestionViewModel question, CancellationToken ct = default(CancellationToken))
        {
            await _questionRepository.UpdateQuestionAsync(question, ct);
        }
        public async Task<Discount> GetActiveDiscountCodebyCodeAsync(string code, CancellationToken ct = default(CancellationToken))
        {
            return await _discountRepository.GetActiveDiscountByCodeAsync(code, ct);
        }
        public async Task<List<Discount>> GetActiveDiscountCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _discountRepository.GetActiveDiscountCodesAsync(ct);
        }
        public async Task UpdateDiscountAsync(Discount discount, CancellationToken ct = default(CancellationToken))
        {
            await _discountRepository.UpdateDiscountCodeAsync(discount, ct);
        }
        public async Task<Discount> ValidateDiscountCodeAsync(string Code, int ShowId, CancellationToken ct = default(CancellationToken))
        {
            return await _showRepository.ValidateDiscountCode(Code, ShowId, ct);
        }
        public async Task<bool> DeleteQualifierAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.DeleteQualifierAsync(id, ct);
        }
        public async Task<bool> DeleteQuestionAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.DeleteQuestionAsync(id, ct);
        }
        public async Task<bool> DeleteOptionAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.DeleteOptionAsync(id, ct);
        }
        public async Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierId(int QualifierId, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.GetQuestionByQualifierIdAsync(QualifierId, ct);
        }
        public async Task<List<CreateQuestionViewModel>> GetQuestionsByQualifierIdFromDevice(int qualifierId, int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.GetQuestionsByQualifierIdFromDevice(qualifierId, showId, deviceIdentifier, ct);
        }
        public async Task<bool> OrderQuestionsAsync(QuestionOrderingViewModel questionOrdering, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.OrderingQuestionAsync(questionOrdering, ct);
        }
        public async Task<Invoice> CreateInvoiceAsync(Invoice invoice, CancellationToken ct = default(CancellationToken))
        {
            return await _invoiceRepository.CreateInvoiceAsync(invoice, ct);
        }
        public async Task UpdateInvoiceAsync(Invoice invoiceViewModel, CancellationToken ct = default(CancellationToken))
        {
            await _invoiceRepository.UpdateInvoiceAsync(invoiceViewModel, ct);
        }
        public async Task<string> GetResponseAPI(string paymentId, CancellationToken ct = default(CancellationToken))
        {
            return await _showRepository.GetRequestResponse(paymentId, ct);
        }
        public async Task<bool> DeleteAllOptionsAsync(List<int> ids, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.DeleteAllOptionsAsync(ids, ct);
        }
        public async Task CreateLeadsAsync(SaveLeadViewModel leads, CancellationToken ct = default(CancellationToken))
        {
            await _leadsRepository.CreateLeadsAsync(leads, ct);
        }
        public async Task<string> GenerateRACCodes(int showId, int userId, int Quantity, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GenerateKeys(showId, userId, Quantity, 8, null);
        }
        public async Task<Qualifier> CreateWebQualifierAsync(Qualifier qualifier, int userId, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.CreateQualifierWebAsync(qualifier, userId, ct);
        }
        public async Task<List<CreateQuestionWebViewModel>> CreateQuestionsWebAsync(List<CreateQuestionWebViewModel> questions, int usedId, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.CreateQuestionWebAsync(questions, usedId, ct);
        }
        public async Task<string> GenerateActivationKeys(int showId, int userId, int Quantity, CancellationToken ct = default(CancellationToken))
        {
            return _showRepository.GenerateKeys(showId, userId, Quantity, 7, 0);
        }
        public async Task<Show> GetShowbyShowKey(string showKey, CancellationToken ct = default(CancellationToken))
        {
            return await _showRepository.GetShowByShowKey(showKey);
        }
        public async Task<Qualifier> UpdateQualifierWebAsync(Qualifier qualifier, CancellationToken ct = default(CancellationToken))
        {
            return await _qualifierRepository.UpdateQualifierWebAsync(qualifier, ct);
        }
        public async Task<List<CreateQuestionWebViewModel>> UpdateQuestionsWebAsync(List<CreateQuestionWebViewModel> questions, int qualifierId, List<int> questionDelete, int userId, CancellationToken ct = default(CancellationToken))
        {
            return await _questionRepository.UpdateQuestionWebAsync(questions, qualifierId, questionDelete, userId, ct);
        }
        public async Task<List<Activation>> GetAllRestrictedCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _activationRepository.GetAllRestrictedCodesAsync(ct);
        }
        public async Task<List<GetAllLeadsFromDevice>> GetLeadsByShowIdDeviceIdAsync(int showId, string deviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            return await _leadsRepository.GetLeadsByShowIdDeviceIdAsync(showId, deviceIdentifier, ct);
        }
        public async Task<List<LeadsQualifier>> GetLeadsByLeadIdQId(int LeadId, int QualifierId, CancellationToken ct = default(CancellationToken))
        {
            return await _leadsRepository.GetLeadsByLeadIdQId(LeadId, QualifierId, ct);
        }
        public async Task<Activation> ValidateRestrictedCodeAsync(string Code, int ShowId, CancellationToken ct = default(CancellationToken))
        {
            return await _activationRepository.ValidateRestrictedCode(Code, ShowId, ct);
        }
        public async Task<Sduactivation> GetSDUActivationByDeviceAsync(int showId, int deviceId, CancellationToken ct = default(CancellationToken))
        {
            return await _sduActivationRepository.GetSDUActivationByDeviceAsync(showId, deviceId, ct);
        }
        public async Task<Invoice> GetInvoicebyActivationKeyAsync(string activationKey, CancellationToken ct = default(CancellationToken))
        {
            return await _invoiceRepository.GetByActivationKeyAsync(activationKey, ct);
        }
        public async Task<Show> CheckShowActivationAsync(int id, CancellationToken ct)
        {
            return await _showRepository.CheckShowActivationAsync(id, ct);
        }
        public async Task<QualifierUsers> DeleteQualifierUserRelation(int qualifierId, int userid, CancellationToken ct)
        {
            return await _qualifierRepository.DeleteQualifierUserRealtion(qualifierId, userid, ct);
        }
        public async Task<ShowDiscount> CreateShowDiscountAsync(ShowDiscount showDiscount, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.CreateShowDiscountAsync(showDiscount, ct);
        }
        public async Task<ShowDiscount> GetShowDiscountRelationAsync(int ShowId, int DiscountId, CancellationToken ct = default(CancellationToken))
        {
            return await _showRepository.GetShowDiscountRelationAsync(ShowId, DiscountId, ct);
        }
        public async Task<List<Discount>> GetShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.GetShowDiscountByShowIdAsync(ShowId, ct);
        }
        public async Task<List<ShowDiscount>> DeleteShowDiscountsRelationsAsync(int ShowId, CancellationToken ct = default(CancellationToken))
        {
            return await _ELIRepository.DeleteShowDiscountsRelationsAsync(ShowId, ct);
        }
        public async Task<List<LeadsCountViewModel>> LeadCountReportAsync(string ShowKey, CancellationToken ct = default(CancellationToken))
        {
            return await _reportRepository.LeadsCountReportAsync(ShowKey, ct);
        }

        public async Task<List<AccountListReportViewModel>> AccountListReportAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _reportRepository.AccountListReportAsync(ct);
        }
        public async Task<List<FinancialReconciliationReportViewModel>> FinancialReconciliationReportAsync(DateTime year, CancellationToken ct = default(CancellationToken))
        {
            return await _reportRepository.FinancialReconciliationReportAsync(year, ct);
        }

        public async Task<List<CodeListReportViewModel>> CodeListReportAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _reportRepository.CodeListReportAsync(ct);
        }
        public bool ValidateDeviceIdentifier(string deviceIdentifier)
        {
            return _ELIRepository.ValidateDeviceIdentifier(deviceIdentifier);
        }
        public async Task<string> AUSSuccessCase(int invoiceId, string responseCode)
        {
            return await _ELIRepository.AUSSuccessCase(invoiceId, responseCode);
        }
        public async Task<string> AUSFailCase(int invoiceId, string responseCode)
        {
            return await _ELIRepository.AUSFailCase(invoiceId, responseCode);
        }

        #region List

        #region AgentList
        public async Task<int> CreateAgentAsync(AgentViewModel agent)
        {
            return await _listRepository.CreateAgentAsync(agent);
        }

        

        public async Task<AgentViewModel> GetAgentAsync(int agentID)
        {
            return await _listRepository.GetAgentAsync(agentID);
        }

        public async Task<bool> UpdateAgentAsync(AgentViewModel agent)
        {
            return await _listRepository.UpdateAgentAsync(agent);
        }

        public async Task<bool> ActivateAgentAsync(AgentViewModel agent)
        {
            return await _listRepository.ActivateAgentAsync(agent);
        }

        public async Task<AllResponse<AgentViewModel>> GetAllAgent(AgentRequestVm agent)
        {
            return await _listRepository.GetAllAgent(agent);
        }

        public async Task<RoomsViewModel> GetRomeList(int roomListID)
        {
            return await _listRepository.GetRomeListAsync(roomListID);
        }

        public async Task<AllResponse<RoomsList>> GetAllRomeList(RoomsRequestVm rooms)
        {
            return await _listRepository.GetAllRomeList(rooms);
        }

        

        #endregion

        #region RoomsList
        public async Task<int> CreateRoomListAsync(RoomsViewModel roomsViewModel)
        {
            return await _listRepository.CreateRoomListAsync(roomsViewModel);
        }

        public async Task<bool> UpdateRoomListAsync(RoomsViewModel roomsViewModel)
        {
            return await _listRepository.UpdateRoomListAsync(roomsViewModel);
        }
        public async Task<bool> ActivateRoom(RoomsViewModel roomsViewModel)
        {
            return await _listRepository.ActivateRoom(roomsViewModel);
        }
        #endregion


        #region Trips
        //Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        public async Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel)
        {
            return await _listRepository.CreateTirpsAsync(tripsViewModel);
        }

        public async Task<TripsViewModel> GetTirpsAsync(int agentID)
        {
            return await _listRepository.GetTripAsync(agentID);
        }

        public async Task<AllResponse<TripsViewModel>> GetAllTripsList(TripsRequestVm rooms)
        {
            return await _listRepository.GetAllTripsList(rooms);
        }

        public async Task<bool> UpdateTripsAsync(TripsViewModel trips)
        {
            return await _listRepository.UpdateTirpsAsync(trips);
        }

        public async Task<bool> ActivateTripsAsync(TripsViewModel trips)
        {
            return await _listRepository.ActivateTripsAsync(trips);
        }
        #endregion
        #endregion

        #region groups
        public async Task<int> AddGroupAsync(GroupViewModel group)
        {
            return await _groupRepository.AddGroupAsync(group);
        }

        public async Task<GroupViewModel> GetGroupAsync(int groupID)
        {
            return await _groupRepository.GetGroupAsync(groupID);
        }
        public async Task<bool> UpdateGroupAsync(GroupViewModel group)
        {
            return await _groupRepository.UpdateGroupAsync(group);
        }

        public async Task<bool> GroupPrograme(GroupViewModel group)
        {
            group.AddinsID = string.Join(",", group.ProgrameAddins.ToArray());
            return await _groupRepository.GroupPrograme(group);
        }

        public async Task<bool> GroupTrips(GroupViewModel group)
        {
            group.GroupTripsID = string.Join(",", group.GroupTrips.ToArray());
            return await _groupRepository.GroupTrips(group);
        }


        public async Task<bool> GroupPayment(GroupViewModel group)
        {
            return await _groupRepository.GroupPayment(group);
        }

        public async Task<AllResponse<GroupViewModel>> GetAllGroupList(AllRequest<GroupViewModel> groups)
        {
            return await _groupRepository.GetAllGroupList(groups);
        }

        public async Task<bool> ActivateGroup(GroupViewModel group)
        {
            return await _groupRepository.ActivateGroup(group);
        }

        #region PaymentsGroups
        public async Task<int> AddPaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            return await _groupRepository.AddPaymentGroupAsync(paymentGroup);
        }
        public async Task<bool> UpdatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            return await _groupRepository.UpdatePaymentGroupAsync(paymentGroup);
        }
        public async Task<PaymentsGroupsViewModel> GetPaymentGroupAsync(int paymentGroupID)
        {
            return await _groupRepository.GetPaymentGroupAsync(paymentGroupID);
        }
        public async Task<List<PaymentsGroupsViewModel>> GetAllPaymentGroupByGroupIdAsync(int groupID)
        {
            return await _groupRepository.GetAllPaymentGroupByGroupIdAsync(groupID);
        }
        public async Task<bool> ActivatePaymentGroupAsync(PaymentsGroupsViewModel paymentGroup)
        {
            return await _groupRepository.ActivatePaymentGroupAsync(paymentGroup);
        }
        #endregion
        #endregion

        #region HomeStay
        public async Task<int> CreateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            return await _listRepository.CreateHomeStayAsync(homeStayViewModel);
        }

        public async Task<HomeStayViewModel> GetHomeStayAsync(int homeStayId)
        {
            return await _listRepository.GetHomeStayAsync(homeStayId);
        }
        public async Task<AllResponse<HomeStayViewModel>> GetAllHomeStay(HomeStayRequestVm HomeStay)
        {
            return await _listRepository.GetAllHomeStay(HomeStay);
        }

        public async Task<bool> UpdateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            return await _listRepository.UpdateHomeStayAsync(homeStayViewModel);
        }

        public async Task<bool> ActivateHomeStayAsync(HomeStayViewModel homeStayViewModel)
        {
            return await _listRepository.ActivateHomeStayAsync(homeStayViewModel);
        }

        #endregion


        #region Addins
        //Task<int> CreateTirpsAsync(TripsViewModel tripsViewModel);
        public async Task<int> CreateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            return await _listRepository.CreateAddinsAsync(addinsViewModel);
        }

        public async Task<AddinsViewModel> GetAddins(int addinsId)
        {
            return await _listRepository.GetAddins(addinsId);
        }

        public async Task<AllResponse<AddinsViewModel>> GetAllAddinsList(AddinsRequestVm addinsList)
        {
            return await _listRepository.GetAllAddinsList(addinsList);
        }


        public async Task<bool> UpdateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            return await _listRepository.UpdateAddinsAsync(addinsViewModel);
        }

        public async Task<bool> ActivateAddinsAsync(AddinsViewModel addinsViewModel)
        {
            return await _listRepository.ActivateAddinsAsync(addinsViewModel);
        }
        #endregion


        public async Task<List<LookupValueViewModel>> GetListBaseonLookupTable(string lookupTable)
        {
            return await _listRepository.GetListBaseonLookupTable(lookupTable);
        }

        #region Campus

        public async Task<int> CreateCampusAsync(CampuseViewModel campusViewModel)
        {
            return await _listRepository.CreateCampusAsync(campusViewModel);
        }

        public async Task<CampuseViewModel> GetCampus(int campusId)
        {
            return await _listRepository.GetCampus(campusId);
        }

        public async Task<AllResponse<CampuseViewModel>> GetAllCampus(CampuseViewModel campusList)
        {
            return await _listRepository.GetAllCampus(campusList);
        }

        public async Task<bool> UpdateCampusAsync(CampuseViewModel campusViewModel)
        {
            return await _listRepository.UpdateCampusAsync(campusViewModel);
        }

        public async Task<bool> ActivateCampusAsync(CampuseViewModel campusViewModel)
        {
            return await _listRepository.ActivateCampusAsync(campusViewModel);
        }

        #endregion

        #region Program

        public async Task<int> CreateProgramAsync(ProgramViewModel programViewModel)
        {
            return await _listRepository.CreateProgramAsync(programViewModel);
        }

        public async Task<ProgramViewModel> GetProgramAsync(int programId)
        {
            return await _listRepository.GetProgramAsync(programId);
        }

        public async Task<AllResponse<ProgramViewModel>> GetAllProgramAsync(AllRequest<ProgramViewModel> programList)
        {
            return await _listRepository.GetAllProgramAsync(programList);
        }

        public async Task<bool> UpdateProgramAsync(ProgramViewModel programViewModel)
        {
            return await _listRepository.UpdateProgramAsync(programViewModel);
        }

        public async Task<bool> ActivateProgramAsync(ProgramViewModel programViewModel)
        {
            return await _listRepository.ActivateProgramAsync(programViewModel);
        }

        #endregion

        #region SubProgram

        public async Task<int> CreateSubProgramAsync(SubProgramViewModel subProgramViewModel)
        {
            return await _listRepository.CreateSubProgramAsync(subProgramViewModel);
        }

        public async Task<SubProgramViewModel> GetSubProgramAsync(int subProgramId)
        {
            return await _listRepository.GetSubProgramAsync(subProgramId);
        }

        public async Task<AllResponse<SubProgramViewModel>> GetAllSubProgramAsync(AllRequest<SubProgramViewModel> subProgramList)
        {
            return await _listRepository.GetAllSubProgramAsync(subProgramList);
        }

        public async Task<bool> UpdateSubProgramAsync(SubProgramViewModel subProgramViewModel)
        {
            return await _listRepository.UpdateSubrogramAsync(subProgramViewModel);
        }

        public async Task<bool> ActivateSubProgramAsync(SubProgramViewModel subProgramViewModel)
        {
            return await _listRepository.ActivateSubProgramAsync(subProgramViewModel);
        }

        #endregion

    }
}
