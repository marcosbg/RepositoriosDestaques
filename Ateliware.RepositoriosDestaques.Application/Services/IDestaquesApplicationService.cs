using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Application.ViewModels;

namespace Ateliware.RepositoriosDestaques.Application.Services
{
    public interface IDestaquesApplicationService
    {
        List<RepositoriosListagemViewModel> ListarRepositoriosDestaques();
    }
}