using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;
        private readonly ILogger<BlockchainController> _logger;

        public BlockchainController(IBlockchainService blockchainService, ILogger<BlockchainController> logger)
        {
            _blockchainService = blockchainService;
            _logger = logger;
        }

        // GET: api/blockchain/fetch/eth/main
        [HttpGet("fetch/{*path}")]
        public async Task<IActionResult> FetchAndSave(string path)
        {
            _logger.LogInformation("Request received to fetch and save blockchain data for path: {Path}", path);

            if (string.IsNullOrWhiteSpace(path))
            {
                _logger.LogWarning("Fetch attempted with empty path.");
                return BadRequest("Path parameter is required for example: btc/main.");
            }

            try
            {
                var result = await _blockchainService.GetAndSaveBlockchainDataAsync(path);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing {Path}", path);
                return StatusCode(500, "Internal server error occurred.");
            }

        }

        // GET: api/blockchain/history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            _logger.LogInformation("Request received for blockchain history.");

            try
            {
                var history = await _blockchainService.GetStoredHistoryAsync();
                return Ok(history);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error occurred while retrieving history.");
                return StatusCode(500, "Could not retrieve history records.");
            }
        }
    }
}
