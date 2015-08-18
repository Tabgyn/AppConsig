BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR PERFIL MASTER
INSERT INTO [dbo].[Perfil]
           ([Nome]
           ,[Descricao]
           ,[CriadoPor]
           ,[DataCriacao]
           ,[AtualizadoPor]
           ,[DataAtualizacao]
           ,[Excluido])
     VALUES
           ('Master'
           ,'Perfil master dos usuários'
           ,'SysAdmin' --Usuário quando do próprio sistema
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO

--INSERIR USUARIO MASTER
INSERT INTO [dbo].[Usuario]
           ([Senha]
           ,[Nome]
           ,[Sobrenome]
           ,[Email]
           ,[PerfilId]
           ,[CriadoPor]
           ,[DataCriacao]
           ,[AtualizadoPor]
           ,[DataAtualizacao]
           ,[Excluido])
     VALUES
           ('9999:1VCja1oXqOrMxIp7BPZxqZLRBUfFQBa2:zlB+uCANJVV+p8nyMaLi0QsRYbwL+Hgv' --123
           ,'Administrador'
           ,''
           ,'admin@appconsig.com.br'
           ,1
           ,'SysAdmin'
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO

--INSERIR PERMISSOES PAI
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Visão Geral','Dados e estatísticas','','Index','VisaoGeral','fa fa-dashboard',0,1,1,0,'')
		   ,('Minha conta','','','Conta','Usuario','',0,2,0,0,'')
		   ,('Arquivos','','/0','','','fa fa-archive',0,3,1,0,'')
		   ,('Controles','','/1','','','fa fa-cogs',0,4,1,0,'')
		   ,('Consignações','Cadastro de consignações','','Index','Consignacao','fa fa-certificate',0,5,1,0,'')
		   ,('Consignatárias','Cadastro de consignatárias','','Index','Consignataria','fa fa-bank',0,6,1,0,'')
		   ,('Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',0,7,1,0,'')
		   ,('Relatórios','','/2','','','fa fa-bar-chart',0,8,1,0,'')
		   ,('Servidores','Cadastro de servidores','','Index','Servidor','fa fa-users',0,9,1,0,'')
GO

--INSERIR PERMISSOES FILHO

--Arquivos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Arquivos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Folha','Importação de arquivos da folha','','Folha','Arquivo','',@id,1,1,0,'')
		   ,('Lote','Importação de arquivos em lote','','Lote','Arquivo','',@id,2,1,0,'')
		   ,('Movimento','Exportação de arquivos de movimento','','Movimento','Arquivo','',@id,3,1,0,'')
		   ,('Retorno','Exportação de arquivos de retorno','','Retorno','Arquivo','',@id,4,1,0,'')
GO

--Controles
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Controles')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Acessos','Controle de acessos dos usuários','','Index','Usuario','',@id,1,1,0,'')
		   ,('Auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,1,0,'')
		   ,('Avisos','Controle de avisos aos usuários','','Index','Aviso','',@id,3,1,0,'')
		   ,('Orgãos','Controle de orgãos dos servidores','','Index','Orgao','',@id,4,1,0,'')
		   ,('Parâmetros','Controle de parâmetros do portal','','Index','Parametro','',@id,5,1,0,'')
		   ,('Perfis','Controle de perfis dos usuários','','Index','Perfil','',@id,6,1,0,'')
		   ,('Serviços','Controle de tipos de serviços','','Index','Servico','',@id,7,1,0,'')
GO

--Relatórios
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Relatórios')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Contratos','Relatórios de contratos','','Contrato','Relatorio','',@id,1,1,0,'')
		   ,('Servidores','Relatórios de servidores','','Servidor','Relatorio','',@id,2,1,0,'')
GO

--ADICIONAR CRUD

--Acessos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Acessos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar acesso','Criar novo acesso de usuário','','Criar','Usuario','',@id,1,0,1,'id')
		   ,('Detalhar acesso','Detalhar acesso do usuário','','Detalhar','Usuario','',@id,2,0,1,'id')
		   ,('Editar acesso','Editar acesso do usuário','','Editar','Usuario','',@id,3,0,1,'id')
		   ,('Excluir acesso','Excluir acesso do usuário','','Excluir','Usuario','',@id,4,0,1,'id')
GO

--Avisos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Avisos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar aviso','Criar novo aviso aos usuários','','Criar','Aviso','',@id,1,0,1,'id')
		   ,('Detalhar aviso','Detalhar aviso aos usuários','','Detalhar','Aviso','',@id,2,0,1,'id')
		   ,('Editar aviso','Editar aviso aos usuários','','Editar','Aviso','',@id,3,0,1,'id')
		   ,('Excluir aviso','Excluir aviso aos usuários','','Excluir','Aviso','',@id,4,0,1,'id')
GO

--Orgãos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Orgãos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar orgão','Criar novo orgão aos servidores','','Criar','Orgao','',@id,1,0,1,'id')
		   ,('Detalhar orgão','Detalhar orgão aos servidores','','Detalhar','Orgao','',@id,2,0,1,'id')
		   ,('Editar orgão','Editar orgão aos servidores','','Editar','Orgao','',@id,3,0,1,'id')
		   ,('Excluir orgão','Excluir orgão aos servidores','','Excluir','Orgao','',@id,4,0,1,'id')
GO

--Perfis
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Perfis')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar perfil','Criar novo perfil aos usuários','','Criar','Perfil','',@id,1,0,1,'id')
		   ,('Detalhar perfil','Detalhar perfil aos usuários','','Detalhar','Perfil','',@id,2,0,1,'id')
		   ,('Editar perfil','Editar perfil aos usuários','','Editar','Perfil','',@id,3,0,1,'id')
		   ,('Excluir perfil','Excluir perfil aos usuários','','Excluir','Perfil','',@id,4,0,1,'id')
