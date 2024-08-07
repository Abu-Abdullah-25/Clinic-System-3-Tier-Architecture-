using ClinicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class clsMedicalRecord
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int MedicalRecordID { get; set; }
        public string VisitDescription { set; get; }
        public string Diagnosis { set; get; }
        public string AdditionalNotes { set; get; }

        public clsMedicalRecord()
        {
            this.MedicalRecordID = -1;
            this.VisitDescription = "";
            this.Diagnosis = "";
            this.AdditionalNotes = "";

            Mode = enMode.AddNew;
        }

        private clsMedicalRecord(int MedicalRecordID, string VisitDescription, string Diagnosis,
            string AdditionalNotes)
        {
            this.MedicalRecordID = MedicalRecordID;
            this.VisitDescription = VisitDescription;
            this.Diagnosis = Diagnosis;
            this.AdditionalNotes = AdditionalNotes;

            Mode = enMode.Update;
        }


        private bool _AddNewMedicalRecord()
        {
            //call DataAccess Layer 

            this.MedicalRecordID = clsMedicalRecordData.AddNewMedicalRecord(this.VisitDescription, this.Diagnosis, 
                this.AdditionalNotes);

            return (this.MedicalRecordID != -1);
        }

        private bool _UpdateMedicalRecord()
        {

            //call DataAccess Layer 

            return clsMedicalRecordData.UpdateMedicalRecord(this.MedicalRecordID, this.VisitDescription, 
                this.Diagnosis,this.AdditionalNotes);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewMedicalRecord())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateMedicalRecord();

            }

            return false;
        }

        public static clsMedicalRecord FindByMedicalRecordID(int MedicalRecordID)
        {
            //not Completed here
            string VisitDescription = "", Diagnosis = "", AdditionalNotes = "";

            bool IsFound = clsMedicalRecordData.GetMedicalRecordInfoByID
                                (MedicalRecordID, ref VisitDescription, ref Diagnosis, ref AdditionalNotes);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsMedicalRecord(MedicalRecordID, VisitDescription, Diagnosis, AdditionalNotes);
            else
                return null;
        }


        public static bool DeleteMedicalRecord(int MedicalRecordID)
        {
            return clsMedicalRecordData.DeleteMedicalRecord(MedicalRecordID);
        }

        public static bool isMedicalRecordExists(int MedicalRecordID)
        {
            return clsMedicalRecordData.IsMedicalRecordExist(MedicalRecordID);
        }


        public static DataTable GetAllMedicalRecords()
        {
            return clsMedicalRecordData.GetAllMedicalRecords();
        }

    }
}
