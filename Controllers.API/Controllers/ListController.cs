using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Domain.ViewModels;
using ELI.Data.Repositories.Main;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{

    [Route("api/List")]
    public class ListController : Controller
    {

        private readonly IHostingEnvironment _appEnvironment;
        private readonly IELIService _ELIService;
        public ListController(IELIService eLIService, IHostingEnvironment appEnvironment)
        {
            _ELIService = eLIService;
            _appEnvironment = appEnvironment;
        }


        [HttpPost("createAgent")]
        public async Task<IActionResult> CreateAgent([FromBody] AgentViewModel agentVM)
        {

            if (agentVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateAgentAsync(agentVM));
                    return showResult;

                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Agent model cannot be empty" });
            }
        }

        [HttpGet("getAgent")]
        [Produces(typeof(AgentViewModel))]
        public async Task<IActionResult> GetAgent(int agentID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAgentAsync(agentID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("updateAgent")]
        [Produces(typeof(AgentViewModel))]
        public async Task<IActionResult> UpdateAgent([FromBody] AgentViewModel agentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateAgentAsync(agentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateTrips")]
        public async Task<IActionResult> CreateTrips([FromBody] TripsViewModel tripsViewModel)
        {

            if (tripsViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateTirpsAsync(tripsViewModel));
                    return showResult;

                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Trip model cannot be empty" });
            }
        }

        [HttpGet("getTrip")]
        [Produces(typeof(TripsViewModel))]
        public async Task<IActionResult> GetTrips(int tripId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetTirpsAsync(tripId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllTrimList")]
        [Produces(typeof(List<TripsViewModel>))]
        public async Task<IActionResult> GetAllRoomList()
        {
            try
            {
                AllRequest<TripsViewModel> tripslist = new AllRequest<TripsViewModel>();
                return new ObjectResult(await _ELIService.GetAllTripsList(tripslist));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("updateTrips")]
        [Produces(typeof(TripsViewModel))]
        public async Task<IActionResult> UpdateAgent([FromBody] TripsViewModel tripsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateTripsAsync(tripsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }




    }
}