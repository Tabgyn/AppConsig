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
            ('Visão Geral','Dados e estatísticas','','Index','VisaoGeral','fa fa-dashboard',0,1,1)
		   ,('Arquivos','','/0','','','fa fa-archive',0,2,1)
		   ,('Controles','','/1','','','fa fa-cogs',0,3,1)
		   ,('Consignações','Cadastro de Consignações','','Index','Consignacao','fa fa-certificate',0,4,1)
		   ,('Consignatárias','Cadastro de Consignatárias','','Index','Consignataria','fa fa-bank',0,5,1)
		   ,('Contratos','Contratos e propostas','','Index','Contrato','fa fa-money',0,6,1)
		   ,('Relatórios','','/2','','','fa fa-bar-chart',0,7,1)
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
            ('Folha','Importação de arquivos da folha','','Folha','Arquivo','',@id,1,1)
		   ,('Lote','Importação de arquivos em lote','','Lote','Arquivo','',@id,2,1)
		   ,('Movimento','Exportação de arquivos de movimento','','Movimento','Arquivo','',@id,3,1)
		   ,('Retorno','Exportação de arquivos de retorno','','Retorno','Arquivo','',@id,4,1)
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
            ('Acessos','Controle de acessos dos usuários','','Index','Usuario','',@id,1,1)
		   ,('Auditoria','Controle de auditoria','','Index','Auditoria','',@id,2,1)
		   ,('Avisos','Controle de avisos aos usuários','','Index','Aviso','',@id,3,1)
		   ,('Orgãos','Controle de orgãos dos servidores','','Index','Orgao','',@id,4,1)
		   ,('Parâmetros','Controle de parâmetros do portal','','Index','Parametro','',@id,5,1)
		   ,('Perfis','Controle de perfis dos usuários','','Index','Perfil','',@id,6,1)
		   ,('Serviços','Controle de tipos de serviços','','Index','Servico','',@id,7,1)
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
           ,[VisivelNoMenu])
     VALUES
            ('Contratos','Relatórios de contratos','','Contrato','Relatorio','',@id,1,1)
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