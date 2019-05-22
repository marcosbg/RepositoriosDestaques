using System;
using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Application.ViewModels;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using Ateliware.RepositoriosDestaques.Domain.Services;

namespace Ateliware.RepositoriosDestaques.Application.Services
{
    public class DestaquesApplicationService : IDestaquesApplicationService
    {
        private readonly IImportadorGitHubService _importadorGitHubService;
        private readonly IHistoricoImportacaoRepository _historicoImportacaoRepository;
        private readonly IDestaquesRepository _destaquesRepository;

        public DestaquesApplicationService(IImportadorGitHubService importadorGitHubService,
                                            IHistoricoImportacaoRepository historicoImportacaoRepository,
                                            IDestaquesRepository destaquesRepository)
        {
           _importadorGitHubService = importadorGitHubService;
           _historicoImportacaoRepository = historicoImportacaoRepository;
           _destaquesRepository = destaquesRepository;
        }

        public List<RepositoriosListagemViewModel> ListarRepositoriosDestaques()
        {
            var retornoViewModel = new List<RepositoriosListagemViewModel>();

            var dataUltimaAtualizacao = _historicoImportacaoRepository.ObterUltimaImportacao();

            if(dataUltimaAtualizacao == null || dataUltimaAtualizacao.PrecisaFazerImportacao())
            {
                _importadorGitHubService.ImportarRepositoriosDestaques();
            }

            var listaRepositorios = _destaquesRepository.ObterDestaques();

            foreach (var repositorio in listaRepositorios)
            {
                retornoViewModel.Add(new RepositoriosListagemViewModel
                {
                    IdExterno = repositorio.IdExterno,
                    Nome = repositorio.Nome,
                    Descricao = repositorio.Descricao,
                    Linguagem = repositorio.Linguagem,
                    Avaliacao = repositorio.Avaliacao.ToString(),
                    UltimaAtualicao = repositorio.DataAtualizacao.ToString("dd/MM/yyyy")
                });
            }

            return retornoViewModel;
        }
    }
}
