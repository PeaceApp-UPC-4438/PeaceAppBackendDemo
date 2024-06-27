using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using PeaceApp.API.Communication.Domain.Model.Aggregates;
using PeaceApp.API.IAM.Domain.Model.Aggregates;
using PeaceApp.API.Organization.Domain.Model.Aggregates;
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
        
        // ReportManagement BC
        builder.Entity<ReportManagement>().ToTable("Reports");
        builder.Entity<ReportManagement>().HasKey(f => f.Id);
        builder.Entity<ReportManagement>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ReportManagement>().Property(f => f.Type).IsRequired().HasMaxLength(50);
        builder.Entity<ReportManagement>().Property(f => f.Date).IsRequired().HasMaxLength(20);
        builder.Entity<ReportManagement>().Property(f => f.Time).IsRequired().HasMaxLength(20);
        builder.Entity<ReportManagement>().Property(f => f.District).IsRequired().HasMaxLength(20);
        builder.Entity<ReportManagement>().Property(f => f.Location).IsRequired().HasMaxLength(20);
        builder.Entity<ReportManagement>().Property(f => f.Description).IsRequired().HasMaxLength(200);
        builder.Entity<ReportManagement>().Property(f => f.UrlEvidence).IsRequired().HasMaxLength(500);
        
        //  entity configurations for Organization Accounts
        builder.Entity<OrganizationAccount>().ToTable("OrganizationAccounts");
        builder.Entity<OrganizationAccount>().HasKey(f => f.Id);
        builder.Entity<OrganizationAccount>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<OrganizationAccount>().Property(f => f.OrganizationName).IsRequired();
        builder.Entity<OrganizationAccount>().Property(f => f.Cellphone).IsRequired();
        builder.Entity<OrganizationAccount>().Property(f => f.Location).IsRequired();
        
        //Communication BC
        builder.Entity<Notification>().ToTable("Notifications");
        builder.Entity<Notification>().HasKey(n => n.Id);
        builder.Entity<Notification>().Property(n => n.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Notification>().Property(n => n.Message).IsRequired();
        builder.Entity<Notification>().Property(n => n.Priority).IsRequired();
        
        // Citizens Context

        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>().HasKey(c => c.Id);
        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>().OwnsOne(c => c.Name,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(c => c.FirstName).HasColumnName("FirstName");
                n.Property(c => c.LastName).HasColumnName("LastName");
            });

        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>().OwnsOne(c => c.Email,
            e =>
            {
                e.WithOwner().HasForeignKey("Id");
                e.Property(a => a.Address).HasColumnName("EmailAddress");
            });

        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>().OwnsOne(c => c.Address,
            a =>
            {
                a.WithOwner().HasForeignKey("Id");
                a.Property(s => s.Street).HasColumnName("AddressStreet");
                a.Property(s => s.Number).HasColumnName("AddressNumber");
                a.Property(s => s.City).HasColumnName("AddressCity");
                a.Property(s => s.PostalCode).HasColumnName("AddressPostalCode");
                a.Property(s => s.Country).HasColumnName("AddressCountry");
            });

        builder.Entity<Citizen.Domain.Model.Aggregates.Citizen>()
            .HasMany(c => c.Reports)
            .WithOne(t => t.Citizen)
            .HasForeignKey(t => t.CitizenId)
            .HasPrincipalKey(t => t.Id);

        
        
        //IAM CONTEXT//

        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
    
}