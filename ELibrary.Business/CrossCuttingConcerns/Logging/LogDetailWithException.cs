using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Business.CrossCuttingConcerns.Logging
{
    public class LogDetailWithException
    {
        public string ExceptionMessage { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> Parameters { get; set; }
    }
}
