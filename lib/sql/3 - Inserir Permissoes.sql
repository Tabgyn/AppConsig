BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR PERMISSOES PAI
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Vis�o Geral','Dados e estat�sticas','','Index','VisaoGeral','fa fa-dashboard',NULL,1,1,1,0,'')
		   ,(NEWID(),'Minha conta','','','Conta','Usuario','',NULL,2,1,0,0,'')
		   ,(NEWID(),'Arquivos','','/0','','','fa fa-archive',NULL,3,0,1,0,'')
		   ,(NEWID(),'Controles','','/1','','','fa fa-cogs',NULL,4,0,1,0,'')
		   ,(NEWID(),'Consigna��es','Cadastro de consigna��es','','Index','Consignacao','fa fa-certificate',NULL,5,0,1,0,'')
		   ,(NEWID(),'Consignat�rias','Cadastro de consignat�rias','','Index','Consignataria','fa fa-bank',NULL,6,0,1,0,'')
		   ,(NEWID(),'Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',NULL,7,0,1,0,'')
		   ,(NEWID(),'Relat�rios','','/2','','','fa fa-bar-chart',NULL,8,0,1,0,'')
		   ,(NEWID(),'Servidores','Cadastro de servidores','','Index','Servidor','fa fa-users',NULL,9,0,1,0,'')
GO

--INSERIR PERMISSOES FILHO

--Arquivos
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Arquivos')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Folha','Importa��o de arquivos da folha','','Folha','Arquivo','',@id,1,0,1,0,'')
		   ,(NEWID(),'Lote','Importa��o de arquivos em lote','','Lote','Arquivo','',@id,2,0,1,0,'')
		   ,(NEWID(),'Movimento','Exporta��o de arquivos de movimento','','Movimento','Arquivo','',@id,3,0,1,0,'')
		   ,(NEWID(),'Retorno','Exporta��o de arquivos de retorno','','Retorno','Arquivo','',@id,4,0,1,0,'')
GO

--Controles
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Controles')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Acessos','Controle de acessos dos usu�rios','','Index','Usuario','',@id,1,0,1,0,'')
		   ,(NEWID(),'Trilhas de auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,0,1,0,'')
		   ,(NEWID(),'Avisos','Controle de avisos aos usu�rios','','Index','Aviso','',@id,3,0,1,0,'')
		   ,(NEWID(),'Org�os','Controle de org�os dos servidores','','Index','Orgao','',@id,4,0,1,0,'')
		   ,(NEWID(),'Par�metros','Controle de par�metros do portal','','Index','Parametro','',@id,5,0,1,0,'')
		   ,(NEWID(),'Perfis','Controle de perfis dos usu�rios','','Index','Perfil','',@id,6,0,1,0,'')
		   ,(NEWID(),'Servi�os','Controle de tipos de servi�os','','Index','Servico','',@id,7,0,1,0,'')
GO

--Relat�rios
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Relat�rios')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Contratos','Relat�rios de contratos','','Contrato','Relatorio','',@id,1,0,1,0,'')
		   ,(NEWID(),'Servidores','Relat�rios de servidores','','Servidor','Relatorio','',@id,2,0,1,0,'')
GO

--ADICIONAR CRUD

--Acessos
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Acessos')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar acesso','Criar novo acesso de usu�rio','','Criar','Usuario','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar acesso','Detalhar acesso do usu�rio','','Detalhar','Usuario','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar acesso','Editar acesso do usu�rio','','Editar','Usuario','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir acesso','Excluir acesso do usu�rio','','Excluir','Usuario','',@id,4,0,0,1,'id')
GO

--Avisos
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Avisos')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar aviso','Criar novo aviso aos usu�rios','','Criar','Aviso','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar aviso','Detalhar aviso aos usu�rios','','Detalhar','Aviso','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar aviso','Editar aviso aos usu�rios','','Editar','Aviso','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir aviso','Excluir aviso aos usu�rios','','Excluir','Aviso','',@id,4,0,0,1,'id')
GO

--Org�os
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Org�os')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar org�o','Criar novo org�o aos servidores','','Criar','Orgao','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar org�o','Detalhar org�o aos servidores','','Detalhar','Orgao','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar org�o','Editar org�o aos servidores','','Editar','Orgao','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir org�o','Excluir org�o aos servidores','','Excluir','Orgao','',@id,4,0,0,1,'id')
GO

--Perfis
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Perfis')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar perfil','Criar novo perfil aos usu�rios','','Criar','Perfil','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar perfil','Detalhar perfil aos usu�rios','','Detalhar','Perfil','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar perfil','Editar perfil aos usu�rios','','Editar','Perfil','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir perfil','Excluir perfil aos usu�rios','','Excluir','Perfil','',@id,4,0,0,1,'id')
GO

--Servi�os
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servi�os')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar servi�o','Criar novo tipo de servi�o as consigna��es','','Criar','Servico','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar servi�o','Detalhar tipo de servi�o as consigna��es','','Detalhar','Servico','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar servi�o','Editar tipo de servi�o as consigna��es','','Editar','Servico','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir servi�o','Excluir tipo de servi�o as consigna��es','','Excluir','Servico','',@id,4,0,0,1,'id')
GO

--Consigna��es
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consigna��es')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar consigna��o','Criar nova consigna��o','','Criar','Consignacao','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar consigna��o','Detalhar consigna��o','','Detalhar','Consignacao','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar consigna��o','Editar consigna��o','','Editar','Consignacao','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir consigna��o','Excluir consigna��o','','Excluir','Consignacao','',@id,4,0,0,1,'id')
GO

--Consignat�rias
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignat�rias')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
            (NEWID(),'Criar consignat�ria','Criar nova consignat�ria','','Criar','Consignataria','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar consignat�ria','Detalhar consignat�ria','','Detalhar','Consignataria','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar consignat�ria','Editar consignat�ria','','Editar','Consignataria','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir consignat�ria','Excluir consignat�ria','','Excluir','Consignataria','',@id,4,0,0,1,'id')
GO

--Servidores
--
DECLARE @id uniqueidentifier;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servidores')
INSERT INTO [dbo].[Permissao]
           ([Id]
		   ,[Nome]
           ,[Descricao]
           ,[Url]
           ,[Action]
           ,[Controller]
           ,[Icone]
           ,[ParenteId]
           ,[Ordem]
           ,[Padrao]
           ,[MostrarNoMenu]
           ,[Crud]
           ,[Atributos])
     VALUES
		    (NEWID(),'Detalhar servidor','Detalhar servidor','','Detalhar','Servidor','',@id,1,0,0,1,'id')
GO