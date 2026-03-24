using EventPlusTorloni.WebAPI.BdContextEvent;
using EventPlusTorloni.WebAPI.Interfaces;
using EventPlusTorloni.WebAPI.Models;

namespace EventPlusTorloni.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    // Injeção de Dependência: Recebe o contexto pronto para uso
    public InstituicaoRepository(EventContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Atualiza uma instituição usando o Change Tracking do EF
    /// </summary>
    /// <param name="id">Id da instituição a ser atualizada</param>
    /// <param name="Instituicao">Novos dados da instituição</param>
    public void Atualizar(Guid id, Instituicao Instituicao)
    {
        // Ao usar o Find, o EF começa a "observar" este objeto
        var instituicaoBuscada = _context.Instituicaos.Find(id);

        if (instituicaoBuscada != null)
        {
            instituicaoBuscada.Cnpj = String.IsNullOrWhiteSpace(Instituicao.Cnpj) ? instituicaoBuscada.Cnpj : Instituicao.Cnpj;
            instituicaoBuscada.Endereco = String.IsNullOrWhiteSpace(Instituicao.Endereco) ? instituicaoBuscada.Endereco : Instituicao.Endereco;
            instituicaoBuscada.NomeFantasia = String.IsNullOrWhiteSpace(Instituicao.NomeFantasia) ? instituicaoBuscada.NomeFantasia : Instituicao.NomeFantasia;

            // Não é necessário chamar _context.Update() se o objeto foi buscado no mesmo contexto
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma instituição por id
    /// </summary>
    /// <param name="id">id da instituição a ser buscada</param>
    /// <returns>Objeto da instituição com as informações da instituição buscada</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra uma nova instituição
    /// </summary>
    /// <param name="Instituicao">instituição a ser cadastrada</param>
    public void Cadastrar(Instituicao Instituicao)
    {
        _context.Instituicaos.Add(Instituicao);
        _context.SaveChanges();
    }

    /// <summary>
    /// Deleta uma instituição
    /// </summary>
    /// <param name="IdInstituicao">id da instituição a ser deletada</param>
    public void Deletar(Guid IdInstituicao)
    {
        var instituicaoBuscada = _context.Instituicaos.Find(IdInstituicao);

        if (instituicaoBuscada != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscada);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de instituição cadastradas
    /// </summary>
    /// <returns>Uma lista de instituições</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.ToList();
    }
}
