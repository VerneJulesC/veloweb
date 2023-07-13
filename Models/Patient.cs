using Newtonsoft.Json;
using veloapp.Utils;

namespace veloapp.Models
{
    public class Patient
    {
        public Patient()
        {
            rowclass = "";
            filtered = false;
            dbcols = new string[] {
                "patient_id",
                "doctor_id",
                "patient_doctor",
                "patient_fname",
                "patient_mname",
                "patient_lname",
                "patient_address",
                "patient_coordinates",
                "patient_city",
                "patient_state",
                "patient_zip",
                "patient_bdate",
                "patient_sex",
                "patient_phone",
                "patient_email"
            };
            pk = new string[] { "oatient_id" };
            colsForUpdate = new string[] {
                "doctor_id",
                "patient_doctor",
                "patient_fname",
                "patient_mname",
                "patient_lname",
                "patient_address",
                "patient_coordinates",
                "patient_city",
                "patient_state",
                "patient_zip",
                "patient_bdate",
                "patient_sex",
                "patient_phone",
                "patient_email"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
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
        public string? rowclass { get; set; }
        public bool filtered { get; set; }
    }
}
