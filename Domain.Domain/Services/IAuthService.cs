using ELI.Domain.ViewModels;
using ELI.Entity;
using ELI.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Services
{
    public interface IELIAuthService
    {
        Task<Users> AuthenticateAsync(string username, string password, CancellationToken ct = default(CancellationToken));
        Task<IEnumerable<Users>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Users> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Users> CreateAsync(Users user, string password, CancellationToken ct = default(CancellationToken)); //rehanchange
        Task UpdateAsync(Users user, string password = null, CancellationToken ct = default(CancellationToken));
        Task<Users> ForgetAuthentication(string email, CancellationToken ct = default(CancellationToken));
        bool EmailDuplicationCheck(string Email, CancellationToken ct = default(CancellationToken));
        Task<UserRoles> RoleAddAsync(int UserId, int RoleId, CancellationToken ct = default(CancellationToken));
        Task<UserRoles> RoleUpdateAsync(int UserId, int RoleId, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel UpdatePasswordVM, string password = null, CancellationToken ct = default(CancellationToken));
        Task<Users> GetUserByEmail(string email, CancellationToken ct);
    }
}
