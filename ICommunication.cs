using System;

namespace ClinicBusiness
{
    public interface ICommunication
    {
        void SendEmail(string title, string body);
        void SendFax(string title, string body);
        void SendSMS(string title, string body);
    }
}