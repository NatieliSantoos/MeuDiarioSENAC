RepositorioDiario repositorio = new RepositorioDiario();


Console.WriteLine("Bem vindo ao seu diario");

string opcao;
do
{
    Console.WriteLine("");
    Console.WriteLine("escolha uma opçao");
    Console.WriteLine("");
    Console.WriteLine("1 = adicionar um novo registro");
    Console.WriteLine("");
    Console.WriteLine("2 = Listar seus Registros");
    Console.WriteLine("");
    Console.WriteLine("3 = Pesquisar seus registros");
    Console.WriteLine("");
    Console.WriteLine("4 = Deletar  registro");
    Console.WriteLine("");
    Console.WriteLine("5 =  SAIR ");
    Console.WriteLine("============================");
    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":

            Registro cadastrar = new Registro();
            Console.WriteLine("");
            Console.WriteLine("Titulo");
            cadastrar.Titulo = Console.ReadLine();
            Console.WriteLine("");

            Console.WriteLine("Abra seu coraçao");
            cadastrar.Conteudo = Console.ReadLine();
            Console.WriteLine("");

            repositorio.CadastrarRegistro(cadastrar.Titulo, cadastrar.Conteudo);

            Console.WriteLine("Registro SALVO com sucesso!");
            break;
        case "2":
            List<Registro> listar = repositorio.ListarRegistros();

            foreach (Registro registro in listar)
            {
                Console.WriteLine($"ID: {registro.IdRegistro}");
                Console.WriteLine($"Título: {registro.Titulo}");
                Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                Console.WriteLine($"Data: {registro.Data}");
                Console.WriteLine("========================");
                Console.WriteLine("");
            }
            break;

        case "3":
    
            Console.Write("Digite o ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            List<Registro> pesquisa = repositorio.ListarRegistros();

            foreach (Registro registro in pesquisa)
            {
                Console.WriteLine("");
                Console.WriteLine($"ID: {registro.IdRegistro}");
                Console.WriteLine($"Título: {registro.Titulo}");
                Console.WriteLine($"Conteúdo: {registro.Conteudo}");
                Console.WriteLine($"Data: {registro.Data}");
                Console.WriteLine("========================");
                Console.WriteLine("");
            }

            repositorio.PesquisarRegistro(id);
            break;

        case "4":
           
            Console.WriteLine("Digite o ID que quer remolver: ");
            int idremover = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("");

            repositorio.RemoverRegistro(idremover);
            break;

        case "5":
            Console.WriteLine("");
            Console.WriteLine("volte sempre!");
            break;
    }
} while (opcao != "5");
