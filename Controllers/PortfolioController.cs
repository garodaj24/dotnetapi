using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapi.Extensions;
using dotnetapi.Interfaces;
using dotnetapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapi.Controllers
{
    [Route("api/portfolio")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager,
        IStockRepository stockRepository,
        IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username!);
            var userPortfolio = await _portfolioRepository.GetPortfolio(appUser!);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio([FromQuery] string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username!);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock not found");
            }

            var userPortfolio = await _portfolioRepository.GetPortfolio(appUser!);

            if (userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock already in portfolio");
            }

            var portfolio = new Portfolio
            {
                AppUserId = appUser!.Id,
                StockId = stock.Id
            };

            await _portfolioRepository.AddPortfolio(portfolio);

            if (portfolio == null)
            {
                return BadRequest("Failed to add stock to portfolio");
            }
            else 
            {
                return Created();
            }
        }
    }
}