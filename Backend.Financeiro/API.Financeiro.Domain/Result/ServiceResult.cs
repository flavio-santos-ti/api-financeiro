using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Domain.Result;

public class ServiceResult
{
    public Guid TransactionId { get; set; }
    public string TransactionName { get; set; }
    public DateTime TransactionCreatedAt { get; init; } = DateTime.Now;
    public int Count { get; set; }
    public bool Successed { get; set; }
    public string Message { get; set; }
    public object Data { get; set; }
}
