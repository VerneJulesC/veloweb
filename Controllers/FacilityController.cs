using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using veloapp.Models;
using veloservices.Models;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public FacilityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<FacilityController>
        [HttpGet]
        public string Get()
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("FacilitiesList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<Facility> facilities = new List<Facility>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Facility facility = new Facility();
                    if (dt.Rows[i] is not null)
                    {
                        facility.facility_id = Convert.ToInt32(dt.Rows[i]["facility_id"]);
                        facility.facility_name = Convert.ToString(dt.Rows[i]["facility_name"]);
                        facility.facility_doctor_id = Convert.ToInt32(dt.Rows[i]["facility_doctor_id"]);
                        facility.facility_doctor = Convert.ToString(dt.Rows[i]["facility_doctor"]);
                        facility.facility_address = Convert.ToString(dt.Rows[i]["facility_address"]);
                        facility.facility_coordinates = Convert.ToString(dt.Rows[i]["facility_coordinates"]);
                        facility.facility_city = Convert.ToString(dt.Rows[i]["facility_city"]);
                        facility.facility_state = Convert.ToString(dt.Rows[i]["facility_state"]);
                        facility.facility_zip = Convert.ToString(dt.Rows[i]["facility_zip"]);
                        facility.facility_ein = Convert.ToString(dt.Rows[i]["facility_ein"]);
                        facility.facility_ssn = Convert.ToString(dt.Rows[i]["facility_ssn"]);
                        facility.facility_npi = Convert.ToString(dt.Rows[i]["facility_npi"]);
                        facility.facility_fax = Convert.ToString(dt.Rows[i]["facility_fax"]);
                        facility.facility_email = Convert.ToString(dt.Rows[i]["facility_email"]);
                        facility.facility_phone = Convert.ToString(dt.Rows[i]["facility_phone"]);
                        facilities.Add(facility);
                    }
                }
            }
            /*if (facilities.Count > 0)
            {
                return JsonConvert.SerializeObject(facilities);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(facilities);
        }

        // GET api/<FacilityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM facility where facility_id = @id", con);
            da.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            });
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Facility> facilities = new List<Facility>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Facility facility = new Facility();
                    if (dt.Rows[i] is not null)
                    {
                        facility.facility_id = Convert.ToInt32(dt.Rows[i]["facility_id"]);
                        facility.facility_name = Convert.ToString(dt.Rows[i]["facility_name"]);
                        facility.facility_doctor_id = Convert.ToInt32(dt.Rows[i]["facility_doctor_id"]);
                        facility.facility_doctor = Convert.ToString(dt.Rows[i]["facility_doctor"]);
                        facility.facility_address = Convert.ToString(dt.Rows[i]["facility_address"]);
                        facility.facility_coordinates = Convert.ToString(dt.Rows[i]["facility_coordinates"]);
                        facility.facility_city = Convert.ToString(dt.Rows[i]["facility_city"]);
                        facility.facility_state = Convert.ToString(dt.Rows[i]["facility_state"]);
                        facility.facility_zip = Convert.ToString(dt.Rows[i]["facility_zip"]);
                        facility.facility_ein = Convert.ToString(dt.Rows[i]["facility_ein"]);
                        facility.facility_ssn = Convert.ToString(dt.Rows[i]["facility_ssn"]);
                        facility.facility_npi = Convert.ToString(dt.Rows[i]["facility_npi"]);
                        facility.facility_fax = Convert.ToString(dt.Rows[i]["facility_fax"]);
                        facility.facility_email = Convert.ToString(dt.Rows[i]["facility_email"]);
                        facility.facility_phone = Convert.ToString(dt.Rows[i]["facility_phone"]);
                        facilities.Add(facility);
                    }
                }
            }
            /*if (facilities.Count > 0)
            {
                return JsonConvert.SerializeObject(facilities);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(facilities);
        }

        // POST api/<FacilityController>/5
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] FacilityAddRequest value)
        {
            Facility fac = new Facility();
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UpdateFacility", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            List<Facility> facilities = new List<Facility>();
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_name",
                    Value = value.facility_name,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_doctor_id",
                    Value = value.facility_doctor_id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_doctor",
                    Value = value.facility_doctor,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_address",
                    Value = value.facility_address,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_coordinates",
                    Value = value.facility_coordinates,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_city",
                    Value = value.facility_city,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_state",
                    Value = value.facility_state,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_zip",
                    Value = value.facility_zip,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_ein",
                    Value = value.facility_ein,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_ssn",
                    Value = value.facility_ssn,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_npi",
                    Value = value.facility_npi,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_fax",
                    Value = value.facility_fax,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_email",
                    Value = value.facility_email,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_phone",
                    Value = value.facility_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
            }
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Facility facility = new Facility();
                    if (dt.Rows[i] is not null)
                    {
                        facility.facility_id = Convert.ToInt32(dt.Rows[i]["facility_id"]);
                        facility.facility_name = Convert.ToString(dt.Rows[i]["facility_name"]);
                        facility.facility_doctor_id = Convert.ToInt32(dt.Rows[i]["facility_doctor_id"]);
                        facility.facility_doctor = Convert.ToString(dt.Rows[i]["facility_doctor"]);
                        facility.facility_address = Convert.ToString(dt.Rows[i]["facility_address"]);
                        facility.facility_coordinates = Convert.ToString(dt.Rows[i]["facility_coordinates"]);
                        facility.facility_city = Convert.ToString(dt.Rows[i]["facility_city"]);
                        facility.facility_state = Convert.ToString(dt.Rows[i]["facility_state"]);
                        facility.facility_zip = Convert.ToString(dt.Rows[i]["facility_zip"]);
                        facility.facility_ein = Convert.ToString(dt.Rows[i]["facility_ein"]);
                        facility.facility_ssn = Convert.ToString(dt.Rows[i]["facility_ssn"]);
                        facility.facility_npi = Convert.ToString(dt.Rows[i]["facility_npi"]);
                        facility.facility_fax = Convert.ToString(dt.Rows[i]["facility_fax"]);
                        facility.facility_email = Convert.ToString(dt.Rows[i]["facility_email"]);
                        facility.facility_phone = Convert.ToString(dt.Rows[i]["facility_phone"]);
                        facilities.Add(facility);
                    }
                }
            }
            if (facilities.Count > 0)
            {
                return JsonConvert.SerializeObject(facilities);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to update facility";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<FacilityController>
        [HttpPost]
        public string Post([FromBody] FacilityAddRequest value)
        {
            Facility fac = new Facility();
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("AddFacility", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_name",
                    Value = value.facility_name,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_doctor_id",
                    Value = value.facility_doctor_id,
                    SqlDbType = SqlDbType.Int
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_doctor",
                    Value = value.facility_doctor,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_address",
                    Value = value.facility_address,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_coordinates",
                    Value = value.facility_coordinates,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_city",
                    Value = value.facility_city,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_state",
                    Value = value.facility_state,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_zip",
                    Value = value.facility_zip,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_ein",
                    Value = value.facility_ein,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_ssn",
                    Value = value.facility_ssn,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_npi",
                    Value = value.facility_npi,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_fax",
                    Value = value.facility_fax,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@facility_email",
                    Value = value.facility_email,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_phone",
                    Value = value.facility_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@facility_id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@facility_id"].Value);
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to add facility";
                return JsonConvert.SerializeObject(response);
            }
        }

        // PUT api/<FacilityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacilityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
