using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using veloapp.Models;
using veloservices.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace veloapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        public readonly IConfiguration _configuration;

        public ScheduleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<ScheduleController>
        [HttpGet]
        public string Get()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VeloAppCon").ToString());
            SqlCommand cmd = new SqlCommand("SchedulesList", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            List<Schedule> schedules = new List<Schedule>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Schedule schedule = new Schedule();
                    if (dt.Rows[i] is not null)
                    {
                        schedule.sched_id = Convert.ToInt32(dt.Rows[i]["sched_id"]);
                        schedule.sched_date = Convert.ToDateTime(dt.Rows[i]["sched_date"]);
                        schedule.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        schedule.patient_name = Convert.ToString(dt.Rows[i]["patient_name"]);
                        schedule.sched_type = Convert.ToString(dt.Rows[i]["sched_type"]);
                        schedule.location_desc = Convert.ToString(dt.Rows[i]["location_desc"]);
                        schedule.location_coord = Convert.ToString(dt.Rows[i]["location_coord"]);
                        schedule.destination_desc = Convert.ToString(dt.Rows[i]["destination_desc"]);
                        schedule.destination_coord = Convert.ToString(dt.Rows[i]["destination_coord"]);
                        schedule.status = Convert.ToString(dt.Rows[i]["status"]);
                        schedule.last_modified = Convert.ToDateTime(dt.Rows[i]["last_modified"]);
                        schedules.Add(schedule);
                    }
                }
            }
            /*if (schedules.Count > 0)
            {
                return JsonConvert.SerializeObject(schedules);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(schedules);
        }

        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VeloAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM schedule where sched_id = @id", con);
            da.SelectCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = "@id",
                Value = id,
                SqlDbType = SqlDbType.Int
            });
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Schedule> schedules = new List<Schedule>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Schedule schedule = new Schedule();
                    if (dt.Rows[i] is not null)
                    {
                        schedule.sched_id = Convert.ToInt32(dt.Rows[i]["sched_id"]);
                        schedule.sched_date = Convert.ToDateTime(dt.Rows[i]["sched_date"]);
                        schedule.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        schedule.patient_name = Convert.ToString(dt.Rows[i]["patient_name"]);
                        schedule.sched_type = Convert.ToString(dt.Rows[i]["sched_type"]);
                        schedule.location_desc = Convert.ToString(dt.Rows[i]["location_desc"]);
                        schedule.location_coord = Convert.ToString(dt.Rows[i]["location_coord"]);
                        schedule.destination_desc = Convert.ToString(dt.Rows[i]["destination_desc"]);
                        schedule.destination_coord = Convert.ToString(dt.Rows[i]["destination_coord"]);
                        schedule.status = Convert.ToString(dt.Rows[i]["status"]);
                        schedule.last_modified = Convert.ToDateTime(dt.Rows[i]["last_modified"]);
                        schedules.Add(schedule);
                    }
                }
            }
            /*if (schedules.Count > 0)
            {
                return JsonConvert.SerializeObject(schedules);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }*/
            return JsonConvert.SerializeObject(schedules);
        }

        // POST api/<ScheduleController>/5
        [HttpPost("{id}")]
        public string Post(int id, [FromBody] ScheduleAddRequest value)
        {
            Schedule sched = new Schedule();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VeloAppCon").ToString());
            SqlCommand cmd = new SqlCommand("UpdateSchedule", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cmd.CommandType = CommandType.StoredProcedure;

            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@sched_id",
                    Value = id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@sched_date",
                    Value = value.sched_date,
                    SqlDbType = SqlDbType.DateTime
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@patient_id",
                    Value = value.patient_id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@sched_type",
                    Value = value.sched_type,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@location_desc",
                    Value = (value.location_desc is null)?DBNull.Value:value.location_desc,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@location_coord",
                    Value = (value.location_coord is null) ? DBNull.Value : value.location_coord,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@destination_desc",
                    Value = (value.destination_desc is null) ? DBNull.Value : value.destination_desc,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@destination_coord",
                    Value = (value.destination_coord is null) ? DBNull.Value : value.destination_coord,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@status",
                    Value = value.status,
                    SqlDbType = SqlDbType.NVarChar
                });
            }
            da.Fill(dt);
            List<Schedule> schedules = new List<Schedule>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Schedule schedule = new Schedule();
                    if (dt.Rows[i] is not null)
                    {
                        schedule.sched_id = Convert.ToInt32(dt.Rows[i]["sched_id"]);
                        schedule.sched_date = Convert.ToDateTime(dt.Rows[i]["sched_date"]);
                        schedule.patient_id = Convert.ToInt32(dt.Rows[i]["patient_id"]);
                        schedule.patient_name = Convert.ToString(dt.Rows[i]["patient_name"]);
                        schedule.sched_type = Convert.ToString(dt.Rows[i]["sched_type"]);
                        schedule.location_desc = Convert.ToString(dt.Rows[i]["location_desc"]);
                        schedule.location_coord = Convert.ToString(dt.Rows[i]["location_coord"]);
                        schedule.destination_desc = Convert.ToString(dt.Rows[i]["destination_desc"]);
                        schedule.destination_coord = Convert.ToString(dt.Rows[i]["destination_coord"]);
                        schedule.status = Convert.ToString(dt.Rows[i]["status"]);
                        schedule.last_modified = Convert.ToDateTime(dt.Rows[i]["last_modified"]);
                        schedules.Add(schedule);
                    }
                }
            }
            if (schedules.Count > 0)
            {
                return JsonConvert.SerializeObject(schedules[0]);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to Update Schedule";
                return JsonConvert.SerializeObject(response);
            }
        }

        // POST api/<ScheduleController>
        // Add Schedule
        [HttpPost]
        public string Post([FromBody] ScheduleAddRequest value)
        {
            Schedule sched = new Schedule();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("VeloAppCon").ToString());
            SqlCommand cmd = new SqlCommand("AddSchedule", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            Response response = new Response();
            string? returnval= null;
            cmd.CommandType = CommandType.StoredProcedure;
            if (value is not null)
            {
                cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@sched_date",
                    Value = value.sched_date,
                    SqlDbType = SqlDbType.DateTime
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@patient_id",
                    Value = value.patient_id,
                    SqlDbType = SqlDbType.Int
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@sched_type",
                    Value = value.sched_type,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@location_desc",
                    Value = (value.location_desc is null)?DBNull.Value:value.location_desc,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@location_coord",
                    Value = (value.location_coord is null) ? DBNull.Value : value.location_coord,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@destination_desc",
                    Value = (value.destination_desc is null) ? DBNull.Value : value.destination_desc,
                    SqlDbType = SqlDbType.NVarChar
                });
	            cmd.Parameters.Add(new SqlParameter{
                    ParameterName = "@destination_coord",
                    Value = (value.destination_coord is null) ? DBNull.Value : value.destination_coord,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@status",
                    Value = value.status,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@sched_id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                });
            }
            con.Open();
            cmd.ExecuteNonQuery();
            returnval = Convert.ToString(cmd.Parameters["@sched_id"].Value);
            con.Close();
            if (returnval is not null)
            {
                return JsonConvert.SerializeObject(returnval);
            }
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "Failed to add Schedule";
                return JsonConvert.SerializeObject(response);
            }
        }

        // PUT api/<ScheduleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
