using System.Text;
using System.Text.Json;

namespace DesignPatterns.Structural.Facade
{
    public class AntiFraudFacade : IAntiFraudFacade
    {
        public AntiFraudOutput Check(AntiFraudInput input)
        {
            var json = JsonSerializer.Serialize(input);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "https://63178ecbece2736550b65df3.mockapi.io/api/v1/anti-fraud";

            using var client = new HttpClient();

            var response = client.PostAsync(url, content).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<AntiFraudOutput>(responseContent)!;
        }
    }
}
