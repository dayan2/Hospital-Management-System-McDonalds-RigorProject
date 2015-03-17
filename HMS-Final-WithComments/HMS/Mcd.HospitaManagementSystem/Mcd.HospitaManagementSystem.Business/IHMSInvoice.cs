using Mcd.HospitaManagementSystem.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mcd.HospitaManagementSystem.Business
{
    public interface IHMSInvoice
    {
        void InsertInvoice(InvoiceDTO Invoice);

        void EditInvoice(InvoiceDTO Invoice);

        void DeleteInvoice(int InvoiceId);

        IEnumerable<InvoiceDTO> ViewtInvoice();

        InvoiceDTO ViewtInvoiceById(int? InvoiceId);

        void Dispose(bool disposing);
    }
}
