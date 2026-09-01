using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

public class RepositorioDiario
{
    private MeuDiarioSENACContext context = new MeuDiarioSENACContext();

    public Usuario ObterOuCriarUsuario(string nomeUsuario)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("O nome do usuário não pode ser vazio.", nameof(nomeUsuario));

        var nome = nomeUsuario.Trim();
        var usuario = context.Usuarios.FirstOrDefault(u => u.Nome == nome);

        if (usuario is null)
        {
            usuario = new Usuario
            {
                Nome = nome,
                Registros = new List<Registro>()
            };

            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        return usuario;
    }

    public void CadastrarRegistro(Registro registro)
    {
        if (registro is null)
            throw new ArgumentNullException(nameof(registro));

        if (string.IsNullOrWhiteSpace(registro.Titulo))
            throw new ArgumentException("O título do registro não pode ser vazio.", nameof(registro));

        if (string.IsNullOrWhiteSpace(registro.Conteudo))
            throw new ArgumentException("O conteúdo do registro não pode ser vazio.", nameof(registro));

        var usuario = context.Usuarios.FirstOrDefault(u => u.Id == registro.UsuarioId);

        if (usuario is null)
        {
            throw new InvalidOperationException("Usuário inválido para este registro.");
        }

        registro.Usuario = usuario;
        registro.UsuarioId = usuario.Id;
        registro.Data = registro.Data == default ? DateTime.Now : registro.Data;

        context.Registros.Add(registro);
        context.SaveChanges();
    }

    public List<Registro> ListarRegistrosPorUsuario(int usuarioId)
    {
        return context.Registros
            .Where(r => r.UsuarioId == usuarioId)
            .OrderBy(r => r.Id)
            .ToList();
    }

    public Registro? ObterRegistroPorId(int id, int usuarioId)
    {
        return context.Registros
            .FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);
    }

    public void RemoverRegistro(int id, int usuarioId)
    {
        var registro = context.Registros
            .FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);

        if (registro is not null)
        {
            context.Registros.Remove(registro);
            context.SaveChanges();
        }
    }

}