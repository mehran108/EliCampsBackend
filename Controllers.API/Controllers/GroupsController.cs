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
        public async Task<IActionResult> GetGroupAsync(int groupID, bool IsInvoice)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetGroupAsync(groupID,IsInvoice));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        

        [HttpGet("getAllGroups")]
        [Produces(typeof(List<GroupViewModel>))]
        public async Task<IActionResult> GetAllGroups([FromQuery] GroupViewModel groupVM)
        {
            try
            {
                AllRequest<GroupViewModel> grouplist = new AllRequest<GroupViewModel>();
                grouplist.Data = groupVM;
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
        
        [HttpPut("deleteGroup")]
        [Produces(typeof(GroupViewModel))]
        public async Task<IActionResult> DeleteGroup([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.DeleteGroup(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("groupPayment")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> GroupPaymentAsync([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.GroupPayment(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateGroupPrograme")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> GroupPrograme([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.GroupPrograme(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateGroupTrips")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> GroupTrips([FromBody] GroupViewModel groupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.GroupTrips(groupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("addPaymentGroup")]
        public async Task<IActionResult> AddPaymentGroupAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {

            if (paymentGroupVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.AddPaymentGroupAsync(paymentGroupVM));
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
                return BadRequest(new { message = "payment group cannot be empty" });
            }
        }

        [HttpGet("getPaymentGroup")]
        [Produces(typeof(PaymentsGroupsViewModel))]
        public async Task<IActionResult> GetPaymentGroupAsync(int paymentGroupID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetPaymentGroupAsync(paymentGroupID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getAllPaymentGroupByGroupId")]
        [Produces(typeof(List<PaymentsGroupsViewModel>))]
        public async Task<IActionResult> GetAllPaymentGroupByGroupIdAsync(int groupID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllPaymentGroupByGroupIdAsync(groupID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updatePaymentGroup")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdatePaymentGroupAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdatePaymentGroupAsync(paymentGroupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activatePaymentGroup")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivatePaymentGroupAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivatePaymentGroupAsync(paymentGroupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("addPaymentGroupLeader")]
        public async Task<IActionResult> AddPaymentGroupLeaderAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {

            if (paymentGroupVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.AddPaymentGroupLeaderAsync(paymentGroupVM));
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
                return BadRequest(new { message = "payment group cannot be empty" });
            }
        }

        [HttpGet("getPaymentGroupLeader")]
        [Produces(typeof(PaymentsGroupsViewModel))]
        public async Task<IActionResult> GetPaymentGroupLeaderAsync(int paymentGroupID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetPaymentGroupLeaderAsync(paymentGroupID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getAllPaymentGroupLeaderByGroupId")]
        [Produces(typeof(List<PaymentsGroupsViewModel>))]
        public async Task<IActionResult> GetAllPaymentGroupLeaderByGroupIdAsync(int groupID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllPaymentGroupLeaderByGroupIdAsync(groupID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updatePaymentGroupLeader")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdatePaymentGroupLeaderAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdatePaymentGroupLeaderAsync(paymentGroupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activatePaymentGroupLeader")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivatePaymentGroupLeaderAsync([FromBody] PaymentsGroupsViewModel paymentGroupVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivatePaymentGroupLeaderAsync(paymentGroupVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}