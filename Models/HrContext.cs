using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HrProject.Models;

public class HrContext : IdentityDbContext<HrUser>
{
    public HrContext()
    {
    }

    public HrContext(DbContextOptions<HrContext> options) : base(options) { }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeHoliday> EmployeeHolidays { get; set; }
    public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }

    #region Old


    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=.;Database=HR;Trusted_Connection=True;TrustServerCertificate=True");

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Attendance>(entity =>
    //        {
    //            entity.HasKey(e => e.Date);

    //            entity.HasIndex(e => e.Employeeid, "IX_Attendances_employeeid");

    //            entity.Property(e => e.Date).HasColumnName("date");
    //            entity.Property(e => e.ArrivalTime).HasColumnName("arrivalTime");
    //            entity.Property(e => e.DepartureTime).HasColumnName("departureTime");
    //            entity.Property(e => e.Employeeid).HasColumnName("employeeid");

    //            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances).HasForeignKey(d => d.Employeeid);
    //        });

    //        modelBuilder.Entity<Department>(entity =>
    //        {
    //            entity.Property(e => e.Id).HasColumnName("id");
    //            entity.Property(e => e.DeptName).HasColumnName("dept_name");
    //        });

    //        modelBuilder.Entity<Employee>(entity =>
    //        {
    //            entity.HasIndex(e => e.Departmentid, "IX_Employees_departmentid");

    //            entity.Property(e => e.Id).HasColumnName("id");
    //            entity.Property(e => e.ArrivalTime).HasColumnName("arrivalTime");
    //            entity.Property(e => e.BirthDate).HasColumnName("birthDate");
    //            entity.Property(e => e.City).HasColumnName("city");
    //            entity.Property(e => e.Country).HasColumnName("country");
    //            entity.Property(e => e.Departmentid).HasColumnName("departmentid");
    //            entity.Property(e => e.DepartureTime).HasColumnName("departureTime");
    //            entity.Property(e => e.FirstName).HasColumnName("firstName");
    //            entity.Property(e => e.Gender).HasColumnName("gender");
    //            entity.Property(e => e.HireDate).HasColumnName("hireDate");
    //            entity.Property(e => e.LastName).HasColumnName("lastName");
    //            entity.Property(e => e.NationalId).HasColumnName("nationalId");
    //            entity.Property(e => e.Nationality).HasColumnName("nationality");
    //            entity.Property(e => e.Phone).HasColumnName("phone");
    //            entity.Property(e => e.Salary).HasColumnName("salary");

    //            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasForeignKey(d => d.Departmentid);
    //        });

    //        modelBuilder.Entity<EmployeeHoliday>(entity =>
    //        {
    //            entity.HasKey(e => e.Holiday);

    //            entity.HasIndex(e => e.Employeeid, "IX_EmployeeHolidays_employeeid");

    //            entity.Property(e => e.Holiday)
    //                .ValueGeneratedNever()
    //                .HasColumnName("holiday");
    //            entity.Property(e => e.Employeeid).HasColumnName("employeeid");

    //            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeHolidays).HasForeignKey(d => d.Employeeid);
    //        });

    //        OnModelCreatingPartial(modelBuilder);
    //    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    #endregion
}
