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
    public class PerformanceServiceTests
    {
        private ApplicationDbContext context;
        private PerformanceService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new PerformanceService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_Should_Add_Performance()
        {
            var dto = new PerformanceDto
            {
                Title = "Performance",
                Description = "Test Description" 
            };

            await service.AddAsync(dto);

            Assert.That(context.Performances.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetAll_Should_Return_Performances()
        {
            var dto = new PerformanceDto
            {
                Title = "Perf",
                Description = "Desc"
            };

            await service.AddAsync(dto);

            var result = await service.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetFilteredAsync_Should_Filter_By_All_Parameters()
        {
            
            var eventTypeId = Guid.NewGuid();
            var locationId = Guid.NewGuid();
            var date = DateTime.Today;

            var eventType = new EventType
            {
                Id = eventTypeId,
                Name = "Concert"
            };

            var location = new Location
            {
                Id = locationId,
                Name = "Hall",
                Address = "Center",
                City = "Sofia"
            };

            var ev = new Event
            {
                Id = Guid.NewGuid(),
                Title = "Test Event",
                Description = "Desc",
                Duration = 120,
                EventType = eventTypeId,
                Type = eventType
            };

            var performance = new Performance
            {
                Id = Guid.NewGuid(),
                Title = "Perf",
                Description = "Perf Desc",
                Date = date,
                EventId = ev.Id,
                Event = ev,
                LocationId = locationId,
                Location = location
            };

            context.EventTypes.Add(eventType);
            context.Locations.Add(location);
            context.Events.Add(ev);
            context.Performances.Add(performance);

            context.SaveChanges();

            
            var result = await service.GetFilteredAsync(date, locationId, eventTypeId);

          
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Title, Is.EqualTo("Test Event"));
        }
        [Test]
        public async Task DeleteAsync_Should_Delete_Performance_And_Schedules()
        {
            
            var performance = new Performance
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Desc",
                Date = DateTime.Now
            };

            var schedule1 = new Schedule
            {
                Id = Guid.NewGuid(),
                PerformanceId = performance.Id,
                Type = "Test",
                Date = DateTime.Now,
                IsPublic = false
            };

            var schedule2 = new Schedule
            {
                Id = Guid.NewGuid(),
                PerformanceId = performance.Id,
                Type = "Test",
                Date = DateTime.Now,
                IsPublic = false
            };

            context.Performances.Add(performance);
            context.Schedules.AddRange(schedule1, schedule2);
            context.SaveChanges();

            
            await service.DeleteAsync(performance.Id);

          
            Assert.That(context.Performances.Count(), Is.EqualTo(0));
            Assert.That(context.Schedules.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task DeleteAsync_Should_Not_Crash_When_NotFound()
        {
            var id = Guid.NewGuid();

            await service.DeleteAsync(id);

            Assert.Pass();
        }
    }
}