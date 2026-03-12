using System.Net.Http.Json;
using GlazerOps.Models;

namespace GlazerOps.Services
{


    public class JobCardService
    {
        private readonly HttpClient _http;

        public JobCardService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<JobCard>> GetJobCards()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<JobCardResponse>(
                "https://mkcpedfhpiebdggrszrl.supabase.co/functions/v1/vw-job-card?limit=50"
            );

                return result?.data ?? new List<JobCard>();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"JobCardService.GetJobCards failed: {ex}");
                return new List<JobCard>();
            }
        }
    }
}
