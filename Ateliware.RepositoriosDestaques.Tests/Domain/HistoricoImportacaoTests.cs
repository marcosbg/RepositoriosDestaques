using System;
using Ateliware.RepositoriosDestaques.Domain.Models;
using NUnit.Framework;

namespace Ateliware.RepositoriosDestaques.Tests.Domain
{
    public class HistoricoImportacaoTests
    {
        HistoricoImportacao importacaoOntem;
        HistoricoImportacao importacaoHoje;

        [SetUp]
        public void Setup()
        {
            importacaoHoje = HistoricoImportacao.CriarHistorico(1);
            importacaoOntem = HistoricoImportacao.CriarHistorico(1);
            importacaoOntem.DataImportacao = importacaoOntem.DataImportacao.AddDays(-1);
        }

        [Test]
        public void PrecisaFazerImportacao_DataMenorQueAtual_DeveRetornarVerdadeiro()
        {
            var result = importacaoOntem.PrecisaFazerImportacao();
            Assert.IsTrue(result);
        }

        [Test]
        public void PrecisaFazerImportacao_DataAtual_DeveRetornarFalso()
        {
            var result = importacaoHoje.PrecisaFazerImportacao();
            Assert.IsFalse(result);
        }
    }
}
