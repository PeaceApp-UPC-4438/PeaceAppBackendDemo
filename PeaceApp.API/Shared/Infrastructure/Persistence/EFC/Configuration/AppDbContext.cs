using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.Report.Domain.Model.Aggregates;
using PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace PeaceApp.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Enable Audit Fields Interceptors
        builder.AddCreatedUpdatedInterceptor();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Place here you entity configurations
        builder.Entity<ReportManagement>().ToTable("Reports");
        builder.Entity<ReportManagement>().HasKey(f => f.Id);
        builder.Entity<ReportManagement>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ReportManagement>().Property(f => f.KindOfReport).IsRequired();
        builder.Entity<ReportManagement>().Property(f => f.Description).IsRequired();
        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(n => n.Message).IsRequired();
        builder.Entity<Notification>().Property(n => n.Priority).IsRequired();
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
    
}