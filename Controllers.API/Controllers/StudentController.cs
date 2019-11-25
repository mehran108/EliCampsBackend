using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ELI.Data.Repositories.Main;
using ELI.Domain.Helpers;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELI.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Student")]
    public class StudentController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IELIService _ELIService;
        public StudentController(IELIService eLIService, IHostingEnvironment appEnvironment)
        {
            _ELIService = eLIService;
            _appEnvironment = appEnvironment;
        }

        [HttpPost("createStudent")]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentRegistration studentVM)
        {

            if (studentVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.AddStudentAsync(studentVM));
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
                return BadRequest(new { message = "Student model cannot be empty" });
            }
        }

        [HttpPut("updateStudent")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] StudentRegistration studentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateStudentAsync(studentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getStudent")]
        [Produces(typeof(StudentRegistration))]
        public async Task<IActionResult> GetStudentAsync(int studentID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetStudentAsync(studentID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activateStudent")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivateStudentAsync([FromBody] StudentRegistration studentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivateStudentAsync(studentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllStudent")]
        [Produces(typeof(List<StudentRegistration>))]
        public async Task<IActionResult> GetAllStudentAsync([FromQuery] StudentRegistration studentVM)
        {
            try
            {
                AllRequest<StudentRegistration> studentlist = new AllRequest<StudentRegistration>();
                studentlist.Data = studentVM;
                return new ObjectResult(await _ELIService.GetAllStudentAsync(studentlist));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("createPaymentStudent")]
        public async Task<IActionResult> AddPaymentStudentAsync([FromBody] PaymentsViewModel paymentStudentVM)
        {

            if (paymentStudentVM != null)
            {
                try
                {
                    var showResult = new ObjectResult(await _ELIService.AddPaymentStudentAsync(paymentStudentVM));
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
                return BadRequest(new { message = "Payment model cannot be empty" });
            }
        }

        [HttpPut("updatePaymentStudent")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdatePaymentStudentAsync([FromBody] PaymentsViewModel paymentStudentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdatePaymentStudentAsync(paymentStudentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("getPaymentStudent")]
        [Produces(typeof(PaymentsViewModel))]
        public async Task<IActionResult> GetPaymentStudentAsync(int paymentStudentID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetPaymentStudentAsync(paymentStudentID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("activatePaymentStudent")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> ActivatePaymentStudentAsync([FromBody] PaymentsViewModel paymentStudentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.ActivatePaymentStudentAsync(paymentStudentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getAllPaymentStudentByStudentId")]
        [Produces(typeof(List<PaymentsViewModel>))]
        public async Task<IActionResult> GetAllPaymentStudentByStudentIdAsync([FromQuery] int studentID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllPaymentStudentByStudentIdAsync(studentID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}