using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using ELibrary.Business.CrossCuttingConcerns.Logging;
using ELibrary.Business.CrossCuttingConcerns.Logging.Log4Net;
using ELibrary.Business.Utility.Interceptors;

namespace ELibrary.Business.Aspects.Autofac.Logging
{
    public class LogAspect:MethodInterception
    {
        private LoggerService _loggerService;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType!=typeof(LoggerService))
            {
                throw new System.Exception("Wrong logger type!");
            }

            _loggerService = (LoggerService)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerService.Info(GetLogDetail(invocation));
        }

        private LogDetails GetLogDetail(IInvocation invocation)
        {
           var logParameters=new List<LogParameter>();
           for (int i = 0; i < invocation.Arguments.Length; i++)
           {
               logParameters.Add(new LogParameter
               {
                   Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                   Value = invocation.Arguments[i],
                   Type = invocation.Arguments[i].GetType().Name
               });
           }
           var logDetails=new LogDetails
           {
               MethodName = invocation.Method.Name,
               Parameters = logParameters

           };
           return logDetails;
        }
    }
}
