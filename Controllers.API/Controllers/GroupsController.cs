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

       
        [HttpPost("addGroup")]
        public async Task<IActionResult> AddGroupAsync([FromBody] GroupViewModel groupVM)
        {

            if (groupVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.AddGroupAsync(groupVM));
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
                return BadRequest(new { message = "group model cannot be empty" });
            }
        }

        [HttpGet("getGroup")]
        [Produces(typeof(GroupViewModel))]
        public async Task<IActionResult> GetGroupAsync(int groupID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetGroupAsync(groupID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpGet("getAllGroups")]
        [Produces(typeof(List<GroupViewModel>))]
        public async Task<IActionResult> GetAllGroups()
        {
            try
            {
                AllRequest<GroupViewModel> grouplist = new AllRequest<GroupViewModel>();
                return new ObjectResult(await _ELIService.GetAllGroupList(grouplist));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateGroup")]
        [Produces(typeof(GroupViewModel))]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateGroupAsync(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateGroup")]
        [Produces(typeof(GroupViewModel))]
        public async Task<IActionResult> ActivateGroupAsync([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateGroup(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }



    }
}