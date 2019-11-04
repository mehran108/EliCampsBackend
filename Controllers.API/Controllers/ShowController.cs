using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ELI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ELI.Entity.Main;
using ELI.Domain.Services;
using ELI.Domain.ViewModels;
using ELI.Domain.Helpers;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;

namespace ELI.API.Controllers
{
    [Route("api/[controller]")]
    public class ShowController : Controller
    {
        private readonly IELIService _ELIService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _appEnvironment;
        string RegionId;
        string ImgPath;
        public ShowController(IELIService ELISupervisor, IMapper mapper, IHostingEnvironment appEnvironment)
        {
            _ELIService = ELISupervisor;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.json", true)
            .AddEnvironmentVariables();
            var config = builder.Build();
            ImgPath = config.GetSection("ConnectionStrings").GetSection("ImagePathKey").Value;
            RegionId = config.GetConnectionString("RegionId");
        }
        [HttpGet("GetShows")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> GetShows(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllShowAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetRShows")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> GetRShows(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetAllRShowAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        //[HttpGet("GetGridShow")]
        //[Produces(typeof(List<ShowViewModel>))]
        //public async Task<IActionResult> GetGridShow(int key, int length, CancellationToken ct = default(CancellationToken))
        //{
        //    try
        //    {
        //        return new ObjectResult(await _ELIService.GetLimitedShowAsync(key, length, ct));
        //    }
        //    catch (Exception ex)
        //    {
        //        new ExceptionHandlingService(ex, null, null).LogException();
        //        // return error message if there was an exception
        //        return BadRequest(new { message = "Something went wrong!!! Try Again later" });
        //    }
        //}
        [HttpGet("GetAllshowsByRegion")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> GetAllshowsByRegion(int RegionId, string DeviceIdentifier, CancellationToken ct = default(CancellationToken))
        {
            if (RegionId != 0 && DeviceIdentifier != "")
            {
                try
                {
                    return new ObjectResult(await _ELIService.GetAllShowByRegionAsync(RegionId, DeviceIdentifier, ct));
                }
                catch (Exception ex)
                {
                    new ExceptionHandlingService(ex, null, null).LogException();
                    return BadRequest(new { message = ex.Message });
                }
            }
            else
            {
                return BadRequest(new { message = "RegionId & Device Identifier cannot be empty" });
            }
        }
        [HttpPost("CreateShows")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> CreateShows([FromBody] ShowViewModel showVM, CancellationToken ct = default(CancellationToken))
        {
            if (showVM != null)
            {
                Show show = new Show();
                var region = await _ELIService.GetRegionById(Convert.ToInt32(RegionId));
                ShowPricing showPricing = new ShowPricing();
                Pricing pricing = new Pricing();
                show = _mapper.Map<Show>(showVM);
                show.IsDeleted = false;
                show.IsActive = true;
                show.RegionId = region.RegionId;
                show.CreatedBy = showVM.CreatedBy;


                //show.Logo = showVM.Logo;
                show.CreatedDate = DateTime.Now;
                showPricing = _mapper.Map<ShowPricing>(showVM);
                showPricing.IsDeleted = false;
                showPricing.CreatedDate = DateTime.Now;
                pricing = _mapper.Map<Pricing>(showVM);
                pricing.RegionId = region.RegionId;
                pricing.CurrencyIso = region.CurrencyIso;
                pricing.IsDeleted = false;
                pricing.IsDefault = false;
                try
                {
                    if (showVM.Logo != "")
                    {
                        string imageName = showVM.ShowKey + ".jpg";
                        string imgPath = Path.Combine(ImgPath, imageName);
                        byte[] imageBytes = Convert.FromBase64String(showVM.Logo.Replace("data:image/jpeg;base64,", ""));
                        Image image;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            image = Image.FromStream(ms);
                            image.Save(imgPath);
                        }
                        show.Logo = imageName;
                    }
                    else
                    {
                        show.Logo = null;
                    }
                    var pricingResult = await _ELIService.CreatePricingAsync(pricing);
                    var showResult = new ObjectResult(await _ELIService.CreateShowAsync(show));
                    showPricing.ShowId = show.ShowId;
                    showPricing.PricingId = pricing.PricingId;
                    var showPricingResult = new ObjectResult(await _ELIService.CreateShowPricingAsync(showPricing));
                    if (showVM.DiscountCodes != null && showVM.DiscountCodes.Count != 0)
                    {
                        foreach (var Code in showVM.DiscountCodes)
                        {
                            var discountCode = await _ELIService.GetDiscountByIdAsync(Code);
                            if (discountCode != null && discountCode.IsConsumed == false)
                            {
                                ShowDiscount SD = new ShowDiscount();
                                SD.DiscountId = discountCode.DiscountId;
                                SD.ShowId = show.ShowId;
                                await _ELIService.CreateShowDiscountAsync(SD,ct);
                                discountCode.IsConsumed = true;
                                await _ELIService.UpdateDiscountAsync(discountCode);
                            }
                            else
                            {
                                return BadRequest(new { message = "Discount Code already used." });
                            }
                        }

                    }
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
                return BadRequest(new { message = "Show model cannot be empty" });
            }
        }
        [HttpPost("ActiveShow")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> ActiveShow([FromBody] ActiveShowViewModel activeShowVM, CancellationToken ct = default(CancellationToken))
        {
            if (activeShowVM != null)
            {
                Sduactivation activation = new Sduactivation();
                Device device = new Device();
                activation = _mapper.Map<Sduactivation>(activeShowVM);
                activation.IsDeleted = false;
                activation.IsActive = true;
                device = _mapper.Map<Device>(activeShowVM);
                device.IsDeleted = false;
                device.IsActive = true;
                try
                {
                    var activationKey = await _ELIService.GetActivationByIdAsync(activeShowVM.ActivationKey);
                    if (activationKey != null && activationKey.ActivationTypeId == 7)//Activation key does not exist
                    {
                        var sduactivation = await _ELIService.GetSDUActivationByActivationIdAsync(activationKey.ActivationId);
                        if (sduactivation != null)
                        {
                            var show = await _ELIService.CheckShowActivationAsync(sduactivation.ShowId, ct);

                            //if (sduactivation != null && sduactivation.ShowId == activeShowVM.ShowId)//activation key is purchased for show
                            //{
                            if (show != null)
                            {
                                if (sduactivation.IsConsumed == false)
                                {
                                    var devicetobeCreated = await _ELIService.GetDeviceByDeviceIdentifierAsync(activeShowVM.DeviceIdentifier);

                                    if (devicetobeCreated == null)
                                    {
                                        var deviceResult = await _ELIService.CreateDeviceAsync(device);
                                        activation.DeviceId = deviceResult.DeviceId;
                                        activation.IsConsumed = true;
                                        activation.SduactivationId = sduactivation.SduactivationId;
                                        var sduActivationResult = new ObjectResult(await _ELIService.UpdateSduActivationAsync(activation));
                                        return sduActivationResult;
                                    }
                                    else
                                    {
                                        //update device info :todo
                                        var activatedShow = await _ELIService.GetSDUActivationByDeviceAsync(sduactivation.ShowId, devicetobeCreated.DeviceId);
                                        if (activatedShow == null)
                                        {
                                            activation.DeviceId = devicetobeCreated.DeviceId;
                                            activation.IsConsumed = true;
                                            activation.SduactivationId = sduactivation.SduactivationId;
                                            var sduActivationResult = new ObjectResult(await _ELIService.UpdateSduActivationAsync(activation));
                                            return sduActivationResult;
                                        }
                                        else
                                        {
                                            return Ok(new { message = "This show is already activated against this device" });
                                        }
                                    }
                                }
                                else
                                {
                                    var device1 = await _ELIService.GetDeviceByIdAsync(sduactivation.DeviceId == null ? (int)sduactivation.DeviceId : -99);
                                    if (device1.DeviceIdentifier == activeShowVM.DeviceIdentifier)
                                    {
                                        return Ok(new { message = "Device Activated" });
                                    }
                                    return Ok(new { message = "Activation Key is already used" });
                                }
                            }
                            else
                            {
                                return Ok(new { message = "Show you are trying to activate is expired" });
                            }
                        }
                        else
                        {
                            return Ok(new { message = "Activation Key is already used" });
                        }


                        //}
                        //else
                        //{
                        //    return BadRequest(new { message = "Key is not Valid for this show." });
                        //}
                    }
                    else
                    {
                        return Ok(new { message = "Wrong Activation Key" });
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
                return Ok(new { message = "Activate show model cannot be empty" });
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShowById(int id, CancellationToken ct = default(CancellationToken))
        {
            if (id != 0)
            {
                try
                {
                    var show = new ObjectResult(await _ELIService.GetShowByIdAsync(id, ct));
                    return show;
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
        [HttpPut("UpdateShow")]
        public async Task<IActionResult> UpdateShow([FromBody] ShowViewModel showVM, CancellationToken ct = default(CancellationToken))
        {
            if (showVM != null)
            {
                try
                {
                    Show showTemp = new Show();
                    showTemp = _mapper.Map<Show>(showVM);
                    try
                    {
                        if (showVM.Logo != "")
                        {
                            //set the image Name
                            string imageName = showVM.ShowKey + ".jpeg";
                            if (showVM.Logo == "" || !showVM.Logo.Contains("jpeg"))
                            {
                                showTemp.Logo = "";
                            }
                            else if ((showVM.Logo).Contains(imageName))
                            {
                                showTemp.Logo = imageName;
                            }
                            else
                            {
                                string imgPath = Path.Combine(ImgPath, imageName);
                                byte[] imageBytes = Convert.FromBase64String(showVM.Logo.Replace("data:image/jpeg;base64,", ""));
                                Image image;
                                using (MemoryStream ms = new MemoryStream(imageBytes))
                                {
                                    image = Image.FromStream(ms);
                                    image.Save(imgPath);
                                }
                                showTemp.Logo = imageName;
                            }
                        }
                        else
                        {
                            showTemp.Logo = null;
                        }
                        //showTemp.Logo = null;
                        showTemp.UpdatedBy = showVM.UpdatedBy;
                        await _ELIService.UpdateShowAsync(showTemp);
                        var pricing = await _ELIService.GetPricingByShowIdAsync(showTemp.ShowId);
                        if (pricing != null)
                        {
                            pricing.KeyAmount = showVM.KeyAmount;
                            pricing.Tax = showVM.Tax;
                            pricing.TaxName = showVM.TaxName;
                            await _ELIService.UpdatePricingAsync(pricing);
                        }
                        if (showVM.DiscountCodes != null && showVM.DiscountCodes.Count != 0)
                        {
                            await _ELIService.DeleteShowDiscountsRelationsAsync(showTemp.ShowId, ct);
                            foreach (var Code in showVM.DiscountCodes)
                            {
                                var discountCode = await _ELIService.GetDiscountByIdAsync(Code);
                                if (discountCode != null)
                                {
                                    var showDiscount = await _ELIService.GetShowDiscountRelationAsync(showTemp.ShowId, discountCode.DiscountId, ct);
                                    if (showDiscount == null)
                                    {
                                        ShowDiscount SD = new ShowDiscount();
                                        SD.DiscountId = discountCode.DiscountId;
                                        SD.ShowId = showTemp.ShowId;
                                        await _ELIService.CreateShowDiscountAsync(SD, ct);
                                        discountCode.IsConsumed = true;
                                        await _ELIService.UpdateDiscountAsync(discountCode);
                                    }
                                }
                                else  
                                {
                                    return BadRequest(new { message = "Discount Code already used." });
                                }
                            }

                        }
                        return Ok(new { message = "Show Updated" });
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
                return BadRequest(new { message = "Show model cannot be null" });
            }
        }
        [AllowAnonymous]
        [HttpPost("ShowKeyDuplicationCheck/{showkey}")]
        public async Task<IActionResult> ShowKeyDuplicationCheck(string showkey)  //rehanchange
        {
            if (showkey != "")
            {
                try
                {
                    var result = _ELIService.ShowKeyDuplicationCheck(showkey);
                    if (result == false)
                        return Ok(new { message = "ShowKey Available" });
                    else
                    {
                        return BadRequest(new { message = "ShowKey is already registered" });
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
                return BadRequest(new { message = "Showkey cannot be empty" });
            }
        }
        [HttpGet("GetShowsforPurchaseActivation")]
        [Produces(typeof(List<Show>))]
        public async Task<IActionResult> GetShowsforPurchaseActivation(CancellationToken ct = default(CancellationToken))
        {
            try
            {
                return new ObjectResult(await _ELIService.GetShowsforPurchaseActivationAsync(ct));
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("ValidateDiscountCode")]
        public async Task<IActionResult> ValidateDiscountCode(string code, int showId)  //rehanchange
        {
            if (code != "" && showId != 0)
            {
                try
                {
                    var Result = new ObjectResult(await _ELIService.ValidateDiscountCodeAsync(code, showId));
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
                return BadRequest(new { message = "Code & ShowId cannot be null" });
            }
        }
        [HttpGet("GetResponse")]
        public async Task<IActionResult> GetResponse(string PaymentId)  //rehanchange
        {
            if (PaymentId != "")
            {
                try
                {
                    var Result = new ObjectResult(await _ELIService.GetResponseAPI(PaymentId));
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
                return BadRequest(new { message = "PaymentId cannot be empty" });
            }
        }
        [HttpPost("GenerateRACCodes")]
        public async Task<IActionResult> GenerateRACCodes(int showId, int userId, int Quantity)  //rehanchange
        {
            if (showId != 0 && userId != 0 && Quantity != 0)
            {
                try
                {
                    var Result = new ObjectResult(await _ELIService.GenerateRACCodes(showId, userId, Quantity));
                    if (Result.Value.Equals("Keys Generated"))
                        return Ok(new { message = "Keys Generated" });
                    else
                    {
                        return BadRequest(new { message = "Keys Not Generated" });
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
                return BadRequest(new { message = "ShowId, UserId & Quantity cannot be null" });
            }
        }



        
        [HttpGet("UpdateLeadsInfobyInfoWeb")]
        [Produces(typeof(List<Leads>))]
        public async Task<IActionResult> UpdateLeadsInfobyInfoWeb(int ShowId, CancellationToken ct = default(CancellationToken))
        {
     
            if (!(ShowId <= 0))
            {
                try
                {
                    var leads = await _ELIService.GetLeadsByShowIdAsync(ShowId, ct);
                    if (leads.Count != 0)
                    {
                        return Ok("Leads successfully updated against this show.");
                    }
                    else
                    {
                        return BadRequest(new { message = "No leads found against this show." });
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
                return BadRequest(new { message = "ShowKey & Barcode cannot be empty" });
            }
        }
    }



}