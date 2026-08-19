using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    private readonly string stringConexao =
        "Server=127.0.0.1;Port=3306;Database=DiarioSenac;Uid=root;Pwd=S&nac2024;SslMode=Preferred;AllowPublicKeyRetrieval=True;ConnectionTimeout=30;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
    }
}