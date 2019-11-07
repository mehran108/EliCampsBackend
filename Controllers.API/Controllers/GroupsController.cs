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

using Microsoft.AspNetCore.Mvc;


namespace ELI.API.Controllers
{

    [Route("api/Groups")]
    public class GroupsController : Controller
    {

        private readonly IHostingEnvironment _appEnvironment;
        private readonly IELIService _ELIService;
        public GroupsController(IELIService eLIService, IHostingEnvironment appEnvironment)
        {
            _ELIService = eLIService;
            _appEnvironment = appEnvironment;
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

        [HttpGet("getRoomList")]
        [Produces(typeof(RoomsViewModel))]
        public async Task<IActionResult> GetRoomList(int roomID)
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
                AllRequest<RoomsList> roomlist  = new  AllRequest<RoomsList>();
                return new ObjectResult(await _ELIService.GetAllRomeList(roomlist));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}