using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {

            if (!typeof(LoggerServiceBase).IsAssignableFrom(loggerService))
            {
                throw new System.Exception("AspectMessages.WrongLoggerType");
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        protected override void OnException(IInvocation invocation, Exception e)
        {
            _loggerServiceBase.Error(GetLogDetailWithException(invocation, e));
        }

        private LogDetailWithException GetLogDetailWithException(IInvocation invocation, Exception e)
        {
            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = GetLogParameters(invocation),
                DateAndTime = DateTime.UtcNow,
                ExceptionMessage = e.Message+e.StackTrace
            };
            return logDetailWithException;
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = GetLogParameters(invocation),
                DateAndTime = DateTime.UtcNow
            };
            return logDetail;
        }

        private List<LogParameter> GetLogParameters(IInvocation invocation)
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
            return logParameters;
        }
    }
}
