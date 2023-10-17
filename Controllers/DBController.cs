using Microsoft.AspNetCore.Mvc;
using System.Data;
using veloservices.Models;
using veloapp.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public DBController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<DBController>
        [HttpGet]
        public string Get()
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("DBList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<string> databases = new List<string>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i] is not null)
                    {
                        string? dbn = Convert.ToString(dt.Rows[i]["dbname"]);
                        if(dbn is not null){
                            databases.Add(dbn);
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(databases);
        }
    }
}
