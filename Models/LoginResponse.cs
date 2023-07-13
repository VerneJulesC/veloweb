using System.Collections;

namespace veloservices.Models
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            roles = new List<string>();
            vlogin = "N";
            token = "anythingfornow";
            rtoken = "0";
        }
        public int user_id { get; set; }
        public string? username { get; set; }
        public List<string> roles { get; set; }
        public string vlogin { get; set; }
        public string token { get; set; }
        public string rtoken { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
