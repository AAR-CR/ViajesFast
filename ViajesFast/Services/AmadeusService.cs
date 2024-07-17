using System;
using System.Collections.Generic; 
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ViajesFast.Services
{
    public class AmadeusService
    {
        private static readonly string clientId = "GpmyixUWBv4Z2ySYMPUsqyXWGJrGoQFH";
        private static readonly string clientSecret = "pR9mEyg9rtoUfQ7n";
        private static readonly string tokenUrl = "https://test.api.amadeus.com/v1/security/oauth2/token";

        public static async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var requestBody = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret)
                });

                var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
                {
                    Content = requestBody
                };

                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenData = JObject.Parse(responseBody);

                return tokenData["access_token"].ToString();
            }
        }


        public static async Task<string> GetFlightOffers(string origin, string destination, string departureDate, int adults)
        {
            string accessToken = await GetAccessToken();
            string flightOffersUrl = $"https://test.api.amadeus.com/v2/shopping/flight-offers?originLocationCode={origin}&destinationLocationCode={destination}&departureDate={departureDate}&adults={adults}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync(flightOffersUrl);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }

    }
}
