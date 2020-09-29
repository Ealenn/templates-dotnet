using Api.Dotnet.Template.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Xunit;

namespace Api.Dotnet.Template.UnitTests.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        #region AddMvc
        [Fact]
        public void AddMvc_WithNullIServiceCollection_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = null;
            IWebHostEnvironment hostingEnvironment = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddMvc(hostingEnvironment));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddMvc_WithNullIHostEnvironment_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IWebHostEnvironment hostingEnvironment = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddMvc(hostingEnvironment));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddMvc()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IWebHostEnvironment hostingEnvironment = Mock.Of<IWebHostEnvironment>(e => e.EnvironmentName == "Development");

            // A
            serviceCollection.AddMvc(hostingEnvironment);
            var buildServiceProvider = serviceCollection.BuildServiceProvider();

            // A
            Assert.NotNull(buildServiceProvider);
        }
        #endregion

        #region AddHeaderForward
        [Fact]
        public void AddHeaderForward_WithNullIServiceCollection_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = null;
            IWebHostEnvironment hostingEnvironment = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddHeaderForward(hostingEnvironment));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddHeaderForward_WithNullIHostEnvironment_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IWebHostEnvironment hostingEnvironment = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddHeaderForward(hostingEnvironment));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddHeaderForward()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IWebHostEnvironment hostingEnvironment = Mock.Of<IWebHostEnvironment>(e => e.EnvironmentName == "Development");

            // A
            serviceCollection.AddHeaderForward(hostingEnvironment);
            var buildServiceProvider = serviceCollection.BuildServiceProvider();

            // A
            Assert.NotNull(buildServiceProvider);
        }
        #endregion

        #region AddCors
        [Fact]
        public void AddCors_WithNullIServiceCollection_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = null;
            IConfiguration configuration = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddCors(configuration));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddCors_WithNullIConfiguration_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddCors(configuration));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddCors()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder().Build();

            // A
            serviceCollection.AddCors(configuration);
            var buildServiceProvider = serviceCollection.BuildServiceProvider();

            // A
            Assert.NotNull(buildServiceProvider);
        }
        #endregion

        #region AddOpenApi
        [Fact]
        public void AddOpenApi_WithNullIServiceCollection_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = null;
            IConfiguration configuration = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddOpenApi(configuration));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddOpenApi_WithNullIConfiguration_ThrowArgumentNullException()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddOpenApi(configuration));

            // A
            Assert.NotNull(argumentNullException);
        }

        [Fact]
        public void AddOpenApi()
        {
            // A
            IServiceCollection serviceCollection = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder().Build();

            // A
            serviceCollection.AddOpenApi(configuration);
            var buildServiceProvider = serviceCollection.BuildServiceProvider();

            // A
            Assert.NotNull(buildServiceProvider);
        }
        #endregion
    }
}
