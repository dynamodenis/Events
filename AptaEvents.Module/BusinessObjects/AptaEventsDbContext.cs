using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;
using Microsoft.Extensions.Configuration;

namespace AptaEvents.Module.BusinessObjects;

// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class AptaEventsContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<AptaEventsEFCoreDbContext>()
            .UseNpgsql(";")
            .UseChangeTrackingProxies()
            .UseObjectSpaceLinkProxies();
        return new AptaEventsEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class AptaEventsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AptaEventsEFCoreDbContext> {
	public AptaEventsEFCoreDbContext CreateDbContext(string[] args) {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "AptaEvents.Blazor.Server"))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AptaEventsEFCoreDbContext>();
        var connectionString = configuration.GetConnectionString("ConnectionString");

        ArgumentNullException.ThrowIfNull(connectionString);

        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseChangeTrackingProxies();
        optionsBuilder.UseObjectSpaceLinkProxies();
		return new AptaEventsEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(AptaEventsContextInitializer))]
public class AptaEventsEFCoreDbContext : DbContext
{
    public AptaEventsEFCoreDbContext() : base()
    {
    }

    public AptaEventsEFCoreDbContext(DbContextOptions<AptaEventsEFCoreDbContext> options) : base(options) {
    }
    //public DbSet<ModuleInfo> ModulesInfo { get; set; }
    public DbSet<ModelDifference> ModelDifferences { get; set; }
	public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
	public DbSet<PermissionPolicyRole> Roles { get; set; }
	public DbSet<AptaEvents.Module.BusinessObjects.ApplicationUser> Users { get; set; }
    public DbSet<AptaEvents.Module.BusinessObjects.ApplicationUserLoginInfo> UserLoginInfos { get; set; }

    public DbSet<Tab> Tabs { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangingAndChangedNotificationsWithOriginalValues);
        modelBuilder.UsePropertyAccessMode(PropertyAccessMode.PreferFieldDuringConstruction);
        modelBuilder.Entity<AptaEvents.Module.BusinessObjects.ApplicationUserLoginInfo>(b => {
            b.HasIndex(nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.LoginProviderName), nameof(DevExpress.ExpressApp.Security.ISecurityUserLoginInfo.ProviderUserKey)).IsUnique();
        });
        modelBuilder.Entity<ModelDifference>()
            .HasMany(t => t.Aspects)
            .WithOne(t => t.Owner)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
