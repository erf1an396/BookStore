using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest , TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;
        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request , RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
              var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

                Type type = typeof(TResponse);
                MethodInfo failMethotOfApiResult = type.GetMethod("Fail");
                if (failMethotOfApiResult != null)
                {
                    if( ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        var tar  = Activator.CreateInstance(type);

                        object[] arg = { "این فیلد به دلیل وابستگی قابل حذف نمی باشد" };

                        failMethotOfApiResult.Invoke(tar, arg);

                        return (TResponse)Convert.ChangeType(tar, typeof(TResponse));


                    }

                    var target = Activator.CreateInstance(type);

                    object[] args = { "خطا رخ داده است" };
                    failMethotOfApiResult.Invoke(target, args);

                    return (TResponse)Convert.ChangeType(target, typeof(TResponse));
                }
                throw;
            }
        }
    }
}
