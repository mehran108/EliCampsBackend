using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Domain.Contracts.Auth
{
    public interface IUserRepository : IDisposable
    {
        Task<Users> AuthenticateAsync(string username, string password, CancellationToken ct = default(CancellationToken));
        Task<IEnumerable<Users>> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<Users> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken));
        Task<Users> CreateAsync(Users user, string password, CancellationToken ct = default(CancellationToken));
        Task UpdateAsync(Users user, string password = null, CancellationToken ct = default(CancellationToken));
        Task<Users> ForgetAuthenticationAsync(string email, CancellationToken ct = default(CancellationToken));
        bool CheckEmailDuplication(string email, CancellationToken ct = default(CancellationToken));
        Task<Users> GetByEmailAsync(string email, CancellationToken ct = default(CancellationToken));
        Task<UserRoles> AddRoleAsync(int userId, int RoleId, CancellationToken ct = default(CancellationToken));
        Task<UserRoles> UpdateRoleAsync(int userId, int RoleId, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel UpdatePasswordVM, CancellationToken ct = default(CancellationToken));
    }
}
