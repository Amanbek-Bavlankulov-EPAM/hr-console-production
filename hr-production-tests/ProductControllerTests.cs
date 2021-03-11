using hr_console_production.Controllers;
using NUnit.Framework;
using Serilog;
using System;

namespace hr_production_tests
{
    public class ProductControllerTests
    {
        ProductController _controller;
        ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = new LoggerConfiguration().CreateLogger();
            _controller = new ProductController(_logger);
        }

        [Test]
        public void ThrowException()
        {
            Assert.Throws(typeof(NotImplementedException), () => _controller.GetException());
        }
    }
}