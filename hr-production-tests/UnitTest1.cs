using hr_console_production.Controllers;
using Serilog;
using System;
using Xunit;

namespace hr_production_tests
{
    public class UnitTest1
    {
        [Fact]
        public void ThrowsException()
        {
            var logger = new LoggerConfiguration()
                .CreateLogger();
            var controller = new ProductController(logger);

            Assert.Throws<NotImplementedException>(() => controller.GetException());
        }
    }
}
