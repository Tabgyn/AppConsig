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
            (NEWID(),'Visão Geral','Dados e estatísticas','','Index','VisaoGeral','fa fa-dashboard',NULL,1,1,1,0,'')
		   ,(NEWID(),'Minha conta','','','Conta','Usuario','',NULL,2,1,0,0,'')
		   ,(NEWID(),'Arquivos','','/0','','','fa fa-archive',NULL,3,0,1,0,'')
		   ,(NEWID(),'Controles','','/1','','','fa fa-cogs',NULL,4,0,1,0,'')
		   ,(NEWID(),'Consignações','Cadastro de consignações','','Index','Consignacao','fa fa-certificate',NULL,5,0,1,0,'')
		   ,(NEWID(),'Consignatárias','Cadastro de consignatárias','','Index','Consignataria','fa fa-bank',NULL,6,0,1,0,'')
		   ,(NEWID(),'Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',NULL,7,0,1,0,'')
		   ,(NEWID(),'Relatórios','','/2','','','fa fa-bar-chart',NULL,8,0,1,0,'')
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
            (NEWID(),'Folha','Importação de arquivos da folha','','Folha','Arquivo','',@id,1,0,1,0,'')
		   ,(NEWID(),'Lote','Importação de arquivos em lote','','Lote','Arquivo','',@id,2,0,1,0,'')
		   ,(NEWID(),'Movimento','Exportação de arquivos de movimento','','Movimento','Arquivo','',@id,3,0,1,0,'')
		   ,(NEWID(),'Retorno','Exportação de arquivos de retorno','','Retorno','Arquivo','',@id,4,0,1,0,'')
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
            (NEWID(),'Acessos','Controle de acessos dos usuários','','Index','Usuario','',@id,1,0,1,0,'')
		   ,(NEWID(),'Trilhas de auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,0,1,0,'')
		   ,(NEWID(),'Avisos','Controle de avisos aos usuários','','Index','Aviso','',@id,3,0,1,0,'')
		   ,(NEWID(),'Orgãos','Controle de orgãos dos servidores','','Index','Orgao','',@id,4,0,1,0,'')
		   ,(NEWID(),'Parâmetros','Controle de parâmetros do portal','','Index','Parametro','',@id,5,0,1,0,'')
		   ,(NEWID(),'Perfis','Controle de perfis dos usuários','','Index','Perfil','',@id,6,0,1,0,'')
		   ,(NEWID(),'Serviços','Controle de tipos de serviços','','Index','Servico','',@id,7,0,1,0,'')
GO

--Relatórios
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Relatórios')
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
            (NEWID(),'Contratos','Relatórios de contratos','','Contrato','Relatorio','',@id,1,0,1,0,'')
		   ,(NEWID(),'Servidores','Relatórios de servidores','','Servidor','Relatorio','',@id,2,0,1,0,'')
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
            (NEWID(),'Criar acesso','Criar novo acesso de usuário','','Criar','Usuario','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar acesso','Detalhar acesso do usuário','','Detalhar','Usuario','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar acesso','Editar acesso do usuário','','Editar','Usuario','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir acesso','Excluir acesso do usuário','','Excluir','Usuario','',@id,4,0,0,1,'id')
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
            (NEWID(),'Criar aviso','Criar novo aviso aos usuários','','Criar','Aviso','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar aviso','Detalhar aviso aos usuários','','Detalhar','Aviso','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar aviso','Editar aviso aos usuários','','Editar','Aviso','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir aviso','Excluir aviso aos usuários','','Excluir','Aviso','',@id,4,0,0,1,'id')
GO

--Orgãos
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Orgãos')
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
            (NEWID(),'Criar orgão','Criar novo orgão aos servidores','','Criar','Orgao','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar orgão','Detalhar orgão aos servidores','','Detalhar','Orgao','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar orgão','Editar orgão aos servidores','','Editar','Orgao','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir orgão','Excluir orgão aos servidores','','Excluir','Orgao','',@id,4,0,0,1,'id')
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
            (NEWID(),'Criar perfil','Criar novo perfil aos usuários','','Criar','Perfil','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar perfil','Detalhar perfil aos usuários','','Detalhar','Perfil','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar perfil','Editar perfil aos usuários','','Editar','Perfil','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir perfil','Excluir perfil aos usuários','','Excluir','Perfil','',@id,4,0,0,1,'id')
GO

--Serviços
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Serviços')
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
            (NEWID(),'Criar serviço','Criar novo tipo de serviço as consignações','','Criar','Servico','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar serviço','Detalhar tipo de serviço as consignações','','Detalhar','Servico','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar serviço','Editar tipo de serviço as consignações','','Editar','Servico','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir serviço','Excluir tipo de serviço as consignações','','Excluir','Servico','',@id,4,0,0,1,'id')
GO

--Consignações
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignações')
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
            (NEWID(),'Criar consignação','Criar nova consignação','','Criar','Consignacao','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar consignação','Detalhar consignação','','Detalhar','Consignacao','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar consignação','Editar consignação','','Editar','Consignacao','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir consignação','Excluir consignação','','Excluir','Consignacao','',@id,4,0,0,1,'id')
GO

--Consignatárias
DECLARE @id uniqueidentifier;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignatárias')
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
            (NEWID(),'Criar consignatária','Criar nova consignatária','','Criar','Consignataria','',@id,1,0,0,1,'id')
		   ,(NEWID(),'Detalhar consignatária','Detalhar consignatária','','Detalhar','Consignataria','',@id,2,0,0,1,'id')
		   ,(NEWID(),'Editar consignatária','Editar consignatária','','Editar','Consignataria','',@id,3,0,0,1,'id')
		   ,(NEWID(),'Excluir consignatária','Excluir consignatária','','Excluir','Consignataria','',@id,4,0,0,1,'id')
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