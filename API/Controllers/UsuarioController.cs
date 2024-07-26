using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using API.Context;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly APIDbContext _context;

    public UsuariosController(APIDbContext context)
    {
        _context = context;
    }

    // Método GET para listar todos os usuários
    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> Get()
    {
        return _context.Usuario.ToList(); // Retorna todos os usuários
    }

    // Método GET para obter um usuário por ID
    [HttpGet("{id}")]
    public ActionResult<Usuario> Get(int id)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }
        return usuario; // Retorna o usuário encontrado
    }

    // Método POST para criar um novo usuário
    [HttpPost]
    public ActionResult<Usuario> Post([FromBody] Usuario novoUsuario)
    {
        _context.Usuario.Add(novoUsuario); // Adiciona o usuário ao contexto
        _context.SaveChanges(); // Salva as mudanças no banco de dados
        return CreatedAtAction(nameof(Get), new { id = novoUsuario.Id }, novoUsuario);
    }

    // Método PUT para atualizar um usuário existente
    [HttpPut("{id}")]
    public ActionResult<Usuario> Put(int id, [FromBody] Usuario usuarioAtualizado)
    {
        var usuarioExistente = _context.Usuario.FirstOrDefault(u => u.Id == id);
        if (usuarioExistente == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        // Atualiza os campos do usuário existente
        usuarioExistente.Login = usuarioAtualizado.Login;
        usuarioExistente.Senha = usuarioAtualizado.Senha; // Cuidado: Em cenários reais, a senha deve ser tratada com segurança

        _context.SaveChanges(); // Salva as mudanças no banco de dados

        return usuarioExistente; // Retorna o usuário atualizado
    }

    // Método DELETE para excluir um usuário por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var usuario = _context.Usuario.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
        {
            return NotFound(); // Retorna 404 se não encontrado
        }

        _context.Usuario.Remove(usuario); // Remove o usuário do contexto
        _context.SaveChanges(); // Salva as mudanças no banco de dados

        return NoContent(); // Retorna 204 (sem conteúdo)
    }
}
