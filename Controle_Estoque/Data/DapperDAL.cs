using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Controle_Estoque.Data
{
    public class DapperDAL : IDapperDal
    {
        private readonly IConfiguration _config;
        private readonly string _conn;

        public DapperDAL(IConfiguration config)
        {
            _config = config;
            _conn = config.GetConnectionString("default");
        }
        public DbConnection GetConnection()
        {
            return new MySqlConnection(_conn);
        }
        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new MySqlConnection(_conn);
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }
        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new MySqlConnection(_conn);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }
        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new MySqlConnection(_conn);
            return db.Execute(sp, parms, commandType: commandType);
        }
        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new MySqlConnection(_conn);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                    result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                    tran.Commit();
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return result;
#pragma warning restore CS8603 // Possível retorno de referência nula.
        }
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new MySqlConnection(_conn);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
#pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                    result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
#pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
#pragma warning disable CS8603 // Possível retorno de referência nula.
            return result;
#pragma warning restore CS8603 // Possível retorno de referência nula.
        
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

   
    }
}
