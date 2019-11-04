using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ELI.Domain.Helpers;
using ELI.Entity.Auth;
using System.Threading.Tasks;
using ELI.Data.Repositories.Auth;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Domain.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ELI.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IELIAuthService _ELIAuthService;
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        string ConString ;

        public UsersController(
            IELIAuthService ELIAuthService, IELIService ELIService,
            IEmailSender emailSender,
            IMapper mapper
            )
        {
            _ELIAuthService = ELIAuthService;
            _ELIService = ELIService;
            _emailSender = emailSender;
            _mapper = mapper; ;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            ConString = config.GetConnectionString("ELIDb");

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserViewModel userVM)
        {
            if (userVM != null)
            {
                try
                {
                    var user = await _ELIAuthService.AuthenticateAsync(userVM.Email, userVM.Password);
                    if (user == null)
                        return BadRequest(new { message = "Email or password is incorrect" });
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var Encryptionkey = _ELIService.GetEncryptionKey(LookupValueEnum.EncryptionKey);
                    var key = Encoding.ASCII.GetBytes(Encryptionkey.Description.ToString());// _appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    // return basic user info (without password) and token to store client side

                    List<string> roles = new List<string>();
                    foreach (var role in user.AuthUserRoles)
                    {
                        roles.Add(role.Role.Name.ToString());
                    }
                    return Ok(new
                    {
                        Id = user.Id, //user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        SurName = user.SurName,
                        Token = tokenString,
                        UserRoles = roles,/*user.AuthUserRoles*/
                        Message = "Successfully  Logged in",
                        TempUser = user.TempUser,
                        Company = user.Company
                    });
                }
                catch (Exception exp)
                {
                    new ExceptionHandlingService(exp, null, null).LogException();
                    return BadRequest(new { message = exp.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "User model cannot be empty" });
            }
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserViewModel userVM)  //rehanchange
        {
            if (userVM != null)
            {
                Users user = new Users();
                if (userVM.RoleId == 0)
                {
                    userVM.RoleId = 2;
                }
                user = _mapper.Map<Users>(userVM);
                user.IsDeleted = false;
                user.IsActive = true;
                user.CreatedDate = DateTime.Now;
                try
                {
                    var result = new ObjectResult(await _ELIAuthService.CreateAsync(user, userVM.Password));
                    var roleEntry = await _ELIAuthService.RoleAddAsync(user.Id, userVM.RoleId);
                    //await _emailSender.SendEmail(null, user.Email, EmailTemplate.Welcome);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var Encryptionkey = _ELIService.GetEncryptionKey(LookupValueEnum.EncryptionKey);
                    var key = Encoding.ASCII.GetBytes(Encryptionkey.Description.ToString());// _appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    var userCreated = await _ELIAuthService.AuthenticateAsync(userVM.Email, userVM.Password);
                    List<string> roles = new List<string>();
                    foreach (var role in userCreated.AuthUserRoles)
                    {
                        roles.Add(role.Role.Name.ToString());
                    }
                    return Ok(new
                    {
                        Id = userCreated.Id, //user.Id,
                        Username = userCreated.UserName,
                        Email = userCreated.Email,
                        FirstName = userCreated.FirstName,
                        LastName = userCreated.LastName,
                        Token = tokenString,
                        UserRoles = roles,/*user.AuthUserRoles*/
                        Message = "Account has been successfully created"
                    });

                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "User model cannot be empty" });
            }
        }
        [AllowAnonymous]
        [HttpPost("EmailDuplicationCheck/{email}")]
        public async Task<IActionResult> EmailDuplicationCheck(string email)  //rehanchange
        {
            if (email != "")
            {
                try
                {
                    var result = _ELIAuthService.EmailDuplicationCheck(email);
                    if (result == false)
                        return Ok(new { message = "Email Available" });
                    else
                    {
                        return BadRequest(new { message = "Email is already registered" });
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
                return BadRequest(new { message = "Email cannot be empty" });
            }
        }

        [AllowAnonymous]
        [HttpPost("GetAppURLs")]
        public async Task<IActionResult> GetAppURLs()  //rehanchange
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    string sql = "spDashoboardGetActivation";
                    using (SqlCommand sqlCmd = new SqlCommand("select v.* from LookupValue v inner join LookupTable t on v.lookuptableid = t.id where t.name like 'APP_URL'", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.Text;

                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
                return Ok(dt.Rows);
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok();
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            if (id != 0)
            {
                try
                {
                    var user = await _ELIAuthService.GetByIdAsync(id);
                    user.PasswordHash = null;
                    user.PasswordSalt = null;
                    return Ok(user);
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
        //[HttpPut("{id}")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserViewModel user)
        {
            if (user != null)
            {
                try
                {
                    Users user1 = new Users();
                    user1 = _mapper.Map<Users>(user);
                    try
                    {
                        await _ELIAuthService.UpdateAsync(user1, user.Password);
                        await _ELIAuthService.RoleUpdateAsync(user1.Id, user.RoleId);
                        return Ok(new { message = "User Updated" });
                    }
                    catch (AppException ex)
                    {
                        new ExceptionHandlingService(ex, null, null).LogException();
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
                return BadRequest(new { message = "User model cannot be empty" });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok();
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (model != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var user = await _ELIAuthService.ForgetAuthentication(model.Email);
                        if (user == null)
                        {
                            return BadRequest(new { message = "We couldn't find an account associated with your email address." });
                        }
                        var resetPassword = _emailSender.GeneratePasswordResetTokenAsync();
                        user.SecurityStamp = resetPassword.SecurityStamp;
                        await _ELIAuthService.UpdateAsync(user);
                        await _emailSender.SendEmail(resetPassword.SecurityStamp, user.Email, EmailTemplate.ForgetPasswordEmail);
                        return Ok(new { message = "We have emailed your reset password link." });
                    }
                    // If we got this far, something failed, redisplay form
                    return BadRequest(ModelState);
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Forget model cannot be empty" });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _ELIAuthService.ForgetAuthentication(model.Email);
                    if (user == null)
                    {
                        return BadRequest(new { message = "We couldn't find an account associated with your email address." });
                    }
                    else
                    {
                        if (user.SecurityStamp != model.SecurityStamp || user.SecurityStamp == null)
                        {
                            return BadRequest(new { message = "Sorry, the link has been expired. Please use forget password to request a new link." });
                        }
                        user.SecurityStamp = null;
                        await _ELIAuthService.UpdateAsync(user, model.NewPassword);
                        await _emailSender.SendEmail(null, user.Email, EmailTemplate.ResetSuccessfully);
                    }
                    return Ok(new { message = "Password reset successfully!" });
                }
                return BadRequest(ModelState);
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordViewModel UpdatePasswordVM)
        {
            if (UpdatePasswordVM != null)
            {
                try
                {
                    var result = await _ELIAuthService.UpdatePasswordAsync(UpdatePasswordVM);
                    if (result == true)
                    {
                        return Ok(new { message = "Password Updated" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Password not updated" });
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
                return BadRequest(new { message = "Update model cannot be empty" });
            }
        }
        [AllowAnonymous]
        [HttpPost("BulkAccountCreation")]
        public async Task<Users> BulkAccountCreation([FromBody] UserViewModel userVM)  //rehanchange
        {
            Users user = new Users();
            if (userVM.RoleId == 0)
            {
                userVM.RoleId = 2;
            }
            user = _mapper.Map<Users>(userVM);
            user.IsDeleted = false;
            user.IsActive = true;
            user.CreatedDate = DateTime.Now;
            user.TempUser = true;
            try
            {
                var UserResult = await _ELIAuthService.CreateAsync(user, userVM.Password);
                var roleEntry = await _ELIAuthService.RoleAddAsync(user.Id, userVM.RoleId);
                await _emailSender.SendEmail(null, user.Email, EmailTemplate.BulkCodeAccount);
                UserResult.PasswordHash = null; UserResult.PasswordSalt = null;
                return UserResult;
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return user;
            }


        }

        [AllowAnonymous]
        [HttpPost("GetAusCld")]
        public async Task<IActionResult> GetAusCld()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    using (SqlCommand sqlCmd = new SqlCommand("select description from LookupValue where name like 'AusCids'", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.Text;

                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
                return Ok(dt.Rows);
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        [AllowAnonymous]
        [HttpGet("GetInvoiceLKP")]
        public async Task<IActionResult> GetInvoiceLKP()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sqlConn = new SqlConnection(ConString))
                {
                    using (SqlCommand sqlCmd 
                        = new SqlCommand
                        ("select v.name,v.description from LookupValue v" 
                        + " inner join LookupTable t on v.lookupTableId = t.Id" 
                        +" where t.Name like 'Invoice_LKP'", sqlConn))
                    {
                        sqlCmd.CommandType = CommandType.Text;

                        sqlConn.Open();
                        using (SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCmd))
                        {
                            sqlAdapter.Fill(dt);
                        }
                    }
                }
                string Company="",Address="",Contact="",Site_URL="",ABN = "";

                foreach (DataRow r in dt.Rows)
                {
                    var name = r["name"]+"";
                    switch(name)
                    {
                        case "Company":
                            Company = r["description"] + "";
                            break;
                        case "Address":
                            Address = r["description"] + "";
                            break;
                        case "Contact":
                            Contact = r["description"] + "";
                            break;
                        case "site_url":
                            Site_URL = r["description"] + "";
                            break;
                        case "ABN":
                            ABN = r["description"] + "";
                            break;
                        default:
                            break;
                    }
                }
                return Ok(new
                {
                    company = Company,
                    address = Address,
                    contact = Contact,
                    site_URL = Site_URL,
                    abn = ABN
                }
                    );
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
