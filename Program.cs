using GerenciadorChamados.Dominio;
using GerenciadorChamados.Persistencia;

// configuração 
string caminho = "chamados.json";
var repositorio = new RepositorioChamados(caminho);

// carrega do arquivo (ou lista vazia se não existir)
var chamados = repositorio.Carregar();
var gerenciador = new Gerenciador(chamados);

Console.WriteLine("=== TESTE MANUAL — GERENCIADOR DE CHAMADOS ===\n");

// cria dois chamados
var c1 = gerenciador.AbrirChamado("Chamado 1");
var c2 = gerenciador.AbrirChamado("Chamado 10");

Console.WriteLine($"Chamado criado: ID {c1.Id} | {c1.Status} | {c1.Descricao}");
Console.WriteLine($"Chamado criado: ID {c2.Id} | {c2.Status} | {c2.Descricao}");

// conclui o primeiro
gerenciador.ConcluirChamado(c1.Id);
Console.WriteLine($"\nChamado {c1.Id} concluído em: {c1.DataConclusao}");

// salva no JSON
repositorio.Salvar(gerenciador.ListarTodos());
Console.WriteLine("\nDados salvos em chamados.json");

// recarrega e imprime pra provar que persistiu
var recarregados = repositorio.Carregar();
Console.WriteLine("\n=== CHAMADOS RECARREGADOS DO ARQUIVO ===");
foreach (var c in recarregados)
{
    Console.WriteLine($"ID: {c.Id} | Status: {c.Status} | Abertura: {c.DataAbertura:dd/MM/yyyy HH:mm} | Descrição: {c.Descricao}");
}

Console.WriteLine("\nPressione qualquer tecla para sair...");
Console.ReadKey();