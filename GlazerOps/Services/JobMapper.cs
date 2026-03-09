using GlazerOps.Models.Data;
using GlazerOps.Models.Domain;

namespace GlazerOps.Services
{
    /// <summary>
    /// Maps between Supabase data models and domain models.
    /// </summary>
    public class JobMapper
    {
        public static Job MapToDomain(JobData data)
        {
            return new Job
            {
                Id = data.Id,
                PONumber = data.PONumber,
                JobName = data.JobName,
                SiteId = data.SiteId,
                OwnerUserId = data.OwnerUserId,
                Assigned = data.Assigned,
                Site = data.Site != null ? MapSiteToDomain(data.Site) : null,
                Contacts = data.Contacts.Select(MapContactToDomain).ToArray(),
                ScheduledDates = data.ScheduledDates.Select(MapScheduleDateToDomain).ToArray(),
                Notes = data.Notes.Select(MapNoteToDomain).ToArray()
            };
        }

        public static List<Job> MapToDomain(IEnumerable<JobData> dataList)
        {
            return dataList.Select(MapToDomain).ToList();
        }

        private static Site MapSiteToDomain(SiteData data)
        {
            return new Site
            {
                Id = data.Id,
                SiteName = data.SiteName,
                Address1 = data.Address1,
                Address2 = data.Address2,
                OwnerUserId = data.OwnerUserId
            };
        }

        private static JobContact MapContactToDomain(JobContactData data)
        {
            return new JobContact
            {
                Id = data.Id,
                JobId = data.JobId,
                Name = data.Name,
                Phone = data.Phone,
                Email = data.Email,
                Role = data.Role,
                Notes = data.Notes,
                IsCurrent = data.IsCurrent,
                OwnerUserId = data.OwnerUserId
            };
        }

        private static JobScheduleDate MapScheduleDateToDomain(JobScheduleDateData data)
        {
            return new JobScheduleDate
            {
                Id = data.Id,
                JobId = data.JobId,
                ScheduledDate = data.ScheduledDate,
                Status = data.Status,
                Note = data.Note,
                IsPrimary = data.IsPrimary,
                OwnerUserId = data.OwnerUserId
            };
        }

        private static JobNote MapNoteToDomain(JobNoteData data)
        {
            return new JobNote
            {
                Id = data.Id,
                JobId = data.JobId,
                Note = data.Note,
                Pinned = data.Pinned,
                Marked = data.Marked,
                NoteDate = data.NoteDate,
                OwnerUserId = data.OwnerUserId
            };
        }

        public static SiteContact MapSiteContactToDomain(SiteContactData data)
        {
            return new SiteContact
            {
                Id = data.Id,
                SiteId = data.SiteId,
                Name = data.Name,
                Phone = data.Phone,
                Email = data.Email,
                Role = data.Role,
                Notes = data.Notes,
                Active = data.Active,
                OwnerUserId = data.OwnerUserId
            };
        }
    }
}
