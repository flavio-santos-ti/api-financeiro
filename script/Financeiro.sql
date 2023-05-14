-- Database PostgreSQL versão 10

CREATE DATABASE financeiro
TEMPLATE=template0
LC_CTYPE="Portuguese_Brazil.1252"
LC_COLLATE="Portuguese_Brazil.1252";


-- 1) Categoria : Primeiro cadastro a ser realizado

CREATE TABLE IF NOT EXISTS public.categoria
(
    id BIGSERIAL NOT NULL,
    nome CHARACTER VARYING(50) NOT NULL,
    tipo CHAR(1) NOT NULL,
    CONSTRAINT pk_categoria PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Categoria Tipo: E = Entrada, S = Saída, I = Saldo Inicial e F = Saldo Final';

INSERT INTO public.categoria 
(
    nome,
    tipo
)
VALUES
(
    'Entradas',
    'E'
);

INSERT INTO public.categoria 
(
    nome,
    tipo
)
VALUES
(
    'Saídas',
    'S'
);

INSERT INTO public.categoria 
(
    nome,
    tipo
)
VALUES
(
    'Saldo Inicial',
    'I'
);

INSERT INTO public.categoria 
(
    nome,
    tipo
)
VALUES
(
    'Saldo Final',
    'F'
);

-- 1) Pessoa

CREATE TABLE IF NOT EXISTS public.pessoa
(
	id BIGSERIAL NOT NULL,
	nome CHARACTER VARYING(50) NOT NULL,
    hash_nome CHARACTER VARYING(32) NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_pessoa PRIMARY KEY(id)
);

CREATE INDEX ix_pessoa_hash_nome ON public.pessoa( hash_nome );

-- 2) Cliente

CREATE TABLE IF NOT EXISTS public.cliente
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_cliente PRIMARY KEY(id), 
	CONSTRAINT fk_cliente_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);

-- 3) Fornecedor

CREATE TABLE IF NOT EXISTS public.fornecedor
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_fornecedor PRIMARY KEY(id), 
	CONSTRAINT fk_fornecedor_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);


/*
  Extratos    
  
  Como definimos um minúsculo escopo de registro de entradas e saídas financeiras, 
  resolvi adicionar o fornecedor e cliente na própria entidade de extrato, utilizando
  o sua herança de entidade pessoa.
*/

CREATE TABLE IF NOT EXISTS public.extrato 
(
    id BIGSERIAL NOT NULL,
    categoria_id BIGINT NOT NULL,
    pessoa_id BIGINT NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor DECIMAL NOT NULL,
    saldo DECIMAL NOT NULL,
    valor_relatorio DECIMAL NOT NULL,
    dt_extrato TIMESTAMP NOT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_extrato PRIMARY KEY(id), 
    CONSTRAINT fk_extrato_categoria FOREIGN KEY (categoria_id) REFERENCES public.pessoa(id),
    CONSTRAINT fk_extrato_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);

COMMENT ON TABLE public.categoria IS 'Extrato - Tipo: D = Débito, C = Crédito e S = Saldo';

-- Saldos díarios

CREATE TABLE IF NOT EXISTS public.saldo_diario 
(
    id BIGSERIAL NOT NULL,
    dt_saldo TIMESTAMP NOT NULL,
    tipo CHAR(1) NOT NULL,
    valor DECIMAL NOT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
	CONSTRAINT pk_saldo_diario PRIMARY KEY(id), 
	CONSTRAINT fk_sado_diario_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);

CREATE INDEX ix_saldo_diario_periodo ON public.saldo_diario( dt_saldo, tipo );

COMMENT ON TABLE public.saldo_diario IS 'Extrato - Tipo: I = Inicial e F = Final';



