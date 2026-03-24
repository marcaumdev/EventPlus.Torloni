using EventPlusTorloni.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private IPresencaRepository _presencaRepository;
    public PresencaController(IPresencaRepository presencaRepository)
    {
        _presencaRepository = presencaRepository;
    }

    /// <summary>
    /// Endpoint da API que retorna uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>Status code 200 e presença buscada</returns>
    [HttpGet("{id}")]
    public IActionResult BuscarPorId(Guid id)
    {
        try
        {
            return Ok(_presencaRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    /// <summary>
    /// Endpoint da API que retorna um lista de presenças filtrada por usuário
    /// </summary>
    /// <param name="idUsuario">id do usuário para fitlragem</param>
    /// <returns>uma lista de presenças filtrad pelo usuário</returns>
    [HttpGet("ListarMinhas/{idUsuario}")]
    public IActionResult BuscarPorUsuario(Guid idUsuario)
    {
        try
        {
            return Ok(_presencaRepository.ListarMinhas(idUsuario));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
