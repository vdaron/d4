using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Core.Kernel
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IServiceProvider validator)
        {
            _validator = validator.GetService<IValidator<TRequest>>();
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            _validator?.ValidateAndThrow(request); // Check out the other methods for more advanced handling of validation errors 
            return next();
        }
    }

}