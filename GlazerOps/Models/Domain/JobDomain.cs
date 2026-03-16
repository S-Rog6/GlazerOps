namespace GlazerOps.Models.Domain
{
    /// <summary>
    /// Domain model for a job and its related business entities.
    /// </summary>
    public class Job
    {
        public long Id { get; set; }
        public long? PONumber { get; set; }
        public string JobName { get; set; } = string.Empty;
        public long SiteId { get; set; }
        public Guid? OwnerUserId { get; set; }
        public string[] Assigned { get; set; } = Array.Empty<string>();
        public long[] ContactIds { get; set; } = Array.Empty<long>();

        public Site? Site { get; set; }
        public JobContact[] Contacts { get; set; } = Array.Empty<JobContact>();
        public JobScheduleDate[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDate>();
        public JobNote[] Notes { get; set; } = Array.Empty<JobNote>();

        /// <summary>
        /// Returns the first available contact for the job, if one exists.
        /// </summary>
        public JobContact? GetCurrentContact() =>
            Contacts.FirstOrDefault();

        /// <summary>
        /// Returns the earliest scheduled start date for the job, if one exists.
        /// </summary>
        public JobScheduleDate? GetPrimaryScheduleDate() =>
            ScheduledDates.OrderBy(d => d.StartDate).FirstOrDefault();

        /// <summary>
        /// Returns all notes marked as pinned for the job.
        /// </summary>
        public JobNote[] GetPinnedNotes() =>
            Notes.Where(n => n.Pinned).ToArray();
    }

    /// <summary>
    /// Represents an expected-bucket lookup record used by job planning workflows.
    /// </summary>
    public class ExpectedBucket
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }

    /// <summary>
    /// Represents a site associated with one or more jobs.
    /// </summary>
    public class Site
    {
        public long Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
    }

    /// <summary>
    /// Represents a contact linked to a job.
    /// </summary>
    public class JobContact
    {
        public long Id { get; set; }
        public long PoId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool IsPrimary { get; set; }
    }

    /// <summary>
    /// Represents a scheduled date range linked to a job.
    /// </summary>
    public class JobScheduleDate
    {
        public long Id { get; set; }
        public long PoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// Represents a note attached to a job.
    /// </summary>
    public class JobNote
    {
        public long Id { get; set; }
        public long PoId { get; set; }
        public string Note { get; set; } = string.Empty;
        public bool Pinned { get; set; }
        public bool Marked { get; set; }
        public DateTime? DateDtg { get; set; }
        public Guid? OwnerUserId { get; set; }
    }

    /// <summary>
    /// Represents an uploaded user file payload associated with job import/sync processing.
    /// </summary>
    public class UserFileJob
    {
        public long Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Payload { get; set; } = "{}";
        public bool Processed { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    /// <summary>
    /// Represents a contact stored at the site level.
    /// </summary>
    public class SiteContact
    {
        public long Id { get; set; }
        public long SiteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsPrimary { get; set; }
    }
}
