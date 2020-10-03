using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Dotnet.Cqrs.Api.Tests.Helpers
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object data)
            => new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
    }
}
