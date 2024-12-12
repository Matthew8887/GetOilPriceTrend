using Exercise.Helper;
using Exercise.Model;
using Exercise.Utils;

namespace Exercise.Service
{
    public class OilService : IOilService
    {
        public async Task<T> GetDatasource<T>(string url)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var responseMessage = await new HttpClient().SendAsync(requestMessage);
                var responseBody = await responseMessage.Content.ReadAsStringAsync();

                if (responseMessage.IsSuccessStatusCode)
                    return string.IsNullOrEmpty(responseBody) ? default : responseBody.DeserializeObject<T>();

                throw new Exception($"StatusCode 404 {responseMessage.StatusCode} - Url: {url}");
            }
        }

        public List<OilPrice> GetByDate(DateTime? startdate, DateTime? enddate)
        {
            return CommonConfigVariables.ListOilPrices.Where(x =>
                (startdate.IsNull() || x.Date >= startdate)
                &&
                (enddate.IsNull() || x.Date <= enddate)).ToList();
        }
    }
}
