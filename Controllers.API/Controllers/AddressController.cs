using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ELI.Domain.Services;
using ELI.Entity.Main;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        public AddressController(IELIService ELISupervisor, IMapper mapper)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("GetCountries")]
        [Produces(typeof(List<Countries>))]
        public async Task<IActionResult> GetCountries(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllCountries(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetRegions")]
        [Produces(typeof(List<Region>))]
        public async Task<IActionResult> GetRegions(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllRegions(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetCities")]
        [Produces(typeof(List<Cities>))]
        public async Task<IActionResult> GetCitiesbyCountry(string countryName, CancellationToken ct = default(CancellationToken))
        {
            if (countryName != "")
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllCities(countryName, ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "Country Name cannot be empty"});
            }
        }
        [AllowAnonymous]
        [HttpGet("GetStates")]
        [Produces(typeof(List<States>))]
        public async Task<IActionResult> GetStates(int CountryId, CancellationToken ct = default(CancellationToken))
        {
            if (CountryId != 0)
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllStates(CountryId, ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message ="CountryId cannot be null"});
            }
        }
        [AllowAnonymous]
        [HttpGet("GetRegionById")]
        [Produces(typeof(List<Region>))]
        public async Task<IActionResult> GetRegionById(int regionId, CancellationToken ct = default(CancellationToken))
        {
            if (regionId != 0)
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllRegions(ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "RegionId cannot be null" });
            }
        }


        [AllowAnonymous]
        [HttpGet("GetContactInformation")]
        [Produces(typeof(List<Region>))]
        public async Task<IActionResult> GetContactInformation(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetContactInformation(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}