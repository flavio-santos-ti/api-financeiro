using API.Financeiro.Domain.Pagar;

namespace API.Financeiro.Business.Mappers;

public class PagarMapper : AutoMapper.Profile
{
    public PagarMapper()
    {
        CreateMap<Pagar, CreatePagar>().ReverseMap();
    }
}
