# API - Fluxo de Caixa Financeiro Básico
<p><i>Repositório para versionamento e documentação básica do projeto Fluxo de Caixa no GitHub.</i></p>

| Autor | Última alteração     |
| :------------- | :------------- |
| Flávio dos Santos   | 02 Maio de 2023 |
## 1 - Sobre o projeto

Este é um repositório para mostrar a implementação e o funcionamento de uma aplicação do tipo Web API onde um comerciante precisa controlar seu fluxo de caixa diário com os lançamentos (débitos e créditos) como também um relatório que disponibilize o saldo consolidado.

<p><i>Por questões de tempo, os testes unitários foram implementados na classe <strong>Categoria Service</strong> e no momento estou implementando o mesmo na classe <strong>Cliente Service</strong>.</i></p>

Nessa primeira versão, para fins didáticos e também como utilizei apenas do meu tempo livre disponível, o relatório será apenas um retorno em JSON, mas em futuro próximo estarei implemetando o retorno de um arquivo PDF representando o relatório.

## 2 - Tecnologias utilizadas



<p display="inline-block">
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403488-185ae58f-8d9f-4893-8516-e2e9d53bdded.png" alt="csharp-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403370-29d0ab19-e406-4581-bc98-838691b4968a.png" alt="fluentvalidation-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403187-df0d20a4-d15b-4f68-b449-450500d1ad49.png" alt="automapper-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403028-b4f6ceec-b1b4-48d9-8fca-4a2adab8227f.png" alt="dapper-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221403962-c5b539cf-1f73-4fbf-8937-507f6956b540.png" alt="postgresql-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221404176-630c9bc1-de1c-4b1b-ad74-8e26bc07b6cb.png" alt="dotnetcore-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221405368-aeaed761-e962-4a3b-bc9c-7a5c7f1543b5.png" alt="docker-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221405737-87bc0545-83ea-49cb-8e6d-d5dbb06f91dc.png" alt="ubuntu-logo"/>
  <img width="48" src="https://user-images.githubusercontent.com/62816438/221621748-124b8a8c-dd15-4d8d-8139-b98f11e36c62.png" alt="ubuntu-logo"/>
</p>

 - C#;
 - Fluent Validation;
 - Auto Mapper;
 - Dapper;
 - PostgreSQL;
 - EF .NET Core 7;
 - Visual Studio 2022;
 - Docker;
 - Linux Ubuntu Server;
 - Swagger.

## 3 - Arquitetura

### 3.1 - Aplicação
<p>
  <img width="480" src="https://user-images.githubusercontent.com/62816438/221408389-4b7a39fe-f81a-4d5a-b7fe-d826ba50ad06.png" alt="arquitetura"/>
</p>


* **Application** 
   * A camada **application** Tem a função de receber todas as requisições http e direcioná-las para a camada **business** para aplicar as validações e regras de negócio.
* **Domain** 
  * É a área de definição dos modelos, entidades, DTOs e Interfaces.
* **Business**
  * Na **business**, concentramos toda a regra de negócio do domínio.
* **Infrastructure**
  * Dividida em duas subcamadas, o Data, onde são realziadas as persistênciasno banco de dados, utilizando ou não algum ORM e a camada **Cross-Cutting**, uma camada destinada a ser utilizada para consumo de API externas.

### 3.2 - Servidores

Nosso comerciante fictício irá consumir nossa API hospedada em um modelo **colocation** em um plano da **AWS**, o **Amazon Lightsail** que poucos conhecem, onde o cliente paga um valor fixo por EC2 contratado semelhante as hospedagens tradicionais. 

O **NGINX** irá atuar como **proxy reverso** e nosso **load-balance** uma vez que ele cumpre bem o papel de balanceamento de carga trazendo mais performance para nossa aplicação.  

<p>
  <img width="900" src="https://user-images.githubusercontent.com/62816438/221556556-14126850-cb9d-4dcd-94bd-ac9a7d97ec52.png" alt="arquitetura"/>
</p>

#### 3.2.1 - Configurando o NGINX 

Abaixo, segue arquivo **nginx.conf** do **NGINX** com a configuração do **proxy reverso** e **load balance**:

