using System;
namespace Ateliware.RepositoriosDestaques.Application.ViewModels
{
    public class RepositorioDestaqueViewModel
    {
        public RepositorioDestaqueViewModel()
        {
        }

        public int Id { get; set; }
        public long IdExterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string DataCriacao { get; set; }
        public string DataAtualizacao { get; set; }
        public string Avaliacao { get; set; }
        public string Linguagem { get; set; }
        public string Proprietario { get; set; }
        public int QtdContribuidores { get; set; }
        public int QtdCommits { get; set; }
        public int QtdForks { get; set; }
        public int QtdIssues { get; set; }
    }
}
