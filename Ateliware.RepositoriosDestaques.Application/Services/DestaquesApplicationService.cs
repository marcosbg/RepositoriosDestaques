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
                    Id = repositorio.Id,
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

        public RepositorioDestaqueViewModel ObterRepositorioDestaque(int id)
        {
            var destaque = _destaquesRepository.ObterPorId(id);

            return new RepositorioDestaqueViewModel
            {
                Id = destaque.Id,
                IdExterno = destaque.IdExterno,
                Nome = destaque.Nome,
                Descricao = destaque.Descricao,
                Linguagem = destaque.Linguagem,
                Avaliacao = destaque.Avaliacao.ToString(),
                DataAtualizacao = destaque.DataAtualizacao.ToString("dd/MM/yyyy hh:mm"),
                DataCriacao = destaque.DataCriacao.ToString("dd/MM/yyyy hh:mm"),
                Proprietario = destaque.Proprietario,
                QtdContribuidores = destaque.QtdContribuidores,
                QtdForks = destaque.QtdForks,
                QtdIssues = destaque.QtdIssues

            };
        }
    }
}
