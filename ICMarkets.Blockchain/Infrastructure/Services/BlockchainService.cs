
using System.Net;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly ApplicationDbContext _context;
        private readonly BlockCypherClient _client;
        private readonly ILogger<BlockchainService> _logger;

        public BlockchainService(ApplicationDbContext context, BlockCypherClient client, ILogger<BlockchainService> logger)
        {
            _context = context;
            _client = client;
            _logger = logger;
        }

        public async Task<BlockchainInfo> GetAndSaveBlockchainDataAsync(string path)
        {
            try
            {
                _logger.LogInformation("Processing blockchain data request for path: {Path}", path);

                path = WebUtility.UrlDecode(path);
                path = path.Replace("%2F", "/").Replace("%2f", "/");

                if (string.IsNullOrWhiteSpace(path) || !path.Contains('/'))
                {
                    throw new ArgumentException("Invalid path. Format must be 'chain/network' (e.g., eth/main).");
                }


                var rawJson = await _client.GetRawJsonAsync(path);


                var info = new BlockchainInfo
                {
                    BlockchainName = path,
                    RawJson = rawJson,
                    CreatedAt = DateTime.UtcNow
                };


                _context.Blockchains.Add(info);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully saved blockchain data for {Path} with ID {Id}", path, info.Id);

                return info;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred in BlockchainService for {Path}", path);
                throw;
            }

        }

        public async Task<IEnumerable<BlockchainInfo>> GetStoredHistoryAsync()
        {
            try
            {
                return await _context.Blockchains
                .AsNoTracking()
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve blockchain history.");
                throw;
            }
            
        }
    }
}
