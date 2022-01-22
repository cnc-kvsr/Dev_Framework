using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.CrossCuttingConcerns.Logging
{
    public class LogDetail
    {
        public string FullName { get; set; } //Namespace'deki sınıf
        public string MethodName { get; set; }  //Sınıftaki metodun adı
        public List<LogParameter> Parameters { get; set; }  //Log Parametreleri
    }
}
