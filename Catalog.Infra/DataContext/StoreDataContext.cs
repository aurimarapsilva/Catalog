using Microsoft.EntityFrameworkCore;

namespace catalog.infra.DataContext
{
    /// <summary>
    /// Contexto do banco de dados
    /// </summary>
    public class StoreDataContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base (options) { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        public StoreDataContext() { }
    }
}