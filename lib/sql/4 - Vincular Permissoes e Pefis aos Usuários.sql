BEGIN TRAN
--ROLLBACK
--COMMIT

USE [AppConsig]
GO

--ADICIONAR PERMISSOES AO PERFIL ADMIN
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Admin')
INSERT INTO [dbo].[PerfilPermissao]
           ([PerfilId]
           ,[PermissaoId])
       ((SELECT @id, [Id] FROM [dbo].[Permissao]))
GO

--ADICONAR PERFIL ADMIN AO USUARIO ADMIN
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Admin')
UPDATE [dbo].[Usuario]
   SET [PerfilId] = @id
 WHERE [Nome] = 'Administrador'
GO

--ADICIONAR PERMISSOES AO PERFIL PADRAO
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Padrão')
INSERT INTO [dbo].[PerfilPermissao]
           ([PerfilId]
           ,[PermissaoId])
       ((SELECT @id, [Id] FROM [dbo].[Permissao] WHERE [Padrao] = 1))
GO

--ADICONAR PERFIL PADRAO AO USUARIO PADRAO
DECLARE @id bigint;
SET @id = (SELECT Top 1 [Id] FROM [dbo].[Perfil] WHERE [Nome] = 'Padrão')
UPDATE [dbo].[Usuario]
   SET [PerfilId] = @id
 WHERE [Nome] = 'Usuário'
GO