```nginx
events{}

http{
    include mime.types;
    default_type 'application/json';
    add_header 'Access-Control-Allow-Origin' '*' always;
    add_header 'Access-Control-Allow-Methods' 'GET, POST, PUT, DELETE, OPTIONS' always;
    add_header 'Content-Type' 'application/json';    
    add_header 'Content-Type' 'application/x-www-form-urlencoded';
    add_header 'Content-Type' 'application/form-data';
    client_max_body_size 200M;
	  

	upstream fluxocaixa {
		server api.financeiro.com.br:81;
		server api.financeiro.com.br:82;
		server api.financeiro.com.br:83;
	}

	server {
		listen 80;
		#server_name app.financeiro.com.br;
		client_max_body_size 200M;
		server_name 127.0.0.1;
        gzip on;

		# ----------------------------------------------------------------------
		# 1 - endpoints da Empresa Financeiro --> http://app.financeiro.com.br/fluxocaixa/
		# ----------------------------------------------------------------------

		# 1.1 - Cadastrar um Cliente
		location /fluxocaixa/api/v1/Cliente {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Cliente/;
		}

		# 1.2 - Listar Clientes
		location /fluxocaixa/api/v1/Cliente {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Cliente/;
		}

		# 1.3 - Cadastrar um Fornecedor
		location /fluxocaixa/api/v1/Fornecedor {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Fornecedor/;
		}

		# 1.4 - Listar Fornecedores
		location /fluxocaixa/api/v1/Fornecedor {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Fornecedor/;
		}

		# 1.5 - Cadastrar uma Categoria
		location /fluxocaixa/api/v1/Categoria {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Categoria/;
		}

		# 1.6 - Listar Categorias 
		location /fluxocaixa/api/v1/Categoria {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Categoria/;
		}

		# 1.7 - Remover uma Categoria
		location /fluxocaixa/api/v1/Categoria {
			proxy_method DELETE;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Categoria/;
		}

		# 1.8 - Situação do Caixa
		location /fluxocaixa/api/v1/Caixa/Situacao {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Caixa/Situacao/;
		}

		# 1.9 - Abrir Caixa
		location /fluxocaixa/api/v1/Caixa/Abrir {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Caixa/Abrir/;
		}

		# 1.10 - Fechar Caixa
		location /fluxocaixa/api/v1/Caixa/Fechar {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Caixa/Fechar/;
		}

		# 1.11 - Registrar um Recebimento
		location /fluxocaixa/api/v1/TituloReceber {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/TituloReceber/;
		}

		# 1.12 - Listar os Recebimentos registrados
		location /fluxocaixa/api/v1/TituloReceber {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/TituloReceber/;
		}

		# 1.13 - Registrar um Pagamento
		location /fluxocaixa/api/v1/TituloPagar {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/TituloPagar/;
		}

		# 1.14 - Listar os Pagamentos registrados
		location /fluxocaixa/api/v1/TituloPagar {
			proxy_method GET;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/TituloPagar/;
		}

		# 1.15 - Emitir um Extrato
		location /fluxocaixa/api/v1/Extrato {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Extrato/;
		}

		# 1.15 - Emitir um Extrato
		location /fluxocaixa/api/v1/Extrato {
			proxy_method POST;
			proxy_set_header content-type "application/json";
            		proxy_set_header Upgrade $http_upgrade;
            		proxy_set_header Host $host;
		    	proxy_cache_bypass $http_upgrade;
            		proxy_set_header Connection 'upgrade';
            		proxy_pass http://fluxocaixa/api/v1/Extrato/;
		}


	}

}
```

Após a configuração do arquivo **nginx.conf**, basta reiniciar o **NGINX** para que as alterações no arquivo conf tenha efeito:

```nginx
sudo service nginx restart
```

#### 3.2.2 - Configurando o DockerFile

Em nosso caso, não vamos utilizar o docker file minimalista, onde iremos disponibilizar a build de nossa API em uma pasta específica para que o docker apenas copie a mesma para dentro do container:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app/data
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY /build /app

