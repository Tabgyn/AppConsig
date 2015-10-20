BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR PERFIL ADMIN
INSERT INTO [dbo].[Perfil]
           ([Nome]
           ,[Descricao]
           ,[EhEditavel]
           ,[CriadoPor]
           ,[CriadoEm]
           ,[AtualizadoPor]
           ,[AtualizadoEm]
           ,[Excluido])
     VALUES
           ('Admin'
           ,'Perfil master dos usuários'
		   ,0
           ,'SysAdmin' --Usuário quando do próprio sistema
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO

--INSERIR PERFIL PADRAO
INSERT INTO [dbo].[Perfil]
           ([Nome]
           ,[Descricao]
           ,[EhEditavel]
           ,[CriadoPor]
           ,[CriadoEm]
           ,[AtualizadoPor]
           ,[AtualizadoEm]
           ,[Excluido])
     VALUES
           ('Padrão'
           ,'Perfil padrão dos usuários'
		   ,0
           ,'SysAdmin' --Usuário quando do próprio sistema
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO