using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class LeadsController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        string RegionId;
        string ImgPath;
        public LeadsController(IELIService ELISupervisor, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            ImgPath = config.GetSection("ConnectionStrings").GetSection("ImagePathKey").Value;
            RegionId = config.GetConnectionString("RegionId");
        }
        [HttpPost("CreateLeads")]
        [Produces(typeof(List<SaveLeadViewModel>))]
        public async Task<IActionResult> CreateLeads([FromBody] object leadsVM)
        {
            if (leadsVM != null)
            {
            var leadJson = Newtonsoft.Json.JsonConvert.SerializeObject(leadsVM);
            var leadDifination = new {
                DeviceIdentifier = "",
                ShowId = "",
                QualifierId = "",
                Barcode = "",
                QualifierDetails = new[] {
                    new {
                        QuestionId = "",
                        Response =  default(object)  
                    }
                }
            };
            var leadModel = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(leadJson, leadDifination);
            if (!_ELIService.ValidateDeviceIdentifier(leadModel.DeviceIdentifier))
            {
                return BadRequest(new { message = "UnAuthorised Device" });
            }
            SaveLeadViewModel leadVm = new SaveLeadViewModel();
            leadVm.Barcode = leadModel.Barcode;
            leadVm.DeviceIdentifier = leadModel.DeviceIdentifier;
            leadVm.QualifierId = leadModel.QualifierId;
            leadVm.ShowId = leadModel.ShowId;
            leadVm.QualifierDetails = new List<QualifierDetailViewModel>();
            foreach (var det in leadModel.QualifierDetails)
            {
                QualifierDetailViewModel detailVm = new QualifierDetailViewModel();
                detailVm.QuestionId = det.QuestionId;
                    detailVm.Response = new List<string>();
                    if (det.Response.GetType() == typeof(string) || det.Response.GetType() == typeof(bool))
                        detailVm.Response.Add(Convert.ToString(det.Response));
                    else
                    {
                        foreach (var item in ((Newtonsoft.Json.Linq.JContainer)det.Response))
                        {
                            string repsonseJson = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                            var response = repsonseJson.Split(':');
                            if(response[1] == "true")
                            {
                                detailVm.Response.Add(response[0].Replace("\"",""));
                            }
                        }
                    }
                    leadVm.QualifierDetails.Add(detailVm);
            }
            try
            {
                //var showResult = 
                        await _ELIService.CreateLeadsAsync(leadVm);
                    return Ok(new { message = "Leads created successfully." });
                    //return showResult;
                }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
            }
            else
            {
                return BadRequest(new { message = "Leads model cannot be null" });
            }
        }
        [HttpGet("GetLeadsByShowIdDeviceId")]
        [Produces(typeof(List<GetAllLeadsFromDevice>))]
        public async Task<IActionResult> GetLeadsByShowIdDeviceId(int ShowId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            if (ShowId != 0 && DeviceIdentifier != "")
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(DeviceIdentifier))
                    {
                        return new ObjectResult(await _ELIService.GetLeadsByShowIdDeviceIdAsync(ShowId, DeviceIdentifier, ct));
                    }
                    else
                    {
                        return BadRequest(new { message = "UnAuthorised Device" });
                    }
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "ShowId & Device Identifier cannot be null" });
            }
        }

        [HttpGet("GetLeadsByLeadIdQId")]
        [Produces(typeof(List<GetAllLeadsFromDevice>))]
        public async Task<IActionResult> GetLeadsByLeadIdQId(int LeadId, int QualifierId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            if (LeadId != 0 && QualifierId != 0)
            {
                try
                {
                    if (_ELIService.ValidateDeviceIdentifier(DeviceIdentifier))
                    {
                        return new ObjectResult(await _ELIService.GetLeadsByLeadIdQId(LeadId, QualifierId, ct));
                    }
                    else
                    {
                        return BadRequest(new { message = "UnAuthorised Device" });
                    }
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "ShowId & Device Identifier cannot be null" });
            }
        }
    }
}