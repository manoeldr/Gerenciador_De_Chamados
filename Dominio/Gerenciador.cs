namespace GerenciadorChamados.Dominio
{
    public class Gerenciador
    {
        private List<Chamado> _chamados;

        public Gerenciador(List<Chamado> chamados)
        {
            _chamados = chamados;
        }

        public Chamado AbrirChamado(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição não pode ser vazia");

            int novoId = _chamados.Count > 0
                ? _chamados.Max(c => c.Id) + 1
                : 1;

            var chamado = new Chamado
            {
                Id = novoId,
                Descricao = descricao.Trim(),
                Status = StatusChamado.Aberto,
                DataAbertura = DateTime.Now,
                DataConclusao = null
            };

            _chamados.Add(chamado);
            return chamado;
        }

        public List<Chamado> ListarTodos()
        {
            return _chamados;
        }

        public Chamado? PesquisarId(int id)
        {
            return _chamados.FirstOrDefault(c => c.Id == id);
        }

        public bool ConcluirChamado(int id)
        {
            var chamado = PesquisarId(id);
            if (chamado == null) return false;

            chamado.Concluir();
            return true;
        }

        public int TotalAbertos()
        {
            return _chamados.Count(c => c.EstaAberto());
        }
    }
}