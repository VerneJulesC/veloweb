using Newtonsoft.Json;
using veloapp.Utils;

namespace veloapp.Models
{
    public class PatientAddRequest
    {
        public PatientAddRequest()
        {
        }
        public int patient_id { get; set; }
        public int? doctor_id { get; set; }
        public string? patient_doctor { get; set; }
        public string? patient_fname { get; set; }
        public string? patient_mname { get; set; }
        public string? patient_lname { get; set; }
        public string? patient_address { get; set; }
        public string? patient_coordinates { get; set; }
        public string? patient_city { get; set; }
        public string? patient_state { get; set; }
        public string? patient_zip { get; set; }
        public string? patient_bdate { get; set; }
        public string? patient_sex { get; set; }
        public string? patient_phone { get; set; }
        public string? patient_email { get; set; }
    }
}
