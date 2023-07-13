namespace veloapp.Models
{
    public class Doctor
    {
        public Doctor()
        {
            rowclass = "";
            filtered = false;
            dbcols = new string[] {
                "doctor_id",
                "provider_id",
                "doctor_fname",
                "doctor_mname",
                "doctor_lname",
                "doctor_address",
                "doctor_city",
                "doctor_state",
                "doctor_zip",
                "doctor_ein",
                "doctor_upin",
                "doctor_ssn",
                "doctor_npi",
                "doctor_license",
                "doctor_fax",
                "doctor_email",
                "doctor_phone"
            };
            pk = new string[] { "doctor_id" };
            colsForUpdate = new string[] {
                "provider_id",
                "doctor_fname",
                "doctor_mname",
                "doctor_lname",
                "doctor_address",
                "doctor_city",
                "doctor_state",
                "doctor_zip",
                "doctor_ein",
                "doctor_upin",
                "doctor_ssn",
                "doctor_npi",
                "doctor_license",
                "doctor_fax",
                "doctor_email",
                "doctor_phone"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
        public int doctor_id { get; set; }
        public string? provider_id { get; set; }
        public string? doctor_fname { get; set; }
        public string? doctor_mname { get; set; }
        public string? doctor_lname { get; set; }
        public string? doctor_address { get; set; }
        public string? doctor_city { get; set; }
        public string? doctor_state { get; set; }
        public string? doctor_zip { get; set; }
        public string? doctor_ein { get; set; }
        public string? doctor_upin { get; set; }
        public string? doctor_ssn { get; set; }
        public string? doctor_npi { get; set; }
        public string? doctor_license { get; set; }
        public string? doctor_fax { get; set; }
        public string? doctor_email { get; set; }
        public string? doctor_phone { get; set; }
        public string? rowclass { get; set; }
        public bool filtered { get; set; }
    }
}
