using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Domain.ViewModels;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        



    }
}