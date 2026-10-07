using MeuDiarioSenac.Service;
using MeuDiarioSenac.Model;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapPost("/login/auth", () =>){
    TokenService authService = new TokenService(builder.Configuration);
    return authService.GerarToken(null);
}

app.MapGet("/", () => "Hello World!");

var registrosGroup = app.MapGroup("/registros");
var usuariosGroup = app.MapGroup("/usuarios");

registrosGroup.MapGet("/", (int usuarioId) =>
{
    var registros = new RegistroService(new RegistroDAO())
        .ListarRegistrosPorUsuario(usuarioId);

    return registros.Select(registro => new
    {
        registro.Id,
        registro.Titulo,
        registro.Data,
        registro.Conteudo,
        registro.UsuarioId
    });
});

registrosGroup.MapGet("/{id:int}", (int id, int usuarioId) =>
{
    var registro = new RegistroService(new RegistroDAO())
        .ObterRegistroPorId(id, usuarioId);

    return registro is null
        ? Results.NotFound()
        : Results.Ok(new
        {
            registro.Id,
            registro.Titulo,
            registro.Data,
            registro.Conteudo,
            registro.UsuarioId
        });
});

registrosGroup.MapPost("/", (RegistroRequest request) =>
{
    var registro = new Registro
    {
        Titulo = request.Titulo,
        Conteudo = request.Conteudo,
        Data = request.Data ?? default,
        UsuarioId = request.UsuarioId
    };

    try
    {
        new RegistroService(new RegistroDAO()).CadastrarRegistro(registro);

        return Results.Created($"/registros/{registro.Id}?usuarioId={registro.UsuarioId}", new
        {
            registro.Id,
            registro.Titulo,
            registro.Data,
            registro.Conteudo,
            registro.UsuarioId
        });
    }
    catch (ArgumentException exception)
    {
        return Results.BadRequest(new { mensagem = exception.Message });
    }
    catch (InvalidOperationException exception)
    {
        return Results.NotFound(new { mensagem = exception.Message });
    }
});

registrosGroup.MapPut("/{id:int}", (int id, int usuarioId, RegistroRequest request) =>
{
    var registro = new RegistroService(new RegistroDAO())
        .AtualizarRegistro(id, usuarioId, request.Titulo, request.Conteudo, request.Data);

    return registro is null
        ? Results.NotFound()
        : Results.Ok(new
        {
            registro.Id,
            registro.Titulo,
            registro.Data,
            registro.Conteudo,
            registro.UsuarioId
        });
});

registrosGroup.MapDelete("/{id:int}", (int id, int usuarioId) =>
{
    var service = new RegistroService(new RegistroDAO());
    var registro = service.ObterRegistroPorId(id, usuarioId);

    if (registro is null)
        return Results.NotFound();

    service.RemoverRegistro(id, usuarioId);
    return Results.NoContent();
});

usuariosGroup.MapPost("/", (UsuarioRequest request) =>
{
    try
    {
        var usuario = new UsuarioService(new UsuarioDAO())
            .CadastrarUsuario(request.Nome, request.Senha);

        return Results.Created($"/usuarios/{usuario.Id}", new
        {
            usuario.Id,
            usuario.Nome
        });
    }
    catch (ArgumentException exception)
    {
        return Results.BadRequest(new { mensagem = exception.Message });
    }
    catch (InvalidOperationException exception)
    {
        return Results.Conflict(new { mensagem = exception.Message });
    }
});

usuariosGroup.MapPost("/login", (UsuarioRequest request) =>
{
    var usuario = new UsuarioService(new UsuarioDAO())
        .AutenticarUsuario(request.Nome, request.Senha);

    return usuario is null
        ? Results.Unauthorized()
        : Results.Ok(new { usuario.Id, usuario.Nome });
});

usuariosGroup.MapGet("/{id:int}", (int id) =>
{
    var usuario = new UsuarioService(new UsuarioDAO()).ObterUsuarioPorId(id);

    return usuario is null
        ? Results.NotFound()
        : Results.Ok(new { usuario.Id, usuario.Nome });
});

usuariosGroup.MapPut("/{id:int}", (int id, UsuarioRequest request) =>
{
    var service = new UsuarioService(new UsuarioDAO());
    var usuario = service.ObterUsuarioPorId(id);

    if (usuario is null)
        return Results.NotFound();

    usuario.Nome = request.Nome;
    usuario.Senha = request.Senha;

    try
    {
        var atualizado = service.AlterarUsuario(usuario);
        return Results.Ok(new { atualizado.Id, atualizado.Nome });
    }
    catch (ArgumentException exception)
    {
        return Results.BadRequest(new { mensagem = exception.Message });
    }
    catch (InvalidOperationException exception)
    {
        return Results.Conflict(new { mensagem = exception.Message });
    }
});

usuariosGroup.MapDelete("/{id:int}", (int id) =>
{
    var service = new UsuarioService(new UsuarioDAO());

    if (service.ObterUsuarioPorId(id) is null)
        return Results.NotFound();

    service.ExcluirUsuario(id);
    return Results.NoContent();
});

app.Run();


