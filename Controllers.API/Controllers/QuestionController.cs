using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        string RegionId;
        public QuestionController(IELIService ELISupervisor, IMapper mapper)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            RegionId = config.GetConnectionString("RegionId");
        }
        [HttpPost("CreateQuestion")]
        [Produces(typeof(List<CreateQuestionViewModel>))]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionViewModel questionVM)
        {
            if (questionVM != null)
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(questionVM.DeviceIdentifier))
                    {
                        var showResult = new ObjectResult(await _ELIService.CreateQuestionAsync(questionVM));
                        return showResult;
                    }
                    else
                    {
                        return BadRequest(new { message = "UnAuthorised Device" });
                    }
                  
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Question model cannot be empty" });
            }
        }
        [HttpGet("GetQuestionTypes")]
        [Produces(typeof(List<QuestionType>))]
        public async Task<IActionResult> GetQuestionTypes(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllQuestionTypesAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion([FromBody] CreateQuestionViewModel QuestionVM)
        {
            if (QuestionVM != null)
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(QuestionVM.DeviceIdentifier))
                    {
                        await _ELIService.UpdateQuestionAsync(QuestionVM);
                        return Ok(new { message = "Question Updated" });
                    }
                    else
                    {
                        return BadRequest(new { message = "UnAuthorised Device" });
                    }
                    
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Question cannot be empty" });
            }
        }
        [HttpDelete("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int id, string DeviceIdentifier)
        {
            if (id != 0)
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(DeviceIdentifier))
                    {
                        var result = await _ELIService.DeleteQuestionAsync(id);
                        if (result == true)
                            return Ok(new { message = "Question Deleted" });
                        else
                            return BadRequest(new { message = "Question not Deleted." });
                    }
                    else
                    {
                        return BadRequest(new { message = "UnAuthorised Device" });
                    }
                   

                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Id cannot be null" });
            }
        }
        [HttpDelete("DeleteOption")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            if (id != 0)
            {
                try
                {
                    var result = await _ELIService.DeleteOptionAsync(id);
                    if (result == true)
                        return Ok(new { message = "Option Deleted" });
                    else
                        return BadRequest(new { message = "Option not Deleted." });
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Id cannot be null" });
            }
        }
        [HttpGet("GetQuestionsByQualifierId")]
        public async Task<IActionResult> GetQuestionsByQualifierId(int qualifierId, CancellationToken ct = default(CancellationToken))
        {
            if (qualifierId != 0)
            {
                try
                {
                    var questions = new ObjectResult(await _ELIService.GetQuestionsByQualifierId(qualifierId, ct));
                    return questions;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "QualifierId cannot be null" });
            }
        }
        [HttpPost("OrderQuestions")]
        [Produces(typeof(List<QuestionOrderingViewModel>))]
        public async Task<IActionResult> OrderQuestions([FromBody] QuestionOrderingViewModel questionVM)
        {
            if (questionVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.OrderQuestionsAsync(questionVM));
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
                return BadRequest(new { message = "Question model cannot be empty" });
            }
        }
        [HttpPost("DeleteAllOptions")]
        public async Task<IActionResult> DeleteAllOptions([FromBody] List<int> ids)
        {
            if (ids.Count != 0)
            {
                try
                {
                    var result = await _ELIService.DeleteAllOptionsAsync(ids);
                    if (result == true)
                        return Ok(new { message = "Options Deleted" });
                    else
                        return BadRequest(new { message = "Options not Deleted." });
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "List cannot be empty while deleting" });
            }
        }
    }
}