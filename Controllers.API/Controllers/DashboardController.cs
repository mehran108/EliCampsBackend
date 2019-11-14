using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
//using AutoMapper.Configuration;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IELIAuthService _ELIAuthService;
        string AuthConString;
        string ConString;
        public DashboardController(IELIService ELISupervisor, IELIAuthService ELIAuthService, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = configuration.GetConnectionString("ELIDb");
            AuthConString = config.GetConnectionString("ELIAuthDb");
            _ELIService = ELISupervisor;
            _ELIAuthService = ELIAuthService;
        }
        [HttpGet("getshows")]
        [Produces(typeof(List<ShowViewModel>))]
        public async Task<IActionResult> GetShows(int userId, string role, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllShowByRoleAsync(role, userId, isDashboard, ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("getusers")]
        [Produces(typeof(List<UserViewModel>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers(string role, CancellationToken ct = default(CancellationToken))
        {
            if (role != "")
            {
                if (role.Contains("Admin"))
                {
                    //try
                    //{
                    //    return new ObjectResult(await _ELIAuthService.GetAllAsync(ct));
                    //}
                    //catch (Exception ex)
                    //{
                    //    new ExceptionHandlingService(ex, null, null).LogException();
                    //    return BadRequest(new { message = ex.Message });
                    //}
                    DataTable dt = new DataTable();
                    List<Users> dal = new List<Users>();
                    try
                    {
                        using (SqlConnection sqlConn = new SqlConnection(ConString))
                        {
                            string sql = "spGetAllUsers";
                            using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                            {
                                sqlCmd.CommandType = CommandType.StoredProcedure;
                                sqlConn.Open();
                                using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                                {
                                    sqlAdapter.Fill(dt);
                                }
                            }
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //u.id,u.email,u.FirstName,u.LastName,ur.RoleId
                            Users da = new Users();
                            da.Id = dt.Rows[i]["id"] == DBNull.Value ? 0 : (int)dt.Rows[i]["id"];
                            da.Email = dt.Rows[i]["email"] + "";
                            da.FirstName = dt.Rows[i]["FirstName"] + "";
                            da.SurName = dt.Rows[i]["SurName"] + ""; ;

                            int roleid = dt.Rows[i]["RoleId"] == DBNull.Value ? 0 : (int)dt.Rows[i]["RoleId"];
                            if (roleid == 2)
                            {
                                da.AuthUserRoles.Add(new UserRoles { RoleId = roleid, UserId = da.Id, Role = new Roles { Name = "Exhibitor" } });
                            }
                            else if (roleid == 1)
                            {
                                da.AuthUserRoles.Add(new UserRoles { RoleId = roleid, UserId = da.Id, Role = new Roles { Name = "Admin" } });
                            }
                            else
                            {
                                da.AuthUserRoles.Add(new UserRoles { RoleId = roleid, UserId = da.Id, Role = new Roles { Name = "Exhibitor" } });
                            }

                            dal.Add(da);
                        }
                        return new ObjectResult(dal);
                    }
                    catch (Exception ex)
                    {
                        new ExceptionHandlingService(ex, null, null).LogException();
                        return BadRequest(new { message = ex.Message });
                    }
                }
                else
                {
                    return BadRequest(new { message = "Only Admin can view all users." });
                }
            }
            else
            {
                return BadRequest(new { message = "Role cannot be empty" });
            }
        }
        [HttpGet("getactivation")]
        [Produces(typeof(List<Activation>))]
        public async Task<IActionResult> GetActivation(int userid, string role, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            DataTable dt = new DataTable();
            List<DashboardActivation> dal = new List<DashboardActivation>();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spDashoboardGetActivation";
                    using (SqlCommand sqlCmd = new SqlCommand(sql, sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@userid", userid);
                        sqlCmd.Parameters.AddWithValue("@role", role.ToString());
                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DashboardActivation da = new DashboardActivation();
                    da.ActivationKey = dt.Rows[i]["ActivationKey"] + "";
                    da.DeviceName = dt.Rows[i]["deviceName"] + "";
                    da.ShowName = dt.Rows[i]["showName"]+"";
                    da.IsActive = dt.Rows[i]["isActive"] + "" == "True" ? true : false;
                    da.IsConsumed = dt.Rows[i]["IsConsumed"] + "" == "True" ? true : false;
                    da.InvoiceId = dt.Rows[i]["invoiceId"] == DBNull.Value ? 0:  (int)dt.Rows[i]["invoiceId"];
                    da.Email = dt.Rows[i]["Email"] + "";
                    da.UserName = dt.Rows[i]["FirstName"]+", " + dt.Rows[i]["LastName"] + "";
                    da.manufacturer = dt.Rows[i]["manufacturer"] + "";
                    da.model = dt.Rows[i]["model"] + "";
                    da.platform = dt.Rows[i]["platform"] + "";
                    da.Company = dt.Rows[i]["Company"] + "";
                    da.StandNumber = dt.Rows[i]["StandNumber"] + "";
                    da.Name = dt.Rows[i]["Name"] + "";
                    dal.Add(da);
                }
                return new ObjectResult(dal);
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("heartbeat")]
        //[Produces("application/json")]
        [AllowAnonymous]
        public async Task<IActionResult> HeartBeat(CancellationToken ct = default(CancellationToken))
        {
                return Ok(new { message = "Hi Genius!" });   
        }
    }
}