GO

--Serviços
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Serviços')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar serviço','Criar novo tipo de serviço as consignações','','Criar','Servico','',@id,1,0,1,'id')
		   ,('Detalhar serviço','Detalhar tipo de serviço as consignações','','Detalhar','Servico','',@id,2,0,1,'id')
		   ,('Editar serviço','Editar tipo de serviço as consignações','','Editar','Servico','',@id,3,0,1,'id')
		   ,('Excluir serviço','Excluir tipo de serviço as consignações','','Excluir','Servico','',@id,4,0,1,'id')
GO

--Consignações
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignações')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar consignação','Criar nova consignação','','Criar','Consignacao','',@id,1,0,1,'id')
		   ,('Detalhar consignação','Detalhar consignação','','Detalhar','Consignacao','',@id,2,0,1,'id')
		   ,('Editar consignação','Editar consignação','','Editar','Consignacao','',@id,3,0,1,'id')
		   ,('Excluir consignação','Excluir consignação','','Excluir','Consignacao','',@id,4,0,1,'id')
GO

--Consignatárias
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignatárias')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
            ('Criar consignatária','Criar nova consignatária','','Criar','Consignataria','',@id,1,0,1,'id')
		   ,('Detalhar consignatária','Detalhar consignatária','','Detalhar','Consignataria','',@id,2,0,1,'id')
		   ,('Editar consignatária','Editar consignatária','','Editar','Consignataria','',@id,3,0,1,'id')
		   ,('Excluir consignatária','Excluir consignatária','','Excluir','Consignataria','',@id,4,0,1,'id')
GO

--Servidores
--
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servidores')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Visivel]
           ,[IsCrud]
           ,[Atributos])
     VALUES
		    ('Detalhar servidor','Detalhar servidor','','Detalhar','Servidor','',@id,1,0,1,'id')
GO

--ADICIONAR PERMISSOES AO PERFIL MASTER
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Master')
INSERT INTO [dbo].[PerfilPermissao]
           ([PerfilId]
           ,[PermissaoId])
       ((SELECT @id, [Id] FROM [dbo].[Permissao]))
GO

--ADICONAR PERFIL MASTER AO USUARIO MASTER
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Master')
UPDATE [dbo].[Usuario]
   SET [PerfilId] = @id
 WHERE [Nome] = 'Administrador'
GO
