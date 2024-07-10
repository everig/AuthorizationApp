using AuthorizationApp.src.Entities;
using System.Net.Http.Json;
using AuthorizationApp.src.Exceptions;
using System.Text.Json;
using System.Net;

namespace AuthorizationApp.src.Services
{
    public class RxLoyaltyService
    {
        public AccountInfo? accountInfo { get; private set; }
        private readonly JsonSerializerOptions options;
        private static HttpClient httpClient;
        public  RxLoyaltyService()
        {
            options = new JsonSerializerOptions();
            options.AllowTrailingCommas = true;
            httpClient = new HttpClient();
        }

        public async Task Authorize(Account account)
        {
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Content = JsonContent.Create(account),
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://testwork.cloud39.ru/BonusWebApi/Account/Login"),

            };

            var response = await httpClient.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new Exception($"Ошибка {((int)response.StatusCode)} - {response.StatusCode}");
            }

            accountInfo = await response.Content.ReadFromJsonAsync<AccountInfo>(options);

            if (accountInfo.Token == null)
            {
                throw new AuthoriationException();
            }
        }
    }
}
