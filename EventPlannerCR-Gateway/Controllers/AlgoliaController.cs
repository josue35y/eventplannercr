using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Algolia.Search.Clients;

namespace EventPlannerCR_Gateway.Controllers
{
    public class AlgoliaController
    {
        async Task AlgoliaSearch()
        {
            var url = "https://dashboard.algolia.com/api/1/sample_datasets?type=movie";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<List<dynamic>>(content);
            var client = new SearchClient(new SearchConfig("UQLNNIIUNN", "77e0c6deaed712d73ad4f7c394180791"));
            
            try
            {
                var result = await client.SaveObjectsAsync("movies_index", movies);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}