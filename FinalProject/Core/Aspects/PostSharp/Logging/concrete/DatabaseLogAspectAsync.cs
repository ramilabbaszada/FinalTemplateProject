using Core.Aspects.PostSharp.Logging.@abstract;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.PostSharp.Logging.concrete
{
    [PSerializable]
    public class DatabaseLogAspectAsync:LogAspectAsync
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            LoggerServiceBase _loggerServiceBase = new DatabaseLogger();
            _loggerServiceBase.Info(GetLogDetail(args));
        }
        public override void OnException(MethodExecutionArgs args)
        {
            LoggerServiceBase _loggerServiceBase = new DatabaseLogger();
            _loggerServiceBase.Error(GetLogDetailWithException(args));
        }
    }
}