ENTRYPOINT ["dotnet", "Financeiro.FluxoCaixa.dll"]
```

#### 3.2.3 - Configurando o Docker-Compose

Agora vamos configurar os 3(três) arquivos onde iremos subir nossas builds e com isso podemos atualizá-las de forma incremental uma a uma sem deixar nossa API indisponível. O primeiro arquivo iremos subir nossa aplicação na porta 81:

```yaml
# -- Lista as imagens Ativas e Inativas
# docker ps -a 
# docker-compose -f docker-compose.yaml up -d
# docker-compose -f docker-compose.yaml down --rmi local
version: "3.8"
# ------- serviços -----------------------------
services:
  api:
    container_name: api-fluxocaixa
    ports:
      - "81:80"
    build:
      context: .
      dockerfile: Dockerfile
    volumes:
       - /mnt/arquivos:/app/data
    restart: always
```

O segundo arquivo, iremos subir nossa aplicação na porta 82:

```yaml
# -- Lista as imagens Ativas e Inativas
# docker ps -a 
# docker-compose -f docker-compose.yaml up -d
# docker-compose -f docker-compose.yaml down --rmi local
version: "3.8"
# ------- serviços -----------------------------
services:
  api:
    container_name: api-fluxocaixa
    ports:
      - "82:80"
    build:
      context: .
      dockerfile: Dockerfile
    volumes:
       - /mnt/arquivos:/app/data
    restart: always
```

E o terceiro e último arquivo, iremos subir nossa aplicação na porta 83:

```yaml
# -- Lista as imagens Ativas e Inativas
# docker ps -a 
# docker-compose -f docker-compose.yaml up -d
# docker-compose -f docker-compose.yaml down --rmi local
version: "3.8"
# ------- serviços -----------------------------
services:
  api:
    container_name: api-fluxocaixa
    ports:
      - "83:80"
    build:
      context: .
      dockerfile: Dockerfile
    volumes:
       - /mnt/arquivos:/app/data
    restart: always
```

Para subir os contaners, basta executar o comando abaixo:

```sh

docker-compose -f docker-compose.yaml up -d

```

E caso queira parar um container específico para realizar atualização:

```sh

docker-compose -f docker-compose.yaml down --rmi local

```

É importante lembrar que deve-se criar uma pasta para cada instância de container que deseja subir, podendo até ser api81, api82 e api83. E dentro dessas pastas os arquivos, Dockerfile, docker-compose.yaml e a pasta build com a release do projeto .NET.
Os comandos de subida e stop dos container devem ser aplicados dentro de cada respectivas pastas/diretórios. 

## 4 - Banco de Dados

O SGBD que estamos utilizando nesse projeto é o [PostgreSQL](https://www.postgresql.org/) e o mesmo estava instalado instalado em um servidor [Linux Ubuntu Server](https://ubuntu.com/download/server), mas nada impede que seja instalado localmente em uma maquina Windows 10 Desktop. 
Para a criação do banco e a a execução dos seus respectivos scripts DDL, utilizamos a ferramenta [Pg Admin 4](https://www.pgadmin.org/download/pgadmin-4-windows/). 

### 4.1 - Modelagem

Na Entidade **Pessoa** para fins didáticos, não estamos utilizando o campo **CPF/CNPJ** para tornar a identificação forte, e no lugar estamos utilizando o **HasNome** para otimizar o índice da tabela. 

<i> Porém eu fiz uma pesquisa rápida em alguns comércios pequenos como **Oficina Mecânica**, e detectei que é comum o estabelecimento comercial não solicitar o número do **CPF/CNPJ** ao cliente.</i>

<p>
  <img width="1050" src="https://user-images.githubusercontent.com/62816438/221549322-7ced45d0-6872-4b73-b1fe-c12867b6def0.png" alt="arquitetura"/>
</p>

<p>
  <img width="1050" src="https://user-images.githubusercontent.com/62816438/221553552-01159273-a58e-49ac-9939-b9fe2cde7ea6.png" alt="arquitetura"/>
</p>


### 4.2 - Scripts

Scripts necessários para a criação do banco de dados esuas respecitvas  tabelas:

#### 4.2.1 Banco de Dados

```sql
CREATE DATABASE financeiro TEMPLATE = template0 LC_CTYPE = "pt_BR.UTF-8" LC_COLLATE = "pt_BR.UTF-8";
```

##### 4.2.2 - Pessoa

```sql
CREATE TABLE IF NOT EXISTS public.pessoa
(
	id BIGSERIAL NOT NULL,
	nome CHARACTER VARYING(50) NOT NULL,
    	hash_nome CHARACTER VARYING(32) NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_pessoa PRIMARY KEY(id)
);

