using ClinicDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBusiness
{
    public class clsDoctor
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int DoctorID { get; set; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;

        public string Specialization { get; set; }
        
        public clsDoctor()
        {
            this.DoctorID = -1;
            this.PersonID = -1;
            this.Specialization = "";

            Mode = enMode.AddNew;
        }

        private clsDoctor(int DoctorID, int PersonID , string Specialization)
        {
            this.DoctorID = DoctorID;
            this.PersonID = PersonID;
            this.Specialization = Specialization;
            this.PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }


        private bool _AddNewDoctor()
        {
            //call DataAccess Layer 

            this.DoctorID = clsDoctorData.AddNewDoctor(this.PersonID , this.Specialization);

            return (this.DoctorID != -1);
        }

        private bool _UpdateDoctor()
        {

            //call DataAccess Layer 

            return clsDoctorData.UpdateDoctor(this.DoctorID, this.PersonID , this.Specialization);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDoctor())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDoctor();

            }

            return false;
        }



        public static clsDoctor FindByDoctorID(int DoctorID)
        {
            //not Completed here
            int PersonID = -1;
            string Specialization = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByDoctorID
                                (DoctorID, ref PersonID , ref Specialization);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsDoctor(DoctorID, PersonID , Specialization);
            else
                return null;
        }

        public static clsDoctor FindByPersonID(int PersonID)
        {
            //not Completed here
            int DoctorID = -1;
            string Specialization = "";

            bool IsFound = clsDoctorData.GetDoctorInfoByPersonID
                                (PersonID, ref DoctorID , ref Specialization);

            if (IsFound)
                //we return new object of that User with the right data
                return new clsDoctor(DoctorID, PersonID , Specialization);
            else
                return null;
        }

        public static bool DeleteDoctor(int DoctorID)
        {
            return clsDoctorData.DeleteDoctor(DoctorID);
        }

        public static bool isDoctorExists(int DoctorID)
        {
            return clsDoctorData.IsDoctorExist(DoctorID);
        }

        public static bool isDoctorExistForPersonID(int PersonID)
        {
            return clsDoctorData.IsDoctorExistForPersonID(PersonID);
        }

        public static DataTable GetAllDoctors()
        {
            return clsDoctorData.GetAllDoctors();
        }

    }
}
