using System;
using System.Collections.Generic;
using Ateliware.RepositoriosDestaques.Domain.Models;
using Ateliware.RepositoriosDestaques.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper.Contrib.Extensions;
using System.Linq;

namespace Ateliware.RepositoriosDestaques.Repository
{
    public class DestaquesRepository : IDestaquesRepository
    {
        private readonly IConfiguration _configuration;

        public DestaquesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnection()
        {
            var connection = _configuration.GetSection("ConnectionStrings").GetSection("DestaquesConnection").Value;
            return connection;
        }

        public void ExcluirTodos()
        {
            var connectionString = GetConnection();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.DeleteAll<RepositorioDestaque>();
            }
        }

        public void Inserir(IList<RepositorioDestaque> listaDestaques)
        {
            var connectionString = GetConnection();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Insert(listaDestaques);
            }
        }

        public IList<RepositorioDestaque> ObterDestaques()
        {
            var connectionString = GetConnection();
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.GetAll<RepositorioDestaque>().ToList();
            }
        }

        public RepositorioDestaque ObterPorId(int id)
        {
            var connectionString = GetConnection();
            using (var connection = new MySqlConnection(connectionString))
            {
                return connection.Get<RepositorioDestaque>(id);
            }
        }
    }
}
