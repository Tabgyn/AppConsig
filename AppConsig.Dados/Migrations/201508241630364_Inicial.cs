namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        SessionId = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        Acao = c.String(),
                        Controle = c.String(),
                        NomeTabela = c.String(),
                        RegistroId = c.Long(nullable: false),
                        ValorOriginal = c.String(),
                        ValorNovo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Aviso",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Texto = c.String(nullable: false, maxLength: 256),
                        CriadoPor = c.String(maxLength: 256),
                        DataCriacao = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(maxLength: 256),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Descricao = c.String(maxLength: 256),
                        CriadoPor = c.String(maxLength: 256),
                        DataCriacao = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(maxLength: 256),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Descricao = c.String(nullable: false, maxLength: 256),
                        Url = c.String(maxLength: 256),
                        Action = c.String(maxLength: 256),
                        Controller = c.String(maxLength: 256),
                        Icone = c.String(maxLength: 256),
                        ParenteId = c.Long(nullable: false),
                        Ordem = c.Int(nullable: false),
                        Padrao = c.Boolean(nullable: false),
                        MostrarNoMenu = c.Boolean(nullable: false),
                        Crud = c.Boolean(nullable: false),
                        Atributos = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Sobrenome = c.String(maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        Senha = c.String(nullable: false),
                        Foto = c.String(),
                        Facebook = c.String(maxLength: 256),
                        Twitter = c.String(maxLength: 256),
                        Telefone = c.String(maxLength: 256),
                        Celular = c.String(maxLength: 256),
                        Endereco = c.String(maxLength: 256),
                        EnderecoComplemento = c.String(maxLength: 256),
                        Admin = c.Boolean(nullable: false),
                        PerfilId = c.Long(nullable: false),
                        CriadoPor = c.String(maxLength: 256),
                        DataCriacao = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(maxLength: 256),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Perfil", t => t.PerfilId, cascadeDelete: true)
                .Index(t => t.PerfilId);
            
            CreateTable(
                "dbo.PerfilPermissao",
                c => new
                    {
                        PerfilId = c.Long(nullable: false),
                        PermissaoId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.PerfilId, t.PermissaoId })
                .ForeignKey("dbo.Perfil", t => t.PerfilId, cascadeDelete: true)
                .ForeignKey("dbo.Permissao", t => t.PermissaoId, cascadeDelete: true)
                .Index(t => t.PerfilId)
                .Index(t => t.PermissaoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.PerfilPermissao", "PermissaoId", "dbo.Permissao");
            DropForeignKey("dbo.PerfilPermissao", "PerfilId", "dbo.Perfil");
            DropIndex("dbo.PerfilPermissao", new[] { "PermissaoId" });
            DropIndex("dbo.PerfilPermissao", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropTable("dbo.PerfilPermissao");
            DropTable("dbo.Usuario");
            DropTable("dbo.Permissao");
            DropTable("dbo.Perfil");
            DropTable("dbo.Aviso");
            DropTable("dbo.Auditoria");
        }
    }
}
