namespace veloapp.Models
{
    public class RoleAddRequest
    {
        public RoleAddRequest()
        {
            this.rolename = new List<string>();
        }
        public int? user_id { get; set; }
        public List<string> rolename { get; set; }
        public string? dbrole { get; set; }
        public int? doctor_id { get; set; }
    }
}
