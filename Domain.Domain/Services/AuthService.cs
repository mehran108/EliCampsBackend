using ELI.Domain.Contracts.Auth;
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
    public class ELIAuthService : IELIAuthService
    {
        private readonly IUserRepository _userRepository;
        public ELIAuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Users> AuthenticateAsync(string email, string password, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.AuthenticateAsync(email, password, ct);
        }
        public async Task<Users> ForgetAuthentication(string email, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.ForgetAuthenticationAsync(email, ct);
        }
        public async Task<IEnumerable<Users>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.GetAllAsync(ct);
        }
        public async Task<Users> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _userRepository.GetByIdAsync(id, ct);
        }
        public async Task<Users> CreateAsync(Users user, string password, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.CreateAsync(user, password, ct);
        }
        public bool EmailDuplicationCheck(string Email, CancellationToken ct = default(CancellationToken))
        {
            return  _userRepository.CheckEmailDuplication(Email, ct);
        }
        public async Task UpdateAsync(Users userParam, string password = null, CancellationToken ct = default(CancellationToken))
        {
            await _userRepository.UpdateAsync(userParam, password, ct);
        }
        public async Task<UserRoles> RoleAddAsync(int UserId,  int RoleId, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.AddRoleAsync(UserId, RoleId, ct);
        }
        public async Task<UserRoles> RoleUpdateAsync(int UserId, int RoleId, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.UpdateRoleAsync(UserId, RoleId, ct);
        }
        public async Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel UpdatePasswordVM, string password = null, CancellationToken ct = default(CancellationToken))
        {
            return await _userRepository.UpdatePasswordAsync(UpdatePasswordVM, ct);
        }
        public async Task<Users> GetUserByEmail(string email, CancellationToken ct)
        {
            return await _userRepository.GetByEmailAsync(email, ct);
        }
    }
}
