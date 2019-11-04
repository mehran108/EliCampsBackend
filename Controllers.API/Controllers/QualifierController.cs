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
    public class QualifierController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IELIAuthService _ELIAuthService;

        private readonly IMapper _mapper;
        string RegionId;
        public QualifierController(IELIService ELISupervisor, IMapper mapper, IELIAuthService ELIAuthService)
        {
            _ELIService = ELISupervisor;
            _ELIAuthService = ELIAuthService;
            _mapper = mapper;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            RegionId = config.GetConnectionString("RegionId");
        }
        [HttpGet("GetQualifiers")]
        [Produces(typeof(List<Qualifier>))]
        public async Task<IActionResult> GetQualifiers(string role, int UserId,CancellationToken ct = default(CancellationToken))
        {
            if (role.Contains("Admin"))
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllQualifiersAsync(ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllQualifiersExhibitorAsync(UserId,ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
        }
        [HttpPost("CreateQualifer")]
        [Produces(typeof(List<Qualifier>))]
        public async Task<IActionResult> CreateQualifer([FromBody] CreateQualifierVIewModel QualifierVM)
        {
            if (QualifierVM != null)
            {
                Qualifier qualifier = new Qualifier();
                qualifier = _mapper.Map<Qualifier>(QualifierVM);
                qualifier.IsDeleted = false;
                qualifier.IsActive = true;
                qualifier.CreatedDate = DateTime.Now;
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(QualifierVM.DeviceIdentifier))
                    {
                        var qualifierResult = new ObjectResult(await _ELIService.CreateQualifierAsync(qualifier, QualifierVM.DeviceIdentifier));
                        return qualifierResult;
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
                return BadRequest(new { message = "Qualifier model cannot be null" });
            }
        }
        [HttpGet("GetQualifierByShowId")]
        public async Task<IActionResult> GetQualifierByShowId(int showid, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            if (showid != 0 && DeviceIdentifier != "")
            {
                try
                {
                    var qualifier = new ObjectResult(await _ELIService.GetQualifierByShowIdAsync(showid, DeviceIdentifier, ct));
                    return qualifier;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "ShowId & Device Identifier cannot be empty" });
            }
        }

        [HttpGet("GetQualifierByQId")]
        public async Task<IActionResult> GetQualifierByQId(int showId, int QId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            if (QId != 0 && DeviceIdentifier != "")
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(DeviceIdentifier))
                    {
                        var qualifier = new ObjectResult(await _ELIService.GetQualifierByQIdAsync(showId, QId, DeviceIdentifier, ct));
                        return qualifier;
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
                return BadRequest(new { message = "ShowId & Device Identifier cannot be empty" });
            }
        }

        [HttpGet("GetQuestionsByQualifierIdFromDevice")]
        public async Task<IActionResult> GetQuestionsByQualifierIdFromDevice(int qualifierId, int showId, string deviceIdentifier,CancellationToken ct = default(CancellationToken))
        {
            if (qualifierId != 0 && showId != 0 && !string.IsNullOrEmpty(deviceIdentifier))
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(deviceIdentifier))
                    {
                        var questions = new ObjectResult(await _ELIService.GetQuestionsByQualifierIdFromDevice(qualifierId, showId, deviceIdentifier, ct));
                        return questions;
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
                return BadRequest(new { message = "QualifierId cannot be null" });
            }
        }
        [HttpGet("GetQuestionsByQualifierId")]
        [Produces(typeof(Qualifier))]
        public async Task<IActionResult> GetQuestionsByQualifierId(string role, int id, string userId, CancellationToken ct = default(CancellationToken))
        {
            if (id != 0)
            {
                try
                {
                    //var qualifier = 
                      return  new ObjectResult(await _ELIService.GetQuestionsByQualifierIdAsync(role,id,userId, ct));
                    //return qualifier;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Id cannot be empty" });
            }
        }
        [HttpGet("GetLeadsInfo")]
        [Produces(typeof(List<GetLeadsInfoViewModel>))]
        public async Task<IActionResult> GetLeadsInfo(int Id, string ShowKey, string barcode, string DeviceIdentifier ,CancellationToken ct = default(CancellationToken))
        {
            if (ShowKey != "" && barcode != "")
            {
                try
                {
                    //if (DeviceIdentifier == "" || DeviceIdentifier == null)
                    //{
                    //    DeviceIdentifier = "9999";
                    //}
                    var qualifier = new ObjectResult(await _ELIService.GetLeadsInfoAsync(Id, ShowKey, DeviceIdentifier, barcode,ct));
                    return qualifier;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "ShowKey & Barcode cannot be empty" });
            }
        }
        [HttpPost("UpdateLeadsInfo")]
        [Produces(typeof(List<GetLeadsInfoViewModel>))]
        public async Task<IActionResult> UpdateLeadsInfo(int Id, string ShowKey, string barcode, string scannedDatetime, string DeviceIdentifier, [FromBody] GetLeadsInfoViewModel LeadVM, CancellationToken ct = default(CancellationToken))
        {
            if (ShowKey != "" && barcode != "" && LeadVM!= null)
            {

                try
                {
                    var leadVM = _mapper.Map<GetLeadsInfoViewModel>(LeadVM);
                    try
                    {
                        if(_ELIService.ValidateDeviceIdentifier(DeviceIdentifier))
                        {
                            return new ObjectResult(await _ELIService.UpdateLeadsInfoAsync(Id, ShowKey, DeviceIdentifier, barcode, scannedDatetime, leadVM));
                        }
                        else
                        {
                            return BadRequest(new { message = "UnAuthorised Device" });
                        }
                    }
                    catch (AppException ex)
                    {
                        return BadRequest(new { message = ex.Message });
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
                return BadRequest(new { message = "ShowKey & Barcode cannot be empty" });
            }
        }
        [HttpPut("UpdateQualifier")]
        public async Task<IActionResult> UpdateQualifier([FromBody] UpdateQualifierViewModel QualifierVM)
        {
            if (QualifierVM != null)
            {
                try
                {
                    Qualifier qualifier = new Qualifier();
                    qualifier = _mapper.Map<Qualifier>(QualifierVM);
                    qualifier.UpdatedDate = DateTime.Now;
                    try
                    {
                        await _ELIService.UpdateQualifierAsync(qualifier);
                        return Ok(new { message = "Qualifier Updated" });
                    }
                    catch (AppException ex)
                    {
                        return BadRequest(new { message = ex.Message });
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
                return BadRequest(new { message = "Qualifier model cannot be empty" });
            }
        }
        [HttpDelete("DeleteQualifier")]
        public async Task<IActionResult> DeleteQualifier(int id)
        {
            if (id != 0)
            {
                try
                {
                    var result = await _ELIService.DeleteQualifierAsync(id);
                    if (result == true)
                        return Ok(new { message = "Qualifier Deleted" });
                    else
                        return BadRequest(new { message = "Qualifier not Deleted." });
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
        [HttpPost("CreateQualiferWeb")]
        [Produces(typeof(List<Qualifier>))]
        public async Task<IActionResult> CreateQualiferWeb([FromBody] CreateQualifierWebViewModel QualifierVM)
        {
            if (QualifierVM != null)
            {
                Qualifier qualifier = new Qualifier();
                qualifier = _mapper.Map(QualifierVM, qualifier);
                //qualifier.QualifierName = QualifierVM.QualifierName;
                //qualifier.ShowId = QualifierVM.ShowId;
                //qualifier.IsPublished = QualifierVM.IsPublished;
                //qualifier.IsDefault = QualifierVM.IsDefault;
                qualifier.IsDeleted = false;
                //qualifier.IsAdmin = QualifierVM.IsAdmin;
                qualifier.IsActive = true;
                //qualifier.IsAdmin = QualifierVM.IsAdmin;
                //qualifier.CreatedBy = QualifierVM.UserId+"";
                qualifier.CreatedDate = DateTime.Now;
                //qualifier.OwnerId = QualifierVM.OwnerId;
                try
                {
                    var qualifierResult = await _ELIService.CreateWebQualifierAsync(qualifier, QualifierVM.UserId);
                    foreach (var item in QualifierVM.Questions)
                    {
                        item.QualifierId = qualifierResult.QualifierId;
                        item.IsAdmin = qualifierResult.IsAdmin;
                        item.IsExhibitor = !qualifierResult.IsAdmin;
                    }
                    var questionsrResult = new ObjectResult(await _ELIService.CreateQuestionsWebAsync(QualifierVM.Questions, QualifierVM.UserId));
                    
                    return questionsrResult;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Qualifier model cannot be empty" });
            }
        }
        [HttpPost("UpdateQualiferWeb")]
        [Produces(typeof(List<Qualifier>))]
        public async Task<IActionResult> UpdateQualiferWeb([FromBody] CreateQualifierWebViewModel QualifierVM, CancellationToken ct = default(CancellationToken))
        {
            if (QualifierVM != null)
            {
                var roleId = 0;
                Qualifier qualifier = new Qualifier();
                qualifier = _mapper.Map(QualifierVM, qualifier);
                
                try
                {
                    var user = await _ELIAuthService.GetByIdAsync(QualifierVM.UserId);
                    foreach (var item in user.AuthUserRoles)
                    {
                        roleId = item.RoleId;
                    }

                    if (roleId == 1 && QualifierVM.IsAdmin == true)
                    {
                        var qualifierResult = await _ELIService.UpdateQualifierWebAsync(qualifier);
                        foreach (var item1 in QualifierVM.Questions)
                        {
                            item1.QualifierId = qualifierResult.QualifierId;
                           
                        }
                        var questionResult = new ObjectResult(await _ELIService.UpdateQuestionsWebAsync(QualifierVM.Questions, QualifierVM.QualifierId, QualifierVM.DeletedQuestions, QualifierVM.UserId));
                        return questionResult;
                    }
                    else if (roleId == 2 && QualifierVM.IsAdmin == true)
                    {

                        var qualifierResult = await _ELIService.UpdateQualifierWebAsync(qualifier);
                        foreach (var item1 in QualifierVM.Questions)
                        {
                            item1.QualifierId = qualifierResult.QualifierId;
                            
                        }
                        var questionResult = new ObjectResult(await _ELIService.UpdateQuestionsWebAsync(QualifierVM.Questions, QualifierVM.QualifierId, QualifierVM.DeletedQuestions, QualifierVM.UserId));
                        return questionResult;
                        //update realtion in qualifieruser table aginst item
                    }
                    else
                    {
                        var qualifierResult = await _ELIService.UpdateQualifierWebAsync(qualifier);
                        foreach (var item1 in QualifierVM.Questions)
                        {
                            item1.QualifierId = qualifierResult.QualifierId;
                        }
                        var questionResult = new ObjectResult(await _ELIService.UpdateQuestionsWebAsync(QualifierVM.Questions, QualifierVM.QualifierId, QualifierVM.DeletedQuestions, QualifierVM.UserId));
                        return questionResult;
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
                return BadRequest(new { message = "Qualifier model cannot be empty" });
            }
        }
        
    }
}