using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Context;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly APIDbContext _context;

    public ProdutosController(APIDbContext context)
    {
        _context = context;
    }

    // Método GET para listar todos os produtos
    [HttpGet]
    public ActionResult<IEnumerable<Produtos>> Get()
    {
        return _context.Produtos.ToList();
    }

    // Exemplo corrigido de um método em ProdutosController
    [HttpGet("{id}")]
    public ActionResult<Produtos> Get(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProductId == id);
        if (produto == null)
        {
            return NotFound();
        }
        return produto;
    }


    // Método POST para criar um novo produto
    [HttpPost]
    public ActionResult<Produtos> Post([FromBody] Produtos novoProduto)
    {
        _context.Produtos.Add(novoProduto);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = novoProduto.ProductId }, novoProduto);
    }

    // Método PUT para atualizar um produto existente
    [HttpPut("{id}")]
    public ActionResult<Produtos> Put(int id, [FromBody] Produtos produtoAtualizado)
    {
        var produtoExistente = _context.Produtos.FirstOrDefault(p => p.ProductId == id);
        if (produtoExistente == null)
        {
            return NotFound();
        }

        produtoExistente.NomeProduto = produtoAtualizado.NomeProduto;
        produtoExistente.Fornecedor = produtoAtualizado.Fornecedor;
        produtoExistente.Quantidade = produtoAtualizado.Quantidade;

        _context.SaveChanges();
        return produtoExistente;
    }

    // Método DELETE para excluir um produto por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProductId == id);
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }
}
