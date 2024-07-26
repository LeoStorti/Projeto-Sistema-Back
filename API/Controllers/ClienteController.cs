using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Context;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly APIDbContext _context;

    public ClientesController(APIDbContext context)
    {
        _context = context;
    }

    // Método GET para listar todos os clientes
    [HttpGet]
    public ActionResult<IEnumerable<Clientes>> Get()
    {
        return _context.CadastroCliente.ToList(); // Retorna todos os clientes do banco de dados
    }

    // Método GET para obter um cliente por ID
    [HttpGet("{id}")]
    public ActionResult<Clientes> Get(int id)
    {
        var cliente = _context.CadastroCliente.FirstOrDefault(c => c.ClienteId == id);
        if (cliente == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }
        return cliente; // Retorna o cliente encontrado
    }

    // Método POST para criar um novo cliente
    [HttpPost]
    public ActionResult<Clientes> Post([FromBody] Clientes novoCliente)
    {
        _context.CadastroCliente.Add(novoCliente); // Adiciona o cliente ao banco de dados
        _context.SaveChanges(); // Salva as mudanças
        return CreatedAtAction(nameof(Get), new { id = novoCliente.ClienteId }, novoCliente);
    }

    // Método PUT para atualizar um cliente existente
    [HttpPut("{id}")]
    public ActionResult<Clientes> Put(int id, [FromBody] Clientes clienteAtualizado)
    {
        var clienteExistente = _context.CadastroCliente.FirstOrDefault(c => c.ClienteId == id);
        if (clienteExistente == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        // Atualiza os campos do cliente existente
        clienteExistente.NomeCliente = clienteAtualizado.NomeCliente;
        clienteExistente.CNPJCliente = clienteAtualizado.CNPJCliente;
        clienteExistente.TelefoneCliente = clienteAtualizado.TelefoneCliente;
        clienteExistente.EnderecoCliente = clienteAtualizado.EnderecoCliente;

        _context.SaveChanges(); // Salva as mudanças

        return clienteExistente; // Retorna o cliente atualizado
    }

    // Método DELETE para excluir um cliente por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var cliente = _context.CadastroCliente.FirstOrDefault(c => c.ClienteId == id);

        if (cliente == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        _context.CadastroCliente.Remove(cliente);
        _context.SaveChanges(); // Salva as mudanças

        return NoContent(); // Retorna 204 (sem conteúdo) para indicar sucesso na deleção
    }
}
