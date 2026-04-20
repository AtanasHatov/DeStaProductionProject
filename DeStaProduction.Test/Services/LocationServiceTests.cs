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
    public class LocationServiceTests
    {
        private ApplicationDbContext context;
        private LocationService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(options);
            service = new LocationService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }

        [Test]
        public async Task AddAsync_Should_Add_Location()
        {
            var dto = new LocationDto
            {
                Name = "Hall",
                Address = "Center",
                City = "Gabrovo" 
            };

            await service.AddAsync(dto);

            Assert.That(context.Locations.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetAll_Should_Return_Locations()
        {
            var dto = new LocationDto
            {
                Name = "Hall",
                Address = "Center",
                City = "Gabrovo"
            };

            await service.AddAsync(dto);

            var result = await service.GetAllAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetById_Should_Return_Location()
        {
            var dto = new LocationDto
            {
                Name = "Hall",
                Address = "Center",
                City = "Gabrovo"
            };

            await service.AddAsync(dto);

            var loc = context.Locations.First();

            var result = await service.GetByIdAsync(loc.Id);

            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task UpdateAsync_Should_Update_Location()
        {
           
            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = "Old",
                Address = "Old Address",
                City = "Old city"
            };

            context.Locations.Add(location);
            context.SaveChanges();

            var dto = new LocationDto
            {
                Id = location.Id,
                Name = "New",
                Address = "New Address"
            };

           
            await service.UpdateAsync(dto);

            var updated = context.Locations.First();

            Assert.That(updated.Name, Is.EqualTo("New"));
            Assert.That(updated.Address, Is.EqualTo("New Address"));
        }

        [Test]
        public async Task DeleteAsync_Should_Delete_Location()
        {

            var location = new Location
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Address = "Address",
                City = "Sofia" 
            };

            context.Locations.Add(location);
            context.SaveChanges();

           
            await service.DeleteAsync(location.Id);

           
            Assert.That(context.Locations.Count(), Is.EqualTo(0));
        }
        [Test]
        public async Task UpdateAsync_Should_Not_Crash_When_NotFound()
        {
            var dto = new LocationDto
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Address = "Test"
            };

            await service.UpdateAsync(dto);

            Assert.Pass();
        }
    }
}