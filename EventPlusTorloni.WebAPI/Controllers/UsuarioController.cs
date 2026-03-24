using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de Buscar um usuário por id
    /// </summary>
    /// <param name="id">id do usuário a ser buscado</param>
    /// <returns>Status code 200 e o usuário buscado</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_usuarioRepository.BuscarPorId(id));
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que faz a chamada para o método de cadastrar um usuário
    /// </summary>
    /// <param name="usuario">Usuário a ser cadastrado</param>
    /// <returns>Status code 201 e o usuário cadastrado</returns>
    [HttpPost]
    public IActionResult Cadastrar(UsuarioDTO usuario)
    {
        try
        {

            // Converte DTO para Entidade
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Senha = usuario.Senha!,
                Email = usuario.Email!,
                IdTipoUsuario = usuario.IdTipoUsuario
            };
            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, usuario);
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }
}
