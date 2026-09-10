using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MeuDiarioSenac.Model;

public class UsuarioDAO
{
    private MeuDiarioSENACContext context = new MeuDiarioSENACContext();

    public Usuario CadastrarUsuario(string nomeUsuario, string senha)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(nomeUsuario));
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("A senha não pode ser vazia.", nameof(senha));

        var nome = nomeUsuario.Trim();
        if (context.Usuarios.Any(u => u.Nome == nome))
            throw new InvalidOperationException("Este nome de usuário já está cadastrado.");

        var usuario = new Usuario
        {
            Nome = nome,
            Senha = senha,
            Registros = new List<Registro>()
        };

        context.Usuarios.Add(usuario);
        context.SaveChanges();
        return usuario;
    }

    public Usuario? AutenticarUsuario(string nomeUsuario, string senha)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario) || string.IsNullOrWhiteSpace(senha))
            return null;

        var usuario = context.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario.Trim());
        return usuario is not null && usuario.Senha == senha ? usuario : null;
    }

    public Usuario ObterOuCriarUsuario(string nomeUsuario, string senha)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(nomeUsuario));
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("A senha não pode ser vazia.", nameof(senha));

        var nome = nomeUsuario.Trim();
        var usuario = context.Usuarios.FirstOrDefault(u => u.Nome == nome);

        if (usuario is null)
        {
            usuario = new Usuario
            {
                Nome = nome,
                Senha = senha,
                Registros = new List<Registro>()
            };

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }
        else if (usuario.Senha != senha)
        {
            throw new UnauthorizedAccessException("Senha incorreta.");
        }

        return usuario;
    }

    public Usuario AlterarUsuario(Usuario usuario)
    {
        if (usuario is null)
            throw new ArgumentNullException(nameof(usuario));
        if (usuario.Id <= 0)
            throw new ArgumentException("O usuário informado é inválido.", nameof(usuario));
        if (string.IsNullOrWhiteSpace(usuario.Nome))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(usuario));
        if (string.IsNullOrWhiteSpace(usuario.Senha))
            throw new ArgumentException("A senha não pode ser vazia.", nameof(usuario));

        var usuarioExistente = context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
        if (usuarioExistente is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        var nome = usuario.Nome.Trim();
        if (context.Usuarios.Any(u => u.Id != usuario.Id && u.Nome == nome))
            throw new InvalidOperationException("Este nome de usuário já está cadastrado.");

        usuarioExistente.Nome = nome;
        usuarioExistente.Senha = usuario.Senha;
        context.SaveChanges();

        return usuarioExistente;
    }

    public void ExcluirUsuario(int id)
    {
        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario is null)
            throw new InvalidOperationException("Usuário não encontrado.");

        context.Usuarios.Remove(usuario);
        context.SaveChanges();
    }

}