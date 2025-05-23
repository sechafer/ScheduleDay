using Blazored.LocalStorage;
using System.Net.Http;

namespace ScheduleDay.Client.Handlers
{
    public class CookieHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public CookieHandler(ILocalStorageService localStorage) : base(new HttpClientHandler())
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}