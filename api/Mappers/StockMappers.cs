using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Purchase = stock.Purchase,
                LastDiv = stock.LastDiv,
                Industry = stock.Industry,
                MarketCap = stock.MarketCap,
                Comments = stock.Comments.Select(x => x.ToCommentDto()).ToList()

            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
           return new Stock
           {
              Symbol = stockDto.Symbol,
              CompanyName = stockDto.CompanyName,
              Purchase = stockDto.Purchase,
              LastDiv = stockDto.LastDiv,
              Industry = stockDto.Industry,
              MarketCap = stockDto.MarketCap

           };
        }

        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                // Symbol = fmpStock.symbol,
                // CompanyName = fmpStock.companyName,
                // Purchase = (decimal)fmpStock.price,
                // LastDiv = (decimal)fmpStock.lastDiv,
                // Industry = fmpStock.industry,
                // MarketCap = fmpStock.mktCap
            };
        }
    }
}