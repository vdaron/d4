using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using d4.Core.Kernel;
using d4.Core.Kernel.Interfaces;
using JetBrains.Annotations;
using MediatR;

namespace d4.Sample.Domain.Projects.Queries
{
    public record GetApprovedProjectsQuery(int? Index=null,int? Max=null,Expression<Func<Project,object>>? OrderBy=null) 
        : BaseQuery<Project>(Index,Max,OrderBy);

    internal class GetApprovedProjectsQueryHandler : IRequestHandler<GetApprovedProjectsQuery, IEnumerable<Project>>
    {
        public class GetApprovedProjectsSpecification : Specification<Project>
        {
            public GetApprovedProjectsSpecification(int? index, int? max, Expression<Func<Project, object>>? orderBy)
            {
                Query.Where(x => x.Approved);
                if (orderBy != null)
                {
                    Query.OrderBy(orderBy!);
                }

                if (index.HasValue)
                {
                    Query.Skip(index.Value);
                }

                if (max.HasValue)
                {
                    Query.Take(max.Value);   
                }

                Query.AsNoTracking();
            }
        }


        private readonly IQueryableStore<Project,string> _queryableStore;

        public GetApprovedProjectsQueryHandler(IQueryableStore<Project,string> queryableStore)
        {
            _queryableStore = queryableStore;
        }
        
        public async Task<IEnumerable<Project>> Handle(GetApprovedProjectsQuery query, CancellationToken cancellationToken)
        {
            return await _queryableStore.ListAsync(new GetApprovedProjectsSpecification(query.Index,query.Max,query.OrderBy));
        }
    }
}