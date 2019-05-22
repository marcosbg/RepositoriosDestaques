using System;
using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Domain.Models;

namespace Ateliware.RepositoriosDestaques.Domain.Repositories
{
    public interface IDestaquesRepository
    {
        void Inserir(IList<RepositorioDestaque> listaDestaques);
        IList<RepositorioDestaque> ObterDestaques();
        RepositorioDestaque ObterPorId(int id);
        void ExcluirTodos();
    }
}
