using System;
using System.Linq;
using System.Reflection;
using ELibrary.Business.CrossCuttingConcerns.Logging;
using ELibrary.Business.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace ELibrary.Business.Aspects.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method,TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect:OnMethodBoundaryAspect
    {
        private Type _loggerType;

        public LogAspect(Type type)
        {
            _loggerType = type;
        }

        private LoggerService _loggerService;
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType!=typeof(LoggerService))
            {
               throw new Exception("Logger type is wrong!");
            }

            _loggerService = (LoggerService) Activator.CreateInstance(_loggerType);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                if (!_loggerService.IsInfoEnabled)
                {
                    return;
                }

                var logParameters = args.Method.GetParameters()
                    .Select((p, i) => new LogParameter
                    {
                        Name = p.Name,
                        Type = p.ParameterType.Name,
                        Value = args.Arguments.GetArgument(i)
                    }).ToList();
                var logDetail=new LogDetails
                {
                    FullName = args.Method.DeclaringType==null
                        ?null:args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };
                _loggerService.Info(logDetail);
            }
            catch
            {

            }
            base.OnEntry(args);
        }
    }
}
