using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlusTorloni.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _eventContext;

    public PresencaRepository(EventContext eventContext)
    {
        _eventContext = eventContext; 
    }

    /// <summary>
    /// Alterna a situação da presença (Toggle)
    /// </summary>
    public void Atualizar(Guid IdPresencaEvento)
    {
        var presencaBuscada = _eventContext.Presencas.Find(IdPresencaEvento);

        if (presencaBuscada != null)
        {
            presencaBuscada.Situacao = !presencaBuscada.Situacao;

            _eventContext.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>
    public Presenca BuscarPorId(Guid id)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
                .ThenInclude(e => e!.IdInstituicaoNavigation)
            .FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Inscrever(Presenca Inscricao)
    {
        throw new NotImplementedException();
    }

    public List<Presenca> Listar()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuário para filtragem</param>
    /// <returns>uma lista de presencas de um usuário específico</returns>
    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _eventContext.Presencas
            .Include(p => p.IdEventoNavigation)
                .ThenInclude(e => e!.IdInstituicaoNavigation)
            .Where(p => p.IdUsuario == IdUsuario)
            .ToList();
    }
}
