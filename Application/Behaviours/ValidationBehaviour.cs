using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    Type type = typeof(TResponse);
                    MethodInfo failMethotOfApiResult = type.GetMethod("Fail");
                    if (failMethotOfApiResult != null)
                    {
                        var target = Activator.CreateInstance(type);

                        foreach (var item in failures.Where(x => !string.IsNullOrEmpty(x.ErrorMessage)))
                        {
                            object[] tempArg = { item.ErrorMessage };
                            failMethotOfApiResult.Invoke(target, tempArg);
                        }

                        return (TResponse)Convert.ChangeType(target, typeof(TResponse));
                    }
                    throw new FluentValidation.ValidationException(failures);
                }
            }
            return await next();
        }


    }
}
