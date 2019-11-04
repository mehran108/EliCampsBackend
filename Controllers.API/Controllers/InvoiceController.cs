using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using ELI.Domain.ViewModels;
using ELI.Domain.Services;
using ELI.Entity.Main;
using AutoMapper;
using ELI.Domain.Helpers;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class InvoiceController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        public InvoiceController(IELIService ELISupervisor, IMapper mapper)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
        }
        [HttpPost("CreateInvoice")]
        [Produces(typeof(List<Invoice>))]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceViewModel invoiceVM)
        {
            if (invoiceVM != null)
            {
                Invoice invoice = new Invoice();
                invoice = _mapper.Map<Invoice>(invoiceVM);
                invoice.IsDeleted = false;
                invoice.IsActive = true;
                invoice.CreatedDate = DateTime.Now;
                try
                {
                    var invoiceResult = new ObjectResult(await _ELIService.CreateInvoiceAsync(invoice));
                    return invoiceResult;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Invoice model cannot be null" });
            }
        }
        [HttpPut("UpdateInvoice")]
        public async Task<IActionResult> UpdateInvoice([FromBody] CreateInvoiceViewModel UpdateInvoiceVM)
        {
            if (UpdateInvoiceVM != null)
            {
                try
                {
                    Invoice invoiceTemp = new Invoice();
                    invoiceTemp = _mapper.Map<Invoice>(UpdateInvoiceVM);
                    await _ELIService.UpdateInvoiceAsync(invoiceTemp);
                    return Ok(new { message = "Invoice Updated" });
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Update invoice model cannot be null" });
            }
        }
        [HttpGet("GetInvoiceByActivationKey")]
        public async Task<IActionResult> GetInvoiceByActivationKey(string activationKey, CancellationToken ct = default(CancellationToken))
        {
            if (activationKey != "")
            {
                try
                {
                    var invoice = new ObjectResult(await _ELIService.GetInvoicebyActivationKeyAsync(activationKey, ct));
                    return invoice;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "ActivationKey cannot be empty" });
            }
        }
    }
}
