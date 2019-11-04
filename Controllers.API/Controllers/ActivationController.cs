using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class ActivationController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IELIAuthService _ELIAuthService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEmailSender _emailSender;
        string RegionId;
        string ConString;
        string ImgPath;
        string DefaultPassword;
        public ActivationController(IEmailSender emailSender, IELIService ELISupervisor, IELIAuthService ELIAuthService, IMapper mapper, IHostingEnvironment appEnvironment)
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
            ConString = config.GetConnectionString("ELIDb");
        }
        [HttpPost("CreateBulkActivationKeys")]
        [Produces(typeof(List<BulkActivationViewModel>))]
        public async Task<IActionResult> CreateBulkActivationKeys([FromBody] List<BulkActivationViewModel> bulkActivation, CancellationToken ct = default(CancellationToken))
        {
            if (bulkActivation.Count != 0)
            {
                try
                {
                    List<BulkActivationViewModel> rejected = new List<BulkActivationViewModel>();

                    foreach (var item in bulkActivation)
                    {
                        var show = await _ELIService.GetShowbyShowKey(item.ShowKey, ct);
                        var user = await _ELIAuthService.GetUserByEmail(item.Email, ct);

                        if (show != null && user != null)
                        {
                            await _ELIService.GenerateActivationKeys(show.ShowId, user.Id, item.Quantity);
                        }
                        else if (show != null && user == null)
                        {
                            UserViewModel u = new UserViewModel();
                            u = _mapper.Map<UserViewModel>(item);
                            u.Password = DefaultPassword;
                            var UserCreated = await new UsersController(_ELIAuthService, _ELIService, _emailSender, _mapper).BulkAccountCreation(u);
                            if (UserCreated != null)
                            {
                                await _ELIService.GenerateActivationKeys(show.ShowId, UserCreated.Id, item.Quantity);
                            }
                            else
                            {
                                throw new AppException("User not created");
                            }
                        }
                        else if (show == null)
                        {
                            rejected.Add(item);
                        }
                    }
                    var Result = new ObjectResult(rejected);
                    return Result;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "No rows valid for key generation" });
            }
        }
        [HttpGet("GetAllRestrictedCodes")]
        [Produces(typeof(List<Activation>))]
        public async Task<IActionResult> GetAllRestrictedCodes(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllRestrictedCodesAsync());
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("ValidateRestrictedCode")]
        public async Task<IActionResult> ValidateRestrictedCode(string code, int showId)  //rehanchange
        {
            if (code != null && showId != 0)
            {
                try
                {
                    var Result = new ObjectResult(await _ELIService.ValidateRestrictedCodeAsync(code, showId));
                    return Result;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Code & Show cannot be empty" });
            }
        }
        [AllowAnonymous]
        [HttpPost("ExportLeadsByActivationKey")]
        public async Task<IActionResult> ExportLeadsByActivationKey(string activationKey)  //rehanchange
        {
            DataTable dt = new DataTable();
            List<ExportLeadsActivationKeyViewModel> dal = new List<ExportLeadsActivationKeyViewModel>();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spExportLeadsByActivationKey";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.Parameters.AddWithValue("@ActivationKey", activationKey);
                        sqlCmd.Parameters.Add("@ActivationKey", SqlDbType.VarChar);
                        sqlCmd.Parameters["@ActivationKey"].Value = activationKey;
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                            //sqlAdapter
                            
                        }
                    }
                }
                var a = dt.Rows.GetEnumerator();

                List<object> rows = new List<object>();
                List<object> columns = new List<object>();

                foreach (var item in dt.Columns)
                {
                    columns.Add(((System.Data.DataColumn)item).ColumnName);
                }
                rows.Add(columns);

                foreach (DataRow row in dt.Rows)
                {
                    rows.Add(row.ItemArray);
                }

                return new ObjectResult(rows);
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("ExportLeadsByShowKey")]
        public async Task<IActionResult> ExportLeadsByShowKey(string showKey,int userId)  //rehanchange
        {
            DataTable dt = new DataTable();
            List<ExportLeadsActivationKeyViewModel> dal = new List<ExportLeadsActivationKeyViewModel>();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spExportLeadsByShowKey";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.Parameters.AddWithValue("@ActivationKey", activationKey);
                        sqlCmd.Parameters.Add("@ShowId", SqlDbType.VarChar);
                        sqlCmd.Parameters["@ShowId"].Value = showKey;
                        sqlCmd.Parameters.Add("@UserId", SqlDbType.Int);
                        sqlCmd.Parameters["@UserId"].Value = userId;
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                            //sqlAdapter

                        }
                    }
                }
                var a = dt.Rows.GetEnumerator();

                List<object> rows = new List<object>();
                List<object> columns = new List<object>();

                foreach (var item in dt.Columns)
                {
                    columns.Add(((System.Data.DataColumn)item).ColumnName);
                }
                rows.Add(columns);

                foreach (DataRow row in dt.Rows)
                {
                    rows.Add(row.ItemArray);
                }

               

                return new ObjectResult(rows);
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("UpdateKey")]
        public async Task<IActionResult> UpdatePassword(string UpdateKey)
        {
            DataTable dt = new DataTable();
            List<ExportLeadsActivationKeyViewModel> dal = new List<ExportLeadsActivationKeyViewModel>();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spUpdateActivationKey";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        //sqlCmd.Parameters.AddWithValue("@ActivationKey", activationKey);
                        sqlCmd.Parameters.Add("@ActivationKey", SqlDbType.VarChar);
                        sqlCmd.Parameters["@ActivationKey"].Value = UpdateKey;
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
                return Ok(new { message = "updated" });
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
