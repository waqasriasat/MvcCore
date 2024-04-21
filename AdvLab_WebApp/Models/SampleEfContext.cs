using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AdvLab_WebApp.Models;

public partial class SampleEfContext : DbContext
{
    public SampleEfContext()
    {
    }

    public SampleEfContext(DbContextOptions<SampleEfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<LoginLog> LoginLogs { get; set; }
    public virtual DbSet<AddLocation> AddLocations { get; set; }
    public virtual DbSet<AddConnLab> AddConnLabs { get; set; }
    public virtual DbSet<AccessRight> AccessRights { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<AddDepart> AddDeparts { get; set; }
    public virtual DbSet<AddSubDepart> AddSubDeparts { get; set; }
    public virtual DbSet<AddPlacement> AddPlacements { get; set; }
    public virtual DbSet<AddDesignation> AddDesignations { get; set; }
    public virtual DbSet<CrystalReportPath> CrystalReportPaths { get; set; }
    public virtual DbSet<ChangePassword> ChangePasswords { get; set; }
    public virtual DbSet<DescCashier> DescCashiers { get; set; }
    public virtual DbSet<DescLab> DescLabs { get; set; }
    public virtual DbSet<DescQ> DescQs { get; set; }
    public virtual DbSet<DuesRecQ> DuesRecQs { get; set; }
    public virtual DbSet<PatReg> PatRegs { get; set; }
    public virtual DbSet<PatReg_Shortkey> PatReg_Shortkeys { get; set; }
    public virtual DbSet<AddClient> AddClients { get; set; }
    public virtual DbSet<AddDescription> AddDescriptions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cnl).HasColumnName("CNL");
            entity.Property(e => e.EmpId).HasColumnName("EmpID");
            entity.Property(e => e.LoginStatusIp).HasColumnName("LoginStatusIP");
            entity.Property(e => e.Nsend).HasColumnName("NSend");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Uid).HasColumnName("UId");
            entity.Property(e => e.Uname).HasColumnName("UName");
            entity.Property(e => e.Upassword).HasColumnName("UPassword");
        });

        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<ChangePassword>().HasNoKey();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
