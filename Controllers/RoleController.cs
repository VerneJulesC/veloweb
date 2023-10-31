using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using veloapp.Models;
using veloservices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public RoleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<RoleController>
        [HttpGet]
        public string Get()
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM velo_role", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<VeloRole> roles = new List<VeloRole>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VeloRole role = new VeloRole();
                    if (dt.Rows[i] is not null)
                    {
                        role.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                        role.rolename = Convert.ToString(dt.Rows[i]["rolename"]);
                        role.doctor_id = Convert.ToInt32(dt.Rows[i]["doctor_id"]);
                        roles.Add(role);
                    }
                }
            }
            if (roles.Count > 0)
            {
                return JsonConvert.SerializeObject(roles);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM velo_role where user_id = @id", con);
            da.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            });
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<VeloRole> roles = new List<VeloRole>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    VeloRole role = new VeloRole();
                    if (dt.Rows[i] is not null)
                    {
                        role.user_id = Convert.ToInt32(dt.Rows[i]["user_id"]);
                        role.rolename = Convert.ToString(dt.Rows[i]["rolename"]);
                        role.doctor_id = Convert.ToInt32(dt.Rows[i]["doctor_id"]);
                        roles.Add(role);
                    }
                }
            }
            if (roles.Count > 0)
            {
                return JsonConvert.SerializeObject(roles);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<RoleController>
        [HttpPost]
        public string Post([FromBody] RoleAddRequest value)

        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UpdateRoles", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
            cmd.CommandType = CommandType.StoredProcedure;
            if(value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@user_id",
                    Value = value.user_id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@rolenames",
                    Value = String.Join(",", value.rolename),
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@dbrole",
                    Value = value.dbrole,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@doctor_id",
                    Value = DBNull.Value,
                    SqlDbType = SqlDbType.NVarChar
                });
            }
            con.Open();
            da.Fill(dt);
            returnval = "Y";
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to add patient";
                return JsonConvert.SerializeObject(response);
            }
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
