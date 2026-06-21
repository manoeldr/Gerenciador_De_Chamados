using GerenciadorChamados.Dominio;
using GerenciadorChamados.Persistencia;

namespace GerenciadorChamados.Console
{
    public class MenuConsole
    {
        private readonly Gerenciador _gerenciador;
        private readonly RepositorioChamados _repositorio;

        public MenuConsole(Gerenciador gerenciador, RepositorioChamados repositorio)
        {
            _gerenciador = gerenciador;
            _repositorio = repositorio;
        }

        public void Executar()
        {
            int opcao;
            do
            {
                ExibirMenu();
                opcao = LerOpcao();

                if (opcao == 1) ExibirNovoChamado();
                else if (opcao == 2) ExibirLista();
                else if (opcao != 0)
                {
                    System.Console.WriteLine("\nOpção inválida. Tente novamente.");
                    AguardarTecla();
                }

            } while (opcao != 0);

            _repositorio.Salvar(_gerenciador.ListarTodos());
        }

        // ################## TELA 1 - EXIBIR MENU ################## 
        private void ExibirMenu()
        {
            System.Console.Clear();
            System.Console.WriteLine("┌──────────────────────────────────────────────┐");
            System.Console.WriteLine("│         GERENCIADOR DE CHAMADOS              │");
            System.Console.WriteLine("│            === MENU PRINCIPAL ===            │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│   [1] Abrir novo chamado                     │");
            System.Console.WriteLine("│   [2] Consultar chamados                     │");
            System.Console.WriteLine("│   [0] Sair                                   │");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            System.Console.WriteLine("│   Total: "+_gerenciador.ListarTodos().Count + "  |  Abertos: " + _gerenciador.TotalAbertos() + "                    │");
            System.Console.WriteLine("│                                              │");
            System.Console.Write("│   Escolha uma opção: ");
        }

        // ################## TELA 2 - EXIBIR NOVO CHAMADO ################## 
        private void ExibirNovoChamado()
        {
            System.Console.Clear();
            System.Console.WriteLine("┌──────────────────────────────────────────────┐");
            System.Console.WriteLine("│         GERENCIADOR DE CHAMADOS              │");
            System.Console.WriteLine("│              === NOVO CHAMADO ===            │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│  Descreva o problema ou solicitação:         │");
            System.Console.Write("│  > ");

            string? descricao = System.Console.ReadLine();

            if (string.IsNullOrWhiteSpace(descricao))
            {
                System.Console.WriteLine("│                                              │");
                System.Console.WriteLine("│  Erro: descrição não pode ser vazia.         │");
                System.Console.WriteLine("└──────────────────────────────────────────────┘");
                AguardarTecla();
                return;
            }

            var chamado = _gerenciador.AbrirChamado(descricao);
            _repositorio.Salvar(_gerenciador.ListarTodos());

            System.Console.Clear();
            System.Console.WriteLine("┌──────────────────────────────────────────────┐");
            System.Console.WriteLine("│         GERENCIADOR DE CHAMADOS              │");
            System.Console.WriteLine("│              === NOVO CHAMADO ===            │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│  Chamado criado com sucesso!                 │");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│  ID...........: " + chamado.Id);
            System.Console.WriteLine("│  Status.......: " + chamado.Status);
            System.Console.WriteLine("│  Abertura.....: " + chamado.DataAbertura.ToString("dd/MM/yyyy HH:mm"));
            System.Console.WriteLine("│  Descrição....: " + chamado.Descricao);
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            AguardarTecla();
        }

