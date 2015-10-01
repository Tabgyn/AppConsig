namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Usuario = c.String(),
                        DataEvento = c.DateTime(nullable: false),
                        TipoEvento = c.String(nullable: false),
                        NomeEntidade = c.String(nullable: false),
                        RegistroId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalheAuditoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Propriedade = c.String(nullable: false),
                        ValorOriginal = c.String(),
                        ValorNovo = c.String(),
                        AuditoriaId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoria", t => t.AuditoriaId, cascadeDelete: true)
                .Index(t => t.AuditoriaId);
            
            CreateTable(
                "dbo.Aviso",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Texto = c.String(nullable: false, maxLength: 256),
                        CriadoPor = c.String(maxLength: 256),
                        DataCriacao = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(maxLength: 256),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orgao",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Codigo = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(maxLength: 100),
                        SistemaFolhaId = c.Guid(nullable: false),
                        CriadoPor = c.String(maxLength: 256),
                        DataCriacao = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(maxLength: 256),
                        DataAtualizacao = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SistemaFolha", t => t.SistemaFolhaId, cascadeDelete: true)
                .Index(t => t.SistemaFolhaId);
            
            CreateTable(
                "dbo.SistemaFolha",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(maxLength: 256),
                        Editavel = c.Boolean(nullable: false),
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
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Descricao = c.String(nullable: false, maxLength: 256),
                        Url = c.String(maxLength: 256),
                        Action = c.String(maxLength: 256),
                        Controller = c.String(maxLength: 256),
                        Icone = c.String(maxLength: 256),
                        ParenteId = c.Guid(nullable: false),
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
                        Id = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Sobrenome = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        Senha = c.String(nullable: false),
                        Foto = c.String(),
                        Facebook = c.String(maxLength: 100),
                        Twitter = c.String(maxLength: 100),
                        Telefone = c.String(maxLength: 100),
                        Celular = c.String(maxLength: 100),
                        Endereco = c.String(maxLength: 100),
                        EnderecoComplemento = c.String(maxLength: 100),
                        Admin = c.Boolean(nullable: false),
                        PerfilId = c.Guid(nullable: false),
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
                        PerfilId = c.Guid(nullable: false),
                        PermissaoId = c.Guid(nullable: false),
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
            DropForeignKey("dbo.Orgao", "SistemaFolhaId", "dbo.SistemaFolha");
            DropForeignKey("dbo.DetalheAuditoria", "AuditoriaId", "dbo.Auditoria");
            DropIndex("dbo.PerfilPermissao", new[] { "PermissaoId" });
            DropIndex("dbo.PerfilPermissao", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropIndex("dbo.Orgao", new[] { "SistemaFolhaId" });
            DropIndex("dbo.DetalheAuditoria", new[] { "AuditoriaId" });
            DropTable("dbo.PerfilPermissao");
            DropTable("dbo.Usuario");
            DropTable("dbo.Permissao");
            DropTable("dbo.Perfil");
            DropTable("dbo.SistemaFolha");
            DropTable("dbo.Orgao");
            DropTable("dbo.Aviso");
            DropTable("dbo.DetalheAuditoria");
            DropTable("dbo.Auditoria");
        }
    }
}
