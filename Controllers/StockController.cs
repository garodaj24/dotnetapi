using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.Data;
using dotnetapi.Dtos.Stock;
using dotnetapi.Interfaces;
using dotnetapi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetapi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllAsync();
            var stocksDto = stocks.Select(s => s.ToStockDto());
            return Ok(stocksDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock = stockDto.ToStockFromCreateDto();
            await _stockRepository.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = await _stockRepository.UpdateAsync(id, stockDto);
            if (stock == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();
            return Ok(stock.ToStockDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}