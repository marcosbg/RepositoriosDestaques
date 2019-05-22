using System;
using System.ComponentModel.DataAnnotations;

namespace Ateliware.RepositoriosDestaques.Domain.Models
{
    public class RepositorioDestaque
    {
        public RepositorioDestaque()
        {
        }

        [Key]
        public int Id { get; set; }
        public long IdExterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public int Avaliacao { get; set; }
        public string Linguagem { get; set; }
        public string Proprietario { get; set; }
        public int QtdContribuidores { get; set; }
        public int QtdCommits { get; set; }
        public int QtdForks { get; set; }
        public int QtdIssues { get; set; }
    }
}
