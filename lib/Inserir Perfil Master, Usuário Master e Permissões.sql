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
           ,'SysAdmin'
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO

--INSERIR USUARIO MASTER
INSERT INTO [dbo].[Usuario]
           ([Login]
           ,[Senha]
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
           ('admin@appconsig.com.br'
           ,'9999:1VCja1oXqOrMxIp7BPZxqZLRBUfFQBa2:zlB+uCANJVV+p8nyMaLi0QsRYbwL+Hgv' --123
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
           ,[VisivelNoMenu])
     VALUES
            ('Vis�o Geral','Dados e estat�sticas','','Index','VisaoGeral','fa fa-dashboard',0,1,1)
		   ,('Arquivos','','/0','','','fa fa-archive',0,2,1)
		   ,('Controles','','/1','','','fa fa-cogs',0,3,1)
		   ,('Consigna��es','Cadastro de Consigna��es','','Index','Consignacao','fa fa-certificate',0,4,1)
		   ,('Consignat�rias','Cadastro de Consignat�rias','','Index','Consignataria','fa fa-bank',0,5,1)
		   ,('Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',0,6,1)
		   ,('Relat�rios','','/2','','','fa fa-bar-chart',0,7,1)
		   ,('Servidores','Cadastro de Servidores','','Index','Servidor','fa fa-users',0,8,1)
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
           ,[VisivelNoMenu])
     VALUES
            ('Folha','Importa��o de arquivos da folha','','Folha','Arquivo','',@id,1,1)
		   ,('Lote','Importa��o de arquivos em lote','','Lote','Arquivo','',@id,2,1)
		   ,('Movimento','Exporta��o de arquivos de movimento','','Movimento','Arquivo','',@id,3,1)
		   ,('Retorno','Exporta��o de arquivos de retorno','','Retorno','Arquivo','',@id,4,1)
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
           ,[VisivelNoMenu])
     VALUES
            ('Acessos','Controle de acessos dos usu�rios','','Index','Usuario','',@id,1,1)
		   ,('Auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,1)
		   ,('Avisos','Controle de avisos aos usu�rios','','Index','Aviso','',@id,3,1)
		   ,('Org�os','Controle de org�os dos servidores','','Index','Orgao','',@id,4,1)
		   ,('Par�metros','Controle de par�metros do portal','','Index','Parametro','',@id,5,1)
		   ,('Perfis','Controle de perfis dos usu�rios','','Index','Perfil','',@id,6,1)
		   ,('Servi�os','Controle de tipos de servi�os','','Index','Servico','',@id,7,1)
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
           ,[VisivelNoMenu])
     VALUES
            ('Contratos','Relat�rios de contratos','','Contrato','Relatorio','',@id,1,1)
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