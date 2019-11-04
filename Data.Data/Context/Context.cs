using Microsoft.EntityFrameworkCore;
using ELI.Data.Configurations;
using ELI.Entity;
using ELI.Entity.Main;

namespace ELI.Data.Context
{
    public class ELIContext : DbContext
    {
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<Show> Show { get; set; }
        public virtual DbSet<Activation> Activation { get; set; }
        public virtual DbSet<ActivationType> ActivationType { get; set; }
        public virtual DbSet<LookupTable> LookupTable { get; set; }
        public virtual DbSet<LookupValue> LookupValue { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<ContactInfo> ContactInfo { get; set; }

        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Pricing> Pricing { get; set; }
        public virtual DbSet<ShowPricing> ShowPricing { get; set; }
        public virtual DbSet<ErrorLogging> ErrorLogging { get; set; }
        public virtual DbSet<Database> Database { get; set; }
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Sduactivation> Sduactivation { get; set; }
        public virtual DbSet<Qualifier> Qualifier { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionOption> QuestionOption { get; set; }
        public virtual DbSet<QuestionType> QuestionType { get; set; }
        public virtual DbSet<QualifierUsers> QualifierUsers { get; set; }
        public virtual DbSet<ShowDiscount> ShowDiscount { get; set; }
        public virtual DbSet<Leads> Leads { get; set; }
        public virtual DbSet<LeadsQualifier> LeadsQualifier { get; set; }
        private readonly string _dbName;

        public ELIContext(DbContextOptions<ELIContext> options) : base(options)
        {

        }

        public ELIContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);

            if (!string.IsNullOrEmpty(_dbName))
                optionsBuilder.UseSqlServer(_dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ActivationConfiguration(modelBuilder.Entity<Activation>());
            new ActivationTypeConfiguration(modelBuilder.Entity<ActivationType>());
            new CitiesConfiguration(modelBuilder.Entity<Cities>());
            new CountriesConfiguration(modelBuilder.Entity<Countries>());
            new CurrencyConfiguration(modelBuilder.Entity<Currency>());
            new DatabaseConfiguration(modelBuilder.Entity<Database>());
            new InvoiceConfiguration(modelBuilder.Entity<Invoice>());
            new ShowConfiguration(modelBuilder.Entity<Show>());
            new LookupValueConfiguration(modelBuilder.Entity<LookupValue>());
            new LookupTableConfiguration(modelBuilder.Entity<LookupTable>());
            new ErrorLoggingConfiguration(modelBuilder.Entity<ErrorLogging>());
            new PricingConfiguration(modelBuilder.Entity<Pricing>());
            new ShowPricingConfiguration(modelBuilder.Entity<ShowPricing>());
            new DeviceConfiguration(modelBuilder.Entity<Device>());
            new ServerConfiguration(modelBuilder.Entity<Server>());
            new SduactivationConfiguration(modelBuilder.Entity<Sduactivation>());
            new ShowDiscountConfiguration(modelBuilder.Entity<ShowDiscount>());
            new LeadsConfiguration(modelBuilder.Entity<Leads>());
            new LeadsQualifierConfiguration(modelBuilder.Entity<LeadsQualifier>());
        }
    }
}