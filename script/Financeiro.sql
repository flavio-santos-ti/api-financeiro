-- Database PostgreSQL versão 10

CREATE DATABASE financeiro
TEMPLATE=template0
LC_CTYPE="Portuguese_Brazil.1252"
LC_COLLATE="Portuguese_Brazil.1252";


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

-- Categoria
CREATE TABLE IF NOT EXISTS public.categoria
(
    id SERIAL NOT NULL,
    nome CHARACTER VARYING(50) NOT NULL,
    tipo CHAR(1) NOT NULL,
	CONSTRAINT pk_categoria PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Categoria de Títulos. Tipo: E = Entrada e S = Saída';

--

CREATE TABLE IF NOT EXISTS public.extrato 
(
    id BIGSERIAL NOT NULL,
    tipo CHAR(1) NOT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor DECIMAL NOT NULL,
    saldo DECIMAL NOT NULL,
    valor_relatorio DECIMAL NOT NULL,
    dt_extrato TIMESTAMP NOT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_extrato PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Extrato - Tipo: D = Débito e C = Crédito';

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

-- Última Posição


-- Titulos a Pagar
CREATE TABLE IF NOT EXISTS public.titulo_pagar
(
    id BIGSERIAL NOT NULL,
    categoria_id INT NOT NULL,
    fornecedor_id BIGINT DEFAULT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor_real DECIMAL NOT NULL,
    dt_real TIMESTAMP,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
	CONSTRAINT pk_titulo_pagar PRIMARY KEY(id), 
	CONSTRAINT fk_titulo_pagar_categoria FOREIGN KEY (categoria_id) REFERENCES public.categoria(id),
	CONSTRAINT fk_titulo_pagar_fornecedor FOREIGN KEY (fornecedor_id) REFERENCES public.fornecedor(id),
	CONSTRAINT fk_titulo_pagar_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);

-- Titulos a Pagar
CREATE TABLE IF NOT EXISTS public.titulo_receber
(
    id BIGSERIAL NOT NULL,
    categoria_id INT NOT NULL,
    descricao CHARACTER VARYING(50) NOT NULL,
    valor_real DECIMAL NOT NULL,
    dt_real TIMESTAMP,
    cliente_id BIGINT DEFAULT NULL,
    dt_inclusao TIMESTAMP NOT NULL,
    extrato_id BIGINT NOT NULL,
	CONSTRAINT pk_titulo_receber PRIMARY KEY(id), 
	CONSTRAINT fk_titulo_receber_categoria FOREIGN KEY (categoria_id) REFERENCES public.categoria(id),
	CONSTRAINT fk_titulo_receber_cliente FOREIGN KEY (cliente_id) REFERENCES public.cliente(id),
	CONSTRAINT fk_titulo_receber_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);

