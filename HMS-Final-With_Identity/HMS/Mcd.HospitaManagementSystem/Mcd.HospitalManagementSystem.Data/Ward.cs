
//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
public partial class Ward
{

    public Ward()
    {

        this.Beds = new HashSet<Bed>();

        this.Doctors = new HashSet<Doctor>();

        this.Nurses = new HashSet<Nurse>();

        this.PatientDetails = new HashSet<PatientDetail>();

    }


    public int Id { get; set; }

    public string WardNo { get; set; }

    public Nullable<decimal> WardFee { get; set; }



    public virtual ICollection<Bed> Beds { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; }

    public virtual ICollection<Nurse> Nurses { get; set; }

    public virtual ICollection<PatientDetail> PatientDetails { get; set; }

}

}
