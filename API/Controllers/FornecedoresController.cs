using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Context; // Importe o namespace do seu contexto de banco de dados

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FornecedoresController : ControllerBase
{
    private readonly APIDbContext _context;

    public FornecedoresController(APIDbContext context)
    {
        _context = context;
    }

    // Método GET para listar todos os fornecedores
    [HttpGet]
    public ActionResult<IEnumerable<Fornecedor>> Get()
    {
        return _context.CadastroFornecedor.ToList(); // Consulta os fornecedores no banco de dados
    }

    // Método GET para obter um fornecedor por ID
    [HttpGet("{id}")]
    public ActionResult<Fornecedor> Get(int id)
    {
        var fornecedor = _context.CadastroFornecedor.FirstOrDefault(f => f.Id == id);
        if (fornecedor == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }
        return fornecedor; // Retorna o fornecedor encontrado
    }

    // Método POST para criar um novo fornecedor
    [HttpPost]
    public ActionResult<Fornecedor> Post([FromBody] Fornecedor novoFornecedor)
    {
        _context.CadastroFornecedor.Add(novoFornecedor); // Adiciona o fornecedor ao contexto
        _context.SaveChanges(); // Salva as mudanças no banco de dados
        return CreatedAtAction(nameof(Get), new { id = novoFornecedor.Id }, novoFornecedor);
    }

    // Método PUT para atualizar um fornecedor existente
    [HttpPut("{id}")]
    public ActionResult<Fornecedor> Put(int id, [FromBody] Fornecedor fornecedorAtualizado)
    {
        var fornecedorExistente = _context.CadastroFornecedor.FirstOrDefault(f => f.Id == id);
        if (fornecedorExistente == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        // Atualiza os campos do fornecedor existente
        fornecedorExistente.Nome = fornecedorAtualizado.Nome;
        fornecedorExistente.CNPJ = fornecedorAtualizado.CNPJ;
        fornecedorExistente.Endereco = fornecedorAtualizado.Endereco;

        _context.SaveChanges(); // Salva as mudanças no banco de dados

        return fornecedorExistente; // Retorna o fornecedor atualizado
    }

    // Método DELETE para excluir um fornecedor por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var fornecedor = _context.CadastroFornecedor.FirstOrDefault(f => f.Id == id);
        if (fornecedor == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        _context.CadastroFornecedor.Remove(fornecedor); // Remove o fornecedor do contexto
        _context.SaveChanges(); // Salva as mudanças no banco de dados

        return NoContent(); // Retorna 204 (sem conteúdo)
    }
}