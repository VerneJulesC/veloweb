namespace veloapp.Models
{
    public class VeloRole
    {
        public VeloRole()
        {
            rowclass = "";
            filtered = false;
            dbcols = new string[] {
                "user_id",
                "rolename",
                "doctor_id"
            };
            pk = new string[] { "user_id" };
            colsForUpdate = new string[] {
                "rolename",
                "doctor_id"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
        public int user_id { get; set; }
        public string? rolename { get; set; }
        public int? doctor_id { get; set; }
        public string? rowclass { get; set; }
        public bool filtered { get; set; }
    }
}
