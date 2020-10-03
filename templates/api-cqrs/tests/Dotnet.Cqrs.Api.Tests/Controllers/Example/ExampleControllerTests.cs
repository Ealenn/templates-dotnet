using Dotnet.Cqrs.Api.Controllers.Example.Models.Queries;
using Dotnet.Cqrs.Api.Controllers.Example.Models.Responses;
using Dotnet.Cqrs.Api.Tests.Fake;
using Dotnet.Cqrs.Api.Tests.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace Dotnet.Cqrs.Api.Tests.Controllers.Example
{
    [Collection(nameof(ExampleControllerTests)), Order(1)]
    public class ExampleControllerTests : BaseWebTest
    {
        private static Guid CurrentOnlineExampleId;
        private static Guid CurrentOfflineExampleId;

        public ExampleControllerTests(FakeApplicationFactory<FakeStartup> applicationFactory, ITestOutputHelper outputHelper)
            : base(applicationFactory, outputHelper)
        {
        }

        [Fact, Order(1)]
        public async Task When_CreateOnlineExample_Return_Created()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();
            var content = new CreateExampleQuery
            {
                Name = "online-example",
                Online = true
            };

            // Act
            var httpResponse = await client.PostAsync("/example", ContentHelper.GetStringContent(content));

            // Assert
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            httpResponse.Headers.Should().NotBeNullOrEmpty();
            httpResponse.Headers.Location.Should().NotBeNull();
            var jsonResponse = JsonConvert.DeserializeObject<GetExampleResponse>(
                await httpResponse.Content.ReadAsStringAsync()
            );

            jsonResponse.Should().NotBeNull();
            jsonResponse.Name.Should().NotBeNullOrEmpty().And.Be(content.Name);
            jsonResponse.Online.Should().Be(content.Online);
            CurrentOnlineExampleId = jsonResponse.Id;
        }

        [Fact, Order(2)]
        public async Task When_CreateOfflineExample_Return_Created()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();
            var content = new CreateExampleQuery
            {
                Name = "offline-example",
                Online = false
            };

            // Act
            var httpResponse = await client.PostAsync("/example", ContentHelper.GetStringContent(content));

            // Assert
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            httpResponse.Headers.Should().NotBeNullOrEmpty();
            httpResponse.Headers.Location.Should().NotBeNull();
            var jsonResponse = JsonConvert.DeserializeObject<GetExampleResponse>(
                await httpResponse.Content.ReadAsStringAsync()
            );

            jsonResponse.Should().NotBeNull();
            jsonResponse.Name.Should().NotBeNullOrEmpty().And.Be(content.Name);
            jsonResponse.Online.Should().Be(content.Online);
            CurrentOfflineExampleId = jsonResponse.Id;
        }

        [Fact, Order(3)]
        public async Task When_GetOnlineExamples_Return_OnlineExample()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();

            // Act
            var httpResponse = await client.GetAsync("/example?Skip=0&Take=5");

            // Assert
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var jsonResponse = JsonConvert.DeserializeObject<IEnumerable<GetExampleResponse>>(
                await httpResponse.Content.ReadAsStringAsync()
            );

            jsonResponse.Should().NotBeNull();
            jsonResponse.Should().HaveCount(1);
            jsonResponse.First().Id.Should().Be(CurrentOnlineExampleId);
        }

        [Fact, Order(4)]
        public async Task When_GetOnlineExamplesById_Return_Example()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();

            // Act
            var httpResponse = await client.GetAsync($"/example/{CurrentOnlineExampleId}");

            // Assert
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var jsonResponse = JsonConvert.DeserializeObject<GetExampleResponse>(
                await httpResponse.Content.ReadAsStringAsync()
            );

            jsonResponse.Should().NotBeNull();
            jsonResponse.Id.Should().Be(CurrentOnlineExampleId);
        }

        [Fact, Order(5)]
        public async Task When_GetOfflineExamplesById_Return_Example()
        {
            // Arrange
            var client = ApplicationFactory.CreateClient();

            // Act
            var httpResponse = await client.GetAsync($"/example/{CurrentOfflineExampleId}");

            // Assert
            httpResponse.Should().NotBeNull();
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var jsonResponse = JsonConvert.DeserializeObject<GetExampleResponse>(
                await httpResponse.Content.ReadAsStringAsync()
            );

            jsonResponse.Should().NotBeNull();
            jsonResponse.Id.Should().Be(CurrentOfflineExampleId);
        }
    }
}
