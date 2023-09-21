namespace veloapp.Models
{
    public class DoctorAddRequest
    {
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
    }
}
