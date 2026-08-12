using MySql.Data.MySqlClient;

public class RepositorioDiario
{
    private MySqlConnection conexao;

    public RepositorioDiario()
    {
        MeuDiarioSENACContext c = new MeuDiarioSENACContext();
        conexao = c.Conectar();

    }

    private bool AbrirConexao()
    {
        if (conexao == null)
        {
            MeuDiarioSENACContext c = new MeuDiarioSENACContext();
            conexao = c.Conectar();
        }

        if (conexao == null)
        {
            return false;
        }

        if (conexao.State != System.Data.ConnectionState.Open)
        {
            conexao.Open();
        }

        return true;
    }


    public void CadastrarRegistro(string titulo, string conteudo)
    {
        if (!AbrirConexao())
        {
            return;
        }
        string sql = "INSERT INTO registro (titulo, conteudo) VALUES (@titulo, @conteudo)";

        using MySqlCommand comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@titulo", titulo);
        comando.Parameters.AddWithValue("@conteudo", conteudo);
        comando.ExecuteNonQuery();
    }

    public List<Registro> ListarRegistros()
    {
        if (!AbrirConexao())
        {
            return new List<Registro>();
        }

        string sql = "SELECT * FROM registro";

        using MySqlCommand comando = new MySqlCommand(sql, conexao);
        using MySqlDataReader leitor = comando.ExecuteReader();

        List<Registro> registros = new List<Registro>();

        while (leitor.Read())
        {

            Registro registro = new Registro();

            registro.IdRegistro = Convert.ToInt32(leitor["IdRegistro"]);
            registro.Titulo = Convert.ToString(leitor["Titulo"]);
            registro.Conteudo = Convert.ToString(leitor["Conteudo"]);
            registro.Data = Convert.ToDateTime(leitor["Data"]);

            registros.Add(registro);
        }

        return registros;
    }

    public List<Registro> PesquisarRegistro(int idRegistro)
    {
        if (!AbrirConexao())
        {
            return new List<Registro>();
        }

        string sql = "SELECT * FROM registro WHERE IdRegistro = @idRegistro";

        using MySqlCommand comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@idRegistro", idRegistro);

        using MySqlDataReader leitor = comando.ExecuteReader();

        List<Registro> registros = new List<Registro>();

        while (leitor.Read())
        {
            Registro registro = new Registro();

            registro.IdRegistro = Convert.ToInt32(leitor["IdRegistro"]);
            registro.Titulo = Convert.ToString(leitor["Titulo"]);
            registro.Conteudo = Convert.ToString(leitor["Conteudo"]);
            registro.Data = Convert.ToDateTime(leitor["Data"]);

            registros.Add(registro);
        }

        return registros;
    }

    public void RemoverRegistro(int idRegistro)
    {
        if (!AbrirConexao())
        {
            return;
        }

        string sql = "DELETE FROM registro WHERE IdRegistro = @idRegistro";

        using MySqlCommand comando = new MySqlCommand(sql, conexao);
        comando.Parameters.AddWithValue("@idRegistro", idRegistro);
        comando.ExecuteNonQuery();
        
    }

}