using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.Dtos.Stock;
using dotnetapi.Models;

namespace dotnetapi.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();

        public Task<Stock?> GetByIdAsync(int id);

        public Task<Stock> CreateAsync(Stock stock);

        public Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        public Task<Stock?> DeleteAsync(int id);

    }
}