using Microsoft.EntityFrameworkCore;
using MeuDiarioSenac.Model;
using MySql.Data.MySqlClient;

public class MeuDiarioSENACContext : DbContext
{
    public DbSet<Registro> Registros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    private readonly string stringConexao =
        "Server=127.0.0.1;Port=3306;Database=DiarioSenac;Uid=root;Pwd=S&nac2024;SslMode=Preferred;AllowPublicKeyRetrieval=True;ConnectionTimeout=30;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Registros)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId);
    }
}