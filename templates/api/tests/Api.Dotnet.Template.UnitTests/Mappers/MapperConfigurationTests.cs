using AutoMapper;
using System.Reflection;
using Xunit;

namespace Api.Dotnet.Template.UnitTests.Mappers
{
    public class MapperConfigurationTests
    {
        [Fact]
        public void TryAssemblyProfilesConfiguration()
        {
            new MapperConfiguration(cfg => cfg.AddMaps(Assembly.GetAssembly(typeof(Program))));
        }
    }
}
