
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class BlockchainService : IBlockchainService
    {
        private readonly ApplicationDbContext _context;
        private readonly BlockCypherClient _client;

        public BlockchainService(ApplicationDbContext context, BlockCypherClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task<BlockchainInfo> GetAndSaveBlockchainDataAsync(string path)
        {

            
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

            return info;
        }

        public async Task<IEnumerable<BlockchainInfo>> GetStoredHistoryAsync()
        {
            
            return await _context.Blockchains
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
    }
}