        // ################## TELA 3 - EXIBIR LISTA ################## 
        private void ExibirLista()
        {
            System.Console.Clear();
            System.Console.WriteLine("┌────────────────────────────────────────────────────────────────┐");
            System.Console.WriteLine("│                 GERENCIADOR DE CHAMADOS                        │");
            System.Console.WriteLine("│                 === CONSULTA DE CHAMADOS ===                   │");
            System.Console.WriteLine("├────────────────────────────────────────────────────────────────┤");

            var lista = _gerenciador.ListarTodos();

            if (lista.Count == 0)
            {
                System.Console.WriteLine("│                                                            │");
                System.Console.WriteLine("│  Nenhum chamado cadastrado.                                │");
                System.Console.WriteLine("│                                                            │");
                System.Console.WriteLine("│  [0] Voltar ao menu                                        │");
                System.Console.WriteLine("└────────────────────────────────────────────────────────────┘");
                AguardarTecla();
                return;
            }

            System.Console.WriteLine("│                                                                │");
            System.Console.WriteLine("│  ID  │ Status    │ Abertura   │ Descrição                      │");
            System.Console.WriteLine("│  ----+-----------+------------+--------------------------------│");

            foreach (var c in lista)
            {
                string trecho = c.Descricao.Length > 27
                    ? c.Descricao.Substring(0, 27) + "..."
                    : c.Descricao.PadRight(30);
                string status = c.Status.ToString().PadRight(9);
                string idStr = c.Id.ToString().PadRight(4);
                string data = c.DataAbertura.ToString("dd/MM/yyyy");
                string linha = "│  " + idStr + "│ " + status + " │ " + data + " │ " + trecho + " │";
                System.Console.WriteLine(linha);
            }

            System.Console.WriteLine("│                                                                │");
            System.Console.WriteLine("├────────────────────────────────────────────────────────────────┤");
            System.Console.WriteLine("│  Digite o ID para ver detalhes (0 = voltar)                    │");
            System.Console.Write("│  > ");

            string? entrada = System.Console.ReadLine();

            if (!int.TryParse(entrada, out int id) || id == 0)
                return;

            ExibirDetalhe(id);
        }

        // ################## TELA 4 - EXIBIR DETALHES ################## 
        private void ExibirDetalhe(int id)
        {
            System.Console.Clear();

            var chamado = _gerenciador.PesquisarId(id);

            if (chamado == null)
            {
                System.Console.WriteLine("┌──────────────────────────────────────────────┐");
                System.Console.WriteLine("│  Chamado com ID " + id + " não encontrado.           │");
                System.Console.WriteLine("└──────────────────────────────────────────────┘");
                AguardarTecla();
                return;
            }

            string conclusao = chamado.DataConclusao.HasValue
                ? chamado.DataConclusao.Value.ToString("dd/MM/yyyy HH:mm")
                : "—";

            System.Console.WriteLine("┌──────────────────────────────────────────────┐");
            System.Console.WriteLine("│         GERENCIADOR DE CHAMADOS              │");
            System.Console.WriteLine("│          === DETALHE DO CHAMADO ===          │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│  ID...........: " + chamado.Id);
            System.Console.WriteLine("│  Status.......: " + chamado.Status);
            System.Console.WriteLine("│  Abertura.....: " + chamado.DataAbertura.ToString("dd/MM/yyyy HH:mm"));
            System.Console.WriteLine("│  Conclusão....: " + conclusao);
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("│  Descrição:                                  │");
            System.Console.WriteLine("│  " + chamado.Descricao);
            System.Console.WriteLine("│                                              │");
            System.Console.WriteLine("├──────────────────────────────────────────────┤");

            if (chamado.EstaAberto())
            {
                System.Console.WriteLine("│  [1] Concluir chamado                        │");
                System.Console.WriteLine("│  [0] Voltar                                  │");
                System.Console.WriteLine("│                                              │");
                System.Console.Write("│  Escolha uma opção: ");

                string? opcao = System.Console.ReadLine();
                if (opcao == "1")
                {
                    _gerenciador.ConcluirChamado(chamado.Id);
                    _repositorio.Salvar(_gerenciador.ListarTodos());
                    System.Console.WriteLine("\n│  Chamado concluído com sucesso!              │");
                    System.Console.WriteLine("└──────────────────────────────────────────────┘");
                    AguardarTecla();
                }
            }
            else
            {
                System.Console.WriteLine("│  Este chamado já está concluído.             │");
                System.Console.WriteLine("│                                              │");
                System.Console.WriteLine("│  [0] Voltar                                  │");
                System.Console.WriteLine("└──────────────────────────────────────────────┘");
                AguardarTecla();
            }
        }
        
        // ################## LER OPCOES ################## 
        private int LerOpcao()
        {
            string? entrada = System.Console.ReadLine();
            if (int.TryParse(entrada, out int opcao))
                return opcao;
            return -1;
        }

        // ################## AGUARDAR TELA ################## 
        private void AguardarTecla()
        {
            System.Console.WriteLine("\nPressione qualquer tecla para continuar...");
            System.Console.ReadKey();
        }
    }
}