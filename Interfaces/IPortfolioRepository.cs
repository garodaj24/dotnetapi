using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.Models;

namespace dotnetapi.Interfaces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetPortfolio(AppUser user);
        public Task<Portfolio> AddPortfolio(Portfolio portfolio);
        public Task<Portfolio?> RemovePortfolio(AppUser user, string symbol);
    }
}