using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class ELIDbController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        public ELIDbController(IELIService ELISupervisor, IMapper mapper)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
            
        }
        [HttpPost("CreateErrorLog")]
        [Produces(typeof(List<ErrorLogging>))]
        public async Task<IActionResult> CreateErrorLog([FromBody] ErrorLogging log)
        {
            if (log != null)
            {
                try
                {
                    var logResult = new ObjectResult(await _ELIService.CreateLogAsync(log));
                    return logResult;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Log cannot be null" });
            }
        }
        [HttpGet("GetServers")]
        [Produces(typeof(List<Server>))]
        public async Task<IActionResult> GetServers(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllServers(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetDatabases")]
        [Produces(typeof(List<Database>))]
        public async Task<IActionResult> GetDatabases(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllDatabase(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetCurrency")]
        [Produces(typeof(List<Currency>))]
        public async Task<IActionResult> GetCurrency(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllCurrency(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetPaymentMethods")]
        [Produces(typeof(List<LookupValue>))]
        public async Task<IActionResult> GetPaymentMethods(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllPaymentMethods(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetDevices")]
        [Produces(typeof(List<Device>))]
        public async Task<IActionResult> GetDevices(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllDevices(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("AusResultPG/{res}")]
        public async Task<IActionResult> AusResultPG(string res)  
        {
            List<object> items = new List<object>();

            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            string SURL = config.GetSection("ConnectionStrings").GetSection("SuccessURLAUS").Value;
            string FURL = config.GetSection("ConnectionStrings").GetSection("FailureURLAUS").Value;

            if(Request.Form["responseCode"] == "00" || Request.Form["responseCode"] == "08" || Request.Form["responseCode"] == "77")
            {
                var paymentId = await _ELIService.AUSSuccessCase(Convert.ToInt32(Request.Form["fullcardnumber"]), Request.Form["responseCode"]);
                return Redirect(SURL);
            }
            var paymentFId = await _ELIService.AUSFailCase(Convert.ToInt32(Request.Form["fullcardnumber"]), Request.Form["responseCode"]);

            return Redirect(FURL);
        }

    }
}