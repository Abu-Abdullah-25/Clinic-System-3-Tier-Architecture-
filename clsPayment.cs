using ClinicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class clsPayment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PaymentID { get; set; }
        public DateTime PaymentDate { set; get; }
        public string PaymentMethod { set; get; }
        public decimal AmountPaid { get; set; }
        public string AdditionalNotes { get; set; }

        public clsPayment()
        {
            this.PaymentID = -1;
            this.PaymentDate = DateTime.Now;
            this.PaymentMethod = "";
            this.AmountPaid = 0;
            this.AdditionalNotes = "";

            Mode = enMode.AddNew;
        }

        private clsPayment(int PaymentID, DateTime PaymentDate, string PaymentMethod, decimal AmountPaid, string AdditionalNotes)
        {
            this.PaymentID = PaymentID;
            this.PaymentDate = PaymentDate;
            this.PaymentMethod = PaymentMethod;
            this.AmountPaid = AmountPaid;
            this.AdditionalNotes = AdditionalNotes;

            Mode = enMode.Update;
        }


        private bool _AddNewPayment()
        {
            //call DataAccess Layer 

            this.PaymentID = clsPaymentData.AddNewPayment(this.PaymentDate,this.PaymentMethod,
                this.AmountPaid,this.AdditionalNotes);

            return (this.PaymentID != -1);
        }

        private bool _UpdatePayment()
        {

            //call DataAccess Layer 

            return clsPaymentData.UpdatePayment(this.PaymentID, this.PaymentDate, this.PaymentMethod,
                this.AmountPaid, this.AdditionalNotes);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPayment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePayment();

            }

            return false;
        }



        public static clsPayment Find(int PaymentID)
        {
            //not Completed here
            DateTime PaymentDate = DateTime.Now;
            string PaymentMethod = "", AdditionalNotes = "";
            decimal AmountPaid = 0;

            bool IsFound = clsPaymentData.GetPaymentInfoByID
                                (PaymentID, ref PaymentDate, ref PaymentMethod, ref AmountPaid, ref AdditionalNotes);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsPayment(PaymentID, PaymentDate, PaymentMethod, AmountPaid, AdditionalNotes);
            else
                return null;
        }



        public static bool DeletePayment(int PaymentID)
        {
            return clsPaymentData.DeletePayment(PaymentID);
        }

        public static bool isPaymentExists(int PaymentID)
        {
            return clsPaymentData.IsPaymentExist(PaymentID);
        }


        public static DataTable GetAllPayments()
        {
            return clsPaymentData.GetAllPayments();
        }

    }
}
