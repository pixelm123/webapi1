using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using api.Dtos.Stock;
using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);

        Task<Stock?> GetByIdAsync(int id);

        Task<Stock?> CreateAsync(Stock stock);

        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);

        Task<Stock?> DeleteAsync(int id) ;

        Task<bool> StockExist(int id);
        Task<Stock> GetBySymbolAsync(string symbol);
    }
}