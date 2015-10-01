BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--INSERIR PERFIL ADMIN
INSERT INTO [dbo].[Perfil]
           ([Id]
		   ,[Nome]
           ,[Descricao]
		   ,[Editavel]
           ,[CriadoPor]
           ,[DataCriacao]
           ,[AtualizadoPor]
           ,[DataAtualizacao]
           ,[Excluido])
     VALUES
           (NEWID()
		   ,'Admin'
           ,'Perfil master dos usu�rios'
		   ,0
           ,'SysAdmin' --Usu�rio quando do pr�prio sistema
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO

--INSERIR PERFIL PADRAO
INSERT INTO [dbo].[Perfil]
           ([Id]
		   ,[Nome]
           ,[Descricao]
		   ,[Editavel]
           ,[CriadoPor]
           ,[DataCriacao]
           ,[AtualizadoPor]
           ,[DataAtualizacao]
           ,[Excluido])
     VALUES
           (NEWID()
		   ,'Padr�o'
           ,'Perfil padr�o dos usu�rios'
		   ,0
           ,'SysAdmin' --Usu�rio quando do pr�prio sistema
           ,GETDATE()
           ,'SysAdmin'
           ,GETDATE()
           ,0)
GO