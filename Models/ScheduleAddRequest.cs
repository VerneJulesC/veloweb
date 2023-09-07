using System;

namespace veloapp.Models
{
    public class ScheduleAddRequest
    {
        public string? sched_docid {  get; set; }
        public DateTime? sched_date { get; set; }
        public int? driver_id { get; set; }
        public int? patient_id { get; set; }
        public string? sched_type { get; set; }
        public string? location_desc { get; set; }
        public string? location_coord { get; set; }
        public string? destination_desc { get; set; }
        public string? destination_coord { get; set; }
        public string? status { get; set; }
    }
}
