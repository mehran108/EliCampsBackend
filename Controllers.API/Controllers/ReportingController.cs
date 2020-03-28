using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Data.Repositories.Main;
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
    public class ReportingController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IELIAuthService _ELIAuthService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEmailSender _emailSender;
        string RegionId;
        string ImgPath;
        string DefaultPassword;
        public ReportingController(IEmailSender emailSender, IELIService ELISupervisor, IELIAuthService ELIAuthService, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            _emailSender = emailSender;
            _ELIService = ELISupervisor;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            _ELIAuthService = ELIAuthService;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            ImgPath = config.GetSection("ConnectionStrings").GetSection("ImagePathKey").Value;
            DefaultPassword = config.GetSection("ConnectionStrings").GetSection("DefaultPassword").Value;
            RegionId = config.GetConnectionString("RegionId");
        }

        [HttpGet("GetLeadsCountReporting")]
        [Produces(typeof(List<LeadsCountViewModel>))]
        public async Task<IActionResult> GetLeadsCountReporting( string showKey, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.LeadCountReportAsync(showKey));
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetAccountListReporting")]
        [Produces(typeof(List<AccountListReportViewModel>))]
        public async Task<IActionResult> GetAccountListReporting(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.AccountListReportAsync());
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetFinancialReconciliationReporting")]
        [Produces(typeof(List<FinancialReconciliationReportViewModel>))]
        public async Task<IActionResult> GetFinancialReconciliationReporting(string year,CancellationToken ct = default(CancellationToken))
        {
            try
            {
                string Text = "22/11/"+ year;
                DateTime date = DateTime.ParseExact(Text, "dd/MM/yyyy", null);
                return new ObjectResult(await _ELIService.FinancialReconciliationReportAsync(date));
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetCodeListReporting")]
        [Produces(typeof(List<CodeListReportViewModel>))]
        public async Task<IActionResult> GetCodeListReporting( CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.CodeListReportAsync(ct));
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetPaymentReportByYear")]
        [Produces(typeof(List<PaymentReportVM>))]
        public async Task<IActionResult> GetAllStudentAsync([FromQuery] string year)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetPaymentReport(year));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}