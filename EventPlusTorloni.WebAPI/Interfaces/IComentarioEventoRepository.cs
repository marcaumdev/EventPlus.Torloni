using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces;

public interface IComentarioEventoRepository
{
    void Cadastrar(ComentarioEvento comentarioEvento);
    void Deletar(Guid id);
    List<ComentarioEvento> Listar(Guid IdEvento);
    ComentarioEvento BuscarPorIdUsuario(Guid IdUsuario, Guid IdEvento);
    List<ComentarioEvento> ListarSomenteExibe(Guid IdEvento);
}
