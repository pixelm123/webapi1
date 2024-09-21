using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Stock;
using Microsoft.Extensions.Primitives;
using api.Helpers;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDBContext _context;
        public StockRepository (ApplicationDBContext context)
        
        {
            _context = context;
        }

        public async Task<Stock?> CreateAsync(Stock stock)
        {
            await _context.Stock.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
           var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

           if (stock == null)
           {
              return null;

           }
           _context.Stock.Remove(stock);
           await _context.SaveChangesAsync();
           return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {

            var stock =  _context.Stock.Include(c => c.Comments).AsQueryable();

             if (!string.IsNullOrWhiteSpace(query.CompanyName))
             {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));
             }

             if (!string.IsNullOrWhiteSpace(query.Symbol))
             {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));
             }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
             {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)) //StringComparison.OrdinalIgnoreCase used to compare two strings while ignoring the case (whether letters are uppercase or lowercase)/special characters
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
             }

            var  skipNumber = (query.PageNumber -1) * query.PageSize;

            return await stock.Skip(skipNumber).Take(query.PageNumber).ToListAsync();

             // return await stock.ToListAsync();

        }
 
        public async Task<Stock?> GetByIdAsync(int id)
         {
            return await _context.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
         }

     
        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stock.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stock.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
           var existingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

           if (existingStock == null)
           {
             return null;
           }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}