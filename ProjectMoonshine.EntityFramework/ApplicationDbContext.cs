using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailType> EmailTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PhoneType> PhoneTypes { get; set; }
        public const string Prefix = "Prefix";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        { }
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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                {
                    FieldInfo[] fields = entityType.ClrType.GetFields(BindingFlags.Public
                        | BindingFlags.Static
                        | BindingFlags.FlattenHierarchy);
                    // should only be one constant for these
                    if(fields.Count() > 1)
                    {
                        throw new Exception("More than 1 const field on object!");
                    }
                    var prefix = fields.First().GetRawConstantValue();

                    var addDate = prefix + nameof(EntityBase.AddDate);
                    var changeDate = prefix + nameof(EntityBase.ChangeDate);
                    var addUser = prefix + nameof(EntityBase.AddUser);
                    var changeUser = prefix + nameof(EntityBase.ChangeUser);

                    entityType.AddProperty(addDate, typeof(DateTimeOffset));
                    entityType.AddProperty(changeDate, typeof(DateTimeOffset?));
                    entityType.AddProperty(addUser, typeof(string));
                    entityType.AddProperty(changeUser, typeof(string));

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
        }

        public void TestOnModelCreating()
        {
            OnModelCreating(new ModelBuilder(new Microsoft.EntityFrameworkCore.Metadata.Conventions.ConventionSet()));
        }

        public new async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Update(entity);
        }

        //private void AddTimestamps()
        //{
        //    var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        //    var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
        //        ? HttpContext.Current.User.Identity.Name
        //        : "Anonymous";

        //    foreach (var entity in entities)
        //    {
        //        if (entity.State == EntityState.Added)
        //        {
        //            ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
        //            ((BaseEntity)entity.Entity).UserCreated = currentUsername;
        //        }

        //        ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
        //        ((BaseEntity)entity.Entity).UserModified = currentUsername;
        //    }
        //}
    }
}
