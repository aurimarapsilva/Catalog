using System;
using System.Data;

namespace catalog.infra.DataContext
{
    /// <summary>
    /// Responsável por executar a procedure
    /// </summary>
    public class TransactionSession
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public delegate T Execution<T>(IDbTransaction transaction);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        public delegate void Execution(IDbTransaction transaction);

        private readonly IDbConnection _sqlcon;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sqlcon"></param>
        public TransactionSession(IDbConnection sqlcon)
        {
            _sqlcon = sqlcon;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec"></param>
        /// <returns></returns>
        public T Execute<T>(Execution<T> exec)
        {
            IDbTransaction transaction = _sqlcon.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                T result = exec(transaction);

                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }

                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exec"></param>
        public void Exec<T>(Execution<T> exec)
        {
            IDbTransaction transac = _sqlcon.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                T result = exec(transac);

                transac.Commit();
            }
            catch (Exception ex)
            {
                if (transac != null)
                {
                    transac.Rollback();
                    transac.Dispose();
                }

                Console.WriteLine(ex.Message);
            }
        }
    }
}
