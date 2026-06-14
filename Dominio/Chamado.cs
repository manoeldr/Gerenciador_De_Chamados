namespace GerenciadorChamados.Dominio
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public StatusChamado Status { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }

        public bool EstaAberto()
        {
            return Status == StatusChamado.Aberto;
        }

        public void Concluir()
        {
            if (!EstaAberto())
                throw new InvalidOperationException("Chamado já concluído");

            Status = StatusChamado.Concluido;
            DataConclusao = DateTime.Now;
        }
    }
}