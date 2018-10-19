using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            TablePrefixs = new Dictionary<string, string>();
            base.ChangeTracker.AutoDetectChangesEnabled = true;
            base.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<CredentialType> CredentialTypes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }

        public DbSet<CredentialProcessType> CredentialProcessTypes { get; set; }
        public DbSet<CredentialProcess> CredentialProcesses { get; set; }

        public DbSet<CredentialRequirementType> CredentialRequirementTypes { get; set; }

        public DbSet<CredentialRequirement> CredentialRequirements { get; set; }

        public DbSet<CredentialStatusType> CredentialStatusTypes { get; set; }

        public DbSet<CredentialRequirementTemplate> CredentialRequirementTemplates { get; set; }

        public DbSet<CredentialTypeXCredentialProcessType> CredentialTypeXCredentialProcessTypes { get; set; }

        public DbSet<CredentialProcessTypeXCredentialRequirementType> CredentialProcessTypeXCredentialRequirementTypes { get; set; }

        public DbSet<StatusStateType> StatusStateTypes { get; set; }

        public DbSet<CEPeriod> CEPeriods { get; set; }

        public DbSet<CEDetail> CEDetails { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<ExamScore> ExamScores { get; set; }

        public DbSet<CommunicationTemplate> CommunicationTemplates { get; set; }



        public const string Prefix = "Prefix";

        private Dictionary<string, string> TablePrefixs { get; set; }



        //
        // Summary:
        //     Override this method to further configure the model that was discovered by convention
        //     from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties
        //     on your derived context. The resulting model may be cached and re-used for subsequent
        //     instances of your derived context.
        //
        // Parameters:
        //   modelBuilder:
        //     The builder being used to construct the model for this context. Databases (and
        //     other extensions) typically define extension methods on this object that allow
        //     you to configure aspects of the model that are specific to a given database.
        //
        // Remarks:
        //     If a model is explicitly set on the options for this context (via Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel))
        //     then this method will not be run.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Uncomment to debug OnModelCreating
            //if (!System.Diagnostics.Debugger.IsAttached)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                {
                    FieldInfo[] fields = entityType.ClrType
                        .GetFields(BindingFlags.Public
                        | BindingFlags.Static
                        | BindingFlags.FlattenHierarchy);

                    // Should only be one constant for these
                    if (fields.Count() > 1)
                    {
                        throw new Exception("No more than 1 const field on object allowed");
                    }
                    var prefix = fields.First().GetRawConstantValue();
                    // Add type prefix to the table
                    TablePrefixs.Add(entityType.ClrType.Name, prefix.ToString());

                    var addDate = prefix + "AddDate";
                    var changeDate = prefix + "ChangeDate";
                    var addUser = prefix + "AddUser";
                    var changeUser = prefix + "ChangeUser";

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property($"{prefix}Key")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(addDate)
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(changeDate)
                        .ValueGeneratedOnAdd();

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(changeUser)
                        .ValueGeneratedOnAdd();

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(addUser)
                        .ValueGeneratedOnAdd();
                }
            }
            modelBuilder
                .Entity<CredentialProcess>()
                .HasOne(p => p.Credential)
                .WithMany(c => c.CredentialProcesses)
                .HasForeignKey(p => p.Cr2Cr1Key);


            modelBuilder
                .Entity<CredentialProcessType>()
                .Property(pt => pt.Ct2DueDateUnit)
                .HasConversion(new EnumToStringConverter<GlobalConstants.DueDateUnit>());

            modelBuilder
                .Entity<CredentialProcessType>()
                .Property(pt => pt.Ct2DueDateRounding)
                .HasConversion(new EnumToStringConverter<GlobalConstants.DueDateRounding>());

            modelBuilder.Entity<CredentialRequirement>()
                .HasOne(r => r.ParentRequirement)
                .WithMany(r => r.DependentRequirements)
                .HasForeignKey(r => r.Cr3Cr3Key);

        }

        public void TestOnModelCreating()
        {
            OnModelCreating(new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet()));
        }

        public new async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps(ChangeTracker);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps(ChangeTracker);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public TEntity Update<TEntity>(TEntity entity, string currentUser) where TEntity : class
        {
            AddTimestamps(ChangeTracker, currentUser);
            return base.Update(entity).Entity;
        }

        private void AddTimestamps(ChangeTracker tracker, string username = null)
        {
            var entities = tracker.Entries().Where(x => x.Entity.GetType().BaseType == typeof(EntityBase) && (x.State != EntityState.Added && x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var Prefix = TablePrefixs.Where(x => x.Key == entity.Entity.GetType().Name).First().Value;
                entity.Properties.First(x => x.Metadata.Name == (Prefix + "ChangeDate")).CurrentValue = DateTimeOffset.Now;
                entity.Properties.First(x => x.Metadata.Name == (Prefix + "ChangeUser")).CurrentValue = username == null ? "Anonymous" : username;
            }
        }
    }
}
