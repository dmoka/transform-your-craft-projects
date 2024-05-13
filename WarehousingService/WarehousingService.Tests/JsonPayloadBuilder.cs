using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace MutationTestingMeetup.Tests
{
    public class JsonPayloadBuilder
    {
        public static StringContent Build(dynamic payloadAsDynamicObject)
        {
            var jsonString = JsonConvert.SerializeObject(payloadAsDynamicObject);

            return CreateJsonPayloadContent(jsonString);
        }

        private static StringContent CreateJsonPayloadContent(dynamic jsonContent)
        {
            return new StringContent(
                jsonContent,
                Encoding.UTF8,
                "application/json");
        }

        public static StringContent EmptyBody()
        {
            return new StringContent(
                "",
                Encoding.UTF8,
                "application/json");
        }
    }
}
