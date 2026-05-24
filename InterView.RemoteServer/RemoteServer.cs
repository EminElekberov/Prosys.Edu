using InterView.RemoteServer.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace InterView.RemoteServer
{
    public class RemoteServer
    {
        private readonly SettingsModel appSettings;
        private IHttpContextAccessor _httpContextAccessor;
        public RemoteServer(IHttpContextAccessor httpContextAccessor, SettingsModel app)
        {
            _httpContextAccessor = httpContextAccessor;
            appSettings = app;

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            IConfiguration configuration = configurationBuilder.Build();
            var appSet = configuration.GetSection("Settings").GetChildren();
            if (appSettings.Service_Url == null)
            {
                appSettings.Service_Url = appSet.FirstOrDefault(w => w.Key == "Service_Url").Value;
                appSettings.ServiceIsAuthorize = Convert.ToBoolean(appSet.FirstOrDefault(w => w.Key == "ServiceIsAuthorize").Value);
            }
        }


        public async Task<object> SendAsync<T>(string url, HttpMethod method, object param = null)
        {
            using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(200) })
            {
                try
                {
                    string accessToken = string.Empty;
                    if ((_httpContextAccessor.HttpContext != null))
                    {
                        accessToken = _httpContextAccessor.HttpContext.Request.Cookies["access_token"];
                    }
                    var request = new HttpRequestMessage();
                    if (appSettings.ServiceIsAuthorize)
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    request.Headers.Accept.Clear();
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Method = method;
                    request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(param, typeof(object), new JsonSerializerOptions()), Encoding.UTF8, "application/json");
                    request.RequestUri = new Uri(@$"{appSettings.Service_Url}{url}");
                    var response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<ApiResponse<T>>(jsonContent);
                    var singleObject = responseObject.Data;

                    return singleObject;

                }
                catch (HttpRequestException httpRequestException)
                {
                    return httpRequestException.Message;
                }
            }
        }
    }

}
