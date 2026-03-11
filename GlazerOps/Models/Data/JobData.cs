using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace GlazerOps.Models.Data
{
    /// <summary>
    /// Data model for Supabase persistence. Internal use only.
    /// </summary>
    [Table("jobs")]
    public class JobData : BaseModel
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
        public string[] Assigned { get; set; } = Array.Empty<string>();

        [JsonPropertyName("contact_ids")]
        public long[] ContactIds { get; set; } = Array.Empty<long>();

        [JsonPropertyName("site")]
        public SiteData? Site { get; set; }

        [JsonPropertyName("scheduled_dates")]
        public JobScheduleDateData[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDateData>();

        [JsonPropertyName("notes")]
        public JobNoteData[] Notes { get; set; } = Array.Empty<JobNoteData>();
    }

    [Table("vw_job_card")]
    public class JobCardViewData : BaseModel
    {
        [JsonPropertyName("job_id")]
        [PrimaryKey("job_id", false)]
        public long JobId { get; set; }

        [JsonPropertyName("job_name")]
        public string JobName { get; set; } = string.Empty;

        [JsonPropertyName("po_number")]
        public long? PONumber { get; set; }

        [JsonPropertyName("site_name")]
        public string? SiteName { get; set; }

        [JsonPropertyName("address_1")]
        public string? Address1 { get; set; }

        [JsonPropertyName("address_2")]
        public string? Address2 { get; set; }

        [JsonPropertyName("contacts")]
        public string? Contacts { get; set; }
    }

    [Table("expected_buckets")]
    public class ExpectedBucketData : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
    }

    [Table("sites")]
    public class SiteData : BaseModel
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
    }

    [Table("job_contacts")]
    public class JobContactData : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }
    }

    [Table("job_schedule_dates")]
    public class JobScheduleDateData : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("job_id")]
        public long JobId { get; set; }

        [JsonPropertyName("scheduled_date")]
        public DateTime ScheduledDate { get; set; }
    }

    [Table("job_notes")]
    public class JobNoteData : BaseModel
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

        [JsonPropertyName("note_dtg")]
        public DateTime? NoteDtg { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("user_file_jobs")]
    public class UserFileJobData : BaseModel
    {
        [JsonPropertyName("id")]
        [PrimaryKey("id", false)]
        public long Id { get; set; }

        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("payload")]
        public string Payload { get; set; } = "{}";

        [JsonPropertyName("processed")]
        public bool Processed { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }
    }

    [Table("site_contacts")]
    public class SiteContactData : BaseModel
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
