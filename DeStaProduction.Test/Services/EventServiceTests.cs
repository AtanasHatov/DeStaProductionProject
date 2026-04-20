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
    public class EventServiceTests
    {
        private ApplicationDbContext context;
        private EventService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new EventService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_Should_Add_Event()
        {
            var dto = new AddEventDto
            {
                Title = "Event",
                Description = "Desc",
                Duration = 120,
                EventTypeId = Guid.Parse("9788b69a-9153-4f89-b0b5-074d62378e28")
            };

            await service.AddAsync(dto);

            Assert.That(context.Events.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAsync_Should_Delete_Event()
        {
            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Desc",
                Duration = 100,
                EventType = Guid.Parse("0bb048ce-963b-4196-8ab8-6c789b58b2c1")
            };

            context.Events.Add(ev);
            context.SaveChanges();

            await service.DeleteAsync(ev.Id);

            Assert.That(context.Events.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task GetAll_Should_Return_Items()
        {
           
            var dto = new AddEventDto
            {
                Title = "Event",
                Description = "Desc",
                Duration = 120,
                EventTypeId = Guid.NewGuid()
            };

            await service.AddAsync(dto);

           
            var result = await service.GetAllAsync();

            
            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetById_Should_Return_Event()
        {
            var dto = new AddEventDto
            {
                Title = "Event",
                Description = "Desc",
                Duration = 120,
                EventTypeId = Guid.NewGuid()
            };

            await service.AddAsync(dto);

            var ev = context.Events.First();

            var result = await service.GetByIdAsync(ev.Id);

            Assert.That(result, Is.Null);
        }

        
    }
}