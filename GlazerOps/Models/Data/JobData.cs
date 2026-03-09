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
        public string? Assigned { get; set; }

        [JsonPropertyName("site")]
        public SiteData? Site { get; set; }

        [JsonPropertyName("contacts")]
        public JobContactData[] Contacts { get; set; } = Array.Empty<JobContactData>();

        [JsonPropertyName("scheduled_dates")]
        public JobScheduleDateData[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDateData>();

        [JsonPropertyName("notes")]
        public JobNoteData[] Notes { get; set; } = Array.Empty<JobNoteData>();
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

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
    }

    [Table("job_contacts")]
    public class JobContactData : BaseModel
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
    public class JobScheduleDateData : BaseModel
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

        [JsonPropertyName("note_date")]
        public DateTime NoteDate { get; set; }

        [JsonPropertyName("owner_user_id")]
        public Guid? OwnerUserId { get; set; }
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
