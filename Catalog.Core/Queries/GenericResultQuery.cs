using System.Collections.Generic;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Queries
{
    public class GenericResultQuery :
        IResultQuery
    {
        public GenericResultQuery() { }
        public GenericResultQuery(int pagIndex, int pagSize, long count, object data)
        {
            PagIndex = pagIndex;
            PagSize = pagSize;
            Count = count;
            Data = data;
        }

        public int PagIndex { get; set; }
        public int PagSize { get; set; }
        public long Count { get; set; }
        public object Data { get; private set; }
    }
}