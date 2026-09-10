using MeuDiarioSenac.Data;
using MeuDiarioSenac.Model;

namespace MeuDiarioSenac.Service;

public class UsuarioService
{
    private readonly UsuarioDAO _repositorio;

    public UsuarioService(UsuarioDAO repositorio)
    {
        _repositorio = repositorio;
    }

    public Usuario CadastrarUsuario(string nomeUsuario, string senha)
    {
        return _repositorio.CadastrarUsuario(nomeUsuario, senha);
    }

    public Usuario? AutenticarUsuario(string nomeUsuario, string senha)
    {
        return _repositorio.AutenticarUsuario(nomeUsuario, senha);
    }

    public Usuario ObterOuCriarUsuario(string nomeUsuario, string senha)
    {
        return _repositorio.ObterOuCriarUsuario(nomeUsuario, senha);
    }

    public Usuario AlterarUsuario(Usuario usuario)
    {
        return _repositorio.AlterarUsuario(usuario);
    }

    public void ExcluirUsuario(int id)
    {
        _repositorio.ExcluirUsuario(id);
    }
}
