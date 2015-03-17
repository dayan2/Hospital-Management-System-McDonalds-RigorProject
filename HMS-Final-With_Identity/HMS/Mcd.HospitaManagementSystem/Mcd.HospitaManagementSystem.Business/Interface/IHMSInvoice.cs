﻿#region Using Directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#endregion

namespace Mcd.HospitaManagementSystem.Business
{
    public interface IHMSInvoice
    {
        bool InsertInvoice(InvoiceDetialsDTO Invoice);

        bool EditInvoice(InvoiceDetialsDTO Invoice);

        bool DeleteInvoice(int InvoiceId);

        IEnumerable<InvoiceDetialsDTO> ViewtInvoice();

        bool CheckPatientAvalabilityForMakeInvoice(int? patientId);

        InvoiceDetialsDTO ViewtInvoiceById(int? InvoiceId);


        InvoiceDetialsDTO ViewInitialInoice(int? patientId);

        decimal CalculateInvoice(InvoiceDetialsDTO allDetails);

        bool validateDatesForInvoices(DateTime admitDate, DateTime invoiceDate);
    }
}
