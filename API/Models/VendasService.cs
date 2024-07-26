using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

public class VendasService
{
    private readonly APIDbContext _context;

    public VendasService(APIDbContext context)
    {
        _context = context;
    }

    public async Task CriarNovaVendaComProduto(Vendas novaVenda, int produtoId, int quantidade, decimal valorUnitario, decimal valorDeVenda)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Adiciona a nova venda
            _context.Vendas.Add(novaVenda);
            await _context.SaveChangesAsync();

            // Associa o produto à venda
            var ItensVenda = new Itens_Venda
            {
                Itens_Venda_PK = novaVenda.Vendas_Id,
                ProdutoId_FK = produtoId,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario,
            };
            _context.Itens_Venda.Add(ItensVenda);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
