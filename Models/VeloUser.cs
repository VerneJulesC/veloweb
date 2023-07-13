using System.Collections;

namespace veloservices.Models
{
    public class VeloUser
    {
        public VeloUser()
        {
            roles = new List<string>();
            rowclass = "";
            filtered = false;
            vlogin = "N";
            dbcols = new string[] {
                "user_id",
                "username",
                "password"
            };
            pk = new string[] { "user_id" };
            colsForUpdate = new string[] {
                "username"
            };
        }
        public string[] dbcols { get; set; }
        public string[] pk { get; set; }
        public string[] colsForUpdate { get; set; }
        public int user_id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public List<string> roles { get; set; }
        public string vlogin { get; set; }
        public string? rowclass { get; set; }
        public bool filtered { get; set; }

    }
}
