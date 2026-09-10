namespace MeuDiarioSenac.Business;
using MeuDiarioSenac.Model;

public class UsuarioBusiness
{
    public bool ValidarUsuario(Usuario usuario)
    {
        return usuario is not null && !string.IsNullOrWhiteSpace(usuario.Nome);
    }

    public bool ValidarUsuario(Usuario usuario, string senha)
    {
        return ValidarUsuario(usuario)
            && !string.IsNullOrWhiteSpace(senha)
            && usuario.Senha == senha;
    }
}