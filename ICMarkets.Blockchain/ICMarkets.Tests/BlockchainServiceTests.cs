using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ICMarkets.Tests
{
    public  class BlockchainServiceTests
    {
        [Fact]
        public async Task GetStoredHistoryAsync_ShouldReturnDataInDescendingOrder()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
                .Options;

            using var context = new ApplicationDbContext(options);
            
            context.Blockchains.Add(new BlockchainInfo { BlockchainName = "BTC", CreatedAt = DateTime.UtcNow.AddMinutes(-10), RawJson = "{}" });
            context.Blockchains.Add(new BlockchainInfo { BlockchainName = "ETH", CreatedAt = DateTime.UtcNow, RawJson = "{}" });
            await context.SaveChangesAsync();

            var mockClient = new Mock<BlockCypherClient>(new HttpClient());
            var service = new BlockchainService(context, mockClient.Object);
            
            var result = (await service.GetStoredHistoryAsync()).ToList();

            Assert.Equal("ETH", result[0].BlockchainName);
            Assert.True(result[0].CreatedAt > result[1].CreatedAt);
        }
    }
}
