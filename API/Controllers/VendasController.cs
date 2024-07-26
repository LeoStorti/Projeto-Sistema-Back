using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Context;
using API.DTOs; // Certifique-se de ter o namespace correto para os DTOs
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly APIDbContext _context;

        public VendasController(APIDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendas>>> GetVendas()
        {
            return await _context.Vendas.ToListAsync();
        }

        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendas>> GetVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        // POST: api/Vendas
        [HttpPost]
        public async Task<ActionResult<Vendas>> PostVenda(VendasDto vendaDto)
        {
            var venda = new Vendas
            {
                ClienteId = vendaDto.ClienteId,
                ValorDeVenda = vendaDto.ValorDeVenda,
                DataVenda = DateTime.Now, // Aqui você pode ajustar conforme necessário
                Itens_Venda = new List<Itens_Venda>()
            };

            foreach (var itemDto in vendaDto.Itens_Venda)
            {
                venda.Itens_Venda.Add(new Itens_Venda
                {
                    ProdutoId_FK = itemDto.ProdutoId,
                    Quantidade = itemDto.Quantidade,
                    ValorUnitario = itemDto.ValorUnitario
                });
            }

            _context.Vendas.Add(venda);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenda), new { id = venda.Vendas_Id }, venda);
        }

        // PUT: api/Vendas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, Vendas venda)
        {
            if (id != venda.Vendas_Id)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
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

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            var venda = await _context.Vendas.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Vendas.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExists(int id)
        {
            return _context.Vendas.Any(e => e.Vendas_Id == id);
        }
    }
}