CREATE INDEX ix_pessoa_hash_nome ON public.pessoa( hash_nome );
```

#### 4.2.3 - Cliente

```sql
CREATE TABLE IF NOT EXISTS public.cliente
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP WITH TIME ZONE NOT NULL,
	CONSTRAINT pk_cliente PRIMARY KEY(id), 
	CONSTRAINT fk_cliente_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);
```

#### 4.2.4 - Fornecedor

```sql
CREATE TABLE IF NOT EXISTS public.fornecedor
(
	id BIGSERIAL NOT NULL,
	pessoa_id BIGINT NOT NULL,
	dt_inclusao TIMESTAMP NOT NULL,
	CONSTRAINT pk_fornecedor PRIMARY KEY(id), 
	CONSTRAINT fk_fornecedor_pessoa FOREIGN KEY (pessoa_id) REFERENCES public.pessoa(id)
);
```

#### 4.2.5 - Categoria

```sql
CREATE TABLE IF NOT EXISTS public.categoria
(
    	id SERIAL NOT NULL,
    	nome CHARACTER VARYING(50) NOT NULL,
    	tipo CHAR(1) NOT NULL,
	CONSTRAINT pk_categoria PRIMARY KEY(id) 
);

COMMENT ON TABLE public.categoria IS 'Categoria de Títulos. Tipo: E = Entrada e S = Saída';
```

#### 4.2.6 - Extrato
```sql
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
```

#### 4.2.7 - Saldos Diários

```sql
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
```

#### 4.2.8 - Títulos a Pagar

```sql
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
```

#### 4.2.9 - Títulos a Receber
```SQL
CREATE TABLE IF NOT EXISTS public.titulo_receber
(
    	id BIGSERIAL NOT NULL,
    	categoria_id INT NOT NULL,
    	cliente_id BIGINT DEFAULT NULL,
    	descricao CHARACTER VARYING(50) NOT NULL,
    	valor_real DECIMAL NOT NULL,
    	dt_real TIMESTAMP,
    	dt_inclusao TIMESTAMP NOT NULL,
    	extrato_id BIGINT NOT NULL,
    	CONSTRAINT pk_titulo_receber PRIMARY KEY(id), 
    	CONSTRAINT fk_titulo_receber_categoria FOREIGN KEY (categoria_id) REFERENCES public.categoria(id),
    	CONSTRAINT fk_titulo_receber_cliente FOREIGN KEY (cliente_id) REFERENCES public.cliente(id),
    	CONSTRAINT fk_titulo_receber_extrato FOREIGN KEY (extrato_id) REFERENCES public.extrato(id)
);
```

## 5 - Aplicação

Para baixar a API, execute o git clone do projeto:

```sh

git clone https://github.com/flavio-santos-ti/API-Financeiro-Fluxo-Caixa.git

```
Para executar o projeto localmente, abra o Visual Studio 2022 e acesse a solução localizado na pasta **API-Financeiro-Fluxo-Caixa\Financeiro.FluxoCaixa** arquivo **Financeiro.FluxoCaixa.sln**. Em seguida e execute-o e o swagger será aberto com toda a documentação da API.

<p>
  <img width="1050" src="https://user-images.githubusercontent.com/62816438/221620131-521dfa97-ff40-4fb4-b1d2-b570533eb1a4.png" alt="arquitetura"/>
</p>

<p>
  <img width="1050" src="https://user-images.githubusercontent.com/62816438/221620896-672e5c2e-6efc-403a-b66b-722af31ea347.png" alt="arquitetura"/>
</p>












