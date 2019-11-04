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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        string RegionId;
        public DiscountController(IELIService ELISupervisor, IMapper mapper)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            RegionId = config.GetConnectionString("RegionId");
        }
        [HttpGet("GetDicountCodes")]
        [Produces(typeof(List<DiscountViewModel>))]
        public async Task<IActionResult> GetDicountCodes(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllDiscountCodesAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost("CreateDiscount")]
        [Produces(typeof(List<Qualifier>))]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountViewModel disCountVM)
        {
            if (disCountVM != null)
            {
                Discount discount = new Discount();
                discount = _mapper.Map<Discount>(disCountVM);
                discount.IsDeleted = false;
                discount.IsActive = true;
                discount.IsConsumed = false;
                discount.CreatedDate = DateTime.Now;
                try
                {
                    var discountResult = new ObjectResult(await _ELIService.CreateDiscountAsync(discount));
                    return discountResult;
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Discount model not valid" });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById(int id, CancellationToken ct = default(CancellationToken))
        {
            if (id != 0)
            {
                try
                {
                    var discount = new ObjectResult(await _ELIService.GetDiscountByIdAsync(id, ct));
                    return discount;
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
        [HttpGet("GetDiscountTypes")]
        public async Task<IActionResult> GetDiscountTypes(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                var discountTypes = new ObjectResult(await _ELIService.GetAllDiscountTypes(ct));
                return discountTypes;
            }
            catch (AppException ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("DiscountCodeDuplicationCheck/{discountCode}")]
        public async Task<IActionResult> DiscountCodeDuplicationCheck(string discountCode)  //rehanchange
        {
            if (discountCode != "")
            {
                try
                {
                    var result = _ELIService.DiscountCodeDuplicationCheck(discountCode);
                    if (result == false)
                        return Ok(new { message = "Discount Code Available" });
                    else
                    {
                        return BadRequest(new { message = "Discount Code is already registered" });
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
                return BadRequest(new { message = " Discount Code cannot be empty" });
            }
        }
        [HttpGet("GetActiveDicountCodes")]
        [Produces(typeof(List<Discount>))]
        public async Task<IActionResult> GetActiveDicountCodes(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetActiveDiscountCodesAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateDiscountCode")]
        public async Task<IActionResult> UpdateDiscountCode([FromBody] CreateDiscountViewModel disCountVM)
        {
            if (disCountVM != null)
            {
                Discount discount = new Discount();
                discount = _mapper.Map<Discount>(disCountVM);
                try
                {
                    await _ELIService.UpdateDiscountAsync(discount);
                    return Ok(new { message = "Discount Code Updated" });
                }
                catch (AppException ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = " Discount Model cannot be null" });
            }
        }
        [HttpGet("GetDicountCodesByShowId")]
        [Produces(typeof(List<Discount>))]
        public async Task<IActionResult> GetDicountCodesByShowId(int showId, CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetShowDiscountsRelationsAsync(showId, ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}