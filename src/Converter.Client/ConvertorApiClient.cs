
using Converter.Core;
using Serilog;

namespace Converter.Client
{
    public class ConvertorApiClient
    {
        private static readonly HttpClient mHttpClient = new HttpClient
        {
            BaseAddress = new Uri(AppSettings.Default.BaseApiUrl)
        };

        public async Task<string> ConvertDollarAsync(long dollars, int cents, string lang) 
        {
            // Forms the uri request
            string uri = $"api/v1/convert?dollars={dollars}&cents={cents}&lang={lang}";

            try 
            {
                Log.Information("Sending request to the server. Target URI: {Uri}", uri);

                HttpResponseMessage response = await mHttpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode) 
                {
                    Log.Debug("HTTP request succeeded with status code: {StatusCode}", (int)response.StatusCode);
                    return await response.Content.ReadAsStringAsync();
                }

                Log.Warning("HTTP request returned a server error. Status Code: {StatusCode}, Reason: {ReasonPhrase}",
                    (int)response.StatusCode, response.ReasonPhrase);
                return $"Server error: Status Code: {(int)response.StatusCode}, Reason: {response.ReasonPhrase}";
            }
            catch (HttpRequestException ex) 
            {
                Log.Error(ex, "HTTP request failed with connection error while querying URI: {Uri}", uri);
                return "HTTP request failed. Can not establish connection to the server";
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Unexpected exception occured for URI: {Uri}", uri);
                return $"Unexpected error: {ex.Message}";
            }
        }
    }
}
