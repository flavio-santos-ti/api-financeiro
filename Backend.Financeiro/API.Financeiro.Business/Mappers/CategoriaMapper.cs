using API.Financeiro.Domain.Categoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Business.Mappers;

public class CategoriaMapper : AutoMapper.Profile
{
    public CategoriaMapper()
    {
        CreateMap<Categoria, ViewCategoria>();
    }
}
