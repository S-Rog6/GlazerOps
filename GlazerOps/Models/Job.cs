using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GlazerOps.Models
{
    [Table("jobs")]
    public class Job : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("job_name")]
        public string JobName { get; set; } = string.Empty;

        [JsonPropertyName("po_number")]
        public string PONumber { get; set; } = string.Empty;

        [JsonPropertyName("site_id")]
        public string SiteId { get; set; } = string.Empty;

        [JsonPropertyName("assign_")]
        public string Assigned { get; set; } = string.Empty;

        [JsonPropertyName("owner_user_id")]
        public string OwnerUserId { get; set; } = string.Empty;

        // Related data (not stored in jobs table but useful for display)
        [JsonIgnore]
        public Site? Site { get; set; }

        [JsonIgnore]
        public JobContact[] Contacts { get; set; } = Array.Empty<JobContact>();

        [JsonIgnore]
        public JobScheduleDate[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDate>();

        [JsonIgnore]
        public JobNote[] Notes { get; set; } = Array.Empty<JobNote>();
    }

    [Table("sites")]
    public class Site : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("site_name")]
        public string SiteName { get; set; } = string.Empty;

        [JsonPropertyName("address_")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("owner_user_id")]
        public string OwnerUserId { get; set; } = string.Empty;
    }

    public class JobContact
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("job_id")]
        public string JobId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("notes")]
        public string Notes { get; set; } = string.Empty;

        [JsonPropertyName("is_current")]
        public bool IsCurrent { get; set; }

        [JsonPropertyName("owner_user_id")]
        public string OwnerUserId { get; set; } = string.Empty;
    }

    public class JobScheduleDate
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("job_id")]
        public string JobId { get; set; } = string.Empty;

        [JsonPropertyName("scheduled_date")]
        public DateTime ScheduledDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("owner_user_id")]
        public string OwnerUserId { get; set; } = string.Empty;
    }

    public class JobNote
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("job_id")]
        public string JobId { get; set; } = string.Empty;

        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        [JsonPropertyName("marked")]
        public bool Marked { get; set; }

        [JsonPropertyName("note_date")]
        public DateTime NoteDate { get; set; }

        [JsonPropertyName("owner_user_id")]
        public string OwnerUserId { get; set; } = string.Empty;
    }
}
