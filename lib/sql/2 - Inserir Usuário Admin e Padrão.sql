BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR USUARIO MASTER
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Admin')
INSERT INTO [dbo].[Usuario]
           ([Nome]
           ,[Sobrenome]
           ,[Email]
           ,[NomeDeUsuario]
           ,[Senha]
           ,[EhAdministrador]
           ,[PerfilId]
           ,[CriadoPor]
           ,[CriadoEm]
           ,[AtualizadoPor]
           ,[AtualizadoEm]
           ,[Excluido]
           ,[Bloqueado])
     VALUES
           ('Administrador'
           ,''
           ,'admin@appconsig.com.br'
		   ,'admin@appconsig.com.br'
		   ,'9999:1VCja1oXqOrMxIp7BPZxqZLRBUfFQBa2:zlB+uCANJVV+p8nyMaLi0QsRYbwL+Hgv' --123
           ,1
		   ,@id
           ,'SysAdmin'
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0
		   ,0)
GO

--INSERIR USUARIO PADRAO
DECLARE @id bigint;
SET @id = (SELECT [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Padr�o')
INSERT INTO [dbo].[Usuario]
           ([Nome]
           ,[Sobrenome]
           ,[Email]
           ,[NomeDeUsuario]
           ,[Senha]
           ,[EhAdministrador]
           ,[PerfilId]
           ,[CriadoPor]
           ,[CriadoEm]
           ,[AtualizadoPor]
           ,[AtualizadoEm]
           ,[Excluido]
           ,[Bloqueado])
     VALUES
           ('Usu�rio'
           ,''
           ,'usuario@appconsig.com.br'
		   ,'usuario@appconsig.com.br'
		   ,'9999:1VCja1oXqOrMxIp7BPZxqZLRBUfFQBa2:zlB+uCANJVV+p8nyMaLi0QsRYbwL+Hgv' --123
           ,0
		   ,@id
           ,'SysAdmin'
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0
		   ,0)
GO