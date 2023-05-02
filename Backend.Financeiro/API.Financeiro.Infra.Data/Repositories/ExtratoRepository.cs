using Api.Crud.Infra.Data.Context;
using Api.Crud.Infra.Data.Repositories.Base;
using API.Financeiro.Domain.Exrato;
using API.Financeiro.Domain.Extrato;
using API.Financeiro.Domain.Fornecedor;
using API.Financeiro.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Financeiro.Infra.Data.Repositories;

public class ExtratoRepository : RepositoryBase, IExtratoRepository
{
    public ExtratoRepository(DatabaseContext context) : base(context) { }

    public async Task AddAsync(Extrato dados) => await base.AddAsync<Extrato>(dados);

    public async Task<IEnumerable<ViewExtrato>> GetListarAsync(PeriodoExtrato filtro)
    {
        const string sqlQuery = @"
        SELECT 
          *
        FROM
          (
		    SELECT
              ext.id AS Id,
		      ext.tipo AS Tipo,
		      ext.descricao AS Descricao,
		      ext.valor AS Valor,
		      ext.dt_extrato as DataExtrato,
              tpg.id AS TituloId,
              'Destino: '||psf.nome AS Nome
            FROM
              extrato ext
              LEFT JOIN titulo_pagar tpg ON (tpg.extrato_id = ext.id)    
              LEFT JOIN fornecedor fnc ON (fnc.id = tpg.fornecedor_id)
              LEFT JOIN pessoa psf ON (psf.id = fnc.pessoa_id)
            WHERE
              ext.tipo = 'D'
              AND ext.dt_extrato BETWEEN @DataInicial AND @DataFinal
            UNION 
            SELECT
              ext.id AS Id,
		      ext.tipo AS Tipo,
		      ext.descricao AS Descricao,
		      ext.valor AS Valor,
		      ext.dt_extrato as DataExtrato,
              trc.id AS TituloId,
              'Origem: '||psc.nome AS Nome
            FROM
              extrato ext
              LEFT JOIN titulo_receber trc ON (trc.extrato_id = ext.id)    
              LEFT JOIN cliente cli ON (cli.id = trc.cliente_id)
              LEFT JOIN pessoa psc ON (psc.id = cli.pessoa_id)
            WHERE
              ext.tipo = 'C'
              AND ext.dt_extrato BETWEEN @DataInicial AND @DataFinal
          ) VI
        ORDER BY
          VI.Id
        ";

        return await base.ListarAsync<ViewExtrato>(sqlQuery, filtro);
    }


}
