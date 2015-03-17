using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mcd.HospitaManagementSystem.Business;
using Mcd.HospitalManagementSystem.Data;
using System.Linq;

namespace Mcd.HospitalManagement.Web.Tests
{
    [TestClass]
    public class InvoiceTest
    {
        /// <summary>
        /// Invoice class ViewtInvoiceById Method should return a Invoice Object to the controller
        /// </summary>
        [TestMethod]
        public void InvoiceDetailsGetInvoiceByIdMethodShouldReturnInvoiceObject()
        {
            IHMSInvoice invoice = new HMSInvoice();
            InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO
            {
                PatientDetailId=18,
                InvoiceDate= new DateTime(2015,02,28),
                

            };
            invoice.InsertInvoice(invoiceDto);

            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                var lastInvoiceIndex = db.Invoices.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedInvoice = invoice.ViewtInvoiceById(Convert.ToInt32(lastInvoiceIndex));

                //Assert
                Assert.AreEqual(18, expectedInvoice.PatientDetailId);
                //Assert.AreEqual(new DateTime(2015, 02, 28), expectedInvoice.InvoiceDate);

                //Deleting the testing Invoice object
                invoice.DeleteInvoice(Convert.ToInt32(lastInvoiceIndex));
            }
        }

        /// <summary>
        ///  HMSInvoice class InsertInvoice Method should save the invoice Object in the Database
        /// </summary>
        [TestMethod]
        public void InvoiceDetailsInsertInvoiceMethodSavesAInvoiceObjectViaContext()
        {
            // Arrange
            IHMSInvoice invoice = new HMSInvoice();
            InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO
            {
                PatientDetailId = 18,
                InvoiceDate = DateTime.Now,


            };

            //act
            invoice.InsertInvoice(invoiceDto);

            using (var db = new LP_HMSDbEntities())
            {
                var lastInvoiceIndex = db.Invoices.OrderByDescending(u => u.Id).Max(c => c.Id);

                var expectedInvoice = invoice.ViewtInvoiceById(Convert.ToInt32(lastInvoiceIndex));

                //Assert
                Assert.IsInstanceOfType(expectedInvoice, typeof(InvoiceDetialsDTO));

                //Deleting the testing invoice object
                invoice.DeleteInvoice(Convert.ToInt32(lastInvoiceIndex));
            }

        }

        /// <summary>
        /// PatientMedicalTestAllocation class DeletePatientMedicalTest Method should remove PatientMedicalTest Objects from the Database
        /// </summary>
        [TestMethod]
        public void InvoiceDetailsDeleteInvoiceMethodShouldRemoveObjectsFromDB()
        {
            // Arrange
            IHMSInvoice invoice = new HMSInvoice();
            InvoiceDetialsDTO invoiceDto = new InvoiceDetialsDTO
            {
                PatientDetailId = 18,
                InvoiceDate = DateTime.Now,


            };

            // Act
            invoice.InsertInvoice(invoiceDto);
            using (LP_HMSDbEntities db = new LP_HMSDbEntities())
            {
                //var lastDoctor = db.Doctors.Max();
                var lastInvocie = db.Invoices.OrderByDescending(u => u.Id).FirstOrDefault();

                //Deleting the testing Doctor object
                invoice.DeleteInvoice(Convert.ToInt32(lastInvocie.Id));
            }
        }
    }
}
