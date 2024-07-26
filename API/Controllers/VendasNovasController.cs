using API.Context;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class VendasNovasController : ControllerBase
{
    private readonly VendasService _vendasService;
    private readonly APIDbContext _context;

    public VendasNovasController(VendasService vendasService, APIDbContext context)
    {
        _vendasService = vendasService ?? throw new ArgumentNullException(nameof(vendasService));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpPost]
    public async Task<IActionResult> CriarNovaVenda([FromBody] VendaRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var novaVenda = new Vendas
        {
            NF = request.NF,
            ClienteId = request.ClienteId,
            ValorDeVenda = request.ValorDeVenda
        };

        try
        {
            foreach (var produto in request.Produtos)
            {
                var produtoDb = await _context.Produtos.FindAsync(produto.ProdutoId);
                if (produtoDb == null)
                {
                    return NotFound($"Produto com ID {produto.ProdutoId} não encontrado.");
                }

                if (produtoDb.Quantidade < produto.Quantidade)
                {
                    return BadRequest($"Quantidade insuficiente para o produto {produtoDb.NomeProduto}. Quantidade disponível: {produtoDb.Quantidade}, quantidade solicitada: {produto.Quantidade}.");
                }

                // Reduz o estoque do produto
                produtoDb.Quantidade -= produto.Quantidade;
                _context.Produtos.Update(produtoDb); // Atualiza o produto no contexto

                await _vendasService.CriarNovaVendaComProduto(novaVenda, produto.ProdutoId, produto.Quantidade, produto.ValorUnitario, produto.ValorDeVenda);
            }

            await _context.SaveChangesAsync(); // Salvar as alterações no banco de dados

            return Ok(novaVenda);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar a venda: {ex.Message}");
        }
    }

    public class VendaRequest
    {
        public string NF { get; set; }
        public int ClienteId { get; set; }
        public decimal ValorDeVenda { get; set; }
        public List<ProdutoVendaRequest> Produtos { get; set; }
    }

    public class ProdutoVendaRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorDeVenda { get; set; }
    }
}
