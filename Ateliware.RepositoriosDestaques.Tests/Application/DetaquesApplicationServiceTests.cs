using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Application.Services;
using Ateliware.RepositoriosDestaques.Domain.Models;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using Ateliware.RepositoriosDestaques.Domain.Services;
using Moq;
using NUnit.Framework;

namespace Ateliware.RepositoriosDestaques.Tests.Application
{
    public class DetaquesApplicationServiceTests
    {
        private IDestaquesApplicationService _destaquesApplicationService;
        private Mock<IImportadorGitHubService> _importadorGitHubService;
        private Mock<IHistoricoImportacaoRepository> _historicoImportacaoRepository;
        private Mock<IDestaquesRepository> _destaquesRepository;
        private int _idRepositorio;

        [SetUp]
        public void Setup()
        {
            _idRepositorio = 1;

            _destaquesRepository = new Mock<IDestaquesRepository>(MockBehavior.Loose);
            _importadorGitHubService = new Mock<IImportadorGitHubService>(MockBehavior.Loose);
            _historicoImportacaoRepository = new Mock<IHistoricoImportacaoRepository>(MockBehavior.Loose);

            _importadorGitHubService.Setup(p => p.ImportarRepositoriosDestaques());

            _historicoImportacaoRepository.Setup(p => p.ObterUltimaImportacao()).Returns(HistoricoImportacao.CriarHistorico(1));

            _destaquesRepository.Setup(p => p.ObterPorId(_idRepositorio)).Returns(new RepositorioDestaque
            {
                Id = _idRepositorio,
                IdExterno = 1001,
                Nome = "Teste",
                Descricao = "",
                Linguagem = "C"
            });

            _destaquesApplicationService = new DestaquesApplicationService(
                _importadorGitHubService.Object, 
                _historicoImportacaoRepository.Object, 
                _destaquesRepository.Object); ;
        }

        [Test]
        public void ListarRepositoriosDestaques_NaoEncontraItens_DeveRetornarListaVazia()
        {
            _destaquesRepository.Setup(p => p.ObterDestaques()).Returns(new List<RepositorioDestaque>());

            var result = _destaquesApplicationService.ListarRepositoriosDestaques();

            Assert.IsTrue(result.Count == 0);
        }

        [Test]
        public void ListarRepositoriosDestaques_EncontraItens_DeveRetornarLista()
        {
            _destaquesRepository.Setup(p => p.ObterDestaques()).Returns(new List<RepositorioDestaque>
            {
                new RepositorioDestaque
                {
                    Id = 1,
                    IdExterno = 1001,
                    Nome = "Teste",
                    Descricao = "",
                    Linguagem = "C"
                },
                new RepositorioDestaque
                {
                    Id = 2,
                    IdExterno = 2001,
                    Nome = "Teste 2",
                    Descricao = "",
                    Linguagem = "C"
                }
            });

            var result = _destaquesApplicationService.ListarRepositoriosDestaques();

            Assert.IsTrue(result.Count >= 1);
        }

        [Test]
        public void ObterRepositorioDestaque_NaoEncontraPeloId_DeveRetornarVazio()
        {
            var result = _destaquesApplicationService.ObterRepositorioDestaque(2);
            Assert.IsNull(result);
        }

        [Test]
        public void ObterRepositorioDestaque_EncontraPeloId_DeveRetornarRepositorio()
        {
            var result = _destaquesApplicationService.ObterRepositorioDestaque(_idRepositorio);
            Assert.IsNotNull(result);
        }
    }
}
