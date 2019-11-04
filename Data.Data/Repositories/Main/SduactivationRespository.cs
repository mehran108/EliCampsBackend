using ELI.Data.Context;
using ELI.Domain.Contracts.Main;
using ELI.Entity.Main;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Main
{
    public class SduactivationRespository : ISduactivationRespository
    {
        private readonly ELIContext _context;
        public SduactivationRespository(ELIContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Sduactivation> CreateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken))
        {
            await _context.Sduactivation.AddAsync(sduactivation, ct);
            await _context.SaveChangesAsync(ct);
            return sduactivation;
        }
        public async Task<Sduactivation> UpdateSduActivationAsync(Sduactivation sduactivation, CancellationToken ct = default(CancellationToken))
        {
            var temp = _context.Sduactivation.FirstOrDefaultAsync(b => b.SduactivationId == sduactivation.SduactivationId && b.IsActive==true && b.IsDeleted==false);
            temp.Result.Company = sduactivation.Company;
            temp.Result.StandNumber = sduactivation.StandNumber;
            temp.Result.DeviceId = sduactivation.DeviceId;
            temp.Result.Name = sduactivation.Name;
            temp.Result.IsConsumed = sduactivation.IsConsumed;
            temp.Result.ActivationTime = DateTime.Now;

            _context.Sduactivation.Update(temp.Result);
            await _context.SaveChangesAsync(ct);
            return temp.Result;
        }
        public async Task<Sduactivation> GetSDUActivationByActivationIdAsync(int activationId, CancellationToken ct = default(CancellationToken))
        {
            return await _context.Sduactivation.Where(a => a.ActivationId == activationId && a.IsConsumed == false && a.IsDeleted== false && a.IsActive==true).FirstOrDefaultAsync(ct);
        }
        public async Task<Sduactivation> GetSDUActivationByDeviceAsync(int showId, int deviceId ,CancellationToken ct = default(CancellationToken))
        {
            var sdu = _context.Sduactivation.Where(a => a.ShowId == showId && a.DeviceId == deviceId && a.IsConsumed == true && a.IsDeleted == false && a.IsActive == true).FirstOrDefaultAsync(ct);
            
            if(sdu != null && sdu.Result != null)
            {
                var activation = _context.Activation.Where(a => a.ActivationId == sdu.Result.ActivationId).FirstOrDefaultAsync(ct);

                if(activation != null && activation.Result != null && activation.Result.IsActive == true)
                {
                    return await sdu;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return await sdu;
            }
        }
    }
}
