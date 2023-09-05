using Core.CrossCuttingConcerns.Logging;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;

namespace Core.Aspects.PostSharp.Logging.@abstract
{
    [PSerializable]
    public abstract class LogAspectAsync : OnMethodBoundaryAspect
    {
        protected LogDetailWithException GetLogDetailWithException(MethodExecutionArgs args)
        {
            var logDetailWithException = new LogDetailWithException
            {
                MethodName = args.Method.Name,
                LogParameters = GetLogParameters(args),
                DateAndTime = DateTime.UtcNow,
                ExceptionMessage = args.Exception.Message+ args.Exception.StackTrace
            };
            return logDetailWithException;
        }

        protected LogDetail GetLogDetail(MethodExecutionArgs args)
        {
            var logDetail = new LogDetail
            {
                MethodName = args.Method.Name,
                LogParameters = GetLogParameters(args),
                DateAndTime = DateTime.UtcNow
            };
            return logDetail;
        }

        protected List<LogParameter> GetLogParameters(MethodExecutionArgs args)
        {

            var logParameters = new List<LogParameter>();
            for (int i = 0; i < args.Arguments.Count; i++)
            {
                logParameters.Add(new LogParameter
                {

                    Name = args.Method.GetParameters()[i].Name,
                    Value = args.Arguments.GetArgument(i),
                    Type = args.Method.GetParameters()[i].ParameterType.Name
                });
            }
            return logParameters;
        }
    }
}