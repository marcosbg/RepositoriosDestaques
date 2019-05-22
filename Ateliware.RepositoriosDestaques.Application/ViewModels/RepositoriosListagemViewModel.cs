using System;
namespace Ateliware.RepositoriosDestaques.Application.ViewModels
{
    public class RepositoriosListagemViewModel
    {
        public RepositoriosListagemViewModel()
        {
        }

        public int Id { get; set; }
        public long IdExterno { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Linguagem { get; set; }
        public string Avaliacao { get; set; }
        public string UltimaAtualicao { get; set; }
    }
}
