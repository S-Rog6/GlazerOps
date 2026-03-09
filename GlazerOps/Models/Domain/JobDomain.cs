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
        public string? Assigned { get; set; }

        public Site? Site { get; set; }
        public JobContact[] Contacts { get; set; } = Array.Empty<JobContact>();
        public JobScheduleDate[] ScheduledDates { get; set; } = Array.Empty<JobScheduleDate>();
        public JobNote[] Notes { get; set; } = Array.Empty<JobNote>();

        /// <summary>
        /// Gets the currently assigned contact for this job.
        /// </summary>
        public JobContact? GetCurrentContact() =>
            Contacts.FirstOrDefault(c => c.IsCurrent);

        /// <summary>
        /// Gets the primary scheduled date for this job.
        /// </summary>
        public JobScheduleDate? GetPrimaryScheduleDate() =>
            ScheduledDates.FirstOrDefault(d => d.IsPrimary);

        /// <summary>
        /// Gets pinned notes for this job.
        /// </summary>
        public JobNote[] GetPinnedNotes() =>
            Notes.Where(n => n.Pinned).ToArray();
    }

    public class Site
    {
        public long Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public Guid? OwnerUserId { get; set; }
    }

    public class JobContact
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Notes { get; set; }
        public bool IsCurrent { get; set; }
        public Guid? OwnerUserId { get; set; }
    }

    public class JobScheduleDate
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Status { get; set; } = "Scheduled";
        public string? Note { get; set; }
        public bool IsPrimary { get; set; }
        public Guid? OwnerUserId { get; set; }
    }

    public class JobNote
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public string Note { get; set; } = string.Empty;
        public bool Pinned { get; set; }
        public bool Marked { get; set; }
        public DateTime NoteDate { get; set; }
        public Guid? OwnerUserId { get; set; }
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
