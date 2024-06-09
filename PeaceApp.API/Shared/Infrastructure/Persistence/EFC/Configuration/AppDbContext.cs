using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
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
        
        //  entity configurations for reports
        builder.Entity<ReportManagement>().ToTable("Reports");
        builder.Entity<ReportManagement>().HasKey(f => f.Id);
        builder.Entity<ReportManagement>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ReportManagement>().Property(f => f.KindOfReport).IsRequired();
        builder.Entity<ReportManagement>().Property(f => f.Description).IsRequired();
        
        //  entity configurations for Organization Accounts
        builder.Entity<OrganizationAccount>().ToTable("OrganizationAccounts");
        builder.Entity<OrganizationAccount>().HasKey(f => f.Id);
        builder.Entity<OrganizationAccount>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<OrganizationAccount>().Property(f => f.OrganizationName).IsRequired();
        builder.Entity<OrganizationAccount>().Property(f => f.Cellphone).IsRequired();
        builder.Entity<OrganizationAccount>().Property(f => f.Location).IsRequired();
        
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

        
        
        
        // Apply SnakeCase Naming Convention
        builder.UseSnakeCaseWithPluralizedTableNamingConvention();
    }
    
}