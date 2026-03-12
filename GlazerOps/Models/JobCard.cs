namespace GlazerOps.Models
{
    public class JobCardModel
    {
        public long job_id { get; set; }

        public string job_name { get; set; } = "";

        public long? po_number { get; set; }

        public long site_id { get; set; }

        public string site_name { get; set; } = "";

        public string address_1 { get; set; } = "";

        public string address_2 { get; set; } = "";

        public string primary_contacts { get; set; } = "";

        public string all_contacts { get; set; } = "";
    }
}
