using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetapi.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long")]
        [MaxLength(10, ErrorMessage = "Symbol must be at most 10 characters long")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Company name must be at least 1 character long")]
        [MaxLength(50, ErrorMessage = "Company name must be at most 50 characters long")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000, ErrorMessage = "Purchase must be between 0 and 1000000")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0, 1000000, ErrorMessage = "Last div must be between 0 and 1000000")]
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "Industry must be at least 1 character long")]
        [MaxLength(50, ErrorMessage = "Industry must be at most 50 characters long")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(0, 1000000, ErrorMessage = "Market cap must be between 0 and 1000000")]
        public long MarketCap { get; set; }
    }
}