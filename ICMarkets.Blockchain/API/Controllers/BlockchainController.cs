using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService _blockchainService;

        public BlockchainController(IBlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        // GET: api/blockchain/fetch/eth/main
        [HttpGet("fetch/{*path}")]
        public async Task<IActionResult> FetchAndSave(string path)
        {
            var result = await _blockchainService.GetAndSaveBlockchainDataAsync(path);
            return Ok(result);
        }

        // GET: api/blockchain/history
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            
            var history = await _blockchainService.GetStoredHistoryAsync();
            return Ok(history);
        }
    }
}
