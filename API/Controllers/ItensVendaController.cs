using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Context;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItensVendaController : ControllerBase
    {
        private readonly APIDbContext _context;

        public ItensVendaController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/ItensVenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itens_Venda>>> GetItensVenda()
        {
            return await _context.Itens_Venda
                .Include(iv => iv.Produtos)
                .Include(iv => iv.Vendas)
                .ToListAsync();
        }

        // GET: api/ItensVenda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Itens_Venda>> GetItensVenda(int id)
        {
            var itensVenda = await _context.Itens_Venda
                .Include(iv => iv.Produtos)
                .Include(iv => iv.Vendas)
                .FirstOrDefaultAsync(iv => iv.Itens_Venda_PK == id);

            if (itensVenda == null)
            {
                return NotFound();
            }

            return itensVenda;
        }

        // PUT: api/ItensVenda/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItensVenda(int id, Itens_Venda itensVenda)
        {
            if (id != itensVenda.Itens_Venda_PK)
            {
                return BadRequest();
            }

            _context.Entry(itensVenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItensVendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ItensVenda
        [HttpPost]
        public async Task<ActionResult<Itens_Venda>> PostItensVenda(Itens_Venda itensVenda)
        {
            _context.Itens_Venda.Add(itensVenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItensVenda", new { id = itensVenda.Itens_Venda_PK }, itensVenda);
        }

        // DELETE: api/ItensVenda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItensVenda(int id)
        {
            var itensVenda = await _context.Itens_Venda.FindAsync(id);
            if (itensVenda == null)
            {
                return NotFound();
            }

            _context.Itens_Venda.Remove(itensVenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItensVendaExists(int id)
        {
            return _context.Itens_Venda.Any(e => e.Itens_Venda_PK == id);
        }
    }
}
