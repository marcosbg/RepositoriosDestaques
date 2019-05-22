using System;
using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Domain.Models;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using Ateliware.RepositoriosDestaques.Domain.Services;

namespace Ateliware.RepositoriosDestaques.DomainServices
{
    public class ImportadorGitHubService : IImportadorGitHubService
    {
        private readonly IDestaquesRepository _destaquesRepository;
        private readonly IHistoricoImportacaoRepository _historicoImportacaoRepository;

        public ImportadorGitHubService( IDestaquesRepository destaquesRepository,
                                        IHistoricoImportacaoRepository historicoImportacaoRepository)
        {
            _destaquesRepository = destaquesRepository;
            _historicoImportacaoRepository = historicoImportacaoRepository;
        }

        public void ImportarRepositoriosDestaques()
        {
            var client = new Octokit.GitHubClient(new Octokit.ProductHeaderValue("marcosbg"));

            // Initialize a new instance of the SearchRepositoriesRequest class
            var request = new Octokit.SearchRepositoriesRequest
            {
                Language = Octokit.Language.CSharp,
                Stars = Octokit.Range.GreaterThan(1000),
                SortField = Octokit.RepoSearchSort.Stars,
                Order = Octokit.SortDirection.Descending
            };

            var result = client.Search.SearchRepo(request).Result;

            _destaquesRepository.ExcluirTodos();

            var listaDestaques = new List<RepositorioDestaque>();

            foreach (var item in result.Items)
            {
                listaDestaques.Add(new RepositorioDestaque
                {
                    IdExterno = item.Id,
                    Nome = item.Name,
                    Descricao = item.Description,
                    Linguagem = item.Language,
                    Avaliacao = item.StargazersCount,
                    DataAtualizacao = item.UpdatedAt.DateTime,
                    DataCriacao = item.CreatedAt.DateTime,
                    Proprietario = item.Owner.Name,
                    QtdContribuidores = item.SubscribersCount,
                    QtdForks = item.ForksCount,
                    QtdIssues = item.OpenIssuesCount
                });
            }

            _destaquesRepository.Inserir(listaDestaques);

            var novaImportacao = new HistoricoImportacao(result.Items.Count);

            _historicoImportacaoRepository.Inserir(novaImportacao);
        }
    }
}
