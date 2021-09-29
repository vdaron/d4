using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediatR;

namespace d4.Core.Kernel
{
    public record BaseQuery<T>(int? Index=null,int? Max=null,Expression<Func<T,object>>? OrderBy=null) : IRequest<IEnumerable<T>>;
}