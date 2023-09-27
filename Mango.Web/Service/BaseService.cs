using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;

        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");
            // token


            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch (requestDto.ApiType)
            {
                case Utility.SD.ApiType.GET:
                    message.Method = HttpMethod.Get;
                    break;
                case Utility.SD.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case Utility.SD.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                default:
                    message.Method = HttpMethod.Delete;
                    break;
            }

            apiResponse = await client.SendAsync(message);

            try
            {
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not found" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Deinied " };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
