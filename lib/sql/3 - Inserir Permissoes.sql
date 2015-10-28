BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR PERMISSOES PAI
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Vis�o Geral','Dados e informa��es gerais','','Index','VisaoGeral','fa fa-dashboard',0,1,1,1,0,'')
		   ,('Minha conta','','','Conta','Usuario','',0,2,1,0,0,'')
		   ,('Arquivos','','/0','','','fa fa-archive',0,3,0,1,0,'')
		   ,('Controles','','/1','','','fa fa-cogs',0,4,0,1,0,'')
		   ,('Consigna��es','Cadastro de consigna��es','','Index','Consignacao','fa fa-certificate',0,5,0,1,0,'')
		   ,('Consignat�rias','Cadastro de consignat�rias','','Index','Consignataria','fa fa-bank',0,6,0,1,0,'')
		   ,('Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',0,7,0,1,0,'')
		   ,('Relat�rios','','/2','','','fa fa-bar-chart',0,8,0,1,0,'')
		   ,('Servidores','Cadastro de servidores','','Index','Servidor','fa fa-users',0,9,0,1,0,'')
GO

--INSERIR PERMISSOES FILHO

--Arquivos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Arquivos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Folha','Importa��o de arquivos da folha','','Folha','Arquivo','',@id,1,0,1,0,'')
		   ,('Lote','Importa��o de arquivos em lote','','Lote','Arquivo','',@id,2,0,1,0,'')
		   ,('Movimento','Exporta��o de arquivos de movimento','','Movimento','Arquivo','',@id,3,0,1,0,'')
		   ,('Retorno','Exporta��o de arquivos de retorno','','Retorno','Arquivo','',@id,4,0,1,0,'')
GO

--Controles
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Controles')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Acessos','Controle de acessos dos usu�rios','','Index','Usuario','',@id,1,0,1,0,'')
		   ,('Trilhas de auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,0,1,0,'')
		   ,('Avisos','Controle de avisos aos usu�rios','','Index','Aviso','',@id,3,0,1,0,'')
		   ,('Departamentos','Controle de departamentos dos servidores','','Index','Departamento','',@id,4,0,1,0,'')
		   ,('Par�metros','Controle de par�metros do portal','','Index','Parametro','',@id,5,0,1,0,'')
		   ,('Perfis','Controle de perfis dos usu�rios','','Index','Perfil','',@id,6,0,1,0,'')
		   ,('Servi�os','Controle de tipos de servi�os','','Index','Servico','',@id,7,0,1,0,'')
GO

--Relat�rios
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Relat�rios')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Contratos','Relat�rios de contratos','','Contrato','Relatorio','',@id,1,0,1,0,'')
		   ,('Servidores','Relat�rios de servidores','','Servidor','Relatorio','',@id,2,0,1,0,'')
GO

--ADICIONAR CRUD

--Acessos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Acessos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar acesso','Criar novo acesso de usu�rio','','Criar','Usuario','',@id,1,0,0,1,'id')
		   ,('Detalhar acesso','Detalhar acesso do usu�rio','','Detalhar','Usuario','',@id,2,0,0,1,'id')
		   ,('Editar acesso','Editar acesso do usu�rio','','Editar','Usuario','',@id,3,0,0,1,'id')
		   ,('Excluir acesso','Excluir acesso do usu�rio','','Excluir','Usuario','',@id,4,0,0,1,'id')
GO

--Avisos
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Avisos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar aviso','Criar novo aviso aos usu�rios','','Criar','Aviso','',@id,1,0,0,1,'id')
		   ,('Detalhar aviso','Detalhar aviso dos usu�rios','','Detalhar','Aviso','',@id,2,0,0,1,'id')
		   ,('Editar aviso','Editar aviso dos usu�rios','','Editar','Aviso','',@id,3,0,0,1,'id')
		   ,('Excluir aviso','Excluir aviso dos usu�rios','','Excluir','Aviso','',@id,4,0,0,1,'id')
GO

--Org�os
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Departamentos')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar departamento','Criar novo departamento aos servidores','','Criar','Departamento','',@id,1,0,0,1,'id')
		   ,('Detalhar departamento','Detalhar departamento dos servidores','','Detalhar','Departamento','',@id,2,0,0,1,'id')
		   ,('Editar departamento','Editar departamento dos servidores','','Editar','Departamento','',@id,3,0,0,1,'id')
		   ,('Excluir departamento','Excluir departamento dos servidores','','Excluir','Departamento','',@id,4,0,0,1,'id')
GO

--Perfis
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Perfis')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar perfil','Criar novo perfil aos usu�rios','','Criar','Perfil','',@id,1,0,0,1,'id')
		   ,('Detalhar perfil','Detalhar perfil dos usu�rios','','Detalhar','Perfil','',@id,2,0,0,1,'id')
		   ,('Editar perfil','Editar perfil dos usu�rios','','Editar','Perfil','',@id,3,0,0,1,'id')
		   ,('Excluir perfil','Excluir perfil dos usu�rios','','Excluir','Perfil','',@id,4,0,0,1,'id')
GO

--Servi�os
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servi�os')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar servi�o','Criar novo tipo de servi�o as consigna��es','','Criar','Servico','',@id,1,0,0,1,'id')
		   ,('Detalhar servi�o','Detalhar tipo de servi�o das consigna��es','','Detalhar','Servico','',@id,2,0,0,1,'id')
		   ,('Editar servi�o','Editar tipo de servi�o das consigna��es','','Editar','Servico','',@id,3,0,0,1,'id')
		   ,('Excluir servi�o','Excluir tipo de servi�o das consigna��es','','Excluir','Servico','',@id,4,0,0,1,'id')
GO

--Consigna��es
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consigna��es')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar consigna��o','Criar nova consigna��o','','Criar','Consignacao','',@id,1,0,0,1,'id')
		   ,('Detalhar consigna��o','Detalhar consigna��o','','Detalhar','Consignacao','',@id,2,0,0,1,'id')
		   ,('Editar consigna��o','Editar consigna��o','','Editar','Consignacao','',@id,3,0,0,1,'id')
		   ,('Excluir consigna��o','Excluir consigna��o','','Excluir','Consignacao','',@id,4,0,0,1,'id')
GO

--Consignat�rias
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Consignat�rias')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
            ('Criar consignat�ria','Criar nova consignat�ria','','Criar','Consignataria','',@id,1,0,0,1,'id')
		   ,('Detalhar consignat�ria','Detalhar consignat�ria','','Detalhar','Consignataria','',@id,2,0,0,1,'id')
		   ,('Editar consignat�ria','Editar consignat�ria','','Editar','Consignataria','',@id,3,0,0,1,'id')
		   ,('Excluir consignat�ria','Excluir consignat�ria','','Excluir','Consignataria','',@id,4,0,0,1,'id')
GO

--Servidores
--
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Permissao] WHERE [Nome] = 'Servidores')
INSERT INTO [dbo].[Permissao]
           ([Nome]
           ,[Descricao]
           ,[Url]
           ,[Acao]
           ,[Controle]
           ,[ClasseIcone]
           ,[ParenteId]
           ,[Ordem]
           ,[EhPadrao]
           ,[MostrarNoMenu]
           ,[EhCRUD]
           ,[Atributos])
     VALUES
		    ('Detalhar servidor','Detalhar servidor','','Detalhar','Servidor','',@id,1,0,0,1,'id')
GO