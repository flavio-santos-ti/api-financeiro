using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Result;

public class ServiceResult
{
    public bool Successed { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
    public long ResultId { get; set; }
}
