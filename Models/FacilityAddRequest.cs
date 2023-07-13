namespace veloapp.Models
{
    public class FacilityAddRequest
    {
        public string? facility_name { get; set; }
        public int? facility_doctor_id { get; set; }
        public string? facility_doctor { get; set; }
        public string? facility_address { get; set; }
        public string? facility_coordinates { get; set; }
        public string? facility_city { get; set; }
        public string? facility_state { get; set; }
        public string? facility_zip { get; set; }
        public string? facility_ein { get; set; }
        public string? facility_ssn { get; set; }
        public string? facility_npi { get; set; }
        public string? facility_fax { get; set; }
        public string? facility_email { get; set; }
        public string? facility_phone { get; set; }
    }
}
