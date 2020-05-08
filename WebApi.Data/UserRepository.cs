using System;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using IBM.Data.DB2.Core;
using WebApi.Domain.Entities;
using WebApi.Domain.Interfaces;

namespace WebApi.Data
{
    public class UserRepository : IUserRepository
    {
        private DB2Connection _conn;

        public UserRepository(string connectionString)
        {
            try
            {
                _conn = new DB2Connection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUser(string userId, CancellationToken ct = default)
        {
            User returnUser = new User();
            try
            {
                if (!_conn.IsOpen)
                {
                    _conn.Open();
                }

                DB2Transaction trans = _conn.BeginTransaction();
                DB2Command cmd = _conn.CreateCommand();

                try
                {
                    cmd.Transaction = trans;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM USER_TABLE WHERE ID = '{userId}'";

                    using (DB2DataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            await reader.ReadAsync();
                            returnUser.FirstName = reader[0].ToString().Trim();
                            returnUser.MiddleName = reader[1].ToString().Trim();
                            returnUser.LastName = reader[2].ToString().Trim();
                            returnUser.UserId = reader[3].ToString().Trim().Replace(".", "");
                        }
                        else
                        {
                            return new User();
                        }
                    }
                    cmd.Transaction.Commit();

                    return returnUser;
                }
                catch(Exception ex)
                {
                    cmd.Transaction.Rollback();
                    throw ex;
                }

            }
            catch(Exception ex)
            {
                if (_conn.IsOpen)
                {
                    _conn.Close();
                }
                throw ex;
            }
            finally
            {
                _conn.Close();
            }

        }
    }
}
