using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace catalog.infra.DataContext
{
    /// <summary>
    /// Realiza a comunicação com o banco de dados
    /// </summary>
    public class DatabaseComunicator
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public DatabaseComunicator(StoreDataContext context)
        {
            _context = context;
        }

        private readonly StoreDataContext _context;

        /// <summary>
        /// Cria uma nova conexão com o banco de dados
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return _context.Database.GetDbConnection();
        }

        /// <summary>
        /// Cria uma linha de comando 
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbCommand GetCommand(IDbConnection connection)
        {
            var command = connection.CreateCommand();
            return command;
        }

        /// <summary>
        /// Executa um reader 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IDataReader GetReader(IDbCommand command)
        {
            var reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// Cria parametros para a transação executada
        ///</summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void GetParameters(IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Realiza a execução de uma stored procedure no banco de dados
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameterSQL"></param>
        /// <returns></returns>
        public string GetResponseDatabase(IDbConnection connection, IDbTransaction transaction, string procedureName, Dictionary<string, object> parameterSQL = null)
        {
            if (string.IsNullOrEmpty(procedureName))
                throw new ArgumentException("Não foi informado o nome da procedure que sera executada");

            using (var command = GetCommand(connection))
            {
                command.CommandText = procedureName;
                command.CommandType = CommandType.StoredProcedure;
                command.Transaction = transaction;

                if (transaction != null)
                {
                    foreach (KeyValuePair<string, object> item in parameterSQL)
                    {
                        GetParameters(command, item.Key, item.Value);
                    }
                }

                var reader = GetReader(command);
                if (reader.Read())
                    return reader.ToString();
            }

            return null;
        }

        /// <summary>
        /// Executa uma transação no banco de dados conforme a procedure desejada
        /// </summary>
        /// <param name="proceduceName"></param>
        /// <param name="parameterSQL"></param>
        /// <returns></returns>
        public string GetResponse(string proceduceName, Dictionary<string, object> parameterSQL = null)
        {
            try
            {
                var connection = GetConnection();
                var responseDatabase = new TransactionSession(connection).Execute((transaction) =>
                {
                    return GetResponseDatabase(connection, transaction, proceduceName, parameterSQL);
                });
                connection.Dispose();
                return responseDatabase;
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Erro:  {e}");
                return null;
            }
            
        }
    }
}
