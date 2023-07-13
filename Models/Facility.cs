namespace veloapp.Models
{
    public class Facility
    {
        public Facility()
        {
            rowclass = "";
            filtered = false;
            dbcols = new string[] {
                "facility_id",
                "facility_name",
                "facility_doctor_id",
                "facility_doctor",
                "facility_address",
                "facility_coordinates",
                "facility_city",
                "facility_state",
                "facility_zip",
                "facility_ein",
                "facility_ssn",
                "facility_npi",
                "facility_fax",
                "facility_email",
                "facility_phone"
            };
            pk = new string[] { "facility_id" };
            colsForUpdate = new string[] {
                "facility_name",
                "facility_doctor_id",
                "facility_doctor",
                "facility_address",
                "facility_coordinates",
                "facility_city",
                "facility_state",
                "facility_zip",
                "facility_ein",
                "facility_ssn",
                "facility_npi",
                "facility_fax",
                "facility_email",
                "facility_phone"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
        public int facility_id { get; set; }
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
        public string? rowclass { get; set; }
        public bool filtered { get; set; }
    }
}
