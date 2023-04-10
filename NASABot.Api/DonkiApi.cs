using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using NASABot.Api.Model;

namespace NASABot.Api.Nasa
{
    /// <summary>
    /// Space Weather Database Of Notifications, Knowledge, Information (DONKI)
    /// </summary>
    public class Donki
    {
        /// <summary>
        /// Coronal Mass Ejection (CME)
        /// https://api.nasa.gov/DONKI/CME?startDate=2020-03-01&endDate=2023-04-10&api_key=wBggKOTD9iACGJLdl93W2cH7d5WdWs6CelkSIAMw
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCoronalMassEjection(string apiKey, DateTime startDate, DateTime endDate)
        {
            var dataString = new StringBuilder();
            
            var coronalMassEjectionData = await Constants.NasaApiUrl
                .AppendPathSegment("DONKI")
                .AppendPathSegment("CME")
                .SetQueryParams(new
                {
                    api_key = apiKey,
                    startDate = startDate.ToString("yyyy-MM-dd"),
                    endDate = endDate.ToString("yyyy-MM-dd"),
                }).GetJsonAsync<List<CoronalMassEjection>>();

            dataString.AppendLine($" ---- Coronal Mass Ejection ----");

            foreach (var item in coronalMassEjectionData)
            {
                dataString.AppendLine($"- Activity was at: {item.ActivityId} ");
                dataString.AppendLine($" {item.Note} ");
            }

            return dataString.ToString();
        }

        /// <summary>
        /// Geomagnetic Storm (GST)
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<string> GetGeomagneticStorm(string apiKey, DateTime startDate, DateTime endDate)
        {
            var dataString = new StringBuilder();

            var result = await Constants.NasaApiUrl
                .AppendPathSegment("DONKI")
                .AppendPathSegment("GST")
                .SetQueryParams(new
                {
                    api_key = apiKey,
                    startDate = startDate.ToString("yyyy-MM-dd"),
                    endDate = endDate.ToString("yyyy-MM-dd"),
                }).GetJsonAsync<List<GeomagneticStorm>>();

            dataString.AppendLine($" ---- Geomagnetic Storm ----");

            if (result != null)
            {
                foreach (var item in result)
                {
                    dataString.AppendLine($"- Activity was at: {item.StartTime} ");
                    dataString.AppendLine($"- Link: {item.Link} ");
                }
            }

            return dataString.ToString();
        }
    }
}
