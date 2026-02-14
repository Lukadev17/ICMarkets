using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BlockchainInfo
    {
        public int Id { get; set; }
        public string BlockchainName { get; set; } = string.Empty; // e.g., BTC, ETH
        public string Network { get; set; } = string.Empty;      // e.g., main, test3

        // Requirement 3: Main data stored as provided in API's JSON response
        public string RawJson { get; set; } = string.Empty;

        // Requirement 3: Additional timestamp
        public DateTime CreatedAt { get; set; }
    }
}
