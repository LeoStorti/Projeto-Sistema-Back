using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComprasController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ComprasController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> GetCompras()
        {
            try
            {
                var compras = await _context.Compras
                    .Include(c => c.Itens_Compra)
                    .ThenInclude(ic => ic.Produtos)
                    .ToListAsync();

                return Ok(compras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar compras: {ex.Message}");
            }
        }

        // GET: api/Compras/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Compras>> GetCompra(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Itens_Compra)
                .ThenInclude(ic => ic.Produtos)
                .FirstOrDefaultAsync(c => c.Compra_Id == id);

            if (compra == null)
            {
                return NotFound("Compra não encontrada.");
            }

            return Ok(compra);
        }

        // POST: api/Compras
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompra(ComprasDTO comprasDto)
        {
            var compra = new Compras
            {
                Fornecedor_Id = comprasDto.Fornecedor_Id,
                ValorDeCompra = comprasDto.ValorDeCompra,
                DataCompra = DateTime.Now, // Aqui você pode ajustar conforme necessário
                Itens_Compra = new List<Itens_Compra>()
            };

            foreach (var itemDto in comprasDto.Itens_Compra)
            {
                compra.Itens_Compra.Add(new Itens_Compra
                {
                    ProductId = itemDto.ProductId,
                    Quantidade = itemDto.Quantidade,
                    ValorUnitario = itemDto.ValorUnitario
                });
            }

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompras), new { id = compra.Compra_Id }, compra);
        }
    }
}