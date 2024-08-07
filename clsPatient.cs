using ClinicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class clsPatient
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int PatientID { get; set; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;

        public clsPatient()
        {
            this.PatientID = -1;
            this.PersonID = -1;

            Mode = enMode.AddNew;
        }

        private clsPatient(int PatientID,int PersonID)
        {
            this.PatientID = PatientID;
            this.PersonID = PersonID;

            Mode = enMode.Update;
        }


        private bool _AddNewPatient()
        {
            //call DataAccess Layer 

            this.PatientID = clsPatientData.AddNewPatient(this.PersonID);

            return (this.PatientID != -1);
        }

        private bool _UpdatePatient()
        {

            //call DataAccess Layer 

            return clsPatientData.UpdatePatient(this.PatientID,this.PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePatient();

            }

            return false;
        }



        public static clsPatient FindByPatientID(int PatientID)
        {
            //not Completed here
            int PersonID = -1;

            bool IsFound = clsPatientData.GetPatientInfoByPatientID
                                (PatientID, ref PersonID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsPatient(PatientID , PersonID);
            else
                return null;
        }

        public static clsPatient FindByPersonID(int PersonID)
        {
            //not Completed here
            int PatientID = -1;

            bool IsFound = clsPatientData.GetPatientInfoByPersonID
                                (PersonID, ref PatientID);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsPatient(PatientID, PersonID);
            else
                return null;
        }

        public static bool DeletePatient(int PatientID)
        {
            return clsPatientData.DeletePatient(PatientID);
        }

        public static bool isPatientExists(int PatientID)
        {
            return clsPatientData.IsPatientExists(PatientID);
        }

        public static bool isPatientExistForPersonID(int PersonID)
        {
            return clsPatientData.IsPatientExistForPersonID(PersonID);
        }


        public static DataTable GetAllPatients()
        {
            return clsPatientData.GetAllPatients();
        }

    }
}
