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
    public class ScheduleServiceTests
    {
        private ApplicationDbContext context;
        private ScheduleService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new ScheduleService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task Add_Should_Add_Schedule()
        {
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                PerformanceId = Guid.NewGuid(),
                Date = DateTime.Now,
                Type = "Test" 
            };

            await service.AddTaskAsync(schedule);

            Assert.That(context.Schedules.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetAll_Should_Return_Schedules()
        {
           
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                PerformanceId = Guid.NewGuid(),
                Date = DateTime.Now,
                Type = "Test"
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();

            var result = await service.GetAllAsync(1, 2025, "Test", Guid.NewGuid());


            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task IsUserAvailable_Should_Return_True_When_No_Record()
        {
           
            var userId = Guid.NewGuid();
            var date = DateTime.Today;

            
            var result = await service.IsUserAvailable(userId, date);

           
            Assert.That(result, Is.True);
        }
        [Test]
        public async Task IsUserAvailable_Should_Return_True_When_Available()
        {
            
            var userId = Guid.NewGuid();
            var date = DateTime.Today;

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Date = date,
                Type = "Availability",
                IsAvailable = true
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();

            var result = await service.IsUserAvailable(userId, date);

         
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task IsUserAvailable_Should_Return_False_When_NotAvailable()
        {
            
            var userId = Guid.NewGuid();
            var date = DateTime.Today;

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Date = date,
                Type = "Availability",
                IsAvailable = false
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();

            
            var result = await service.IsUserAvailable(userId, date);

          
            Assert.That(result, Is.False);
        }

        [Test]
        public async Task SetAvailability_Should_Create_New_Record_When_Not_Exists()
        {
            
            var userId = Guid.NewGuid();
            var date = DateTime.Today;

            
            await service.SetAvailability(userId, date, true);

            
            Assert.That(context.Schedules.Count(), Is.EqualTo(1));

            var schedule = context.Schedules.First();
            Assert.That(schedule.IsAvailable, Is.True);
            Assert.That(schedule.Type, Is.EqualTo("Availability"));
        }
        [Test]
        public async Task SetAvailability_Should_Update_Existing_Record()
        {
            
            var userId = Guid.NewGuid();
            var date = DateTime.Today;

            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Date = date,
                Type = "Availability",
                IsAvailable = false
            };

            context.Schedules.Add(schedule);
            context.SaveChanges();

            
            await service.SetAvailability(userId, date, true);

           
            var updated = context.Schedules.First();

            Assert.That(updated.IsAvailable, Is.True);
            Assert.That(context.Schedules.Count(), Is.EqualTo(1)); 
        }
        [Test]
        public async Task SetAvailability_Should_Create_New_When_Date_Different()
        {
        
            var userId = Guid.NewGuid();

            context.Schedules.Add(new Schedule
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Date = DateTime.Today,
                Type = "Availability",
                IsAvailable = true
            });

            context.SaveChanges();

          
            await service.SetAvailability(userId, DateTime.Today.AddDays(1), false);

            Assert.That(context.Schedules.Count(), Is.EqualTo(2));
        }
    }
}