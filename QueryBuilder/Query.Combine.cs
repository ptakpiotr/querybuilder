using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlKata
{
    public partial class Query
    {

        public Query Combine(string operation, bool all, Query query)
        {
            if (this.Method != "select" || query.Method != "select")
            {
                throw new InvalidOperationException("Only select queries can be combined.");
            }

            return AddComponent("combine", new Combine
            {
                Query = query,
                Operation = operation,
                All = all,
            });
        }

        public Query Union(Query query, bool all = false)
        {
            return Combine("union", all, query);
        }

        public Query UnionAll(Query query)
        {
            return Union(query, true);
        }

        public Query Union(Func<Query, Query> callback, bool all = false)
        {
            var query = callback.Invoke(new Query());
            return Union(query, all);
        }

        public Query UnionAll(Func<Query, Query> callback)
        {
            return Union(callback, true);
        }

        public Query Except(Query query, bool all = false)
        {
            return Combine("except", all, query);
        }

        public Query ExceptAll(Query query)
        {
            return Except(query, true);
        }

        public Query Except(Func<Query, Query> callback, bool all = false)
        {
            var query = callback.Invoke(new Query());
            return Except(query, all);
        }

        public Query ExceptAll(Func<Query, Query> callback)
        {
            return Except(callback, true);
        }

        public Query Intersect(Query query, bool all = false)
        {
            return Combine("intersect", all, query);
        }

        public Query IntersectAll(Query query)
        {
            return Intersect(query, true);
        }

        public Query Intersect(Func<Query, Query> callback, bool all = false)
        {
            var query = callback.Invoke(new Query());
            return Intersect(query, all);
        }

        public Query IntersectAll(Func<Query, Query> callback)
        {
            return Intersect(callback, true);
        }

    }
}