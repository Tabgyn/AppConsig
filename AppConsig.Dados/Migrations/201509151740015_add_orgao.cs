namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_orgao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orgao",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Codigo = c.Long(nullable: false),
                        Nome = c.String(),
                        Descricao = c.String(),
                        SistemaFolhaId = c.Long(nullable: false),
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
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orgao", "SistemaFolhaId", "dbo.SistemaFolha");
            DropIndex("dbo.Orgao", new[] { "SistemaFolhaId" });
            DropTable("dbo.SistemaFolha");
            DropTable("dbo.Orgao");
        }
    }
}
