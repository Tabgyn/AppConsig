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
           ,'Perfil master dos usu�rios'
           ,'SysAdmin' --Usu�rio quando do pr�prio sistema
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
            ('Vis�o Geral','Dados e estat�sticas','','Index','VisaoGeral','fa fa-dashboard',0,1,1,0,'')
		   ,('Minha conta','','','Conta','Usuario','',0,2,0,0,'')
		   ,('Arquivos','','/0','','','fa fa-archive',0,3,1,0,'')
		   ,('Controles','','/1','','','fa fa-cogs',0,4,1,0,'')
		   ,('Consigna��es','Cadastro de consigna��es','','Index','Consignacao','fa fa-certificate',0,5,1,0,'')
		   ,('Consignat�rias','Cadastro de consignat�rias','','Index','Consignataria','fa fa-bank',0,6,1,0,'')
		   ,('Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',0,7,1,0,'')
		   ,('Relat�rios','','/2','','','fa fa-bar-chart',0,8,1,0,'')
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
            ('Folha','Importa��o de arquivos da folha','','Folha','Arquivo','',@id,1,1,0,'')
		   ,('Lote','Importa��o de arquivos em lote','','Lote','Arquivo','',@id,2,1,0,'')
		   ,('Movimento','Exporta��o de arquivos de movimento','','Movimento','Arquivo','',@id,3,1,0,'')
		   ,('Retorno','Exporta��o de arquivos de retorno','','Retorno','Arquivo','',@id,4,1,0,'')
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
            ('Acessos','Controle de acessos dos usu�rios','','Index','Usuario','',@id,1,1,0,'')
		   ,('Auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,1,0,'')
		   ,('Avisos','Controle de avisos aos usu�rios','','Index','Aviso','',@id,3,1,0,'')
		   ,('Org�os','Controle de org�os dos servidores','','Index','Orgao','',@id,4,1,0,'')
		   ,('Par�metros','Controle de par�metros do portal','','Index','Parametro','',@id,5,1,0,'')
		   ,('Perfis','Controle de perfis dos usu�rios','','Index','Perfil','',@id,6,1,0,'')
		   ,('Servi�os','Controle de tipos de servi�os','','Index','Servico','',@id,7,1,0,'')
GO

--Relat�rios
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Relat�rios')
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
            ('Contratos','Relat�rios de contratos','','Contrato','Relatorio','',@id,1,1,0,'')
		   ,('Servidores','Relat�rios de servidores','','Servidor','Relatorio','',@id,2,1,0,'')
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
            ('Criar acesso','Criar novo acesso de usu�rio','','Criar','Usuario','',@id,1,0,1,'id')
		   ,('Detalhar acesso','Detalhar acesso do usu�rio','','Detalhar','Usuario','',@id,2,0,1,'id')
		   ,('Editar acesso','Editar acesso do usu�rio','','Editar','Usuario','',@id,3,0,1,'id')
		   ,('Excluir acesso','Excluir acesso do usu�rio','','Excluir','Usuario','',@id,4,0,1,'id')
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
            ('Criar aviso','Criar novo aviso aos usu�rios','','Criar','Aviso','',@id,1,0,1,'id')
		   ,('Detalhar aviso','Detalhar aviso aos usu�rios','','Detalhar','Aviso','',@id,2,0,1,'id')
		   ,('Editar aviso','Editar aviso aos usu�rios','','Editar','Aviso','',@id,3,0,1,'id')
		   ,('Excluir aviso','Excluir aviso aos usu�rios','','Excluir','Aviso','',@id,4,0,1,'id')
GO

--Org�os
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Org�os')
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
            ('Criar org�o','Criar novo org�o aos servidores','','Criar','Orgao','',@id,1,0,1,'id')
		   ,('Detalhar org�o','Detalhar org�o aos servidores','','Detalhar','Orgao','',@id,2,0,1,'id')
		   ,('Editar org�o','Editar org�o aos servidores','','Editar','Orgao','',@id,3,0,1,'id')
		   ,('Excluir org�o','Excluir org�o aos servidores','','Excluir','Orgao','',@id,4,0,1,'id')
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
            ('Criar perfil','Criar novo perfil aos usu�rios','','Criar','Perfil','',@id,1,0,1,'id')
		   ,('Detalhar perfil','Detalhar perfil aos usu�rios','','Detalhar','Perfil','',@id,2,0,1,'id')
		   ,('Editar perfil','Editar perfil aos usu�rios','','Editar','Perfil','',@id,3,0,1,'id')
		   ,('Excluir perfil','Excluir perfil aos usu�rios','','Excluir','Perfil','',@id,4,0,1,'id')
GO

--Servi�os
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servi�os')
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
            ('Criar servi�o','Criar novo tipo de servi�o as consigna��es','','Criar','Servico','',@id,1,0,1,'id')
		   ,('Detalhar servi�o','Detalhar tipo de servi�o as consigna��es','','Detalhar','Servico','',@id,2,0,1,'id')
		   ,('Editar servi�o','Editar tipo de servi�o as consigna��es','','Editar','Servico','',@id,3,0,1,'id')
		   ,('Excluir servi�o','Excluir tipo de servi�o as consigna��es','','Excluir','Servico','',@id,4,0,1,'id')
GO

--Consigna��es
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consigna��es')
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
            ('Criar consigna��o','Criar nova consigna��o','','Criar','Consignacao','',@id,1,0,1,'id')
		   ,('Detalhar consigna��o','Detalhar consigna��o','','Detalhar','Consignacao','',@id,2,0,1,'id')
		   ,('Editar consigna��o','Editar consigna��o','','Editar','Consignacao','',@id,3,0,1,'id')
		   ,('Excluir consigna��o','Excluir consigna��o','','Excluir','Consignacao','',@id,4,0,1,'id')
GO

--Consignat�rias
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignat�rias')
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
            ('Criar consignat�ria','Criar nova consignat�ria','','Criar','Consignataria','',@id,1,0,1,'id')
		   ,('Detalhar consignat�ria','Detalhar consignat�ria','','Detalhar','Consignataria','',@id,2,0,1,'id')
		   ,('Editar consignat�ria','Editar consignat�ria','','Editar','Consignataria','',@id,3,0,1,'id')
		   ,('Excluir consignat�ria','Excluir consignat�ria','','Excluir','Consignataria','',@id,4,0,1,'id')
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
