using hr_console_production.Controllers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace hr_production_tests
{
    public class ControllerTests
    {
        [Fact]
        public void ThrowsException()
        {
            var logger = new LoggerConfiguration().CreateLogger();
            var controller = new ProductController(logger);

            Assert.Throws<NotImplementedException>(() => controller.GetException());
        }
    }
}
