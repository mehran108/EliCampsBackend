using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ELI.Entity.Auth
{
    public partial class Users
    {
        public Users()
        {
            AuthUserClaims = new HashSet<UserClaims>();
            AuthUserLogins = new HashSet<UserLogins>();
            AuthUserRoles = new HashSet<UserRoles>();
        }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserKey { get; set; }
        public int? UserTypeId { get; set; }
        public int? AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public string Suburb { get; set; }
        public string State_Province { get; set; }
        public string Country { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string ResponsiblePerson { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public string Postcode { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public bool TempUser { get; set; }


        public ICollection<UserClaims> AuthUserClaims { get; set; }
        public ICollection<UserLogins> AuthUserLogins { get; set; }
        public ICollection<UserRoles> AuthUserRoles { get; set; }
    }
}
