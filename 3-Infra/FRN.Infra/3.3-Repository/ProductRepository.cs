using Dapper;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using FRN.Infra._3._1_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FRN.Infra._3._3_Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDomainNotificationHandler _notificator;
        public ProductRepository(FRNContext context,
                                 IConfiguration configuration,
                                 IDomainNotificationHandler notificator) : base(context)
        {
            _configuration = configuration;
            _notificator = notificator;
        }
       
        public IEnumerable<Product> Get()
        {
            StringBuilder errorMessage = new StringBuilder();
            
            try
            {
                using (SqlConnection cnx = new SqlConnection(
                    _context.Database.GetDbConnection().ConnectionString))
                {
                    var dados = cnx.Query<Product>("select * from Product");
                    return dados;
                }
            }
            catch (SqlException Sqlex)
            {
                for (int i = 0; i < Sqlex.Errors.Count; i++)
                {
                    _notificator.Handle(Sqlex.Errors[i].Message);
                }
                return null;
            }
        }

        public void Post(Product product)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"INSERT INTO Product (descricao) VALUES ('{product.descricao}') ";

            using (SqlConnection cnx = new SqlConnection(
                _context.Database.GetDbConnection().ConnectionString))
            {
                cnx.Open();
                var transacao = cnx.BeginTransaction();

                try
                {
                    cnx.Execute(query, transaction: transacao);
                    transacao.Commit();
                }
                catch (SqlException Sqlex)
                {
                    transacao.Rollback();
                    cnx.Close();

                    for (int i = 0; i < Sqlex.Errors.Count; i++)
                    {
                        errorMessage.Append("Mensagem: " + Sqlex.Errors[i].Message);
                    }
                    throw new Exception(errorMessage.ToString());
                }
                catch(Exception ex)
                {
                    transacao.Rollback();
                    cnx.Close();
                    throw new Exception(ex.Message.ToString());
                }
                cnx.Close();
            }
        }

        public void Put(Product product)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"UPDATE Product SET descricao ='{product.descricao}' WHERE id={product.Id}";

            using (SqlConnection cnx = new SqlConnection(
                _context.Database.GetDbConnection().ConnectionString))
            {
                cnx.Open();
                var transacao = cnx.BeginTransaction();

                try
                {
                    cnx.Execute(query, transaction: transacao);
                    transacao.Commit();
                }
                catch (SqlException Sqlex)
                {
                    transacao.Rollback();
                    cnx.Close();

                    for (int i = 0; i < Sqlex.Errors.Count; i++)
                    {
                        errorMessage.Append("Mensagem: " + Sqlex.Errors[i].Message);
                    }
                    throw new Exception(errorMessage.ToString());
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    cnx.Close();
                    throw new Exception(ex.Message.ToString());
                }
                cnx.Close();
            }
        }
    }
}
