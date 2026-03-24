using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuario);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha(string Email, string Senha);
}
