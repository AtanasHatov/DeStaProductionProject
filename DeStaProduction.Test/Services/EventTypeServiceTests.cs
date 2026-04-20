using NUnit.Framework;
using DeStaProduction.Core.Services;
using DeStaProduction.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DeStaProduction.Infrastucture.Entities;

namespace DeStaProduction.Test.Services
{
    public class EventTypeServiceTests
    {
        private ApplicationDbContext context;
        private EventTypeService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new EventTypeService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_Should_Add_EventType()
        {
            var name = "Concert";

            await service.AddAsync(name);

            Assert.That(context.EventTypes.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetAll_Should_Return_EventTypes()
        {
            await service.AddAsync("Concert");

            var result = await service.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task Delete_Should_Remove_EventType()
        {
            var type = new EventType
            {
                Id = Guid.NewGuid(),
                Name = "DeleteMe"
            };

            context.EventTypes.Add(type);
            context.SaveChanges();

            await service.DeleteAsync(type.Id);

            Assert.That(context.EventTypes.Count(), Is.EqualTo(0));
        }
    }
}