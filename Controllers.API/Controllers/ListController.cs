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

        [HttpPut("updateAgent")]
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


        [HttpGet("getAllAgent")]
        [Produces(typeof(List<AgentViewModel>))]
        public async Task<IActionResult> GetAllAgent()
        {
            try
            {
                AllRequest<AgentViewModel> agentList = new AllRequest<AgentViewModel>();
                return new ObjectResult(await _ELIService.GetAllAgent(agentList));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateAgent")]
        [Produces(typeof(AgentViewModel))]
        public async Task<IActionResult> ActivateAgentAsync([FromBody] AgentViewModel agentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateAgentAsync(agentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("createRoom")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomsViewModel agentVM)
        {

            if (agentVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateRoomListAsync(agentVM));
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

        [HttpGet("getRoomById")]
        [Produces(typeof(RoomsViewModel))]
        public async Task<IActionResult> getRoomById(int roomID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetRomeList(roomID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateRoom")]
        [Produces(typeof(RoomsViewModel))]
        public async Task<IActionResult> UpdateRoom([FromBody] RoomsViewModel roomsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateRoomListAsync(roomsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateRoom")]
        [Produces(typeof(RoomsViewModel))]
        public async Task<IActionResult> ActivateRoomAsync([FromBody] RoomsViewModel roomsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateRoom(roomsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>

        [HttpGet("getAllRoomList")]
        [Produces(typeof(List<RoomsViewModel>))]
        public async Task<IActionResult> GetAllRoomList()
        {
            try
            {
                AllRequest<RoomsList> roomlist = new AllRequest<RoomsList>();
                return new ObjectResult(await _ELIService.GetAllRomeList(roomlist));
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

        [HttpGet("getAllTrips")]
        [Produces(typeof(List<TripsViewModel>))]
        public async Task<IActionResult> GettAllTrips()
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
        [HttpPut("activateTrips")]
        [Produces(typeof(TripsViewModel))]
        public async Task<IActionResult> ActivateTripsAsync([FromBody] TripsViewModel tripsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateTripsAsync(tripsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getListTypeByLookupTable")]
        [Produces(typeof(List<LookupValueViewModel>))]
        public async Task<IActionResult> GetListBaseonLookupTable(string lookupTable)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetListBaseonLookupTable(lookupTable));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateHomeStay")]
     
        public async Task<IActionResult> CreateHomeStay([FromBody] HomeStayViewModel homeStayViewModel)
        {

            if (homeStayViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateHomeStayAsync(homeStayViewModel));
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


        [HttpGet("getHomeStay")]
        [Produces(typeof(HomeStayViewModel))]
        public async Task<IActionResult> GetHomeStay(int homeStayId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetHomeStayAsync(homeStayId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllHomeStay")]
        [Produces(typeof(List<HomeStayViewModel>))]
        public async Task<IActionResult> GetAllHomeStay()
        {
            try
            {
                AllRequest<HomeStayViewModel> homeStay = new AllRequest<HomeStayViewModel>();
                return new ObjectResult(await _ELIService.GetAllHomeStay(homeStay));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateHomeStay")]
        [Produces(typeof(HomeStayViewModel))]
        public async Task<IActionResult> UpdateHomeStay([FromBody] HomeStayViewModel homeStayVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateHomeStayAsync(homeStayVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateHomeStay")]
        [Produces(typeof(HomeStayViewModel))]
        public async Task<IActionResult> ActivateHomeStayAsync([FromBody] HomeStayViewModel homeStayViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateHomeStayAsync(homeStayViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("CreateAddins")]
        public async Task<IActionResult> CreateAddins([FromBody] AddinsViewModel addinsViewModel)
        {

            if (addinsViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateAddinsAsync(addinsViewModel));
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
                return BadRequest(new { message = "Addins model cannot be empty" });
            }
        }

        [HttpGet("GetAddins")]
        [Produces(typeof(AddinsViewModel))]
        public async Task<IActionResult> GetAddins(int addinsId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAddins(addinsId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllAddins")]
        [Produces(typeof(List<AddinsViewModel>))]
        public async Task<IActionResult> getAllAddins()
        {
            try
            {
                AllRequest<AddinsViewModel> addinslist = new AllRequest<AddinsViewModel>();
                return new ObjectResult(await _ELIService.GetAllAddinsList(addinslist));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateAddins")]
        [Produces(typeof(AddinsViewModel))]
        public async Task<IActionResult> UpdateAddins([FromBody] AddinsViewModel addinsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateAddinsAsync(addinsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateAddins")]
        [Produces(typeof(AddinsViewModel))]
        public async Task<IActionResult> ActivateAddinsAsync([FromBody] AddinsViewModel addinsViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateAddinsAsync(addinsViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }











    }
}
