using GerenciadorChamados.Dominio;
using GerenciadorChamados.Persistencia;
using GerenciadorChamados.Console;

string caminho = "chamados.json";
var repositorio = new RepositorioChamados(caminho);
var chamados = repositorio.Carregar();
var gerenciador = new Gerenciador(chamados);
var menu = new MenuConsole(gerenciador, repositorio);

menu.Executar();