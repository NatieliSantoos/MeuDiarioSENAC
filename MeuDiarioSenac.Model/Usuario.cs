namespace MeuDiarioSenac.Model;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public List<Registro> Registros { get; set; } = new();
}