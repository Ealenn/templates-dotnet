using Api.Dotnet.Template.Extensions;
using Microsoft.AspNetCore.Builder;
using System;
using Xunit;

namespace Api.Dotnet.Template.UnitTests.Extensions
{
    public class AppBuilderExtensionsTests
    {
        #region UseExceptionHandler
        [Fact]
        public void UseExceptionHandler_WithNullApplicationBuilder_ThrowArgumentNullException()
        {
            // A
            IApplicationBuilder applicationBuilder = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() =>
            {
                return applicationBuilder.UseExceptionHandler(true);
            });

            // A
            Assert.NotNull(argumentNullException);
        }
        #endregion

        #region UseOpenApi
        [Fact]
        public void UseOpenApi_WithNullApplicationBuilder_ThrowArgumentNullException()
        {
            // A
            IApplicationBuilder applicationBuilder = null;

            // A
            var argumentNullException = Assert.Throws<ArgumentNullException>(() => applicationBuilder.UseOpenApi());

            // A
            Assert.NotNull(argumentNullException);
        }
        #endregion
    }
}
