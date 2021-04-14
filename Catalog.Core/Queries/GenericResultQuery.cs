using System.Collections.Generic;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Queries
{
    public class GenericResultQuery<T> where T : IEntity, IResultQuery<T>
    {
        public GenericResultQuery() { }
        public GenericResultQuery(int pagIndex, int pagSize, long count, IEnumerable<T> data)
        {
            PagIndex = pagIndex;
            PagSize = pagSize;
            Count = count;
            Data = data;
        }

        public int PagIndex { get; set; }
        public int PagSize { get; set; }
        public long Count { get; set; }
        public IEnumerable<T> Data { get; private set; }
    }
}