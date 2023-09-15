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
    public class PatientController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public PatientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<PatientController>
        [HttpGet]
        public string Get()
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("PatientsList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<Patient> patients = new List<Patient>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patient patient = new Patient();
                    if (dt.Rows[i] is not null)
                    {
                        patient.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        if (dt.Rows[i]["doctor_id"] == DBNull.Value)
                        {
                            patient.doctor_id = null;
                        }
                        else
                        {
                            Convert.ToInt32(dt.Rows[i]["doctor_id"]);
                        }
                        patient.patient_doctor = Convert.ToString(dt.Rows[i]["patient_doctor"]);
                        patient.patient_fname = Convert.ToString(dt.Rows[i]["patient_fname"]);
                        patient.patient_mname = Convert.ToString(dt.Rows[i]["patient_mname"]);
                        patient.patient_lname = Convert.ToString(dt.Rows[i]["patient_lname"]);
                        patient.patient_address = Convert.ToString(dt.Rows[i]["patient_address"]);
                        patient.patient_coordinates = Convert.ToString(dt.Rows[i]["patient_coordinates"]);
                        patient.patient_city = Convert.ToString(dt.Rows[i]["patient_city"]);
                        patient.patient_state = Convert.ToString(dt.Rows[i]["patient_state"]);
                        patient.patient_zip = Convert.ToString(dt.Rows[i]["patient_zip"]);
                        patient.patient_bdate = Convert.ToString(dt.Rows[i]["patient_bdate"]);
                        patient.patient_sex = Convert.ToString(dt.Rows[i]["patient_sex"]);
                        patient.patient_phone = Convert.ToString(dt.Rows[i]["patient_phone"]);
                        patient.patient_email = Convert.ToString(dt.Rows[i]["patient_email"]);
                        patients.Add(patient);
                    }
                }
            }
            /*if (patients.Count > 0)
            {
                return JsonConvert.SerializeObject(patients);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(patients);
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM patient where patient_id = @id", con);
            da.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            });
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Patient> patients = new List<Patient>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patient patient = new Patient();
                    if (dt.Rows[i] is not null)
                    {
                        patient.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        if (dt.Rows[i]["doctor_id"] == DBNull.Value)
                        {
                            patient.doctor_id = null;
                        }
                        else
                        {
                            Convert.ToInt32(dt.Rows[i]["doctor_id"]);
                        }
                        patient.patient_doctor = Convert.ToString(dt.Rows[i]["patient_doctor"]);
                        patient.patient_fname = Convert.ToString(dt.Rows[i]["patient_fname"]);
                        patient.patient_mname = Convert.ToString(dt.Rows[i]["patient_mname"]);
                        patient.patient_lname = Convert.ToString(dt.Rows[i]["patient_lname"]);
                        patient.patient_address = Convert.ToString(dt.Rows[i]["patient_address"]);
                        patient.patient_coordinates = Convert.ToString(dt.Rows[i]["patient_coordinates"]);
                        patient.patient_city = Convert.ToString(dt.Rows[i]["patient_city"]);
                        patient.patient_state = Convert.ToString(dt.Rows[i]["patient_state"]);
                        patient.patient_zip = Convert.ToString(dt.Rows[i]["patient_zip"]);
                        patient.patient_bdate = Convert.ToString(dt.Rows[i]["patient_bdate"]);
                        patient.patient_sex = Convert.ToString(dt.Rows[i]["patient_sex"]);
                        patient.patient_phone = Convert.ToString(dt.Rows[i]["patient_phone"]);
                        patient.patient_email = Convert.ToString(dt.Rows[i]["patient_email"]);
                        patients.Add(patient);
                    }
                }
            }
            /*if (patients.Count > 0)
            {
                return JsonConvert.SerializeObject(patients);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(patients);
        }

        // POST api/<PatientController>/5
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] PatientAddRequest value)
        {
            Patient pat = new Patient();
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("UpdatePatient", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@doctor_id",
                    Value = DBNull.Value,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_doctor",
                    Value = value.patient_doctor,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_fname",
                    Value = value.patient_fname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_mname",
                    Value = value.patient_mname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_lname",
                    Value = value.patient_lname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_address",
                    Value = value.patient_address,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_coordinates",
                    Value = value.patient_coordinates,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_city",
                    Value = value.patient_city,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_state",
                    Value = value.patient_state,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_zip",
                    Value = value.patient_zip,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_sex",
                    Value = value.patient_sex,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_phone",
                    Value = value.patient_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_email",
                    Value = value.patient_email,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_bdate",
                    Value = value.patient_bdate,
                    SqlDbType = SqlDbType.Date
                });
            }
            da.Fill(dt);
            List<Patient> patients = new List<Patient>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Patient patient = new Patient();
                    if (dt.Rows[i] is not null)
                    {
                        patient.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        if (dt.Rows[i]["doctor_id"] == DBNull.Value)
                        {
                            patient.doctor_id = null;
                        }
                        else
                        {
                            Convert.ToInt32(dt.Rows[i]["doctor_id"]);
                        }
                        patient.patient_doctor = Convert.ToString(dt.Rows[i]["patient_doctor"]);
                        patient.patient_fname = Convert.ToString(dt.Rows[i]["patient_fname"]);
                        patient.patient_mname = Convert.ToString(dt.Rows[i]["patient_mname"]);
                        patient.patient_lname = Convert.ToString(dt.Rows[i]["patient_lname"]);
                        patient.patient_address = Convert.ToString(dt.Rows[i]["patient_address"]);
                        patient.patient_coordinates = Convert.ToString(dt.Rows[i]["patient_coordinates"]);
                        patient.patient_city = Convert.ToString(dt.Rows[i]["patient_city"]);
                        patient.patient_state = Convert.ToString(dt.Rows[i]["patient_state"]);
                        patient.patient_zip = Convert.ToString(dt.Rows[i]["patient_zip"]);
                        patient.patient_bdate = Convert.ToString(dt.Rows[i]["patient_bdate"]);
                        patient.patient_sex = Convert.ToString(dt.Rows[i]["patient_sex"]??"Male");
                        patient.patient_phone = Convert.ToString(dt.Rows[i]["patient_phone"]);
                        patient.patient_email = Convert.ToString(dt.Rows[i]["patient_email"]);
                        patients.Add(patient);
                    }
                }
            }
            if (patients.Count > 0)
            {
                return JsonConvert.SerializeObject(patients[0]);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to update patient";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<PatientController>
        [HttpPost]
        public string Post([FromBody] PatientAddRequest value)
        {
            Patient pat = new Patient();
            string? constring = _configuration.GetConnectionString("VeloAppCon");
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("AddPatient", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            Response response = new Response();
            string? returnval = null;
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@doctor_id",
                    Value = DBNull.Value,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_doctor",
                    Value = value.patient_doctor,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_fname",
                    Value = value.patient_fname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_mname",
                    Value = value.patient_mname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_lname",
                    Value = value.patient_lname,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_address",
                    Value = value.patient_address,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_coordinates",
                    Value = value.patient_coordinates,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_city",
                    Value = value.patient_city,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_state",
                    Value = value.patient_state,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_zip",
                    Value = value.patient_zip,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_sex",
                    Value = value.patient_sex,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_phone",
                    Value = value.patient_phone,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_email",
                    Value = value.patient_email,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_bdate",
                    Value = value.patient_bdate,
                    SqlDbType = SqlDbType.Date
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@patient_id"].Value);
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

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
