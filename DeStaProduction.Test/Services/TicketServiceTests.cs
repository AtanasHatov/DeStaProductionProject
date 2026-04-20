using NUnit.Framework;
using DeStaProduction.Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using DeStaProduction.Infrastucture.Entities;

namespace DeStaProduction.Test.Services
{
    public class TicketServiceTests
    {
        private ApplicationDbContext context;
        private TicketService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new TicketService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task GetAllAsync_Should_Not_Return_Null()
        {
            var result = service.GetAll();

            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public void GetAll_Should_Return_List()
        {
            var result = service.GetAll();

            Assert.That(result, Is.Not.Null);
        }
    }
}