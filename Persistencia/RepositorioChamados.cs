using System.Text.Json;
using GerenciadorChamados.Dominio;

namespace GerenciadorChamados.Persistencia
{
    public class RepositorioChamados
    {
        private readonly string _caminho;

        public RepositorioChamados(string caminho)
        {
            _caminho = caminho;
        }

        public List<Chamado> Carregar()
        {
            if (!File.Exists(_caminho))
                return new List<Chamado>();

            string json = File.ReadAllText(_caminho);
            return JsonSerializer.Deserialize<List<Chamado>>(json)
                   ?? new List<Chamado>();
        }

        public void Salvar(List<Chamado> chamados)
        {
            var opcoes = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(chamados, opcoes);
            File.WriteAllText(_caminho, json);
        }
    }
}