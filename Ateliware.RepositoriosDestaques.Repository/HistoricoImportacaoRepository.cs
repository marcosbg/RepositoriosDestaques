using System;
using System.Data;
using Ateliware.RepositoriosDestaques.Domain.Models;
using Dapper.Contrib.Extensions;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using System.Linq;

namespace Ateliware.RepositoriosDestaques.Repository
{
    public class HistoricoImportacaoRepository : IHistoricoImportacaoRepository
    {
        private readonly IConfiguration _configuration;

        public HistoricoImportacaoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DestaquesConnection").Value;
            return connection;
        }

        public void Inserir(HistoricoImportacao historico)
        {
            var connectionString = GetConnection();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Insert(historico);
            }
        }

        public HistoricoImportacao ObterUltimaImportacao()
        {
            var connectionString = GetConnection();
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                var ultimaImportacao = connection.GetAll<HistoricoImportacao>().OrderByDescending(imp => imp.DataImportacao).FirstOrDefault();

                return ultimaImportacao;
            }
        }

    }
}
