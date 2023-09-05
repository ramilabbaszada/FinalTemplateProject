using Core.Aspects.PostSharp.Logging.@abstract;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Serialization;

namespace Core.Aspects.PostSharp.Logging.concrete
{
    [PSerializable]
    public class FileLogAspectAsync: LogAspectAsync
    {

        public override void OnEntry(MethodExecutionArgs args)
        {
            LoggerServiceBase _loggerServiceBase = new FileLogger();
            _loggerServiceBase.Info(GetLogDetail(args));
        }
        public override void OnException(MethodExecutionArgs args)
        {
            LoggerServiceBase _loggerServiceBase = new FileLogger();
            _loggerServiceBase.Error(GetLogDetailWithException(args));
        }
    }
}
