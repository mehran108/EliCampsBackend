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


        private const string AgentIdParameterName = "PAgentID";
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

    }
}
