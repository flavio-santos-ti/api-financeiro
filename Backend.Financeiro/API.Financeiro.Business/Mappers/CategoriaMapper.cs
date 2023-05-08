using API.Financeiro.Domain.Categoria;

namespace API.Financeiro.Business.Mappers;

public class ClienteMapper : AutoMapper.Profile
{
    public ClienteMapper()
    {
        CreateMap<Categoria, ViewCategoria>();
    }
}
