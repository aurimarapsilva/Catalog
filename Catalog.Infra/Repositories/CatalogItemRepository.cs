using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;
using Catalog.Infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly StoreDataContext _context;

        public bool CreateProductAsync(CatalogItem product)
        {
            // salva o produto
            _context.Entry<CatalogItem>(product).State = EntityState.Added;
            // executa no database e retorna false em caso de erro
            return SaveDatabase().Result;
        }

        public async Task<IResultQuery> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0)
        {
            // counta o numero de registros dentro da schema item
            var totalItems = await _context.Item
                .AsNoTracking()
                .LongCountAsync();

            // seleciona uma lista de itens de agordo com a paginação fornecida ordenando por nome em forma alfabetica 
            var itemsOnPage = await _context.Item
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna a classe com os dados obtidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public async Task<CatalogItem> ItemByIdAsync(int id)
        {
            // valida se o id informado e menor ou igual a zero caso positivo retorna nulo
            if (id <= 0)
                return null;

            // busca no database um produto com o id forneccido
            var item = await _context.Item
                .AsNoTracking()
                .FirstOrDefaultAsync(CatalogItemQuery.GetById(id: id));

            // valida se foi encontrado algum produto se negativo retorna nulo
            if (item == null)
                return null;

            // caso foi localizado o produto retorna o produto
            return item;
        }

        public async Task<IResultQuery> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            // retorna todos os item para a variavel
            var root = (IQueryable<CatalogItem>)_context.Item;

            // valida se o id da marca foi preenchido caso positivo cria um where no select ao databse
            if (catalogBrandId.HasValue)
                root = root
                    .AsNoTracking()
                    .Where(CatalogItemQuery.GetByCatalogBrandId(catalogBrandId: (int)catalogBrandId));

            // atraves desse select é registrado o numero de registros 
            var totalItems = await root
                .AsNoTracking()
                .LongCountAsync();

            // faz paginação no select
            var itemsOnPage = await root
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna uma classe com os dados recebidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public async Task<IResultQuery> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            // retorna todos os item para a variavel
            var root = (IQueryable<CatalogItem>)_context.Item;

            // cria um where para o select constando o id do tipo do produto
            root = root
                .AsNoTracking()
                .Where(CatalogItemQuery.GetByCatalogTypeId(catalogTypeId: catalogTypeId));

            // valida se o id da marca foi preenchido caso positivo cria um where no select ao databse
            if (catalogBrandId.HasValue)
                root = root
                    .AsNoTracking()
                    .Where(CatalogItemQuery.GetByCatalogBrandId(catalogBrandId: (int)catalogBrandId));

            // atraves desse select é registrado o numero de registros 
            var totalItems = await root
                .AsNoTracking()
                .LongCountAsync();

            // faz paginação no select
            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna uma classe com os dados recebidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public async Task<IResultQuery> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0)
        {
            // retorna todos os item para a variavel
            var root = (IQueryable<CatalogItem>)_context.Item;

            // valida se o id da marca foi preenchido caso positivo cria um where no select ao databse
            if (catalogTypeId.HasValue)
                root = root
                    .AsNoTracking()
                    .Where(CatalogItemQuery.GetByCatalogTypeId(catalogTypeId: (int)catalogTypeId));

            // atraves desse select é registrado o numero de registros 
            var totalItems = await root
                .AsNoTracking()
                .LongCountAsync();

            // faz paginação no select
            var itemsOnPage = await root
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna uma classe com os dados recebidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public async Task<IResultQuery> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0)
        {
            // conta o numero de registro no schema de produtos que começa com o nome
            var totalItems = await _context.Item
                .AsNoTracking()
                .Where(CatalogItemQuery.GetByName(name))
                .LongCountAsync();

            // seleciona todos os registro que começa com o nome
            var itemsOnPage = await _context.Item
                .AsNoTracking()
                .Where(CatalogItemQuery.GetByName(name))
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna uma classe com os dados recebidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false)
        {
            // salva o novos registros do produto editado
            _context.Entry<CatalogItem>(product).State = EntityState.Modified;
            // executa no database e retorna false em caso de erro
            return SaveDatabase().Result;
        }

        private async Task<bool> SaveDatabase()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}