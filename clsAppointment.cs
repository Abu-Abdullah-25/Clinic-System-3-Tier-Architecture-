using ClinicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class clsAppointment
    {
        public enum enMode { AddNew, Update }
        public enMode _Mode = enMode.AddNew;

        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public clsPatient PatientInfo { get; set; }

        public int DoctorID { get; set; }
        public clsDoctor DoctorInfo { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        public short AppointmentStatus { get; set; }

        public int MedicalRecordID { get; set; }
        public clsMedicalRecord MedicalRecordInfo { get; set; }

        public int PaymentID { get; set; }
        public clsPayment PaymentInfo{ get; set; }

        public clsAppointment()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = -1;
            this.AppointmentDateTime = DateTime.Now;
            this.AppointmentStatus = -1;
            this.MedicalRecordID = -1;
            this.PaymentID = -1;

            this._Mode = enMode.AddNew;
        }

        public clsAppointment(int AppointmentID, int PatientID, int DoctorID, DateTime AppointmentDateTime,
                              short AppointmentStatus, int MedicalRecordID, int PaymentID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.PatientInfo = clsPatient.FindByPatientID(PatientID);
            this.DoctorID = DoctorID;
            this.DoctorInfo = clsDoctor.FindByDoctorID(DoctorID);
            this.AppointmentDateTime = AppointmentDateTime;
            this.AppointmentStatus = AppointmentStatus;
            this.MedicalRecordID = MedicalRecordID;
            this.MedicalRecordInfo = clsMedicalRecord.FindByMedicalRecordID(MedicalRecordID);
            this.PaymentID = PaymentID;
            this.PaymentInfo = clsPayment.Find(PaymentID);

            this._Mode = enMode.Update;
        }

        private bool _AddNewAppointment()
        {
            this.AppointmentID = clsAppointmentData.AddNewAppointment(this.PatientID,
                this.DoctorID, this.AppointmentDateTime, this.AppointmentStatus, this.MedicalRecordID, this.PaymentID);

            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            return clsAppointmentData.UpdateAppointment(this.AppointmentID, this.PatientID, this.DoctorID, this.AppointmentDateTime,
                              this.AppointmentStatus, this.MedicalRecordID, this.PaymentID);
        }

        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:
                    return _AddNewAppointment();

                case enMode.Update:
                    return _UpdateAppointment();
            }

            return false;
        }

        public static clsAppointment Find(int AppointmentID)
        {
            int PatientID = -1, DoctorID = -1, MedicalRecordID = -1, PaymentID = -1;
            short AppointmentStatus = -1;
            DateTime AppointmentDateTime = DateTime.Now;

            bool isFound = clsAppointmentData.GetAppointmentInfoByID(AppointmentID, ref PatientID, ref DoctorID,
                ref AppointmentDateTime, ref AppointmentStatus, ref MedicalRecordID, ref PaymentID);

            if(isFound)

                return new clsAppointment(AppointmentID, PatientID, DoctorID,AppointmentDateTime,
                    AppointmentStatus,MedicalRecordID, PaymentID);

            else
                return null;
        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            return clsAppointmentData.DeleteAppointment(AppointmentID);
        }

        public static bool isAppointmentExists(int AppointmentID)
        {
            return clsAppointmentData.IsAppointmentExist(AppointmentID);
        }

        public static DataTable GetAllAppointments()
        {
            return clsAppointmentData.GetAllAppointments();
        }

    }
}
