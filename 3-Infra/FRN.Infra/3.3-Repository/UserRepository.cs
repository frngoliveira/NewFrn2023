using Dapper;
using FRN.Domain._2._1_Interface;
using FRN.Domain._2._2_Entity;
using FRN.Infra._3._1_Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace FRN.Infra._3._3_Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {

        private readonly IConfiguration _configuration;
        private readonly IDomainNotificationHandler _notificator;
        public UserRepository(FRNContext context,
                                 IConfiguration configuration,
                                 IDomainNotificationHandler notificator) : base(context)
        {
            _configuration = configuration;
            _notificator = notificator;
        }

        public Users Get(Users user)
        {
            //StringBuilder errorMessage = new StringBuilder();
            string query = $@"SELECT * FROM Users WHERE UserName = '{user.UserName}' AND Password = '{user.Password}'";

            try
            {
                using (SqlConnection cnx = new SqlConnection(
                    _context.Database.GetDbConnection().ConnectionString))
                {
                    var dados = cnx.Query<Users>(query).FirstOrDefault();
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

        public IEnumerable<Users> GetAllUser()
        {
            StringBuilder errorMessage = new StringBuilder();

            try
            {
                using (SqlConnection cnx = new SqlConnection(
                    _context.Database.GetDbConnection().ConnectionString))
                {
                    var dados = cnx.Query<Users>("select * from Users");
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

        public IEnumerable<Users> GetAllUserById(int id)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"select * from Users WHERE id = {id}";

            try
            {
                using (SqlConnection cnx = new SqlConnection(
                    _context.Database.GetDbConnection().ConnectionString))
                {
                    var dados = cnx.Query<Users>(query);
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

        public void Post(Users  user)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"INSERT INTO Users (UserName, Password, Role) VALUES ('{user.UserName}', '{user.Password}', '{user.Role}') ";

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

        public void Put(Users user)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"UPDATE Users SET UserName = '{user.UserName}', Password = '{user.Password}', Role = '{user.Role}' WHERE id={user.Id}";

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

        public void Delete(Users user)
        {
            StringBuilder errorMessage = new StringBuilder();
            string query = $@"Delete FROM Users WHERE id={user.Id}";

            //DELETE FROM Users WHERE Id = @UserId

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
