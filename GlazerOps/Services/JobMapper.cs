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
            return MapToDomain(data, null);
        }

        public static Job MapToDomain(JobData data, IReadOnlyDictionary<long, JobContactData>? contactsById)
        {
            return new Job
            {
                Id = data.Id,
                PONumber = data.PONumber,
                JobName = data.JobName,
                SiteId = data.SiteId,
                OwnerUserId = data.OwnerUserId,
                Assigned = data.Assigned ?? Array.Empty<string>(),
                ContactIds = data.ContactIds ?? Array.Empty<long>(),
                Site = data.Site != null ? MapSiteToDomain(data.Site) : null,
                Contacts = MapContacts(data.ContactIds, contactsById),
                ScheduledDates = data.ScheduledDates.Select(MapScheduleDateToDomain).ToArray(),
                Notes = data.Notes.Select(MapNoteToDomain).ToArray()
            };
        }

        public static List<Job> MapToDomain(IEnumerable<JobData> dataList)
        {
            return dataList.Select(MapToDomain).ToList();
        }

        public static List<Job> MapToDomain(IEnumerable<JobData> dataList, IEnumerable<JobContactData> contacts)
        {
            var contactsById = contacts.ToDictionary(c => c.Id);
            return dataList.Select(data => MapToDomain(data, contactsById)).ToList();
        }

        private static JobContact[] MapContacts(long[]? contactIds, IReadOnlyDictionary<long, JobContactData>? contactsById)
        {
            if (contactsById == null || contactIds == null || contactIds.Length == 0)
            {
                return Array.Empty<JobContact>();
            }

            return contactIds
                .Where(contactsById.ContainsKey)
                .Select(id => MapContactToDomain(contactsById[id]))
                .ToArray();
        }

        private static Site MapSiteToDomain(SiteData data)
        {
            return new Site
            {
                Id = data.Id,
                SiteName = data.SiteName,
                Address1 = data.Address1,
                Address2 = data.Address2
            };
        }

        private static JobContact MapContactToDomain(JobContactData data)
        {
            return new JobContact
            {
                Id = data.Id,
                PoId = data.PoId,
                Name = data.Name,
                Phone = data.Phone,
                Email = data.Email,
                IsPrimary = data.IsPrimary
            };
        }

        private static JobScheduleDate MapScheduleDateToDomain(JobScheduleDateData data)
        {
            return new JobScheduleDate
            {
                Id = data.Id,
                PoId = data.PoId,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            };
        }

        private static JobNote MapNoteToDomain(JobNoteData data)
        {
            return new JobNote
            {
                Id = data.Id,
                PoId = data.PoId,
                Note = data.Note,
                Pinned = data.Pinned,
                Marked = data.Marked,
                DateDtg = data.DateDtg,
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
                IsPrimary = data.IsPrimary
            };
        }

        public static Job MapCardViewToDomain(JobCardViewData data)
        {
            return new Job
            {
                Id = data.JobId,
                PONumber = data.PONumber,
                JobName = data.JobName,
                SiteId = 0,
                Site = new Site
                {
                    Id = 0,
                    SiteName = data.SiteName ?? string.Empty,
                    Address1 = data.Address1,
                    Address2 = data.Address2
                },
                Contacts = string.IsNullOrWhiteSpace(data.PrimaryContact)
                    ? Array.Empty<JobContact>()
                    : new[]
                    {
                        new JobContact
                        {
                            Name = data.PrimaryContact
                        }
                    }
            };
        }

        public static List<Job> MapCardViewToDomain(IEnumerable<JobCardViewData> dataList)
        {
            return dataList.Select(MapCardViewToDomain).ToList();
        }
    }
}
