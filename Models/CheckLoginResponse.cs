using System.Collections;

namespace veloservices.Models
{
    public class CheckLoginResponse
    {
        public CheckLoginResponse()
        {
            roles = new List<string>();
            vlogin = "N";
        }
        public string? username { get; set; }
        public string vlogin { get; set; }
        public List<string> roles { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
