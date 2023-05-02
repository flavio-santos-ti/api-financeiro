using API.Financeiro.Domain.Receber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Mappers;

public class ReceberMapper : AutoMapper.Profile
{
    public ReceberMapper()
    {
        CreateMap<Receber, CreateReceber>().ReverseMap();
    }
}
