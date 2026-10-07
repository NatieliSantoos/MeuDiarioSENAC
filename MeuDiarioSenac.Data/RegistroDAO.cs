using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using MeuDiarioSenac.Model;

public class RegistroDAO
{
    private MeuDiarioSENACContext context = new MeuDiarioSENACContext();
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

    public Registro? AtualizarRegistro(int id, int usuarioId, string titulo, string conteudo, DateTime? data)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("O título do registro não pode ser vazio.", nameof(titulo));

        if (string.IsNullOrWhiteSpace(conteudo))
            throw new ArgumentException("O conteúdo do registro não pode ser vazio.", nameof(conteudo));

        var registro = context.Registros
            .FirstOrDefault(r => r.Id == id && r.UsuarioId == usuarioId);

        if (registro is null)
            return null;

        registro.Titulo = titulo;
        registro.Conteudo = conteudo;
        if (data.HasValue && data.Value != default)
            registro.Data = data.Value;

        context.SaveChanges();
        return registro;
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
