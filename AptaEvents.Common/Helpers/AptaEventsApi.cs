using DevExpress.Persistent.Base;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AptaEvents.Common.Helpers
{
    public class AptaEventsApi
    {
        private IConfiguration _configuration;

        public AptaEventsApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetRequest(string action, string queryString = "", string accessToken = "")
        {
            return await GetApi(accessToken).GetRequest(action, queryString);
        }

        private InternalAPI GetApi(string accessToken = "")
        {
            var apiUrl = "";
            var userName = "";
            var password = "";

            try
            {
                apiUrl = _configuration.GetRequiredSection("AptaEventsIntegrationApi")["Url"];
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogError($"Configuration setting not found for: AptaEventsIntegrationApi.Url");

                throw;
            }

            Encoding apiEncoding = Encoding.UTF8;

            if (!string.IsNullOrEmpty(accessToken))
            {
                return new InternalAPI(apiUrl, accessToken)
                {
                    QueryParameters = new Dictionary<string, string> { },
                    UserName = string.Empty,
                    Password = string.Empty
                };
            }
            else
            {
                return new InternalAPI(apiUrl)
                {
                    QueryParameters = new Dictionary<string, string> { },
                    UserName = userName,
                    Password = password
                };
            }
        }

    }

    internal class InternalAPI
    {
        static readonly HttpClient client = new HttpClient();

        private string _apiUrl;

        internal string AccessToken { get; set; } = String.Empty;
        internal string UserName { get; set; } = String.Empty;
        internal string Password { get; set; } = String.Empty;

        internal Dictionary<string, string> CustomHeaders { get; set; } = new Dictionary<string, string>();
        internal Dictionary<string, string> QueryParameters { get; set; } = new Dictionary<string, string>();

        internal InternalAPI(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        internal InternalAPI(string apiUrl, string accessToken)
        {
            _apiUrl = apiUrl;
            AccessToken = accessToken;
        }

        internal async Task<string> GetRequest(string action, string queryString = "")
        {
            var authQuery = GetAuthQuery();

            var targetUrl = $"{_apiUrl}/{action}" + authQuery;

            targetUrl += (string.IsNullOrEmpty(authQuery) ? "?" : "&") + queryString;

            try
            {
                var message = new HttpRequestMessage(HttpMethod.Get, targetUrl);

                message.Headers.Add("Content-Type", "application/json");

                SetAuthorization(message);

                var response = await client.SendAsync(message);

                return await response.Content.ReadAsStringAsync();
            }
            catch (WebException ex)
            {
                string responseText = "";

                var responseStream = ex.Response?.GetResponseStream();

                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }

                Tracing.Tracer.LogError($"WebException in EventsApi.GetRequest for URL: {targetUrl}; responseStream: {responseStream}; Error: {ex}");

                throw new EventsPluginException(HttpStatusCode.BadRequest, responseText);
            }
            catch (Exception ex)
            {
                Tracing.Tracer.LogError($"Exception in EventsApi.GetRequest for URL: {targetUrl}; Error: {ex}");

                throw new EventsPluginException(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private string GetAuthQuery()
        {
            var queryAuthorizations = new List<string>();

            if (QueryParameters != null)
            {
                foreach (var queryParameter in QueryParameters)
                {
                    queryAuthorizations.Add($"{queryParameter.Key}={queryParameter.Value}");
                }
            }

            var authQuery = queryAuthorizations.Count > 0 ? "?" + String.Join("&", queryAuthorizations) : "";

            return authQuery;
        }

        private void SetAuthorization(HttpRequestMessage message)
        {
            if (!String.IsNullOrEmpty(UserName))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{UserName}:{Password}")));
            }
            else if (!String.IsNullOrEmpty(AccessToken))
            {
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            }
        }
    }

}
