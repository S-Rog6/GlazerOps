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
        public long Id { get; set; }

        [JsonPropertyName("po_number")]
        public long? PONumber { get; set; }

        [JsonPropertyName("job_name")]
        public string JobName { get; set; } = string.Empty;

        [JsonPropertyName("site_id")]
        public long SiteId { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }

        [JsonPropertyName("assigned")]
        public string? Assigned { get; set; }

        // Related data (not stored in jobs table but useful for display)
        [JsonPropertyName("site")]
        public Site? Site { get; set; }

        [JsonPropertyName("contacts")]
        public JobContact[] Contacts { get; set; } = Array.Empty<JobContact>();

        [JsonPropertyName("scheduled_dates")]
        public JobScheduleDate[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDate>();

        [JsonPropertyName("notes")]
        public JobNote[] Notes { get; set; } = Array.Empty<JobNote>();
    }

    [Table("sites")]
    public class Site : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("site_name")]
        public string SiteName { get; set; } = string.Empty;

        [JsonPropertyName("address_1")]
        public string? Address1 { get; set; }

        [JsonPropertyName("address_2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("job_contacts")]
    public class JobContact : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("job_id")]
        public long JobId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("is_current")]
        public bool IsCurrent { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("job_schedule_dates")]
    public class JobScheduleDate : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("job_id")]
        public long JobId { get; set; }

        [JsonPropertyName("scheduled_date")]
        public DateTime ScheduledDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Scheduled";

        [JsonPropertyName("note")]
        public string? Note { get; set; }

        [JsonPropertyName("is_primary")]
        public bool IsPrimary { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("job_notes")]
    public class JobNote : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("job_id")]
        public long JobId { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; } = string.Empty;

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }

        [JsonPropertyName("marked")]
        public bool Marked { get; set; }

        [JsonPropertyName("note_date")]
        public DateTime NoteDate { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("site_contacts")]
    public class SiteContact : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("site_id")]
        public long SiteId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("notes")]
        public string? Notes { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; } = true;

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }
}
