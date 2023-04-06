using InsuranceCorp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;
using System.Collections.Generic;


namespace InsuranceCorp.Data;
public partial class InsCorpDbContext : DbContext
{
    public InsCorpDbContext()
    {
    }

    public InsCorpDbContext(DbContextOptions<InsCorpDbContext> options)
        : base(options)
    {
      //  _connectionString = ((SqlServerOptionsExtension)options.Extensions.First()).ConnectionString;

    }

    public virtual DbSet<Address> Addresses { get; set; }
    public virtual DbSet<Contract> Contracts { get; set; }
    public virtual DbSet<Person> Persons { get; set; }

    public DbSet<RequestLog> LogRequests { get; set; }
    public DbSet<ErrorLog> LogErrors { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=JOPAT\\TOPMES_SERVER;Database=InsCorpDb;User Id=sa;Password=Topmes147***DB2;TrustServerCertificate=True");
      //  optionsBuilder.UseSqlServer("Server=JOPAT\\TOPMES_SERVER;Database=InsCorpDb;User Id=jopat;Password=Pristup123456;TrustServerCertificate=True");

    }

}
