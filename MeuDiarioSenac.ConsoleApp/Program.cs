RepositorioDiario repositorio = new RepositorioDiario();

Console.WriteLine("Digite seu nome de usuario");
string nomeUsuario;

while (true)
{
    nomeUsuario = Console.ReadLine() ?? string.Empty;

    if (!string.IsNullOrWhiteSpace(nomeUsuario))
        break;

    Console.WriteLine("Nome inválido. Digite um nome para continuar.");
}

var usuario = repositorio.ObterOuCriarUsuario(nomeUsuario);

Console.WriteLine($"Bem vindo, {usuario.Nome}!");
Console.WriteLine("============================");

string opcao;
do
{
    Console.WriteLine("");
    Console.WriteLine("escolha uma opçao");
    Console.WriteLine("");
    Console.WriteLine("1 = adicionar um novo registro");
    Console.WriteLine("2 = Listar seus Registros");
    Console.WriteLine("3 = Pesquisar seus registros");
    Console.WriteLine("4 = Deletar  registro");
    Console.WriteLine("5 =  SAIR ");
    Console.WriteLine("============================");
    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            Registro cadastrar = new Registro();
            Console.WriteLine("");
            cadastrar.Usuario = usuario;
            cadastrar.UsuarioId = usuario.Id;
            cadastrar.Data = DateTime.Now;

            Console.WriteLine("Titulo");
            while (true)
            {
                cadastrar.Titulo = Console.ReadLine() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(cadastrar.Titulo))
                    break;

                Console.WriteLine("Título inválido. Digite um título.");
            }
            Console.WriteLine("");

            Console.WriteLine("Abra seu coraçao");
            while (true)
            {
                cadastrar.Conteudo = Console.ReadLine() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(cadastrar.Conteudo))
                    break;

                Console.WriteLine("Conteúdo inválido. Digite algo para registrar.");
            }
            Console.WriteLine("");

            try
            {
                repositorio.CadastrarRegistro(registro: cadastrar);
                Console.WriteLine("Registro SALVO com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar registro: {ex.Message}");
            }

            Thread.Sleep(2000);
            break;

        case "2":
            List<Registro> listar = repositorio.ListarRegistrosPorUsuario(usuario.Id);

            foreach (Registro registro in listar)
            {
                Console.WriteLine($"ID: {registro.Id}");
                Console.WriteLine($"Título: {registro.Titulo}");
                Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                Console.WriteLine($"Data: {registro.Data}");
                Console.WriteLine("========================");
                Console.WriteLine("");
            }
            Thread.Sleep(2000);
            break;

        case "3":
            int id;
            while (true)
            {
                Console.Write("Digite o ID: ");
                var entradaId = Console.ReadLine();

                if (int.TryParse(entradaId, out id))
                    break;

                Console.WriteLine("ID inválido. Digite apenas números.");
            }

            Registro? registroEncontrado = repositorio.ObterRegistroPorId(id, usuario.Id);

            if (registroEncontrado is null)
            {
                Console.WriteLine("Nenhum registro encontrado para este ID.");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: {registroEncontrado.Id}");
                Console.WriteLine($"Título: {registroEncontrado.Titulo}");
                Console.WriteLine($"Conteúdo: {registroEncontrado.Conteudo}");
                Console.WriteLine($"Data: {registroEncontrado.Data}");
                Console.WriteLine("========================");
                Console.WriteLine("");
            }

            Thread.Sleep(2000);
            break;

        case "4":
            int idremover;
            while (true)
            {
                Console.WriteLine("Digite o ID que quer remover: ");
                var entradaIdRemover = Console.ReadLine();

                if (int.TryParse(entradaIdRemover, out idremover))
                    break;

                Console.WriteLine("ID inválido. Digite apenas números.");
            }

            repositorio.RemoverRegistro(idremover, usuario.Id);
            Console.WriteLine("Registro removido com sucesso!");
            Console.WriteLine("");
            Thread.Sleep(2000);
            break;

        case "5":
            Console.WriteLine("");
            Console.WriteLine("volte sempre!");
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
} while (opcao != "5");
