using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using veloapp.Models;
using veloservices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public string Get()
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UsersList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<VeloUser> vu = new List<VeloUser>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VeloUser vuser = new VeloUser();
                    if (dt.Rows[i] is not null)
                    {
                        vuser.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                        vuser.username = Convert.ToString(dt.Rows[i]["username"]);
                        string? userroles = Convert.ToString(dt.Rows[i]["roles"])??" ";
                        string[] roles = userroles.Split(',');
                        if(userroles.Length > 1) {
                            foreach(string r in roles) {
                                vuser.roles.Add(r);
                            }
                        }
                        vu.Add(vuser);
                    }
                }
            }
            /*if (vu.Count > 0)
            {
                return JsonConvert.SerializeObject(vu);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(vu);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{uname}")]
        public string Get(string uname)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("getUserByUname", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter {
                ParameterName = "@username",
                Value = uname,
                SqlDbType = SqlDbType.NVarChar
            });
            da.Fill(dt);
            List<VeloUser> vu = new List<VeloUser>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VeloUser vuser = new VeloUser();
                    if (dt.Rows[i] is not null)
                    {
                        vuser.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                        vuser.username = Convert.ToString(dt.Rows[i]["username"]);
                        vuser.vlogin = Convert.ToString(dt.Rows[i]["vlogin"])??"N";
                        //vuser.password = Convert.ToString(dt.Rows[i]["password"]);
                        vu.Add(vuser);
                    }
                }
            }
            /*if (vu.Count > 0)
            {
                return JsonConvert.SerializeObject(vu);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(vu);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public string Post([FromBody] UserAddRequest value)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("CreateUser", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
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
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@user_id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@user_id"].Value);
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to add user";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<ValuesController>
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] UserAddRequest value)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UpdatePassword", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@user_id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                });
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
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@user_id"].Value);
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to update user";
                return JsonConvert.SerializeObject(response);
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
