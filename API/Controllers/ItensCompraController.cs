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
    public class ItensCompraController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ItensCompraController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/ItensCompra
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itens_Compra>>> GetItensCompras()
        {
            try
            {
                var itensCompras = await _context.Itens_Compra
                    .Include(ic => ic.Compras)
                    .Include(ic => ic.Produtos)
                    .ToListAsync();

                return Ok(itensCompras);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar itens de compras: {ex.Message}");
            }
        }

        // POST: api/ItensCompra
        [HttpPost]
        public async Task<ActionResult<Itens_Compra>> PostItensCompra(Itens_Compra itensCompra)
        {
            try
            {
                _context.Itens_Compra.Add(itensCompra);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItensCompra", new { id = itensCompra.Itens_Compra_PK }, itensCompra);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar item de compra: {ex.Message}");
            }
        }
    }
}
