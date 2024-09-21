using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Dtos.Stock;
using api.Models;
using api.Mappers;
using api.Interfaces;
using api.Helpers;

namespace api.Controllers
{
   [Route("api/[controller]")]
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
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query) // (AsQueryable) to delay sql query(to list) - defer for filteriing/limit etc
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stocks = await _stockRepository.GetAllAsync(query);
        var stockDtos = stocks.Select(s => s.ToStockDto());
        return Ok(stockDtos);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = stockDto.ToStockFromCreateDto();
        await _stockRepository.CreateAsync(stock);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = stock.Id }, stock.ToStockDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); 

        var stock = await _stockRepository.UpdateAsync(id, updateDto);
        if (stock == null)
        {
            return NotFound();
        }
        return Ok(stock.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
             
        var stock = await _stockRepository.DeleteAsync(id);
        if (stock == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}

}