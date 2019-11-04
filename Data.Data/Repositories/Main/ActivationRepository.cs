using ELI.Data.Context;
using ELI.Domain.Contracts;
using ELI.Domain.Contracts.Main;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class ActivationRepository : IActivationRepository
    {
        private readonly ELIContext _context;
        public ActivationRepository(ELIContext context)
        {
            _context = context;
        }
        public async Task<List<Activation>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _context.Activation.Where(a => a.IsDeleted == false && a.IsActive == true).ToListAsync(ct);
        }
        public async Task<Activation> GetActivationByIdAsync(string activationKey, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Activation.FirstOrDefaultAsync(a => a.ActivationKey == activationKey && a.IsDeleted == false && a.IsActive == true);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Activation> CreateActivationAsync(Activation activation, CancellationToken ct = default(CancellationToken))
        {
            await _context.Activation.AddAsync(activation, ct);
            await _context.SaveChangesAsync(ct);
            return activation;
        }
        public async Task<List<Activation>> GetAllActivationsByUserIdAsync(string role, int userid, bool isDashboard, CancellationToken ct = default(CancellationToken))
        {
            if (isDashboard == true)
            {



            }

            else
            {


            }
            if (role == "Admin")
            {
                List<Activation> activationKeysDashboard = new List<Activation>();
                var activationKeys = await _context.Activation.Where(a => a.IsActive == true && a.IsDeleted == false).OrderBy(a => a.CreatedDate).ThenBy(a => a.IsActive).ToListAsync(ct);
                foreach (var a in activationKeys)
                {
                    //DashboardActivation key = new DashboardActivation();
                    //key.ActivationKey = a.ActivationKey;
                    //key.DeviceName = a.de


                }
                return activationKeysDashboard;
            }
            else
            {
                //var activationKeys = (from a in _context.Activation
                //                      join sdu in _context.Sduactivation on a.ActivationId equals sdu.ActivationId
                //                      join d in _context.Device on sdu.DeviceId equals d.DeviceId 
                //                      join s in _context.Show on sdu.ShowId equals s.ShowId 
                //                      where a.IsActive == true && a.IsDeleted == false && (sdu.UserId == userid)
                //                      orderby a.CreatedDate
                //                      orderby a.IsActive
                //                      select new Activation
                //                      {
                //                          ActivationKey = a.ActivationKey,
                //                          DeviceName = d.DeviceName,
                //                         ShowName =s.ShowName,
                //                         IsConsumed = sdu.IsConsumed?? false ,
                //                          IsActive = a.IsActive ?? false,
                //                          //PaymentGatewayId = s.PaymentGatewayId
                //                      }).ToList();
                return null;
            }
        }
        public async Task<List<Activation>> GetAllRestrictedCodesAsync(CancellationToken ct = default(CancellationToken))
        {
            var restrictedCodes =await (from s in _context.Activation
                                   join sdu in _context.Sduactivation on s.ActivationId equals sdu.ActivationId
                                   join sh in _context.Show on sdu.ShowId equals sh.ShowId
                                   where
                                  s.IsActive == true &&
                                  s.IsDeleted == false &&
                                  s.ActivationTypeId == 8
                                   orderby s.CreatedDate
                                   select new Activation
                                   {
                                       ShowId = sdu.ShowId,
                                       show = sh,
                                       ActivationId = s.ActivationId,
                                       ActivationKey = s.ActivationKey,
                                       IsDeleted = s.IsDeleted,
                                       IsConsumed = s.IsConsumed,
                                       ActivationTypeId = s.ActivationTypeId,
                                       IsActive = s.IsActive,
                                       CreatedDate = s.CreatedDate
                                   }).ToListAsync();
            return restrictedCodes;
        }
        public async Task<Activation> ValidateRestrictedCode(string code, int showId, CancellationToken ct = default(CancellationToken))
        {
            var restrictedCode = await _context.Activation.Where(a => a.ActivationKey == code && a.IsActive == true && a.IsDeleted == false && a.ActivationTypeId==8).FirstOrDefaultAsync();
            if (restrictedCode != null)
            {
                var sdu = await _context.Sduactivation.Where(a => a.ShowId == showId && a.ActivationId== restrictedCode.ActivationId && a.IsDeleted == false && a.IsActive == true && a.IsConsumed==false).FirstOrDefaultAsync();
                if (sdu != null)
                {
                    if (sdu.ActivationId == restrictedCode.ActivationId)
                    {
                        return restrictedCode;
                    }
                    else
                    {
                        throw new AppException("Restricted Code not valid for this show.");
                    }
                }
                else
                {

                    throw new AppException("Restricted Code not valid for this show.");

                }
            }
            else
            {
                throw new AppException("Invalid Restricted code.");
            }
        }
    }
}

