using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using ELibrary.Business.CrossCuttingConcerns.Logging;
using ELibrary.Business.CrossCuttingConcerns.Logging.Log4Net;
using ELibrary.Business.Utility.Interceptors;

namespace ELibrary.Business.Aspects.Autofac.Exception
{
    public class ExceptionLogAspect:MethodInterception
    {
        private LoggerService _loggerService;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerService))
            {
                throw new System.Exception("Wrong logger type!");
            }

            _loggerService = (LoggerService)Activator.CreateInstance(loggerService);
        }

        protected override void OnException(IInvocation invocation, System.Exception exception)
        {
            LogDetailWithException logDetailWithException = GetLogDetail(invocation);
            logDetailWithException.ExceptionMessage = exception.Message;
            _loggerService.Error(logDetailWithException);
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters

            };
            return logDetailWithException;
        }
    }
}
