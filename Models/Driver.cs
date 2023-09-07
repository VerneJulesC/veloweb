namespace veloapp.Models
{
    public class Driver
    {
        public Driver()
        {
            rowclass = "";
            filtered = false;
            dbcols = new string[] {
                "driver_id",
                "driver_docid",
                "driver_fname",
                "driver_mname",
                "driver_lname",
                "driver_phone",
                "driver_car_color",
                "driver_car_model"
            };
            pk = new string[] { "driver_id" };
            colsForUpdate = new string[] {
                "driver_docid",
                "driver_fname",
                "driver_mname",
                "driver_lname",
                "driver_phone",
                "driver_car_color",
                "driver_car_model"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
        public int driver_id { get; set; }
        public string? driver_docid { get; set; }
        public string? driver_fname { get; set; }
        public string? driver_mname { get; set; }
        public string? driver_lname { get; set; }
        public string? driver_phone { get; set; }
        public string? driver_car_color { get; set; }
        public string? driver_car_model { get; set; }
        public string? rowclass { get; set; }
        public bool filtered { get; set; }
    }
}
