using System;
using Ateliware.RepositoriosDestaques.Domain.Models;

namespace Ateliware.RepositoriosDestaques.Domain.Repositories
{
    public interface IHistoricoImportacaoRepository
    {
        void Inserir(HistoricoImportacao historico);
        HistoricoImportacao ObterUltimaImportacao();
    }
}