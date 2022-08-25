using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using ELI.Data.Repositories.Main;
using ELI.Data.Repositories.Main.Extensions;
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
        private readonly IEmailSender _EmailSender;
        public StudentController(IELIService eLIService, IHostingEnvironment appEnvironment, IEmailSender emailSender)
        {
            _ELIService = eLIService;
            _appEnvironment = appEnvironment;
            _EmailSender = emailSender;
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
        [HttpPut("deleteStudent")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> DeleteStudent([FromBody] StudentRegistration studentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.DeleteStudent(studentVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateProfilePic")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> UpdateStudentProfilePicAsync([FromBody] StudentRegistration studentVM)
        {
            try
            {
                return new ObjectResult(await _ELIService.UpdateStudentProfilePicAsync(studentVM));
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

        [HttpGet("getAllPaymentStudentByGroupId")]
        [Produces(typeof(List<PaymentsViewModel>))]
        public async Task<IActionResult> GetAllPaymentStudentByGroupIdAsync([FromQuery] int studentID)
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllPaymentStudentByGroupIdAsync(studentID));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("uploadDocuments")]
       // [Produces(typeof(StudentDocuments))]
        public async  Task<IActionResult> UploadDocuments([FromForm] StudentDocuments documents)
        {
            // List<int> Ids = new List<int>();
            try
            {
                int documentId = 0;
                foreach (var formFile in documents.Files)
                {
                    if (formFile.Length > 0)
                    {


                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                        var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "www/Images", fileName);
                        documents.FilePath = new List<string>();
                        using (var stream = new FileStream(SavePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            documents.FilePath.Add(SavePath);
                            documents.FileName = fileName;
                        }
                        documentId = await _ELIService.UploadDocuments(documents);
                        //   Ids.Add(documentId);
                    }
                }

                return Ok(documentId);
            }
            catch (Exception ex)
            {
                LogMessage.LogMessageNow(ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            
        }


        [HttpPost("emailSend")]
        [Produces(typeof(bool))]
        public async Task<IActionResult> EmailSendWithDocument([FromBody] EmailSendVM emailSendVM)
        {
            try
            {
                return new ObjectResult(await _EmailSender.SendRegistrationEmailWithDocument(emailSendVM));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
           
        }

        [HttpPost("documentGetByStudentId")]
        [Produces(typeof(byte[]))]
        public async Task<IActionResult> DocumentGet([FromBody] EmailSendVM documentVM)
        {
            IActionResult fileContentResult;
            try
            {
                Stream arrayStream = await this._EmailSender.DocumentGet(documentVM);
                var bytes = new byte[arrayStream.Length];
                arrayStream.Seek(0, SeekOrigin.Begin);
                await arrayStream.ReadAsync(bytes, 0, bytes.Length);
                arrayStream.Dispose();
                base.HttpContext.Response.ContentType = "application/pdf";
                fileContentResult = new FileContentResult(bytes, "application/pdf")
                {
                    FileDownloadName = "test.pdf"
                };
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                (new ExceptionHandlingService(exception, null, null)).LogException();
                fileContentResult = this.BadRequest(new { message = exception.Message });
            }
            return fileContentResult;
        }
        [HttpPost("downloadCertificateZipped")]
        [Produces(typeof(byte[]))]
        public async Task<IActionResult> DownloadCertificateZipped([FromBody] EmailSendVM documentVM)
        {
            var certificates = new List<InMemoryFile>();
            IActionResult fileContentResult;
            try
            {
                documentVM.StudentNames.ForEach(async student =>
                {
                    Stream arrayStream = await this._EmailSender.GetCertificate(student);
                    var bytes = new byte[arrayStream.Length];
                    arrayStream.Seek(0, SeekOrigin.Begin);
                    await arrayStream.ReadAsync(bytes, 0, bytes.Length);
                    arrayStream.Dispose();
                    base.HttpContext.Response.ContentType = "application/zip";
                    fileContentResult = new FileContentResult(bytes, "application/pdf")
                    {
                        FileDownloadName = "test.pdf"
                    };
                    InMemoryFile file = new InMemoryFile();
                    file.FileName = student;
                    file.Content = bytes;
                    certificates.Add(file);
                });
   
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                (new ExceptionHandlingService(exception, null, null)).LogException();
                fileContentResult = this.BadRequest(new { message = exception.Message });
            }
            var archive = GetZipArchive(certificates);
            fileContentResult = new FileContentResult(archive, "application/zip");
            return fileContentResult;
        }
        public static byte[] GetZipArchive(List<InMemoryFile> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.FileName + ".pdf", CompressionLevel.Fastest);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }

                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }


    }
}
public class InMemoryFile
{
    public string FileName { get; set; }
    public byte[] Content { get; set; }
}