using Azure;
using Azure.AI.ContentSafety;
using EventPlusTorloni.WebAPI.DTO;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlusTorloni.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioEventoController : ControllerBase
{
    private readonly ContentSafetyClient _contentSafetyClient;
    private readonly IComentarioEventoRepository _comentarioEventoRepository;

    public ComentarioEventoController(ContentSafetyClient contentSafetyClient, IComentarioEventoRepository comentarioEventoRepository)
    {
        _contentSafetyClient = contentSafetyClient;
        _comentarioEventoRepository = comentarioEventoRepository;
    }

    /// <summary>
    /// Endpoint da API que cadastra e modera um comentário
    /// </summary>
    /// <param name="comentarioEvento">comentário a ser moderado</param>
    /// <returns>Status Code 201 e o comentário criado</returns>
    [HttpPost]
    public async Task<IActionResult> Cadastrar(ComentarioEventoDTO comentarioEvento)
    {
        try
        {
            if (string.IsNullOrEmpty(comentarioEvento.Descricao))
            {
                return BadRequest("O texto a ser moderado não pode estar vazio.");
            }

            // criar objeto de análise
            var request = new AnalyzeTextOptions(comentarioEvento.Descricao);

            // chamar a API do Azure Content Safety
            Response<AnalyzeTextResult> response = await _contentSafetyClient.AnalyzeTextAsync(request);

            // Verificar se o texto tem alguma severidade maior que 0
            bool temConteudoImproprio = response.Value.CategoriesAnalysis.Any(comentario => comentario.Severity > 0);

            var novoComentario = new ComentarioEvento
            {
                Descricao = comentarioEvento.Descricao,
                IdUsuario = comentarioEvento.IdUsuario,
                IdEvento = comentarioEvento.IdEvento,
                DataComentarioEvento = DateTime.Now,
                //Define se o comentário vai ser exibido
                Exibe = !temConteudoImproprio
            };

            // cadastrar o comentário
            _comentarioEventoRepository.Cadastrar(novoComentario);

            return StatusCode(201, novoComentario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
