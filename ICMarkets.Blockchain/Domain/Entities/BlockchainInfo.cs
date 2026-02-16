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
        public string BlockchainName { get; set; } = string.Empty; 
        public string Network { get; set; } = string.Empty;  
        public string RawJson { get; set; } = string.Empty;     
        public DateTime CreatedAt { get; set; }
    }
}
