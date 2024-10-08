﻿using System;
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
        [HttpPost("createLookupValue")]
        public async Task<IActionResult> createLookupValue([FromBody] LookupValueViewModel lookupValueModel)
        {

            if (lookupValueModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateLookupValueAsync(lookupValueModel));
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
        [HttpPost("DeleteLookupValue")]
        public async Task<IActionResult> DeleteLookupValue([FromBody] LookupValueViewModel lookupValueModel)
        {

            if (lookupValueModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.DeleteLookupValue(lookupValueModel));
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
        public async Task<IActionResult> GetAllAgent([FromQuery] AgentRequestVm requestVm)
        {
            try
            {
                //AllRequest<AgentViewModel> agentList = new AllRequest<AgentViewModel>();
                return new ObjectResult(await _ELIService.GetAllAgent(requestVm));
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
        public async Task<IActionResult> GetAllRoomList([FromQuery] RoomsRequestVm requestVm)
        {
            try
            {
               
                return new ObjectResult(await _ELIService.GetAllRomeList(requestVm));
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
        public async Task<IActionResult> GettAllTrips([FromQuery] TripsRequestVm requestVm)
        {
            try
            {
                
                return new ObjectResult(await _ELIService.GetAllTripsList(requestVm));
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


        [HttpPost("UpdateLookupValue")]

        public async Task<IActionResult> UpdateLookupValue([FromBody] LookupValueViewModel model)
        {

            if (model != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.UpdateLookupValue(model));
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
        public async Task<IActionResult> GetAllHomeStay([FromQuery] HomeStayRequestVm requestVm)
        {
            try
            {

                return new ObjectResult(await _ELIService.GetAllHomeStay(requestVm));
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
        public async Task<IActionResult> getAllAddins([FromQuery] AddinsRequestVm requestVm)
        {
            try
            {
               
                return new ObjectResult(await _ELIService.GetAllAddinsList(requestVm));
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



        [HttpPost("createCampus")]
        public async Task<IActionResult> CreateCampusAsync([FromBody] CampuseViewModel campusViewModel)
        {

            if (campusViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateCampusAsync(campusViewModel));
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
                return BadRequest(new { message = "Campus model cannot be empty" });
            }
        }


        [HttpGet("Getcampus")]
        [Produces(typeof(CampuseViewModel))]
        public async Task<IActionResult> GetCampus(int campusId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetCampus(campusId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("updateCampus")]
        [Produces(typeof(CampuseViewModel))]
        public async Task<IActionResult> UpdateCampusAsync([FromBody] CampuseViewModel campusViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateCampusAsync(campusViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getAllCampus")]
        [Produces(typeof(List<CampuseViewModel>))]
        public async Task<IActionResult> GetAllCampus([FromQuery] CampuseViewModel requestVm)
        {
            try
            {
                
                return new ObjectResult(await _ELIService.GetAllCampus(requestVm));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateCampus")]
        [Produces(typeof(CampuseViewModel))]
        public async Task<IActionResult> ActivateCampusAsync([FromBody] CampuseViewModel campusViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateCampusAsync(campusViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpPost("createProgram")]
        public async Task<IActionResult> CreateProgramAsync([FromBody] ProgramViewModel programViewModel)
        {

            if (programViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateProgramAsync(programViewModel));
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
                return BadRequest(new { message = "Program model cannot be empty" });
            }
        }


        [HttpGet("getProgram")]
        [Produces(typeof(ProgramViewModel))]
        public async Task<IActionResult> GetProgramAsync(int programId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetProgramAsync(programId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("updateProgram")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateProgramAsync([FromBody] ProgramViewModel programViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateProgramAsync(programViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getAllProgram")]
        [Produces(typeof(List<ProgramViewModel>))]
        public async Task<IActionResult> GetAllProgramAsync([FromQuery] ProgramViewModel programViewModel)
        {
            try
            {
                AllRequest<ProgramViewModel> programList = new AllRequest<ProgramViewModel>();
                programList.Data = programViewModel;
                return new ObjectResult(await _ELIService.GetAllProgramAsync(programList));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateProgram")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivateProgramAsync([FromBody] ProgramViewModel programViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateProgramAsync(programViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("createSubProgram")]
        public async Task<IActionResult> CreateSubProgramAsync([FromBody] SubProgramViewModel subProgramViewModel)
        {

            if (subProgramViewModel != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.CreateSubProgramAsync(subProgramViewModel));
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
                return BadRequest(new { message = "SubProgram model cannot be empty" });
            }
        }


        [HttpGet("getSubProgram")]
        [Produces(typeof(SubProgramViewModel))]
        public async Task<IActionResult> GetSubProgramAsync(int subProgramId)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetSubProgramAsync(subProgramId));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("updateSubProgram")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateSubProgramAsync([FromBody] SubProgramViewModel subProgramViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateSubProgramAsync(subProgramViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getAllSubProgram")]
        [Produces(typeof(List<SubProgramViewModel>))]
        public async Task<IActionResult> GetAllSubProgramAsync([FromQuery] SubProgramViewModel subProgramViewModel)
        {
            try
            {
                AllRequest<SubProgramViewModel> subProgramList = new AllRequest<SubProgramViewModel>();
                subProgramList.Data = subProgramViewModel;
                return new ObjectResult(await _ELIService.GetAllSubProgramAsync(subProgramList));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateSubProgram")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivateSubProgramAsync([FromBody] SubProgramViewModel subProgramViewModel)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateSubProgramAsync(subProgramViewModel));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}
