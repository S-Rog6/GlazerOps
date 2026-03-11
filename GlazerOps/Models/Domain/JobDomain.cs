namespace GlazerOps.Models.Domain
{
    /// <summary>
    /// Domain model for Job - represents business logic and behavior.
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
        /// Gets a contact for this job.
        /// </summary>
        public JobContact? GetCurrentContact() =>
            Contacts.FirstOrDefault();

        /// <summary>
        /// Gets the earliest scheduled date for this job.
        /// </summary>
        public JobScheduleDate? GetPrimaryScheduleDate() =>
            ScheduledDates.OrderBy(d => d.ScheduledDate).FirstOrDefault();

        /// <summary>
        /// Gets pinned notes for this job.
        /// </summary>
        public JobNote[] GetPinnedNotes() =>
            Notes.Where(n => n.Pinned).ToArray();
    }

    public class ExpectedBucket
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }

    public class Site
    {
        public long Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
    }

    public class JobContact
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public class JobScheduleDate
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public DateTime ScheduledDate { get; set; }
    }

    public class JobNote
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string Note { get; set; } = string.Empty;
        public bool Pinned { get; set; }
        public bool Marked { get; set; }
        public DateTime? NoteDtg { get; set; }
        public Guid? OwnerUserId { get; set; }
    }

    public class UserFileJob
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Payload { get; set; } = "{}";
        public bool Processed { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class SiteContact
    {
        public long Id { get; set; }
        public long SiteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Notes { get; set; }
        public bool Active { get; set; } = true;
        public Guid? OwnerUserId { get; set; }
    }
}
