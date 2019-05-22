using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ateliware.RepositoriosDestaques.Domain.Models
{
    [Table("historicoimportacao")]
    public class HistoricoImportacao
    {
        public HistoricoImportacao()
        {

        }

        public HistoricoImportacao(int qtdRegistros)
        {
            QtdRegistro = qtdRegistros;
            DataImportacao = DateTime.Now;
        }

        public int Id { get; set; }
        public int QtdRegistro { get; set; }
        public DateTime DataImportacao { get; set; }

        public bool PrecisaFazerImportacao()
        {
            return DataImportacao.Date < DateTime.Today;
        }
    }
}
