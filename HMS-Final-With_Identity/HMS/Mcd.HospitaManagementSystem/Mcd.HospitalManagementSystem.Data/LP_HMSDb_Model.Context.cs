﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mcd.HospitalManagementSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LP_HMSDbEntities : DbContext
    {
        public LP_HMSDbEntities()
            : base("name=LP_HMSDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bed> Beds { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorRecomendation> DoctorRecomendations { get; set; }
        public virtual DbSet<DoctorSpeciatly> DoctorSpeciatlies { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Medical_Test_Type> Medical_Test_Type { get; set; }
        public virtual DbSet<Nurse> Nurses { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Patient_Detail> Patient_Detail { get; set; }
        public virtual DbSet<Patient_Medical_Test> Patient_Medical_Test { get; set; }
        public virtual DbSet<PatientFeedback> PatientFeedbacks { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
    }
}
