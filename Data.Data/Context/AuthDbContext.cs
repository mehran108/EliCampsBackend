using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ELI.Entity.Auth
{
    public partial class ELIAuthDbContext : DbContext
    {
        public virtual DbSet<RoleClaims> RoleClaims { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<UserClaims> UserClaims { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserTokens> UserTokens { get; set; }

        private readonly string _dbName;

        public ELIAuthDbContext(DbContextOptions<ELIAuthDbContext> options) : base(options)
        {
        }

        public ELIAuthDbContext(string dbName)
        {
            _dbName = dbName;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"Server=10.100.101.185;Database=ELIDb;User id=sa; password=abcd@1234;");
//            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)        {
            modelBuilder.Entity<RoleClaims>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AuthRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserClaims>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AuthUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AuthUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(450);

                entity.Property(e => e.Company).HasMaxLength(450);

                entity.Property(e => e.Country)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Postcode).HasMaxLength(450);

                entity.Property(e => e.ResponsiblePerson).HasMaxLength(450);

                entity.Property(e => e.State_Province)
                    .HasColumnName("State_Province")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Suburb)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SurName).HasMaxLength(250);

                entity.Property(e => e.UpdatedBy).HasMaxLength(450);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserKey).HasMaxLength(450);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<UserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();
            });
        }
    }
}
