namespace AppConsig.Data.Migrations
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
                        Id = c.Long(nullable: false, identity: true),
                        SessaoId = c.String(nullable: false),
                        Usuario = c.String(nullable: false),
                        EnderecoIP = c.String(nullable: false),
                        Acao = c.String(nullable: false),
                        Controle = c.String(nullable: false),
                        DataEvento = c.DateTime(nullable: false),
                        Dados = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Aviso",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Texto = c.String(nullable: false, maxLength: 256),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CodigoDepartamento = c.Long(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(maxLength: 100),
                        SistemaFolhaId = c.Long(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SistemaFolha", t => t.SistemaFolhaId, cascadeDelete: true)
                .Index(t => t.SistemaFolhaId);
            
            CreateTable(
                "dbo.SistemaFolha",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Descricao = c.String(maxLength: 256),
                        EhEditavel = c.Boolean(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 256),
                        Descricao = c.String(maxLength: 256),
                        Url = c.String(maxLength: 256),
                        Acao = c.String(maxLength: 256),
                        Controle = c.String(maxLength: 256),
                        ClasseIcone = c.String(maxLength: 256),
                        ParenteId = c.Long(),
                        Ordem = c.Int(nullable: false),
                        EhPadrao = c.Boolean(nullable: false),
                        MostrarNoMenu = c.Boolean(nullable: false),
                        EhCRUD = c.Boolean(nullable: false),
                        Atributos = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        Sobrenome = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100),
                        NomeDeUsuario = c.String(nullable: false),
                        Senha = c.String(nullable: false),
                        Foto = c.String(),
                        Facebook = c.String(maxLength: 100),
                        Twitter = c.String(maxLength: 100),
                        Telefone = c.String(maxLength: 100),
                        Celular = c.String(maxLength: 100),
                        Endereco = c.String(maxLength: 100),
                        EnderecoComplemento = c.String(maxLength: 100),
                        EhAdministrador = c.Boolean(nullable: false),
                        PerfilId = c.Long(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.Departamento", "SistemaFolhaId", "dbo.SistemaFolha");
            DropIndex("dbo.PerfilPermissao", new[] { "PermissaoId" });
            DropIndex("dbo.PerfilPermissao", new[] { "PerfilId" });
            DropIndex("dbo.Usuario", new[] { "PerfilId" });
            DropIndex("dbo.Departamento", new[] { "SistemaFolhaId" });
            DropTable("dbo.PerfilPermissao");
            DropTable("dbo.Usuario");
            DropTable("dbo.Permissao");
            DropTable("dbo.Perfil");
            DropTable("dbo.SistemaFolha");
            DropTable("dbo.Departamento");
            DropTable("dbo.Aviso");
            DropTable("dbo.Auditoria");
        }
    }
}
