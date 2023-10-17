using Microsoft.AspNetCore.Mvc;
using System.Data;
using veloservices.Models;
using veloapp.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloapp.Controllers
{
    [Route("api/{dbname}/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public DriverController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<DriverController>
        [HttpGet]
        public string Get(string dbname)
        {
            string? constring = (_configuration.GetConnectionString("VeloAppCon")??"velo").Replace("velo", dbname);
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("DriversList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<Driver> drivers = new List<Driver>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Driver driver = new Driver();
                    if (dt.Rows[i] is not null)
                    {
                        driver.driver_id = Convert.ToInt32(dt.Rows[i]["driver_id"]);
                        driver.driver_docid = Convert.ToString(dt.Rows[i]["driver_docid"]);
                        driver.driver_fname = Convert.ToString(dt.Rows[i]["driver_fname"]);
                        driver.driver_mname = Convert.ToString(dt.Rows[i]["driver_mname"]);
                        driver.driver_lname = Convert.ToString(dt.Rows[i]["driver_lname"]);
                        driver.driver_phone = Convert.ToString(dt.Rows[i]["driver_phone"]);
                        driver.driver_car_color = Convert.ToString(dt.Rows[i]["driver_car_color"]);
                        driver.driver_car_model = Convert.ToString(dt.Rows[i]["driver_car_model"]);
                        drivers.Add(driver);
                    }
                }
            }
            /*if (drivers.Count > 0)
            {
                return JsonConvert.SerializeObject(drivers);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(drivers);
        }

        // GET api/<DriverController>/5
        [HttpGet("{id}")]
        public string Get(string dbname, int id)
        {
            string? constring = (_configuration.GetConnectionString("VeloAppCon")??"velo").Replace("velo", dbname);
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM driver where driver_id = @id", con);
            da.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            });
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Driver> drivers = new List<Driver>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Driver driver = new Driver();
                    if (dt.Rows[i] is not null)
                    {
                        driver.driver_id = Convert.ToInt32(dt.Rows[i]["driver_id"]);
                        driver.driver_docid = Convert.ToString(dt.Rows[i]["driver_docid"]);
                        driver.driver_fname = Convert.ToString(dt.Rows[i]["driver_fname"]);
                        driver.driver_mname = Convert.ToString(dt.Rows[i]["driver_mname"]);
                        driver.driver_lname = Convert.ToString(dt.Rows[i]["driver_lname"]);
                        driver.driver_phone = Convert.ToString(dt.Rows[i]["driver_phone"]);
                        driver.driver_car_color = Convert.ToString(dt.Rows[i]["driver_car_color"]);
                        driver.driver_car_model = Convert.ToString(dt.Rows[i]["driver_car_model"]);
                        drivers.Add(driver);
                    }
                }
            }
            /*if (drivers.Count > 0)
            {
                return JsonConvert.SerializeObject(drivers);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(drivers);
        }

        // POST api/<DriverController>
        // Update Driver
        [HttpPost("{id}")]
        public string Post(string dbname, int id, [FromBody] DriverAddRequest value)
        {
            Driver doc = new Driver();
            string? constring = (_configuration.GetConnectionString("VeloAppCon")??"velo").Replace("velo", dbname);
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UpdateDriver", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            List<Driver> drivers = new List<Driver>();
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_docid",
                    Value = value.driver_docid,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_fname",
                    Value = value.driver_fname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_mname",
                    Value = value.driver_mname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_lname",
                    Value = value.driver_lname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_phone",
                    Value = value.driver_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_car_color",
                    Value = value.driver_car_color,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@driver_car_model",
                    Value = value.driver_car_model,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@driver_id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                });
            }
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Driver driver = new Driver();
                    if (dt.Rows[i] is not null)
                    {
                        driver.driver_id = Convert.ToInt32(dt.Rows[i]["driver_id"]);
                        driver.driver_docid = Convert.ToString(dt.Rows[i]["driver_docid"]);
                        driver.driver_fname = Convert.ToString(dt.Rows[i]["driver_fname"]);
                        driver.driver_mname = Convert.ToString(dt.Rows[i]["driver_mname"]);
                        driver.driver_lname = Convert.ToString(dt.Rows[i]["driver_lname"]);
                        driver.driver_phone = Convert.ToString(dt.Rows[i]["driver_phone"]);
                        driver.driver_car_color = Convert.ToString(dt.Rows[i]["driver_car_color"]);
                        driver.driver_car_model = Convert.ToString(dt.Rows[i]["driver_car_model"]);
                        drivers.Add(driver);
                    }
                }
            }
            if (drivers.Count > 0)
            {
                return JsonConvert.SerializeObject(drivers[0]);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to update driver";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<DriverController>
        // Add Driver
        [HttpPost]
        public string Post(string dbname, [FromBody] DriverAddRequest value)
        {
            Driver doc = new Driver();
            string? constring = (_configuration.GetConnectionString("VeloAppCon")??"velo").Replace("velo", dbname);
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("AddDriver", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_docid",
                    Value = value.driver_docid,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_fname",
                    Value = value.driver_fname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_mname",
                    Value = value.driver_mname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_lname",
                    Value = value.driver_lname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_phone",
                    Value = value.driver_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@driver_car_color",
                    Value = value.driver_car_color,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@driver_car_model",
                    Value = value.driver_car_model,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@driver_id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@driver_id"].Value);
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to add driver";
                return JsonConvert.SerializeObject(response);
            }
        }

        // PUT api/<DriverController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DriverController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
