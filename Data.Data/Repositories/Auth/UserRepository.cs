using ELI.Domain.Contracts.Auth;
using ELI.Domain.Helpers;
using ELI.Domain.ViewModels;
using ELI.Entity.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ELI.Data.Repositories.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly ELIAuthDbContext _context;
        public UserRepository(ELIAuthDbContext context)
        {
            _context = context;
        }
        public async Task<Users> AuthenticateAsync(string email, string password, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;
            var user = await _context.Users.Include("AuthUserRoles.Role").FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted ==false && x.IsActive== true);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }
        public async Task<Users> ForgetAuthenticationAsync(string email, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(email))
                return null;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email  && x.IsActive== true && x.IsDeleted==false);
            return user;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public async Task<Users> CreateAsync(Users user, string password, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            var availability = CheckEmailDuplication(user.Email);
            if (availability == true)
                throw new AppException("Email \"" + user.Email + "\" is already taken");
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
            return user;
        }
        public bool CheckEmailDuplication(string email, CancellationToken ct = default(CancellationToken))
        {
            if (_context.Users.Any(x => x.Email == email && x.IsDeleted== false && x.IsActive== true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<Users>> GetAllAsync(CancellationToken ct = default(CancellationToken))
        {
            //return await (from u in _context.Users
            //             join ur in _context.UserRoles
            //             on u.Id equals ur.UserId
            //             join r in _context.Roles
            //             on ur.RoleId equals r.Id
            //             where u.IsActive == true && u.IsDeleted == false
            //             orderby u.UpdatedDate ascending
            //             select new Users{
                             
            //             }).ToListAsync();
            return await _context.Users.Where(a=>a.IsDeleted== false).Include("AuthUserRoles.Role").ToListAsync();
        }
        public Task<Users> GetByIdAsync(int id, CancellationToken ct = default(CancellationToken))
        {
            return _context.Users.Include(a=>a.AuthUserRoles).Include("AuthUserRoles.Role").FirstOrDefaultAsync(a => a.Id == id && a.IsDeleted== false && a.IsActive == true);
        }
        public async Task UpdateAsync(Users user, string password = null, CancellationToken ct = default(CancellationToken))
        {
            var tempUser = await _context.Users.FirstOrDefaultAsync(a => a.Id == user.Id && a.IsActive==true && a.IsDeleted==false);
            if (tempUser == null)
                throw new AppException("User not found");
            if (user.Email != tempUser.Email)
            {
                if (_context.Users.Any(x => x.Email == user.Email && x.IsActive==true && x.IsDeleted==false))
                    throw new AppException("Email " + user.Email + " is already taken");
            }
            tempUser.FirstName = user.FirstName;
            tempUser.SurName = user.SurName;
            tempUser.Email = user.Email;
            tempUser.PhoneNumber = user.PhoneNumber;
            tempUser.Company = user.Company;
            tempUser.StreetAddress = user.StreetAddress;
            tempUser.Suburb = user.Suburb;
            tempUser.Country = user.Country;
            tempUser.State_Province = user.State_Province;
            tempUser.PostalCode = user.PostalCode;
            tempUser.SecurityStamp = user.SecurityStamp;
            tempUser.LastName = user.LastName;
            tempUser.UserName = user.UserName;
            tempUser.SecurityStamp = user.SecurityStamp;
            tempUser.TempUser = false;
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);
                tempUser.PasswordHash = passwordHash;
                tempUser.PasswordSalt = passwordSalt;
            }
            _context.Users.Update(tempUser);
            await _context.SaveChangesAsync();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "password");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
        public async Task<Users> GetByEmailAsync(string email, CancellationToken ct = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(email))
                return null;
            var user = await _context.Users.Include("AuthUserRoles.Role").FirstOrDefaultAsync(x => x.Email == email && x.IsDeleted== false && x.IsActive== true);
            return user;
        }
        public async Task<UserRoles> AddRoleAsync(int userId, int RoleId, CancellationToken ct = default(CancellationToken))
        {
            UserRoles userRole = new UserRoles();
            userRole.RoleId = RoleId;
            userRole.UserId = userId;
            await _context.UserRoles.AddAsync(userRole, ct);
            await _context.SaveChangesAsync(ct);
            return userRole;
        }
        public async Task<UserRoles> UpdateRoleAsync(int userId, int RoleId, CancellationToken ct = default(CancellationToken))
        {
            var role = await _context.UserRoles.FirstOrDefaultAsync(a => a.UserId == userId);
            _context.UserRoles.Remove(role);
            await _context.SaveChangesAsync(ct);
           await AddRoleAsync(userId, RoleId);
            return role;
        }
        public async Task<bool> UpdatePasswordAsync(UpdatePasswordViewModel UpdatePasswordVM, CancellationToken ct = default(CancellationToken))
        {
            var tempUser = await _context.Users.FirstOrDefaultAsync(a => a.Id == UpdatePasswordVM.UserId && a.IsActive == true && a.IsDeleted == false);
            if (tempUser == null)
                throw new AppException("User not found");
            if (UpdatePasswordVM.Email != tempUser.Email)
            {
                    throw new AppException("Email is not associated with this user");
            }
            if (!VerifyPasswordHash(UpdatePasswordVM.OldPassword, tempUser.PasswordHash, tempUser.PasswordSalt))
                    throw new AppException("Old password is not correct");

            if (!string.IsNullOrWhiteSpace(UpdatePasswordVM.NewPassword))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(UpdatePasswordVM.NewPassword, out passwordHash, out passwordSalt);
                tempUser.PasswordHash = passwordHash;
                tempUser.PasswordSalt = passwordSalt;
            }
            _context.Users.Update(tempUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
