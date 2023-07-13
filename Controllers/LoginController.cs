using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using veloapp.Models;
using veloservices.Models;

namespace veloapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/<ValuesController>/<username>
        // checkLogin
        [HttpPost("{uname}")]
        public string Post(string uname, [FromHeader(Name = "Authorization")] string authentication)
        {
            string[] auth = authentication.Split(' ');
            string? token = null;
            string? rtoken = null;
            int rolenum = 0;
            List<string> roles = new List<string>();
            CheckLoginResponse response = new CheckLoginResponse();
            if (auth.Count() > 1)
            {
                token = auth[1];
            }
            if (auth.Count() > 2)
            {
                rtoken = auth[2]??"0";
                try
                {
                    rolenum = Convert.ToInt32(rtoken);
                }catch(FormatException fe)
                {
                    rolenum = 0;
                    Console.WriteLine(fe.Message);
                }
            }
            switch (rolenum)
            {
                case 0:
                    break;
                case 1:
                    roles.Add("admin");
                    break;
                case 2:
                    roles.Add("doctor");
                    break;
                case 3:
                    roles.Add("admin");
                    roles.Add("doctor");
                    break;
                default:
                    break;
            }
            response.roles = roles;
            response.username = uname;
            if(token is not null)
            {
                response.vlogin = "Y";
            }
            else {
                response.vlogin = "N";
            }
            return JsonConvert.SerializeObject(response);
        }

        // GET: api/<ValuesController>
        // verifyLogin
        [HttpPost()]
        public string Post([FromBody] LoginRequest value)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VeloAppCon").ToString());
            SqlCommand cmd = new SqlCommand("VerifyLogin", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            LoginResponse lr = new LoginResponse();
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@username",
                    Value = value.username,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@password",
                    Value = value.password,
                    SqlDbType = SqlDbType.NVarChar
                });
                lr.username = value.username;
            }
            con.Open();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0] is not null)
                {
                    lr.user_id = Convert.ToInt32(dt.Rows[0]["user_id"]);
                    lr.vlogin = Convert.ToString(dt.Rows[0]["vlogin"]) ?? "N";
                    lr.username = Convert.ToString(dt.Rows[0]["username"]) ?? lr.username;
                    string userroles = Convert.ToString(dt.Rows[0]["roles"]) ?? "";
                    string[] roles = userroles.Split(',');
                    int rnum = 0;
                    for (int i = 0; i < roles.Length; i++)
                    {
                        lr.roles.Add(roles[i]);
                        if (roles[i] == "admin")
                        {
                            rnum = 1;
                        }
                        if (roles[i] == "doctor")
                        {
                            if (rnum > 0)
                            {
                                rnum = 3;
                            }
                            else
                            {
                                rnum = 2;
                            }
                        }
                    }
                    lr.rtoken = rnum.ToString();
                }
            }
            con.Close();
            return JsonConvert.SerializeObject(lr);
        }
    }
}
